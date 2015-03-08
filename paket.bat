@echo off
REM we need nuget to install tools locally
if not exist build\tools\nuget\nuget.exe (
    ECHO Nuget not found.. Downloading..
    mkdir build\tools\nuget
    PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'build\download-nuget.ps1'"
)

REM we need paket to call paket doh
if not exist build\tools\Paket\tools\paket.exe (
    ECHO Paket not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "Paket" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)

build\tools\Paket\tools\paket.exe %*
