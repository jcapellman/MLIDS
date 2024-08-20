# MLIDS

MLIDS is a Host Intrusion Detection System using Machine Learning.  Several years ago (2014), the original idea behind this was to write a C++ brute force network analyzer for a Cobalt Qube (http://www.jarredcapellman.com/2014/3/9/NetBSD-and-a-Cobalt-Qube-2).  Fast-forward a few years, and my shift to utilizing Machine Learning (ML) every day professionally was a perfect fit for using ML. When it came time to decide on a topic for my dissertation research, this was at the top of my list.


## Status of GitHub Actions
[![SonarQube Analysis](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/SonarQubeAnalysis.yml)

[![CodeQL](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/jcapellman/MLIDS/actions/workflows/codeql-analysis.yml)

## Components
As anyone who has followed my work over the last two decades - I like to use the right tools for the job. O
* Packet Capture Driver (NPCAP NDIS Filter Driver - https://nmap.org/npcap/)
* Packet Capture Application (.NET 8)
* Model Trainer Application (Python)
* Model Harness Application (Rust)
  
## Releases
All artifacts will be built and packaged individually using GitHub Actions. In addition, SonarQube Analysis is being performed for Unit Test coverage, vulnerabilities, bugs, and enterprise readiness.

## Requirements
* Windows 7 SP1+ (.NET 7's oldest supported OS)
* Npcap Driver Installed
* MongoDB Installed or use of LiteDB, JSON or CSV if storing of the data is needed
* .NET 8 Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* Python 3.12
* Rust

## Usage
The idea is to follow the steps:
1. Run the Packet Capture Application to generate a sizeable training and test set
2. Run the Model Trainer Application to generate a model
3. Run the Model within the Model Harness Application to verify performance impact and detection capabilities

## License
As noted this is licensed under the GPL-3.0 License.
