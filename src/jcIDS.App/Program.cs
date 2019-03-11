using System;

using jcIDS.lib.CommonObjects;
using jcIDS.lib.Managers;

namespace jcIDS.app
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sListener = new SocketListener())
            {
                sListener.PacketArrival += PacketArrival;

                var (success, exception) = sListener.Run();

                if (!success)
                {
                    Console.Write(exception);
                }

                Console.ReadLine();
            }
        }

        private static void PacketArrival(object sender, Packet packet)
        {
            Console.WriteLine(packet);
        }
    }
}