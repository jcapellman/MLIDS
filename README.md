# MLIDS

MLIDS is a Network Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning everyday professionally it seemed like a perfect fit.

## Overview
* Windows NDIS Filter Driver (Windows 10/2016/2019)
* ASP.NET Core 3.0 REST Service to record the traffic from the endpoint
* .NET Core 3.0 Daemon to build and train models with ML.NET on if the traffic is malicious or not and
* ASP.NET Core 3.0 MVC App to show metrics and alerts


### Components

### Lightweight Packet Sniffer Library
Currently this is a .NET Core 3.0 library, invoking the core functionality is extremely easy.

Example code:
````
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
        Console.WriteLine(e);
    }
}
````
