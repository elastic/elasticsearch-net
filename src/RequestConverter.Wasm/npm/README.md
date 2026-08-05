# @elastic/request-converter-dotnet

The .NET request converter for Elasticsearch, compiled to WebAssembly. It turns a parsed Dev
Console request into `Elastic.Clients.Elasticsearch` (.NET) client code, with the target
`Elastic.Clients.Elasticsearch` version embedded in the WASM bundle itself.

This package is consumed by `@elastic/request-converter`'s C# exporter, which loads it as an
`ExternalExporter` plugin. It is also usable standalone from Node by calling its `boot()` factory
directly, as shown below.

## Usage

```js
import { boot } from "@elastic/request-converter-dotnet";

const { check, convert } = await boot();

const payload = {
  requests: [
    {
      api: "search",
      body: { query: { term: { "user.id": { value: "kimchy" } } } },
      method: "GET",
      params: { index: "my-index-000001" },
      path: "/my-index-000001/_search",
      query: { from: "40", size: "20" },
      raw_path: "/my-index-000001/_search",
      source: "GET /my-index-000001/_search?from=40&size=20",
      url: "/my-index-000001/_search?from=40&size=20",
    },
  ],
  options: {
    document_type_name: "MyDocument",
    syntax_mode: "descriptor",
    use_strongly_typed_document: true,
  },
};

const checked = JSON.parse(check(JSON.stringify(payload)));
// checked.return === true

const converted = JSON.parse(convert(JSON.stringify(payload)));
console.log(converted.return);
// var request = new SearchRequestDescriptor<MyDocument>() ...
```

## Contract

Both `check` and `convert` are synchronous, take a JSON string, and return a JSON string. Field
names are snake_case throughout.

- `check(input)` - input `{"requests": ParsedRequest[]}`; output `{"return": <bool>, "error"?: "..."}`.
  It is a yes/no probe: a request the converter cannot handle (unsupported endpoint, invalid body,
  ...) makes it return `false` rather than an error.
- `convert(input)` - input `{"requests": ParsedRequest[], "options": ConvertOptions}`; output
  `{"return": <string>, "error"?: "..."}`. On failure, `error` is a concise, user-facing message (no
  stack trace) naming the failing request and the reason.

`ParsedRequest` is produced by the host's parser (turning Dev Console text into an API name and URL
parameters needs the Elasticsearch schema, which this package does not carry), so this package only
converts requests already parsed into that shape.

## Options

`options` on `convert` accepts:

- `document_type_name` (string, default `MyDocument`) - the placeholder document type name used when
  `use_strongly_typed_document` is set.
- `syntax_mode` (`object_initializer` | `descriptor`, default `object_initializer`) - the emitted C#
  syntax. `object_initializer` emits object initializers (`new SearchRequest { Query = ... }`);
  `descriptor` emits the fluent descriptor chain (`new SearchRequestDescriptor().Query(q => ...)`).
- `type_name_style` (`Simplified` | `Fqn` | `GlobalFqn`, default `Simplified`) - how type names are
  spelled in the generated code.
- `use_strongly_typed_document` (bool, default `false`) - switches field references to
  `Infer.Field<DocumentTypeName>(x => x.Path)` lambdas and a generic request's document body to
  `new DocumentTypeName { ... }`. Pairs with `syntax_mode: "descriptor"` to produce the generic
  `XDescriptor<DocumentTypeName>` flavor.
- `debug` (bool, default `false`) - appends the full exception (type and stack trace) to a failed
  `convert`'s `error`, after a `--- debug ---` marker.

The `document_type_name` given (`MyDocument` by default) is not a type the converter generates; the
caller supplies it, so any typed-document example in the generated output is illustrative and
generally does not compile or round-trip as-is.

## Versioning

This package's major and minor version track the elasticsearch-net branch it was built from (for
example a build from the `9.1` branch ships as `9.1.x`); the patch number increments freely on each
publish and carries no meaning beyond ordering. The exact embedded client is recorded in
`package.json` under the `elasticsearch` key: `clientVersion` is the `Elastic.Clients.Elasticsearch`
version compiled into the bundle, and `commit` is the elasticsearch-net commit the bundle was built
from.

Builds from the `main` branch are versioned off the commit they were built from, as
`0.0.0-main.g<commit>` (for example `0.0.0-main.g1a2b3c4d5e6f`). That version names the built commit
and claims no target release, since `main` may be aiming at the next minor or at the next major.
Such builds are published under the `latest-main` dist-tag and never take `latest`; reference that
tag rather than a version range. Their provenance is in the `elasticsearch` metadata as usual:
`commit` for the full commit, and `clientVersion` set to `unreleased`.

## Building from source

This package is assembled from a published .NET WASM AppBundle; see
[`src/RequestConverter.Wasm`](https://github.com/elastic/elasticsearch-net/tree/main/src/RequestConverter.Wasm)
in the elasticsearch-net repository for build prerequisites and instructions.
