@echo off

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)
.paket\paket.exe install
if errorlevel 1 (
  exit /b %errorlevel%
)

SET TARGET="build"
SET VERSION=
SET ESVERSIONS=

IF NOT [%1]==[] (set TARGET="%1")

IF "%1"=="release" (
    IF NOT [%2]==[] ( set VERSION="%2" )
)

IF "%1%"=="integrate" (
    IF NOT [%2]==[] (set ESVERSIONS="%2")
)

shift
shift

"packages\build\FAKE\tools\Fake.exe" "build\\scripts\\Targets.fsx" "target=%TARGET%" "version=%VERSION%" esversions=%ESVERSIONS%