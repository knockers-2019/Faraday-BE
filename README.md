# Faraday-BE
## Backend Group
*Nikolai Christiansen*  
*Mikkel Ertbjerg*  
*Nikolaj Dyring*

## Summary
The backend is build around [gRPC](https://grpc.io/). gRPC is a message protocol that uses HTTP/2 for transport and [Protocol Buffers](https://developers.google.com/protocol-buffers) as the interface description language. This means that any client that wishes to implement this backend, has to have an identical .proto file, see [dependencies](#dependencies).

## Dependencies
### Backend
All of the dependencies can be acquired through Nugget Package Manager:
- Grpc.AspNetCore
- MySql.Data

### Client
Any client that wishes to use this backend requires the following dependencies:
- A matching .proto file from the Backend
All of the following dependencies, which can be acquired through the Nugget Package Manager:
- Google.Protobuf
- Grpc.Net.Client (Given it's a .net client)
- Grpc.Tools

## Installation
### For Devs
*Requires VisualStudio or VisualCode (Or another IDE/Codeeditor with .NET support)*
1. Load the .sln file
2. Make sure all of the required [dependencies](#dependencies) are acquired and installed
3. Rebuild FaradayGrpcServer
4. Launch FaradayGrpcServer (Program.cs)

_If you wish to test the server with the FaradayBEClient make sure to do the following:_
1. Af you've done the four steps above.
2. Make sure all of the required [dependencies](#dependencies) are acquired and installed
3. Rebuild FaradayBEClient
4. Launch FaradayBEClient (Program.cs)

### For Ops
The server is hosted at Nikolai Sjøholm Christiansen, to stop, start or restart the server contact him directly, or another member of the backend team.

## SLA (Service Level Agreement)
### Statement of objective
Team 3 delivers a working piece of software to handle car reservations to team 2.

### Services to be covered
- Correct reported issues on github
- Ensure [server uptime](#uptime) according to [performance metrics](#performance-metrics)
- Guide/help Ops with any questions they might have
- Error/Issue handling within agreed time periode, according to [performance metrics](#performance-metrics

## Developers responseabilities
- Furfilling agreed [services](#services-to-be-covered)
- Furfilling agreed [performance metrics](#performance-metrics)

## operation responseabilities
- Test/use the system for its intended use
- Reportiing issues on GitHub using the GitHub Issues tool
- Monitor performance metrics, to verify they are satisfying


### Performance metrics
#### Uptime
- Atleast 35% (Yearly uptime)
- Server will genreally be available from 08:00 - 20:00 everyday
- If you wish to test/use the system at a specific time outside of above time window, contact Nikolai Sjøholm Christiansen

#### Mean tesponse time
_Disregarding the initial request_
- Less than 2 seconds

#### Mean recovery time
- 24 Hours

#### Failure frequency
- Failures are defined as system crashes
- Less than three failures/day



