# Contributing

Contributing back to the .NET client for Elasticsearch is very much appreciated. 
Whether you [feel the need to change one character](https://github.com/elastic/elasticsearch-net/pull/536) or have a go at 
[mapping new APIs](http://github.com/elastic/elasticsearch-net/pull/376), no pull request (PR) is too small or too big. 

In fact, many of our most awesome features/fixes have been provided to us by 
[these wonderful folks](https://github.com/elastic/elasticsearch-net/graphs/contributors) to which we are forever indebted. 

It's usually best to open an issue first to discuss a feature or bug before opening a pull request. Doing so can save time and help further ascertain the crux of an issue.

## Sign the CLA

We ask that you sign the [Elasticsearch CLA](https://www.elastic.co/contributor-agreement) before we can accept pull requests from you. 

## Coding Styleguide

The repository includes an editor.config file, which will automatically switch to our indentation, whitespace, and newline settings while working on our project
**while leaving your default settings intact**.

In most cases, we won't shun a PR just because it uses the wrong indentation settings, though it'll be **very** much appreciated if it is already done!

## Tests

PRs with tests are more likely to be reviewed faster because it makes reviewing the PR much easier. That being said,
we respect that you may be fixing a bug in your own time and may not have the time/energy to submit a PR with complete tests. 
In those cases, we tend to pull your bits locally and write tests ourselves, but this may mean your PR might sit idle longer than you would like.

## Branches

Convention:

- `main` reflects the latest server version, typically the `current latest minor + 1`. Most PRs should target this version, and we will label it for backporting to appropriate release branches.
- `N.Y` where `N` is the major version, and `Y` is the minor component and represents a release branch. Changes merged to these branches will be released in the next patch release for that version.

Historical:

- `N.x` where N represents the major version component of the Elasticsearch server release it's integrating with; e.g. `6.x`

Examples:

- `main` for the latest server version (currently _8.vNext_)
- `8.0` for 8.x compatible client
- `7.17` for 7.x compatible client
- `6.x` for 6.x compatible client (no longer maintained)
- `5.x` for 5.x compatible client (no longer maintained)
- `2.x` for 2.x compatible client (no longer maintained)
- `1.x` for 1.x compatible client (no longer maintained)

## Git

We do not require rebased/squashed commits, although we greatly appreciate it! 

Please submit your [Pull Requests](https://help.github.com/articles/creating-a-pull-request/) to 

- [`main`](https://github.com/elastic/elasticsearch-net/tree/main) branch for 8.x
- [`7.17`](https://github.com/elastic/elasticsearch-net/tree/7.17) branch for 7.x

# Building the solution

The solution uses several awesome Open Source software tools to ease development:

## Bullseye

[Bullseye](https://github.com/adamralph/bullseye) is used as the build automation system for the solution. To get started after cloning the solution, it's best to run the build script in the root

for Windows 

```
.\build.bat
```

for OSX/Linux

```
./build.sh
```

This will

- Pull down all the dependencies for the build process as well as the solution
- Run the default build target for the solution

You can also compile the solution within Visual Studio if you prefer, but the build script is going to be _much_ faster.

## Tests

The `Tests` project contains both xunit unit and integration tests. A `tests.yaml` file within the root of the `Tests` project determines the test mode when running tests inside Visual Studio.

- `u` for unit tests
- `i' for integration tests
- `m' for mixed-mode i.e. unit and integration tests

The build script has several different build targets to run different types of tests, see the [`Targets.fs` file in the `scripts` project](https://github.com/elastic/elasticsearch-net/blob/main/build/scripts/Targets.fs) for the complete list, but the main ones are:

### Compile and run unit tests

```bat
.\build.bat
```
with no target will run the `Build` target, compiling the solution and running unit tests

### Compile

```bat
.\build.bat skiptests
```

This compiles the solution and skips running tests

### Quick Compile and run integration tests

```bat
.\build.bat integrate [Elasticsearch Version Number e.g. 8.3.2]
```
will quick compile the solution and run integration tests against the target Elasticsearch version. The first time this is run for a version of Elasticsearch, it will download Elasticsearch and unzip Elasticsearch, install the plugins necessary to run the integration tests, and start the node. Because of this, the first run may take some time to start.