param(
     [string]
     $PackageSpec = $(throw "No PackageSpec argument was supplied."),
     [string]
     $ApiKey = $(throw "No ApiKey argument was supplied.")
)

$feedLocation = "https://api.nuget.org/v3/index.json";
$packageFiles = Get-Item $PackageSpec;

Write-Output "Package files matching spec.";
Write-Output $packageFiles.FullName;

dotnet nuget push $packageFiles.FullName --api-key $ApiKey --source $feedLocation;