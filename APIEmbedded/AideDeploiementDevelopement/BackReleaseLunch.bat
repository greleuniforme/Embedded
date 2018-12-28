cd ../
dotnet build .
#RMDIR .build /s
#MKDIR .build
dotnet publish --configuration Release
cd APIEmbedded\bin\Release\netcoreapp2.1\publish\
dotnet APIEmbedded.dll
pause
