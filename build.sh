#!/usr/bin/env bash
set -euo pipefail
dotnet run --project targets -- "$@"
