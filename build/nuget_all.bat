cd ../src/PRS/bin/Release

del *.nupkg

nuget pack "../../PRS.csproj" -Prop Configuration=Release

nuget push "*.nupkg" -Source https://www.nuget.org/api/v2/package

cd ../../../../build
