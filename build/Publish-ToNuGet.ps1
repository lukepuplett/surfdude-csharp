param(
     [string]
     $PackageSpec = $(throw "No PackageSpec argument was supplied."),
     [string]
     $ApiKey = $(throw "No ApiKey argument was supplied.")
)

$feedLocation = "https://api.nuget.org/v3/index.json";
$packageFile = Get-Item $PackageSpec;

dotnet nuget push $packageFile.FullName --api-key $ApiKey --source $feedLocation;