# jcIDS

jcIDS is a Network Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning everyday professionally it seemed like a perfect fit.

## Components
* .NET Core 2.1 Daemon to run predictions and record traffic
* ASP.NET Core 2.1 REST Service to expose the database
* Xamarin Forms Apps for iOS, Android and UWP
* .NET Core 2.1 Model Trainer
