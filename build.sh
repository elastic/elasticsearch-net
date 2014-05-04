NUGET="build/tools/nuget/nuget.exe"
FAKE="build/tools/FAKE/tools/Fake.exe"
NUNIT="build/tools/NUnit.Runners/tools/nunit-console.exe"


#we need nuget to install tools locally
if [[ ! -f "$NUGET" ]]; then
   echo NUGET not found.. Download...
   mkdir -p build/tools/nuget
   curl -o $NUGET http://build.nuget.org/drops/client/master/NuGet.exe
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

# we need wintersmith to build our documentation which in turn needs npm/node
# installing and calling this locally so that yours and CI's systems do not need to be configured prior to running build.bat
#if not exist build\tools\Node.js\node.exe (
#    ECHO Local node not found.. Installing..
#    "build\tools\nuget\nuget.exe" "install" "node.js" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
#)
#if not exist build\tools\Npm\node_modules\npm\cli.js (
#    ECHO Local npm not found.. Installing..
#    "build\tools\nuget\nuget.exe" "install" "npm" "-OutputDirectory" "build\tools" "-ExcludeVersion" "-Prerelease"
#)
#if not exist build\tools\node_modules\wintersmith\bin\wintersmith (
#    ECHO wintersmith not found.. Installing.. 
#    cd build\tools
#
#    "Node.js\node.exe" "Npm\node_modules\npm\cli.js" install wintersmith
#
#    cd ..\..
#)


#SET TARGET="Build"
#SET VERSION="0.1.0"
#IF NOT [%1]==[] (set TARGET="%1")
#IF NOT [%2]==[] (set VERSION="%2")

mono --runtime=v4.0 "$NUGET" install FSharp.Formatting.CommandTool -OutputDirectory build/tools -ExcludeVersion -Prerelease 
mono --runtime=v4.0 "$NUGET" install SourceLink.Fake -OutputDirectory build/tools -ExcludeVersion 
#workaround assembly resolution issues in build.fsx
export FSHARPI=`which fsharpi`
cat - > fsharpi <<"EOF"
#!/bin/bash
libdir=$PWD/build/tools/FAKE/tools/
$FSHARPI --lib:$libdir $@
EOF
chmod +x fsharpi
mono --runtime=v4.0 "$FAKE" build/build.fsx $@
rm fsharpi
#"build\tools\FAKE\tools\Fake.exe" "build\build.fsx" "target=%TARGET%" "version=%VERSION%"
