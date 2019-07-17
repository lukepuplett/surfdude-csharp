param(
     [string]
     $PackageSpec = $(throw "No PackageSpec argument was supplied."),
     [string]
     $ApiKey = $(throw "No ApiKey argument was supplied.")
)

$feedLocation = "https://api.nuget.org/v3/index.json";
$packageFile = Get-Item $PackageSpec;

Write-Output [String]::Concat($packageFile.Count, " packages found to publish");

dotnet nuget push $packageFile.FullName --api-key $ApiKey --source $feedLocation;