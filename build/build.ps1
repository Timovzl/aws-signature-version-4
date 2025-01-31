# -------------------------------------------------------------------------------------------------
# COMMON FUNCTIONS
# -------------------------------------------------------------------------------------------------
function Print {
    param (
        [string]$Category,
        [string]$Message
    )

    if ($Category) {
        Write-Host "[$Category] $Message" -ForegroundColor Green
    } else {
        Write-Host "$Message" -ForegroundColor Green
    }
}

function AssertLastExitCode {
    if ($LASTEXITCODE -ne 0) {
        exit 1
    }
}

# -------------------------------------------------------------------------------------------------
# LOGO
# -------------------------------------------------------------------------------------------------
$logo = (Invoke-WebRequest "https://raw.githubusercontent.com/FantasticFiasco/logo/master/logo.raw").toString();
Print -Message $logo

# -------------------------------------------------------------------------------------------------
# VARIABLES
# -------------------------------------------------------------------------------------------------
$git_sha = "$env:APPVEYOR_REPO_COMMIT".substring(0, 7)
$is_tagged_build = If ("$env:APPVEYOR_REPO_TAG" -eq "true") { $true } Else { $false }
$is_pull_request = If ("$env:APPVEYOR_PULL_REQUEST_NUMBER" -eq "") { $false } Else { $true }
Print "info" "git sha: $git_sha"
Print "info" "is git tag: $is_tagged_build"
Print "info" "is pull request: $is_pull_request"

# -------------------------------------------------------------------------------------------------
# BUILD
# -------------------------------------------------------------------------------------------------
Print "build" "build started"
Print "build" "dotnet cli v$(dotnet --version)"

[xml]$build_props = Get-Content -Path .\Directory.Build.props
$version_prefix = $build_props.Project.PropertyGroup.VersionPrefix
Print "info" "build props version prefix: $version_prefix"
$version_suffix = $build_props.Project.PropertyGroup.VersionSuffix
Print "info" "build props version suffix: $version_suffix"

if ($is_tagged_build) {
    Print "build" "build"
    dotnet build -c Release
    AssertLastExitCode

    Print "build" "pack"
    dotnet pack -c Release -o .\artifacts --no-build
    AssertLastExitCode
} else {
    # Use git tag if version suffix isn't specified
    if ($version_suffix -eq "") {
        $version_suffix = $git_sha
    }

    Print "build" "build"
    dotnet build -c Release --version-suffix=$version_suffix
    AssertLastExitCode

    Print "build" "pack"
    dotnet pack -c Release -o .\artifacts --version-suffix=$version_suffix --no-build
    AssertLastExitCode
}

# -------------------------------------------------------------------------------------------------
# TEST
# -------------------------------------------------------------------------------------------------
Print "test" "test started"

if ($is_pull_request -eq $true) {
    # Exclude integration tests if we run as part of a pull requests. Integration tests rely on
    # secrets, which are omitted by AppVeyor on pull requests.
    dotnet test -c Release --no-build --filter Category!=Integration
    AssertLastExitCode
} else {
    dotnet test -c Release --no-build --collect:"XPlat Code Coverage"
    AssertLastExitCode

    Print "test" "download codecov uploader"
    Invoke-WebRequest -Uri https://uploader.codecov.io/latest/codecov.exe -Outfile codecov.exe

    foreach ($test_result in Get-ChildItem .\test\TestResults\*\coverage.cobertura.xml)
    {
        $relative_test_result = $test_result | Resolve-Path -Relative

        # CodeCode uploader cant handle "\", thus we have to replace these with "/"
        $relative_test_result = $relative_test_result -Replace "\\", "/"

        Print "test" "upload coverage report $relative_test_result"

        .\codecov.exe -f $relative_test_result
        AssertLastExitCode
    }
}

# -------------------------------------------------------------------------------------------------
# INFRASTRUCTURE
# -------------------------------------------------------------------------------------------------
Print "infrastructure" "build started"
Print "infrastructure" "node $(node --version)"
yarn --cwd ./infrastructure
yarn --cwd ./infrastructure build
