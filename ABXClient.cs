using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ABXExchangeClient
{
    internal class ABXClient
    {
        public TcpClient ConnectToServer(string hostname, int port)
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 3000);
            return client;
        }

        public void SendRequest(NetworkStream stream, byte callType, byte resendSeq = 0)
        {
            byte[] payload = new byte[2];
            payload[0] = callType;  // 1 for "Stream All Packets", 2 for "Resend Packet"
            payload[1] = resendSeq;

            stream.Write(payload, 0, payload.Length);
        }

        public List<Packet> ReceivePackets(NetworkStream stream)
        {
            List<Packet> packets = new List<Packet>();
            byte[] buffer = new byte[17]; // Each packet is 17 bytes
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (bytesRead == 17)
                {
                    Packet packet = new Packet
                    {
                        Symbol = Encoding.ASCII.GetString(buffer, 0, 4),
                        BuySellIndicator = (char)buffer[4],
                        Quantity = BitConverter.ToInt32(buffer, 5),
                        Price = BitConverter.ToInt32(buffer, 9),
                        SequenceNumber = BitConverter.ToInt32(buffer, 13)
                    };
                    packets.Add(packet);
                }
            }
            return packets;
        }

        public List<int> GetMissingSequences(List<Packet> packets)
        {
            List<int> missingSequences = new List<int>();
            packets.Sort((a, b) => a.SequenceNumber.CompareTo(b.SequenceNumber));

            for (int i = 0; i < packets.Count - 1; i++)
            {
                int current = packets[i].SequenceNumber;
                int next = packets[i + 1].SequenceNumber;

                for (int seq = current + 1; seq < next; seq++)
                {
                    missingSequences.Add(seq);
                }
            }
            return missingSequences;
        }

        public void RequestMissingPackets(NetworkStream stream, List<int> missingSequences, List<Packet> packets)
        {
            foreach (int seq in missingSequences)
            {
                SendRequest(stream, 2, (byte)seq);
                packets.AddRange(ReceivePackets(stream));
            }
        }

        public void SavePacketsToJson(List<Packet> packets, string filePath)
        {
            string json = JsonConvert.SerializeObject(packets, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}

