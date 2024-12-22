using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ABXExchangeClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hostname = "127.0.0.1";
            int port = 3000;

            try
            {
                TcpClient client = new TcpClient(hostname, port);
                Console.WriteLine("Connected to server!");
                NetworkStream stream = client.GetStream();

                // Request All Packets
                Console.WriteLine("Sending request to stream all packets...");
                byte[] payload = { 1, 0 }; // CallType 1
                stream.Write(payload, 0, payload.Length);

                // Receive Packets
                var packets = ReceivePackets(stream);
                Console.WriteLine($"Total packets received: {packets.Count}");

                // Handle Missing Sequences
                var missingSequences = GetMissingSequences(packets);
                Console.WriteLine($"Missing sequences: {string.Join(", ", missingSequences)}");

                // Request Missing Packets
                if (missingSequences.Count > 0)
                {
                    RequestMissingPackets(stream, missingSequences, packets);
                }

                // Save to JSON
                SavePacketsToJson(packets, "output.json");
                Console.WriteLine("Output saved to output.json.");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
