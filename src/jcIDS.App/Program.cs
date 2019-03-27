using System;

using jcIDS.lib.CommonObjects;
using jcIDS.lib.Enums;
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

                var runResult = sListener.Run();

                if (runResult.ObjectValue != SocketCreationStatusCode.OK)
                {
                    switch (runResult.ObjectValue)
                    {
                        case SocketCreationStatusCode.ACCESS_DENIED:
                            Console.WriteLine("Access Denied, are you running as Administrator/root?");
                            break;
                        case SocketCreationStatusCode.UNKNOWN:
                            Console.WriteLine(runResult.HasObjectError
                                ? $"Run Error: {runResult.ObjectException}"
                                : "Unknown Error Occurred");
                            break;
                    }
                }

                Console.ReadKey();
            }
        }

        private static void PacketArrival(object sender, Packet packet)
        {
            Console.WriteLine(packet);
        }
    }
}