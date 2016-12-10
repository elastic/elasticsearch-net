@echo off

REM build 
REM build build [skiptests]
REM build release [version] [skiptests]
REM build version [version] [skiptests]
REM build integrate [elasticsearch_versions] [clustername]
REM build canary [apikey] [feed] [skiptests]

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
SET SKIPTESTS=0
SET APIKEY=
SET APIKEYPROVIDED="<empty>"
SET FEED="elasticsearch-net"
SET NEST_INTEGRATION_CLUSTER=
SET NEST_TEST_FILTER=

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
	IF /I "%JAVA_HOME%"=="" (
	   ECHO JAVA_HOME not set exiting release early!
	   EXIT /B 1
	)
)

IF /I "%1%"=="integrate" (
	IF NOT [%2]==[] (set ESVERSIONS="%2")
	IF NOT [%3]==[] (set NEST_INTEGRATION_CLUSTER="%3")
	IF NOT [%4]==[] (set NEST_TEST_FILTER="%4")
	IF /I "%JAVA_HOME%"=="" (
	   ECHO JAVA_HOME not set exiting integration tests early!
	   EXIT /B 1
	)
)

IF /I "%1%"=="canary" (
	IF NOT [%2]==[] IF NOT "%2"=="skiptests" (
		set APIKEY="%2"
		SET APIKEYPROVIDED="<redacted>"
	)
	IF NOT [%3]==[] IF NOT "%3"=="skiptests" set FEED="%3"
	IF /I "%4"=="skiptests" (set SKIPTESTS=1)
	IF /I "%3"=="skiptests" (set SKIPTESTS=1)
	IF /I "%2"=="skiptests" (set SKIPTESTS=1)
)

ECHO starting build using target=%TARGET% version=%VERSION% esversions=%ESVERSIONS% skiptests=%SKIPTESTS% apiKey=%APIKEYPROVIDED% feed=%FEED% escluster=%NEST_INTEGRATION_CLUSTER% testfilter=%NEST_TEST_FILTER%
"packages\build\FAKE\tools\Fake.exe" "build\\scripts\\Targets.fsx" "target=%TARGET%" "version=%VERSION%" "esversions=%ESVERSIONS%" "skiptests=%SKIPTESTS%" "apiKey=%APIKEY%" "feed=%FEED%" "escluster=%NEST_INTEGRATION_CLUSTER%" "testfilter=%NEST_TEST_FILTER%"
