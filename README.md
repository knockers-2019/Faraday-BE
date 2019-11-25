# Faraday-BE

## Backend Group
*Nikolai Christiansen*  
*Mikkel Ertbjerg*  
*Nikolaj Dyring*

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
### With VisualStudio or VisualCode (Or another IDE/Codeeditor with .NET support)
1. Load the .sln file
2. Make sure all of the required [dependencies](#dependencies) are acquired and installed
3. Rebuild FaradayGrpcServer
4. Launch FaradayGrpcServer (Program.cs)

_If you wish to test the server with the FaradayBEClient make sure to do the following:_
1. Af you've done the four steps above.
2. Make sure all of the required [dependencies](#dependencies) are acquired and installed
3. Rebuild FaradayBEClient
4. Launch FaradayBEClient (Program.cs)

### With .exe file
*TBD*

## SLA

## Summary
The backend is build around [gRPC](https://grpc.io/). gRPC is a message protocol that uses HTTP/2 for transport and [Protocol Buffers](https://developers.google.com/protocol-buffers) as the interface description language. This means that any client that wishes to implement this backend, has to have an identical .proto file, see [dependencies](#dependencies).

