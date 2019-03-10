using System;

using jcIDS.lib.Managers;
using jcIDS.lib.Objects;

namespace jcIDS.app
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sListener = new SocketListener())
            {
                sListener.PacketArrival += DataArrival;

                sListener.Run();

                Console.ReadLine();
            }
        }

        private static void DataArrival(object sender, PacketArrivedEventArgs e)
        {
            if (e.Protocol.ToUpper() == "TCP")
                Console.WriteLine(e.OriginationAddress + " - " + e.OriginationPort + " - " + e.DestinationAddress + " - " + e.DestinationPort + " - " + e.Protocol + " - " + e.PacketLength + " - " + e.HeaderLength + " - " + e.IPVersion);
        }
    }
}