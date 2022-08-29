<#
.SYNOPSIS
    Contains methods to build and publish `.nupkg` files to `nuget.org`.

.EXAMPLE
    ```
    Import-Script .\build.ps1
    Publish-Nuget
    ```
#>

Publish-Nuget -Verbose

<#
.SYNOPSIS
    Publishes (aka "pushes") a `.nupkg` file to `nuget.org`.

TODO: Move this to the NuPkg class
#>
function Publish-Nuget(
    # The `.nupkg` file that will be published.
    # If the file doesn't exist, then a new file will be built using `Build-Nupkg`.
    $nupkg,
    # An explicit NuGet API key.
    # If falsey, then we will look into `-SecretsFile` instead.
    $ApiKey,
    # A JSON file containing the NuGet API key.
    #  - The key should be in the `nuget_api_key` field.
    #  - An explicit `-ApiKey` will take precedence over this file.
    $SecretsFile = ".\secrets.json"
) {
    if (!$ApiKey) {
        $secrets = Get-Content $SecretsFile | ConvertFrom-Json
        $ApiKey = $secrets.nuget_api_key;
    }

    # example:
    # dotnet nuget push AppLogger.1.0.0.nupkg --api-key qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 --source https://api.nuget.org/v3/index.json
    
    if (!$nupkg) {
        $built_package = Build-Nupkg
        $nupkg = $built_package.FullName
    }

    @{
        nupkg  = $nupkg
        ApiKey = $ApiKey
    } | Out-String | Write-Host -ForegroundColor DarkGray

    dotnet.exe nuget push $nupkg --api-key $ApiKey --source "nuget.org"
}

<#
.SYNOPSIS
    Builds a new `.nupkg` file that can be published to NuGet.

.DESCRIPTION
    Invokes `dotnet.exe pack` in order to create the `.nupkg`, 
    then returns the **latest** `.nupkg` via `Get-LatestNupkg`.
#>
function Build-Nupkg([Parameter(Mandatory=$false)]$BeforeBuildTimestamp) {
    dotnet.exe pack

    return Get-LatestNupkg -OldestTimestamp $BeforeBuildTimestamp
}

#region .csproj and CHANGELOG.md shenanigans

<#
.SYNOPSIS
    Represents a `.csproj` file (a C# project).
#>
class CsProj {
    # not yet implemented!
    # TODO: Move functions like Get-CsProj into here
}

<#
.SYNOPSIS
    Represents a `CHANGELOG.md` file based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).
#>
class CHANGELOG {
    # not yet implemented!
}

<#
.SYNOPSIS
    Finds all of the `.csproj` files in a path and converts them to `[xml]` objects.

TODO: Move to the CsProj class
#>
function Get-CsProj(
    # The file path to search in.
    [string[]]$Path = ".",
    # Whether we should include packages that begin with "Test" or not.
    [switch]$IncludeTest
) {
    $file = Get-ChildItem -Path $Path -Filter "*.csproj" -Exclude ($IncludeTest ? $null : "Test*") -Recurse
    $content = $file | Get-Content
    $xml = $content | ForEach-Object { [xml]$_ }
    return $xml
}

<# 
.SYNOPSIS
    Retrieves the `<PackageVersion>` node from a `.csproj` file.

.PARAMETER CsProj
    The `.csproj` file's `[xml]` content.

TODO: Move to the CsProj class
#>
function Get-CsProjVersion([xml]$CsProj = (Get-CsProj)) {
    $xpath = '/Project/PropertyGroup/PackageVersion'
    return $CsProj | Select-Xml -XPath $xpath
}

# TODO: Move to the CsProj class
function Get-SemVerPattern([string]$GroupName = 'SemVer') {
    $pattern = '(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?'
    
    if ($GroupName) {
        $pattern = "(?<$GroupName>$pattern)"
    }

    return [regex]::new($pattern)
}

<#
.SYNOPSIS
    Matches a version heading according to [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).    

TODO: Move to the CHANGELOG class
#>
function Get-VersionLinePattern() {
    $heading = '(#+)?'
    $ws = "[\s-]*"
    $semVer = Get-SemVerPattern
    $date = "(?<date>.*)"

    $patParts = @(
        $heading,
        "\[",
        $semVer,
        "\]",
        "-",
        $date
    )

    $patStr = $patParts -join $ws
    $patStr = "^$patStr`$"
    return [regex]::new($patStr)
}

<#
.SYNOPSIS
    An _extremely_ silly class that prints line numbers and stuff.
