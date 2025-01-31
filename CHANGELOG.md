# Change Log

All notable changes to this project will be documented in this file.

This project adheres to [Semantic Versioning](http://semver.org/) and is following the [change log format](http://keepachangelog.com/).

## Unreleased

## [2.1.0] - 2021-08-29

### :zap: Added

- [#476](https://github.com/FantasticFiasco/aws-signature-version-4/issues/476) Support for [Unity](https://unity.com/) game engine

## [2.0.0] - 2021-08-01

### :zap: Added

- [#458](https://github.com/FantasticFiasco/aws-signature-version-4/issues/458) [BREAKING CHANGE] Improve support for AWS credentials inheriting from `Amazon.Runtime.AWSCredentials` (proposed by [@cliff-wakefield-acurus](https://github.com/cliff-wakefield-acurus))

**Migration guide**

All properties of class `AwsSignatureHandlerSettings` have had their access modifiers changed from `public` to `internal`, thus the properties have been removed from the public API. If you experience a compilation error because of this, please refactor your code to access `RegionName`, `ServiceName` and the AWS credentials by other means.

## [1.4.1] - 2021-06-09

### :syringe: Fixed

- [#420](https://github.com/FantasticFiasco/aws-signature-version-4/issues/420) Improve error message in exception thrown when region or service name is omitted

## [1.4.0] - 2020-11-26

### :zap: Added

- HTTP message handler `AwsSignatureHandler` designed to be compatible with [IHttpClientFactory](https://docs.microsoft.com/dotnet/api/system.net.http.ihttpclientfactory) and its request pipeline. For more information about message handlers and their usage, please see [HTTP Message Handlers in ASP.NET Web API](https://docs.microsoft.com/aspnet/web-api/overview/advanced/http-message-handlers). (proposed by [@torepaulsson](https://github.com/torepaulsson))

## [1.3.1] - 2020-08-14

### :syringe: Fixed

- NuGet package does not show an icon
- Configure [SourceLink](https://github.com/dotnet/sourcelink) to embed untracked sources
- Configure [SourceLink](https://github.com/dotnet/sourcelink) to use deterministic builds when running on AppVeyor

## [1.3.0] - 2020-06-29

### :zap: Added

- [#1](https://github.com/FantasticFiasco/aws-signature-version-4/issues/1) Support for S3 (Simple Storage Service)

## [1.2.0] - 2019-07-15

### :zap: Added

- Improve searchability by adding NuGet tags `aws`, `sign`, `http` and `request`

## [1.1.0] - 2019-07-13

### :zap: Added

- [#36](https://github.com/FantasticFiasco/aws-signature-version-4/issues/36) Support for .NET Framework 4.5

## [1.0.2] - 2019-06-27

### :syringe: Fixed

- [#28](https://github.com/FantasticFiasco/aws-signature-version-4/issues/28) Default request headers are added twice on Android using Mono. The behavior on Mono is different from the behavior on .NET Framework or .NET Core, where a default request header that already exists on the request message is ignored. (contribution by [@Daniel-NP](https://github.com/Daniel-NP))

## [1.0.1] - 2019-05-28

### :syringe: Fixed

- Update XML comments
- Update dependencies

## [1.0.0] - 2019-05-28

Initial version.
