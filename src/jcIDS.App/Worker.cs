using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using jcIDS.app.Common;
using jcIDS.app.Managers;
using jcIDS.lib.CommonObjects;
using jcIDS.lib.Enums;
using jcIDS.lib.Handlers;
using jcIDS.lib.Managers;
using jcIDS.lib.RESTObjects;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace jcIDS.app
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        private string _deviceToken;

        private async Task<bool> AuthenticateAsync(string hostName, string webServiceURL)
        {
            using (var authHandler = new AuthHandler(webServiceURL))
            {
                var result = await authHandler.RegisterDeviceAsync(hostName);

                if (!result.HasObjectError)
                {
                    return true;
                }

                _deviceToken = result.ObjectValue;

                Console.WriteLine($"Error occurred registering: {result.ObjectException} | {result.ObjectExceptionInformation}");

                return false;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

            Console.WriteLine("App is authenticated, starting socket listener...");

            using (var sListener = new SocketListener())
            {
                sListener.PacketArrival += PacketArrival;

                while (!stoppingToken.IsCancellationRequested)
                {
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
                }
            }
        }

        private async void PacketArrival(object sender, Packet packet)
        {
            using (var pHandler = new PublishHandler(Environment.MachineName))
            {
                await pHandler.SubmitPacketAsync(new PacketRequestItem
                {
                    Packets = new List<Packet> { packet },
                    DeviceToken = _deviceToken
                });

                Console.WriteLine(packet);
            }
        }
    }
}