#>
class LineGroup {
    [string[]]$Content
    [int]$Start
    [int]$End
    [string]$LineNumberFormat = "{0}"
    [string]$LineNumberSeparator = ". "
    [System.ConsoleColor]$PositiveColor = [System.ConsoleColor]::DarkGreen
    [System.ConsoleColor]$NegativeColor = [System.ConsoleColor]::DarkGray
    [System.ConsoleColor]$HeadingColor = [System.ConsoleColor]::Magenta
    [int]$PreLines = 3
    [int]$PostLines = 3

    [string[]] GetLines() {
        if ($this.OutOfBounds($this.Start) -or $this.OutOfBounds($this.End)) {
            throw "$($this.GetType().Name) range was out-of-bounds! [$($this.Start)..$($this.End)), line count: $($this.LineCount())"
        }

        return $this.Content[$this.Start..$this.End]
    }

    [hashtable] ParseVersionLine() {
        $first_line = $this.Content[$this.Start]

        $pattern = Get-VersionLinePattern

        if ($first_line -match $pattern) {
            $m = $Matches
            return $m
        }
        else {
            return $null
        }
    }

    [semver] Version() {
        return [semver]($this.ParseVersionLine()['SemVer'])
    }

    [datetime] Date() {
        return Get-Date $this.ParseVersionLine()['date']
    }

    [int] LineCount() {
        return $this.Content.Count;
    }

    [bool] OutOfBounds([int]$LineNumber) {
        return $LineNumber -lt 0 -or $LineNumber -ge $this.LineCount()
    }

    [bool] PositiveLine([int]$LineNumber) {
        return $LineNumber -ge $this.Start -and $LineNumber -lt $this.End
    }

    [int] GetLineNumberWidth() {
        return $this.FormatLineNumber($this.LineCount()).Length
    }

    [string] FormatLineNumber([int]$LineNumber) {
        return $this.LineNumberFormat -f $LineNumber
        return "$LineNumber"
    }

    [string] GetLineNumberString([int]$LineNumber) {
        $width = $this.GetLineNumberWidth()
        return "{0,-$width}" -f ($this.FormatLineNumber($LineNumber))
    }

    [int] GetContentWidth() {
        $line_width = (Get-Host).UI.RawUI.WindowSize.Width
        $line_number_width = $this.GetLineNumberWidth()
        $separator_width = $this.LineNumberSeparator.Length;
        return $line_width - $line_number_width - $separator_width
    }

    [string] GetLineContent([int]$LineNumber) {
        if ($this.OutOfBounds($LineNumber)) {
            return $null
        }

        $line = $this.Content[$LineNumber]
        $content_width = $this.GetContentWidth()
        
        if ($line.Length -gt $content_width) {
            $line = $line.Substring(0, $content_width - 1) + "…"
        }

        return $line
    }

    [PSCustomObject] GetPrintRange() {
        return [PSCustomObject]@{
            Start = [System.Math]::Clamp($this.Start - $this.PreLines, 0, $this.LineCount())
            End   = [System.Math]::Clamp($this.End + $this.PostLines, 0, $this.LineCount())
        }
    }

    [void] Print() {
        $this.PrintHeading()

        $range = $this.GetPrintRange()

        for ($i = $range.Start; $i -lt $range.End; $i++) {
            $this.PrintLine($i)
        }
    }

    [void] PrintHeading() {
        Write-Host "`n$($this.GetType().Name) # [$($this.Start)..$($this.End))" -ForegroundColor Magenta
    }

    [void] PrintLine([int]$LineNumber) {
        if ($this.OutOfBounds($LineNumber)) {
            return
        }

        $line_content = $this.GetLineContent($LineNumber)

        $content_color = ($LineNumber -ge $this.Start) -and ($LineNumber -lt $this.End) ?
        $this.PositiveColor :
        $this.NegativeColor

        Write-Host $this.GetLineNumberString($LineNumber) -ForegroundColor $content_color -NoNewline
        Write-Host $this.LineNumberSeparator -NoNewline
        Write-Host $line_content -ForegroundColor $content_color
    }
}

<#
.SYNOPSIS
    Splits the sections of a `CHANGELOG.md` file by version.

