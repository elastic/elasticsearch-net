@echo off
if not exist build\tools\nuget\nuget.exe (
    ECHO Nuget not found.. Downloading..
    PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'build\download-nuget.ps1'"
)
if not exist build\tools\FAKE\tools\Fake.exe (
    ECHO FAKE not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)
if not exist build\tools\NUnit.Runners\nunit-console.exe (
    ECHO Nunit not found.. Installing
    "build\tools\nuget\nuget.exe" "install" "NUnit.Runners" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)
SET TARGET="Default"

IF NOT [%1]==[] (set TARGET="%1")

"build\tools\FAKE\tools\Fake.exe" "build\build.fsx" "target=%TARGET%"