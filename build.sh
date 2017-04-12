#!/usr/bin/env bash
FAKE="packages/build/FAKE/tools/FAKE.exe"
BUILDSCRIPT="build/scripts/Targets.fsx"


TARGET="build"
VERSION=
ESVERSIONS=
SKIPTESTS=0
APIKEY=
APIKEYPROVIDED="<empty>"
FEED="elasticsearch-net"
NEST_INTEGRATION_CLUSTER=
NEST_TEST_FILTER=

if [[ "${1,,}" == "skiptests" ]]; then SKIPTESTS=1; shift; fi

if [[ ! -z "$1" ]]; then TARGET=$1; fi

if [[ "${TARGET,,}" == "inc" ]]; then SKIPPAKET=1; fi
if [[ "${TARGET,,}" == "forever" ]]; then SKIPPAKET=1; fi

if [[ $SKIPPAKET -ne 1 ]]; then
	mono .paket/paket.bootstrapper.exe
	mono .paket/paket.exe restore
fi
if [[ $SKIPPAKET -eq 1 && ! -f .paket/paket.exe ]]; then
	mono .paket/paket.bootstrapper.exe
	mono .paket/paket.exe restore
fi
if [[ "${TARGET,,}" == "inc" ]] || [[ "${TARGET,,}" == "forever" ]]; then
	if [[ ! -z "$2" ]]; then NEST_TEST_FILTER=$2; fi
fi

if [[ "${TARGET,,}" == "integrate" ]]; then
	if [[ ! -z "$2" ]]; then ESVERSIONS=$2; fi
	if [[ ! -z "$3" ]]; then NEST_INTEGRATION_CLUSTER=$3; fi
	if [[ ! -z "$4" ]]; then NEST_TEST_FILTER=$4; fi
	if [[ ! -z "$JAVA_HOME" ]]; then
		echo JAVA_HOME not set so no point in running integration tests is there?!
		exit /b 1
	fi
fi

if [[ "${TARGET,,}" == "canary" ]]; then
	if [[ ! -z "$2" ]]; then
		APIKEY=$2
		APKEYPROVIDED="<redacted>"
	fi
	if [[ ! -z "$3" ]]; then FEED=$3; fi
fi

echo build.sh: target=$TARGET skippakket=$SKIPPAKET version=$VERSION esversions=$ESVERSIONS skiptests=$SKIPTESTS apiKey=$APIKEYPROVIDED feed=$FEED escluster=$NEST_INTEGRATION_CLUSTER testfilter=$NEST_TEST_FILTER

export TARGET
export VERSION
export ESVERSIONS
export NEST_INTEGRATION_CLUSTER
export NEST_TEST_FILTER
mono $FAKE $@ --fsiargs -d:MONO $BUILDSCRIPT "target=$TARGET" "version=$VERSION" "esversions=$ESVERSIONS" "skiptests=$SKIPTESTS" "apiKey=$APIKEY" "feed=$FEED" "escluster=$NEST_INTEGRATION_CLUSTER" "testfilter=$NEST_TEST_FILTER"
