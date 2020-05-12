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
Using GitHub Actions, all three Applications will be built and packaged individually (there maybe a launcher created at some point)

## Requirements
* Windows 7 SP1+ (.NET 5's oldest supported OS)
* Npcap Driver Installed
* MongoDB Installed (or write a different DAL)

## Usage
The idea is to follow the steps:
1. Run the Packet Capture Application to generate a sizeable training and test set
2. Run the Model Trainer Application to generate a model
3. Run the Model Testing Application to test the FP/FN of the model along with performance and other metrics

## Remaining work items
* Binary Classification Model once a detection has been made
* Performance Tuning
* Documentation
* Malicious Traffic samples
* Benign Traffic Samples
* Ability to switch DALs (support LiteDB and flat files)
