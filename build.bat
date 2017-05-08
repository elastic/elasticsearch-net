@echo off

.paket\paket.bootstrapper.exe
IF EXIST paket.lock (.paket\paket.exe restore)
IF NOT EXIST paket.lock (.paket\paket.exe install)
"packages\build\FAKE\tools\Fake.exe" "build\\scripts\\Targets.fsx" "cmdline=%*"
