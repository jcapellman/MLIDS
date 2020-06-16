# MLIDS

MLIDS is a Host Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning (ML) everyday professionally it seemed like a perfect fit for using ML.  When it came time to decide on a topic for my dissertation research this was at the top of my list.

## Components
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET 5)
* Model Trainer Application (.NET 5)
* Model Testing Application (.NET 5)
* Attacker Application (.NET 5)
* Scripting Application (.NET 5)
* Shared Code Library (.NET 5)
* Unit Tests (.NET 5)

## Releases
Using GitHub Actions, all three Applications will be built and packaged individually (there maybe a launcher created at some point)

## Requirements
* Windows 7 SP1+ (.NET 5's oldest supported OS)
* Npcap Driver Installed
* MongoDB Installed or use of LiteDB, JSON or CSV if storing of the data is needed

## Usage
The idea is to follow the steps:
1. Run the Packet Capture Application to generate a sizeable training and test set
2. Run the Model Trainer Application to generate a model
3. Run the Model Testing Application to test the FP/FN of the model along with performance and other metrics
4. *Optionally* Run the attacker and scripting tool to recreate scenarios

## Remaining work items
* Binary Classification Model once a detection has been made
* Performance Tuning
* Documentation
* Malicious Traffic samples
* Benign Traffic Samples
* Scripting and Automation tools to promote repeatability

## License
As noted this is licensed under the GPL-3.0 License.
