# AGENTS.md

## Setup commands

The repository builds with the standard .NET SDK CLI; the SDK version is pinned in `global.json`.

- Build the client: `dotnet build src/Elastic.Clients.Elasticsearch/Elastic.Clients.Elasticsearch.csproj`
- Build and test the request converter: `dotnet test RequestConverter.sln` (requires the `wasm-tools` workload:
  `dotnet workload install wasm-tools`)

A legacy Bullseye build system (`./build.sh`, `.\build.bat`, driven by the F# project `build/scripts/scripts.fsproj`)
still backs a few CI workflows. It is planned to be modernized or removed; do not use it for local development.

There is no separate lint command. Code style is defined in `.editorconfig` and checked with `dotnet format` in
reporting mode, e.g. `dotnet format src/Elastic.Clients.Elasticsearch/Elastic.Clients.Elasticsearch.csproj
--verify-no-changes`. The tree carries pre-existing violations, so only make sure your changes do not add new ones.

## Testing

Most of the test suite under `tests/` was never ported from the 7.x client to 8.x, so there is effectively no public
unit or integration test coverage for the client. Do not rely on these projects and do not extend them; verify client
changes by making sure the client project builds cleanly.

The request converter is the exception: `dotnet test RequestConverter.sln` runs `tests/RequestConverter.Tests` and
must pass.

## Project Structure

- **build/** - legacy build scripts (see Setup commands)
- **src/Elastic.Clients.Elasticsearch/** - the Elasticsearch client
- **src/RequestConverter/** - converts Elasticsearch API request examples into .NET client code, with
  **src/RequestConverter.Console/** as CLI frontend and **src/RequestConverter.Wasm/** as WASM/npm packaging
- **tests/** - test projects (see Testing)

### Generated code

`src/Elastic.Clients.Elasticsearch/_Generated/` and `src/RequestConverter/_Generated/` contain code produced by a
source generator that lives in a private repository. Never edit files in these directories manually; any manual
change will be overwritten on the next regeneration.

## Development Workflow

1. Make changes to files under `src/`, never inside a `_Generated/` directory
2. Build the affected project(s) with `dotnet build`
3. For request converter changes, run `dotnet test RequestConverter.sln`

## OS Compatibility

All code and scripts must work on Windows, macOS, and Linux. The `dotnet` CLI commands above are cross-platform.

## Adding new agent instructions

All markdown instructions authored for other agents must be as concise as possible.

If a specific action you learned to do better will be useful to other agents doing the same task in the future, but may not be needed for all agent-related tasks, create or update skills in `.claude/skills/`.

If you learned something that will be useful to any contributor to this project, update `AGENTS.md`.
