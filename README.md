# MLIDS

MLIDS is a Host Intrusion Detection System using Machine Learning.  The original idea behind this several years ago (2014) was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast forward a few years and my own shift to utilizing Machine Learning (ML) everyday professionally it seemed like a perfect fit for using ML.  When it came time to decide on a topic for my dissertation research this was at the top of my list.

## Status
[![MLIDS Model Trainer Tool](https://github.com/jcapellman/MLIDS/actions/workflows/ModelTrainerTool.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/ModelTrainerTool.yml)

[![SonarQube Analysis](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml)

[![MLIDS Data Capture Tool](https://github.com/jcapellman/MLIDS/actions/workflows/DataCaptureTool.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/DataCaptureTool.yml)

[![CodeQL](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml)
## Components
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET 6)
* Model Trainer Application (.NET 6)
* Shared Code Library (.NET 6)
* Unit Tests (.NET 6)

## Releases
Using GitHub Actions, both Applications will be built and packaged individually (there maybe a launcher created at some point).  In addition SonarQube Analysis is being performed for Unit Test coverage, vulnerabilities, bugs and enterprise readiness.

## Requirements
* Windows 7 SP1+ (.NET 6's oldest supported OS)
* Npcap Driver Installed
* MongoDB Installed or use of LiteDB, JSON or CSV if storing of the data is needed

## Usage
The idea is to follow the steps:
1. Run the Packet Capture Application to generate a sizeable training and test set
2. Run the Model Trainer Application to generate a model

## License
As noted this is licensed under the GPL-3.0 License.
