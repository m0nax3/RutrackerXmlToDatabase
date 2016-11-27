
dotnet publish "src\RutrackerImport" --framework netcoreapp1.1 --runtime ubuntu.16.04-x64 --output "Build\ubuntu.16.04-x64" --configuration Release

dotnet publish "src\RutrackerImport" --framework netcoreapp1.1 --runtime win10-x64 --output "Build\win10-x64" --configuration Release
