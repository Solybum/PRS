dotnet pack ./../src/PRS/PRS.csproj

dotnet nuget push ./../src/PRS/bin/Release/PRS.1.1.0.nupkg -s https://api.nuget.org/v3/index.json
