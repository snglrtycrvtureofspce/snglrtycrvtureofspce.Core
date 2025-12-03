$artifacts = ".\artifacts"

if (Test-Path $artifacts) { Remove-Item $artifacts -Force -Recurse }

dotnet restore
dotnet build --configuration Release --no-restore
dotnet pack --configuration Release --no-build --output $artifacts
