---
template: layout.jade
title: Building
menusection: concepts
menuitem: building
---

# Building

Building `Elasticsearch.Net` and `NEST` should be super straightforward. Just fork and clone [the repository on github](https://github.com/elasticsearch/elasticsearch-net) and open `Elasticsearch.sln` and press build. Alternatively you can run `build.bat` from the root to build everything the resulting assemblies will be exported to `build/output`. 

NEST builds strong named assemblies but if you do not have the key the build script and `pre build events` are in place to generate one for you. The project very much believes in the [F5 manifesto](http://www.khalidabuhakmeh.com/the-f5-manifesto-for-net-developers)

## Buildscript
The buildscript depends on quite a few tools

* FAKE - F# make to script the build
* Nuget - used to install some of the dependencies
* NUnit - to run the tests after build
* Nodejs - Use to generate the documentation
* Wintersmith - Used to generate the documentation
* sn.exe - used to check the validity of the signed assemblies during build/release procedures

All of these will be downloaded and installed locally in your repository the first time you run `build.bat` 

**NOTE:** none of these are needed to build from within Visual Studio

### Build target
Running `build.bat` will default to running the `Build` target which will create certificates in the right place if they do not exist, build the application and run all the unit tests. The resulting assemblies are placed in `build/output`.

### DocPreview target
Running `build.bat DocPreview` will serve the files under `docs` at `http://localhost:8080` with live-reload setup. Usefull only when you are working on documentation.

### Release target
Running `build.bat Release versionnumber` will create nuget packages in `output/_packages`. This could be useful if you need a feature that is not yet released you can temporarily switch to local nuget packages.

Release will call the build target and in addition patch the assembly files, build the documentation files. 
