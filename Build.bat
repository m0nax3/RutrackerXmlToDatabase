
dotnet publish "Importer" --framework netcoreapp2.0 --runtime ubuntu.16.04-x64 --output "Build\ubuntu.16.04-x64" --configuration Release

dotnet publish "Importer" --framework netcoreapp2.0 --runtime win10-x64 --output "Build\win10-x64" --configuration Release