.DESCRIPTION
    Sections are delimited by `Get-VersionLinePattern`.
    `Unreleased` is considered a valid version as well, in alignment with [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

    TODO: Move to the CHANGELOG class
#>
function Split-Changelog([Parameter(ValueFromPipeline)][string[]]$Changelog) {
    $unreleasedPattern = [regex]'#+\s*Unreleased'
    $versionPattern = [regex]'#+\s*\[\d+\.\d+\.\d+\]\s*-\s*\d{4}-\d{2}-\d{2}'

    $lines = $Changelog | ForEach-Object { $_ -split "`n" }

    $groups = @()

    $start = $null
    for ($i = 0; $i -lt $lines.Length; $i++) {
        $ln = $lines[$i]
        if (($ln -match $versionPattern) -or ($ln -match $unreleasedPattern)) {
            if ($null -eq $start) {
                Write-Verbose "Starting a new group at [$i]"
                $start = $i
            }
            else {
                Write-Verbose "Finish group from [$start..$i)"
                $LG = [LineGroup]::new()
                $LG.Content = $Lines
                $LG.Start = $start
                $LG.End = $i
                $groups += @($LG)
                $start = $i

                # $LG.Print()
            }
        }
    }

    return $groups
}

#endregion

<#
.SYNOPSIS
    (NOT DONE) A class for managing `.nupkg` files.
#>
class NuPkg {
    [ValidatePattern("\.nupkg`$")]
    [System.IO.FileInfo]$File

    NuPkg([System.IO.FileInfo] $file) {
        # usage of Get-Item ensures that the file exists
        $this.File = Get-Item $file
    }

    [semver] Version() {
        throw "Not yet implemented!"
    }
}

<#
.SYNOPSIS
    Finds the most recently written `.nupkg` file.

.DESCRIPTION
    "Latest" is determined by `FileSystemInfo.LastWriteTime`.
#>
function Get-LatestNupkg(
    # - The path to the **folder** containing the `.nupkg` files.
    # - Corresponds to the `Get-ChildItem -Path` parameter.
    [Parameter()]
    [string[]]$Path = ".\Conjugal\bin\Release",

    # If specified, then only files with a `LastWriteTime >= OldestTimestamp` will be considered.
    [nullable[datetime]]$OldestTimestamp,
    
    # Passed through to the `Get-ChildItem -Recurse` parameter.
    [switch]$Recurse,

    # Determines how many .csproj files to return: 
    #  - Latest -> [Default] The most recent file
    #  - All    -> All located files
    #  - Only   -> Throws an error if we don't find *exactly one* file
    # 
    # 📝 NOTE: An error will _always_ be thrown in the event that 0 files are found.
    [ValidateSet("Latest", "All", "Only")]
    [string]$Return = "Latest",

    # A predicate used to filter the results via `Where-Object`.
    [scriptblock]$Where
) {
    Write-Verbose "Checking for .nupkg files at [$Path]"
    Write-Verbose "Oldest timestamp: $OldestTimestamp"

    $pkgs = Get-ChildItem -Path $Path -Filter "*.nupkg" -Recurse:$Recurse
    $pkgs = $pkgs | Sort-Object LastWriteTime -Descending

    Write-Verbose "Found $($pkgs.Count) .nupkg files"

    # Apply filters
    if ($OldestTimestamp) {
        Write-Verbose "Filtering for files with a LastWriteTime >= $OldestTimestamp (-OldestTimestamp)"
        $pkgs = $pkgs | Where-Object LastWriteTime -GE $OldestTimestamp
        Write-Verbose " -> $($pkgs.Count) files remain"
    }

    if ($Where) {
        Write-Verbose "Applying the Where-Object predicate $where"
        $pkgs = $pkgs | Where-Object -FilterScript $Where
        Write-Verbose " -> $($pkgs.Count) files remain"
    }

    # Validate the results
    if ($pkgs.Count -eq 0) {
        throw "Didn't find ANY packages at ALL!"
        Write-Warning "SHOULD NOT HAVE BEEN REACHABLE"
        return
    }

    Write-Verbose "Applying the -Return parameter: [$Return]"
    switch ($Return) {
        "Latest" { 
            Write-Verbose "Returning the latest of the [$($pkgs.Count)] results"
            return $pkgs | Select-Object -First 1 
        }
        "All" { 
            Write-Verbose "Returning all [$($pkgs.Count)] results"
            return $pkgs 
        }
        "Only" { 
            if($pkgs.Count -ne 1){
                throw "Expected to find EXACTLY ONE package, but found $($pkgs.Count)!"
            }
            Write-Verbose "Returning the single result, as expected"
            return $pkgs[0]
        }
        Default {
            throw "UNHANDLED value for the -Return parameter: [$Return]"
        }
    }
}