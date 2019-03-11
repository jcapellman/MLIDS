using System;
using System.Net.Sockets;

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

                var (success, exception) = sListener.Run();

                if (!success)
                {
                    Console.Write(exception);
                }

                Console.ReadLine();
            }
        }

        private static void DataArrival(object sender, PacketArrivedEventArgs e)
        {
            if (e.Protocol == ProtocolType.Tcp)
            {
                Console.WriteLine(e.OriginationAddress + " - " + e.OriginationPort + " - " + e.DestinationAddress +
                                  " - " + e.DestinationPort + " - " + e.Protocol + " - " + e.PacketLength + " - " +
                                  e.HeaderLength + " - " + e.IPVersion);
            }
        }
    }
}