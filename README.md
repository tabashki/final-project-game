Final Project Game
==================

_(Proper title TBD)_

Prerequisites
=============

This project uses .NET 9, make sure to grab the latest SDK first!

Either run
```
git clone --recursive
```
from the start to get the whole repo and dependencie from the start,
otherwise run:
```
git submodule update --init
```
to make sure you have the full `FNA` source code.

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