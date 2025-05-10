Final Project Game
==================

_(Proper title TBD)_

Prerequisites
=============

This project uses .NET 9, make sure to grab the latest SDK first!

Currently supported runtime platforms:
 - Windows 64-bit
 - Linux 64-bit
 - MacOS (x86-64/arm64)

To get the whole repo and `FNA` dependencies from the start:
```
git clone --recursive
```

Otherwise submodule dependencies can be fetched/updated manually using:
```
git submodule update --init
```

Building
========

Build and run via command line using:
```
dotnet run --project Game/Game.csproj
```

**OR**

Open the `Game.sln` solution in your IDE and build and run from there

Compile any changes to Effects using:
```
dotnet msbuild -t:CompileEffects Game/Game.csproj
```
(NOTE: `wine` must be installed for non-Windows platforms)
