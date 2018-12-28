cd ../
dotnet build .
dotnet publish --configuration Debug
cd APIEmbedded\bin\Debug\netcoreapp2.1\publish\
dotnet APIEmbedded.dll
pause
