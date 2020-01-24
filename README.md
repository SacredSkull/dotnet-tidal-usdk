# .NET Core Tidal Unofficial SDK
![Master build status](https://github.com/SacredSkull/dotnet-tidal-usdk/workflows/master-release/badge.svg)
![develop build status](https://github.com/SacredSkull/dotnet-tidal-usdk/workflows/develop/badge.svg)

## Install with your favourite package manager: 
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/TidalUSDK?label=nuget%20%28pre-release%29)
![Nuget](https://img.shields.io/nuget/v/TidalUSDK?label=nuget%20%28release%29)

# About
This library aims to enable developers to write applications (e.g. media clients) that interact with _TIDAL_, providing audio stream URIs, metadata, etc.

Written in _.NET Core_, meaning that developers can even write programs for platforms that have never had (or perhaps are unlikely to _ever_ have) official clients.

Documentation forthcoming!

# Motiviation
I began writing this library because there didn't seem to be very many up-to-date TIDAL wrappers for C#, and none explicitly for .NET Core. I've wanted for some time to write my own TIDAL client/player, so I felt it would be a good learning exercise to write the API wrapper too.

Many thanks to [node-tidal-api](https://github.com/lucaslg26/TidalAPI) for giving me a good starting point.

# A note on piracy
**You do not have my permission to use this library for piracy purposes, and use of the library requires licence agreement which explicitly forbids any attempt to infringe on intellectual property living on TIDAL's streaming platform.**

Not only is ignoring this unethical, but also makes it more likely that TIDAL will take steps to obfuscate their API (i.e. audio stream encryption) and make development work significantly more difficult. Using this library requires agreement to this - see [the licence](https://github.com/SacredSkull/dotnet-tidal-usdk/blob/develop/LICENSE.md) for more information (it's just an MIT licence with some piracy bootstrap).
