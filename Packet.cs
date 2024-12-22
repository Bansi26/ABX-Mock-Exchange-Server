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
    internal class Packet
    {
        public string Symbol { get; set; }
        public char BuySellIndicator { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SequenceNumber { get; set; }
    }
}
