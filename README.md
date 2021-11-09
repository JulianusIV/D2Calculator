# D2Calculator

A C# WPF App to calculate shots to kill in different circumstances for Destiny2

## Current status

### Currently working:

- Calculate shots to kill for every resilience level given a baseline bodyshot damage value and a baseline crit value (this will be deprecated and removed with the full implementation of the archetype selector)
- Calculate shots to kill for every resilience level for a select few archetypes, this wil replace beforementioned STK with manual damage input
- Accuracy calculation (taking into account accuracy, instead of optimal STK)

### Planned/WIP

- Expand Archetype selection to include all archetypes
- Calculate shots to kill for each resilience level based on archetype and damage modifier (empowering rift etc...)
- Calculate shots to kill based on Range


## NOTE:

**I currently do not have a EV code signing certificate, meaning this executable is unsigned, and your system will warn you with a window saying "Windows protected your pc".
If you wish to execute it you have to click "More info" and "Run anyway"**.

## How to build and run this yourself:

In order to build this project you will need the [.NET SDK x64](https://dotnet.microsoft.com/download).

To download the Project binaries you can go to the [main page of this GitHub repo](https://github.com/JulianusIV/D2Calculator), click the big green "Code" button on the top right, and click download zip:
![grafik](https://user-images.githubusercontent.com/65790187/140717220-fa566ba3-8e18-4ba1-8da4-9befee95bad4.png)

Unpack the archive to a folder of your choice, navigate to the generated folder "D2Calculator-master", where you will see a bunch of files, one of them being "D2Calculator.sln"

Shift + right click the background and select "Open PowerShell window here" ("Open in Windows Terminal" if you are on Win11), or copy the path, open PowerShell and navigate to the folder using cd [FilePath]

run 
```powershell
dotnet restore
dotnet build -c Release
```

After the build is done, you will find the binaries in `D2Calculator-master\D2CalculatorCockpit\bin\Release\net5.0-windows\D2CalculatorCockpit.exe`.
From here you are all set and can just run the executable.
