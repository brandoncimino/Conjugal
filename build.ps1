function Publish-Nuget(
    $nupkg,
    $ApiKey,
    $SecretsFile = ".\secrets.json"
) {
    if (!$ApiKey) {
        $secrets = Get-Content $SecretsFile | ConvertFrom-Json
        $ApiKey = $secrets.nuget_api_key;
    }

    # dotnet nuget push AppLogger.1.0.0.nupkg --api-key qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 --source https://api.nuget.org/v3/index.json
    
    if (!$nupkg) {
        $built_package = Build-Nupkg
        $nupkg = $built_package.FullName
    }

    @{
        nupkg = $nupkg
        ApiKey = $ApiKey
    } | Out-String | Write-Host -ForegroundColor DarkGray

    dotnet.exe nuget push $nupkg --api-key $ApiKey --source "nuget.org"
}

function Build-Nupkg($BeforeBuildTimestamp) {
    dotnet.exe pack

    return Get-LatestNupkg $BeforeBuildTimestamp
}

# function Build-Nupkg($ProjectName = "Conjugal"){
#     # $beforeBuildTime = Get-Date
#     $build_message = dotnet.exe build
#     if($LASTEXITCODE -eq 0){
#         $output_line = $build_message -match "$ProjectName ->"
#         $output_line = $output_line[0]
#         $path_pattern = "$ProjectName -> (?<path>.*)"
#         if($output_line -match $path_pattern){
#             return $Matches.path
#         }
#     }
    
#     throw "Unable to build successfully!"
# }

function Get-LatestNupkg($BeforeBuildTimestamp) {
    $bin = ".\Conjugal\bin\Debug"
    $pkgs = Get-ChildItem $bin -Filter "*.nupkg" 

    if ($pkgs.Count -eq 0) {
        throw "Didn't find any packages at ALL!"
        return
    }
    
    if ($BeforeBuildTimestamp) {
        $pkgs = $pkgs | Where-Object LastWriteTime -GT $BeforeBuildTimestamp
    
        if ($pkgs.Count -eq 0) {
            throw "Didn't find any packages built after the timestamp $BeforeBuildTimestamp!"
            return
        }
        elseif ($pkgs.Count -gt 1) {
            throw "Found MULTIPLE ($($pkgs.Count)) packages built after the timestamp $BeforeBuildTimestamp!"
            return
        }
        else {
            return $pkgs[0]
        }
    }
    else {
        return $pkgs | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    }
}