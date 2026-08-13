# AGENTS.md

## Setup commands

The build system is [Bullseye](https://github.com/adamralph/bullseye), invoked through shell scripts in the repo root.

- Build and run unit tests: `./build.sh` on macOS/Linux, `.\build.bat` on Windows
- Build without tests: `./build.sh skiptests`
- Integration tests (requires a running Elasticsearch): `./build.sh integrate [version]` (e.g. `./build.sh integrate 8.3.2`)

There is no separate lint command - code formatting is enforced at build time.

## Testing

**The full build (`./build.sh`) must pass and exit cleanly before you commit code.**

Integration tests require a reachable Elasticsearch instance. Unit tests have no external dependencies and run as part of the default build target.

## Project Structure

- **src/** - Client source code
- **tests/Tests/** - Unit and integration tests
- **build/** - Build scripts and targets

## Development Workflow

1. Make changes to files under `src/`
2. Run `./build.sh` to compile and run unit tests
3. If your change touches integration behavior, run `./build.sh integrate [version]` against a local Elasticsearch instance

## OS Compatibility

All code and build scripts must work on Windows, macOS, and Linux. Use `./build.sh` on macOS/Linux and `.\build.bat` on Windows.

## Adding new agent instructions

All markdown instructions authored for other agents must be as concise as possible.

If a specific action you learned to do better will be useful to other agents doing the same task in the future, but may not be needed for all agent-related tasks, create or update skills in `.claude/skills/`.

If you learned something that will be useful to any contributor to this project, update `AGENTS.md`.
