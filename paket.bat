@echo off
REM we need nuget to install tools locally
.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe %*
