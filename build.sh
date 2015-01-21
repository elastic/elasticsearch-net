NUGET="build/tools/nuget/nuget.exe"
FAKE="build/tools/FAKE/tools/FAKE.exe"
NUNIT="build/tools/NUnit.Runners/tools/nunit-console.exe"
FSHARPCLI="build/tools/Fsharp.Formatting.CommandTool/Fsharp.Formatting.CommandTool.nupkg"
SOURCELINK="build/tools/SourceLink.Fake/SourceLink.Fake.nupkg"

#we need nuget to install tools locally
if [[ ! -f "$NUGET" ]]; then
   echo NUGET not found.. Download...
   mkdir -p build/tools/nuget
   curl -o $NUGET https://nugetbuild.cloudapp.net/drops/client/master/NuGet.exe
fi

#we need FAKE to process our build scripts
if [[ ! -f "$FAKE" ]]; then
    echo  FAKE not found.. Installing..
    mono --runtime=v4.0 "$NUGET" "install" "FAKE" "-OutputDirectory" "build/tools" "-ExcludeVersion" "-Prerelease"
fi

# we need nunit-console to run our tests
if [[ ! -f "$NUNIT" ]]; then
    echo Nunit not found.. Installing
    mono --runtime=v4.0 "$NUGET" "install" "NUnit.Runners" "-OutputDirectory" "build/tools" "-ExcludeVersion" "-Prerelease"
fi

if [[ ! -f "$FSHARPCLI" ]]; then
    echo Fsharp formatting commandtool not found... Installing..
    mono --runtime=v4.0 "$NUGET" install FSharp.Formatting.CommandTool -OutputDirectory build/tools -ExcludeVersion -Prerelease 
fi
if [[ ! -f "$SOURCELINK" ]]; then
    echo SourceLink not found.. installing
    mono --runtime=v4.0 "$NUGET" install SourceLink.Fake -OutputDirectory build/tools -ExcludeVersion 
fi

#workaround assembly resolution issues in build.fsx
export FSHARPI=`which fsharpi`
cat - > fsharpi <<"EOF"
#!/bin/bash
libdir=$PWD/build/tools/FAKE/tools/
$FSHARPI --lib:$libdir $@
EOF
chmod +x fsharpi
mono --runtime=v4.0 "$FAKE" build/scripts/build.fsx "skiptests=1" $@
MONOEXIT=$?
rm fsharpi
#FORCE exit code to be that of calling fake not the last rm action
exit $MONOEXIT
#"build\tools\FAKE\tools\Fake.exe" "build\build.fsx" "target=%TARGET%" "version=%VERSION%" "skiptests=1"
