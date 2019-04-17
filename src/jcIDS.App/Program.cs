using System;
using System.Threading.Tasks;
    
using jcIDS.app.Common;
using jcIDS.app.Managers;
using jcIDS.lib.CommonObjects;
using jcIDS.lib.Enums;
using jcIDS.lib.Handlers;
using jcIDS.lib.Managers;

namespace jcIDS.app
{
    class Program
    {
        private static async Task<bool> AuthenticateAsync(string hostName, string webServiceURL)
        {
            using (var authHandler = new AuthHandler(webServiceURL))
            {
                var result = await authHandler.RegisterDeviceAsync(hostName);

                if (!result.HasObjectError)
                {
                    return true;
                }

                Console.WriteLine($"Error occurred registering: {result.ObjectException} | {result.ObjectExceptionInformation}");

                return false;
            }
        }

        static async Task Main(string[] args)
        {
            var settings = SettingsManager.LoadSettings();

            if (settings == null)
            {
                settings = new Settings();

                Console.WriteLine("Settings not set, using defaults...");
            }

            Console.WriteLine($"Using {settings.WebServiceURL} to authenticate...");

            var authResult = await AuthenticateAsync(Environment.MachineName, settings.WebServiceURL);

            if (!authResult)
            {
                Console.WriteLine("Quitting due to registration failure");

                return;
            }

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