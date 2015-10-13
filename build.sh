#!/usr/bin/env bash
FAKE="packages/build/FAKE/tools/FAKE.exe"
BUILDSCRIPT="build/scripts/Targets.fsx"

if test "$OS" = "Windows_NT"
then
  # use .Net

  .paket/paket.bootstrapper.exe prerelease
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi

  .paket/paket.exe install
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi

  $FAKE $@ --fsiargs -d:MONO $BUILDSCRIPT
else
  # use mono
  mono .paket/paket.bootstrapper.exe prerelease
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi

  mono .paket/paket.exe install
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
  	exit $exit_code
  fi
  mono $FAKE $@ --fsiargs -d:MONO $BUILDSCRIPT
fi
