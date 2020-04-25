# MLIDS

MLIDS is a Network Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning everyday professionally it seemed like a perfect fit.

## Components
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET Core 3.1)
* Model Trainer Application (.NET Core 3.1)
* Efficacy Tester Application (.NET Core 3.1)
