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
                sListener.PacketArrival += PacketArrival;

                var (success, exception) = sListener.Run();

                if (!success)
                {
                    Console.Write(exception);
                }

                Console.ReadLine();
            }
        }

        private static void PacketArrival(object sender, PacketArrivedEventArgs e)
        {
            Console.WriteLine(e);
        }
    }
}