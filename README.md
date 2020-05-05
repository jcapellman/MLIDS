# MLIDS

MLIDS is a Host Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning everyday professionally it seemed like a perfect fit.

## Components
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET 5)
* Model Trainer Application (.NET 5)
* Model Testing Application (.NET 5)
* Shared Code Library (.NET 5)
* Unit Tests (.NET 5)

## Releases
Using GitHub Actions, all three Applications will be built and packaged individually
