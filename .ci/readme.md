## Jenkins test folder

This `.ci` folder is used by https://clients-ci.elastic.co

Where each Elasticsearch client runs the [rest api spec test](https://github.com/elastic/elasticsearch/tree/main/rest-api-spec/src/main/resources/rest-api-spec/test)
as defined by the Elasticsearch team.

Each client starts the cluster using the same `.ci/run-elasticsearch` from `run-tests` and then bootstraps there own `rest api test runner`.api

The .NET rest api spec runner lives under `src/Tests/Test.YamlRunner`. This runner takes the test yaml files and turns them into .NET instructions.api

Each `do` section in the yaml is mapped to a low level client method. To aid with assertions the tests ask for `DynamicResponse` which is a great 
way to deal with elasticsearch responses in a semi typed fashion.

These `rest-api-spec` tests are in addition to the unit and integration tests that live under `src/Tests/Tests` which for 90% of the cases focuses 
more on the high level client `NEST` where as these `rest-api-spec` tests fully focus on the low level client `Elasticsearch.Net`

The `DockerFile` in this folder sets up this repo inside a docker container ready to run our build.

The `rest-api-spec` runner expects Elasticsearch to be started before invocation and uses the endpoint its passed to discover the current Elasticsearch
`build hash` that is running and downloads the tests matching that `build hash`.

If you want to run the tests the same way that the Jenkins instance on elastic.co does you can call 

```bash
$ STACK_VERSION=8.0.0-SNAPSHOT ./.ci/run-tests 
```

| Variable Name           | Default     | Description |
|-------------------------|-------------|-------------|
| `STACK_VERSION` | `N/A`       | The elasticsearch version to target
| `TEST_SUITE`            | `basic`     | `free` or `platinum` sets which test suite to run and which container to run against. |
| `DOTNET_VERSION`        | `8.0.100`   | The .NET sdk version used to grab the proper container |

If you want to manually spin up elasticsearch for these tests and call the runner afterwards you can use

```bash
$ STACK_VERSION=8.0.0-SNAPSHOT DETACH=true bash .ci/run-elasticsearch.sh
```

Spinning down the cluster can be done by passing `CLEANUP=true` using the same args

```bash
$ STACK_VERSION=8.0.0-SNAPSHOT CLEANUP=true bash .ci/run-elasticsearch.sh
```

To kick off the `rest-api-spec` tests manually after starting the cluster manually:

```bash
$ ./build.sh rest-spec-tests -f create -e http://localhost:9200 -o /sln/build/output/rest-spec-junit.xml
```

See `--help` for a full command line reference

```bash
$ ./build.sh rest-spec-tests -f --help
```

Against in most cases running through `./ci/run-tests` is all you need:

```bash
$ STACK_VERSION=8.0.0-SNAPSHOT ./.ci/run-tests 
```