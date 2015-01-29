@echo off
REM we need nuget to install tools locally
if not exist build\tools\nuget\nuget.exe (
    ECHO Nuget not found.. Downloading..
	mkdir build\tools\nuget
    PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'build\download-nuget.ps1'"
)

REM we need FAKE to process our build scripts
if not exist build\tools\FAKE\tools\Fake.exe (
    ECHO FAKE not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)

if not exist build\tools\gitlink\lib\net45\gitlink.exe (
    ECHO Local node not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "gitlink" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)

REM we need nunit-console to run our tests
if not exist build\tools\NUnit.Runners\tools\nunit-console.exe (
    ECHO Nunit not found.. Installing
    "build\tools\nuget\nuget.exe" "install" "NUnit.Runners" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)

REM we need wintersmith to build our documentation which in turn needs npm/node
REM installing and calling this locally so that yours and CI's systems do not need to be configured prior to running build.bat


if not exist build\tools\Node.js\node.exe (
    ECHO Local node not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "node.js" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)
if not exist build\tools\Npm\node_modules\npm\cli.js (
    ECHO Local npm not found.. Installing..
    "build\tools\nuget\nuget.exe" "install" "npm" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
)
if not exist build\tools\node_modules\wintersmith\bin\wintersmith (
    ECHO wintersmith not found.. Installing.. 
    cd build\tools
    "Node.js\node.exe" "Npm\node_modules\npm\cli.js" install wintersmith
    cd ..\..
)


SET TARGET="Build"
SET VERSION=

IF NOT [%1]==[] (set TARGET="%1")

IF NOT [%2]==[] (set VERSION="%2")

shift
shift

"build\tools\FAKE\tools\Fake.exe" "build\\scripts\\build.fsx" "target=%TARGET%" "version=%VERSION%"