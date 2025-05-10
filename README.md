Final Project Game
==================

_(Proper title TBD)_

Building
========

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

Build and run using:
```
dotnet run --project Game/Game.csproj
```

Compile any changes to Effects using:
```
dotnet msbuild -t:CompileEffects Game/Game.csproj
```
(NOTE: `wine` must be installed for non-Windows platforms)