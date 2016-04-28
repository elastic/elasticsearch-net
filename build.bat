@echo off

REM build 
REM build build [skiptests]
REM build release [version] [skiptests]
REM build version [version] [skiptests]
REM build integrate [elasticsearch_versions] [skiptests]

REM - elasticsearch_versions can be multiple separated with a semi-colon ';'

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)
.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

SET TARGET="build"
SET VERSION=
SET ESVERSIONS=
SET DNXVERSION="default"
SET SKIPTESTS=0
SET APIKEY=
SET FEED="elasticsearch-net"

IF /I "%1"=="skiptests" (
	set SKIPTESTS="1"
	SHIFT
)

IF NOT [%1]==[] (set TARGET="%1")

IF /I "%1"=="version" (
	IF NOT [%2]==[] (set VERSION="%2")
	IF /I "%3"=="skiptests" (set SKIPTESTS=1)
	IF /I "%2"=="skiptests" (set SKIPTESTS=1)
)
IF /I "%1"=="release" (
	IF NOT [%2]==[] (set VERSION="%2")
	IF /I "%3"=="skiptests" (set SKIPTESTS=1)
	IF /I "%2"=="skiptests" (set SKIPTESTS=1)
)

IF /I "%1%"=="integrate" (
	IF NOT [%2]==[] (set ESVERSIONS="%2")
	IF /I "%3"=="skiptests" (set SKIPTESTS=1)
	IF /I "%2"=="skiptests" (set SKIPTESTS=1)
)

IF /I "%1%"=="canary" (
	IF NOT [%2]==[] (set APIKEY="%2")
	IF NOT [%3]==[] (set FEED="%3")
)

"packages\build\FAKE\tools\Fake.exe" "build\\scripts\\Targets.fsx" "target=%TARGET%" "version=%VERSION%" "esversions=%ESVERSIONS%" "skiptests=%SKIPTESTS%" "apiKey=%APIKEY%" "feed=%FEED%"
