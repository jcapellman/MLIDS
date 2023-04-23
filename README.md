# MLIDS

MLIDS is a Host Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning (ML) everyday professionally it seemed like a perfect fit for using ML.  When it came time to decide on a topic for my dissertation research this was at the top of my list.

Looking forward to expanding these capabilities going forward.

## Status of GitHub Actions
[![SonarQube Analysis](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml)

[![CodeQL](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml)
## Components
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET 7)
* Model Trainer Application (.NET 7)
* Shared Code Library (.NET 7)
* Unit Tests (.NET 7)

## Releases
Using GitHub Actions, both Applications will be built and packaged individually (there maybe a launcher created at some point).  In addition SonarQube Analysis is being performed for Unit Test coverage, vulnerabilities, bugs and enterprise readiness.

## Requirements
* Windows 7 SP1+ (.NET 7's oldest supported OS)
* Npcap Driver Installed
* MongoDB Installed or use of LiteDB, JSON or CSV if storing of the data is needed
* .NET 7 Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

## Usage
The idea is to follow the steps:
1. Run the Packet Capture Application to generate a sizeable training and test set
2. Run the Model Trainer Application to generate a model

## License
As noted this is licensed under the GPL-3.0 License.
