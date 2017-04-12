@echo off

REM USAGE:
REM
REM build [skiptests] <command> [params]
REM 
REM COMMANDS:
REM
REM * build  
REM		default target if non provided. Performs a clean, rebuild and test of all target frameworks
REM * quick [testfilter]
REM		incremental build and unit test for .NET 4.5, [testfilter] allows you to do 
REM		a contains match on the tests to be run.
REM * build release <version> 
REM		create a release worthy nuget packages for [version] under build\output
REM * build integrate <elasticsearch_versions> [clustername] [testfilter]  - 
REM		run integration tests for <elasticsearch_versions> which is a semicolon separated list of 
REM		elasticsearch versions to test or `latest`. Can filter tests by <clustername> and <testfilter>
REM * build canary [apikey] [feed] [skiptests] 
REM		create a canary nuget package based on the current version if [feed] and [apikey] are provided 
REM		also pushes to upstream (myget)

SET TARGET="build"
SET VERSION=
SET ESVERSIONS=
SET SKIPTESTS=0
SET APIKEY=
SET APIKEYPROVIDED="<empty>"
SET FEED="elasticsearch-net"
SET NEST_INTEGRATION_CLUSTER=
SET NEST_TEST_FILTER=
SET ELASTICSEARCH=

IF /I "%1"=="skiptests" (
	set SKIPTESTS="1"
	SHIFT
)
IF NOT [%1]==[] (set TARGET=%1)

SET SKIPPAKET=0
IF /I "%TARGET%"=="inc" SET SKIPPAKET=1
IF /I "%TARGET%"=="canary" SET SKIPTESTS=1

IF "%SKIPPAKET%" neq "1" (
	.paket\paket.bootstrapper.exe
	IF EXIST paket.lock (.paket\paket.exe restore)
	IF NOT EXIST paket.lock (.paket\paket.exe install)
)

REM if `build quick` is called on a fresh checkout force a restore anyway
IF "%SKIPPAKET%"=="1" (
	IF NOT EXIST .paket\paket.exe (
		.paket\paket.bootstrapper.exe
	)
	IF EXIST paket.lock (.paket\paket.exe restore)
	IF NOT EXIST paket.lock (.paket\paket.exe install)
)

IF /I "%TARGET%"=="version" (
	IF NOT [%2]==[] (set VERSION="%2")
)
IF /I "%TARGET%"=="release" (
	IF NOT [%2]==[] (set VERSION="%2")
	IF /I "%JAVA_HOME%"=="" (
	   ECHO JAVA_HOME not set exiting release early!
	   EXIT /B 1
	)
)
IF /I "%TARGET%"=="inc" (
	IF NOT [%2]==[] (set NEST_TEST_FILTER="%2")
)
IF /I "%TARGET%"=="integrate" (
	IF NOT [%2]==[] (set ESVERSIONS="%2")
	IF NOT [%3]==[] (set NEST_INTEGRATION_CLUSTER="%~3")
	IF NOT [%4]==[] (set NEST_TEST_FILTER="%4")
	IF /I "%JAVA_HOME%"=="" (
	   ECHO JAVA_HOME not set exiting release early!
	   EXIT /B 1
	)
)
IF /I "%TARGET%"=="canary" (
	IF NOT [%2]==[] (
		set APIKEY="%2"
		SET APIKEYPROVIDED="<redacted>"
	)
	IF NOT [%3]==[] set FEED="%3"
)
IF /I "%TARGET%"=="profile" (
	IF NOT [%2]==[] (set ELASTICSEARCH="%2")
	IF NOT [%3]==[] (set NEST_TEST_FILTER="%3")
)

ECHO build.bat: target=%TARGET% skippakket=%SKIPPAKET% version=%VERSION% esversions=%ESVERSIONS% skiptests=%SKIPTESTS% apiKey=%APIKEYPROVIDED% feed=%FEED% escluster=%NEST_INTEGRATION_CLUSTER% testfilter=%NEST_TEST_FILTER% elasticsearch=%ELASTICSEARCH%
"packages\build\FAKE\tools\Fake.exe" "build\\scripts\\Targets.fsx" "target=%TARGET%" "version=%VERSION%" "esversions=%ESVERSIONS%" "skiptests=%SKIPTESTS%" "apiKey=%APIKEY%" "feed=%FEED%" "escluster=%NEST_INTEGRATION_CLUSTER%" "testfilter=%NEST_TEST_FILTER%" "elasticsearch=%ELASTICSEARCH%"
