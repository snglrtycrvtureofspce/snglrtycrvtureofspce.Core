#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Push NuGet packages to a feed

.DESCRIPTION
    This script pushes all NuGet packages from the artifacts directory to a NuGet feed.

.PARAMETER Source
    NuGet source URL. Default is https://api.nuget.org/v3/index.json

.PARAMETER ApiKey
    NuGet API key. Can also be set via NUGET_API_KEY environment variable.

.PARAMETER SkipDuplicate
    Skip packages that already exist on the feed.

.EXAMPLE
    ./Push.ps1 -ApiKey "your-api-key"
    
.EXAMPLE
    $env:NUGET_API_KEY = "your-api-key"
    ./Push.ps1
#>

[CmdletBinding()]
param(
    [string]$Source = 'https://api.nuget.org/v3/index.json',
    
    [string]$ApiKey = $env:NUGET_API_KEY,
    
    [switch]$SkipDuplicate
)

$ErrorActionPreference = 'Stop'

$artifacts = Join-Path $PSScriptRoot 'artifacts'

if (-not $ApiKey) {
    throw "API key is required. Either pass -ApiKey parameter or set NUGET_API_KEY environment variable."
}

if (-not (Test-Path $artifacts)) {
    throw "Artifacts directory not found. Run Build.ps1 first."
}

$packages = Get-ChildItem $artifacts -Filter "*.nupkg"

if ($packages.Count -eq 0) {
    throw "No packages found in artifacts directory."
}

Write-Host "Pushing packages to $Source..." -ForegroundColor Cyan

$pushArgs = @(
    'nuget', 'push'
    (Join-Path $artifacts '*.nupkg')
    '--api-key', $ApiKey
    '--source', $Source
)

if ($SkipDuplicate) {
    $pushArgs += '--skip-duplicate'
}

& dotnet @pushArgs

if ($LASTEXITCODE -ne 0) {
    throw "Push failed with exit code $LASTEXITCODE"
}

# Push symbol packages if they exist
$snupkgs = Get-ChildItem $artifacts -Filter "*.snupkg" -ErrorAction SilentlyContinue

if ($snupkgs.Count -gt 0) {
    Write-Host "Pushing symbol packages..." -ForegroundColor Yellow
    
    $symbolArgs = @(
        'nuget', 'push'
        (Join-Path $artifacts '*.snupkg')
        '--api-key', $ApiKey
        '--source', $Source
    )
    
    if ($SkipDuplicate) {
        $symbolArgs += '--skip-duplicate'
    }
    
    & dotnet @symbolArgs
    
    # Symbol push failures are not critical
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Symbol package push failed (this is often not critical)" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "Push completed successfully!" -ForegroundColor Green
