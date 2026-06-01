---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/plugin-defined-variants.html
---

# Plugin-defined variant types [plugin-defined-variants]

Elasticsearch plugins can introduce additional field types, token filters, char filters, analyzers, tokenizers, query types, and aggregation types beyond the closed set known to the typed client. The .NET client supports these plugin-defined variants through two complementary mechanisms: a zero-configuration **carrier** that round-trips unknown discriminators as raw JSON, and an opt-in **registration API** that maps a plugin discriminator to a caller-supplied CLR type for strongly-typed access.

- [Round-trip carrier](#carrier)
  - [Reading a plugin field type](#carrier-read)
  - [Writing a plugin token filter](#carrier-write)
- [Registering a strongly-typed CLR variant](#register)
- [Container variants](#containers)
- [Limitations](#limitations)

## Round-trip carrier [carrier]

For every non-container variant family that accepts plugin-defined values, the client emits a public `Unknown{Family}` class that implements the family's interface. When the deserializer encounters a discriminator value that is not part of the closed variant set and that has not been registered, it produces an instance of this carrier instead of throwing.

### Reading a plugin field type [carrier-read]

```csharp
using System.Text;
using System.Linq;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Transport.Extensions;

var json = """
{
  "type": "truncated_collation",
  "max_length": 64,
  "rules": "latin_to_base"
}
""";

var property = client.RequestResponseSerializer.Deserialize<IProperty>(json)!;

if (property is UnknownProperty unknown)
{
    Console.WriteLine(unknown.Type); <1>
    Console.WriteLine(unknown.Content.GetProperty("max_length")); <2>
    Console.WriteLine(unknown.Content.EnumerateObject().Count()); <3>
}
```

`Deserialize<IProperty>` produces an `UnknownProperty` carrier because `truncated_collation` is not in the closed `IProperty` variant set.

1. Outputs: `"truncated_collation"` (the JSON discriminator, stored on `Type`).
2. Outputs: `64`. Individual properties of the plugin object are accessible via `Content`.
3. Outputs: `3`. `Content` preserves all fields from the original JSON object, including `type`, `max_length`, and `rules`.

### Writing a plugin token filter [carrier-write]

```csharp
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Analysis;

using var document = JsonDocument.Parse("""
{
  "type": "sql_normalizer",
  "preserve_original": true
}
""");
var content = document.RootElement.Clone();

var filter = new UnknownTokenFilter("sql_normalizer", content);

var json = client.RequestResponseSerializer.SerializeToString<ITokenFilter>(filter);
// {"type":"sql_normalizer","preserve_original":true}
```

Construct the carrier with the discriminator the server-side plugin expects and a raw JSON object containing the plugin's configuration. Serializing via the union interface (`ITokenFilter`) causes the converter to write the discriminator first, then every other property from `Content`.

## Registering a strongly-typed CLR variant [register]

When the same plugin variant appears in several places, working with raw `JsonElement` content can become awkward. For these cases the client settings expose a per-instance registry, `ElasticsearchClientSettings.Variants`, whose `Register<TVariantFamily, TImplementation>(string discriminator)` method binds a caller-defined CLR type to a discriminator of the variant family `TVariantFamily`. After registration the deserializer produces instances of the CLR type directly. The serializer writes them through the CLR type's own `JsonConverter`. Because the registry lives on the settings instance, different `ElasticsearchClient` instances in the same process can register different CLR types (or none) independently.

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Mapping;

[JsonConverter(typeof(TruncatedCollationPropertyConverter))]
public sealed class TruncatedCollationProperty : IProperty
{
    public string Type => "truncated_collation";
    public int? MaxLength { get; set; }
    public string? Rules { get; set; }
}

public sealed class TruncatedCollationPropertyConverter
    : JsonConverter<TruncatedCollationProperty>
{
    public override TruncatedCollationProperty Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) { /* ... */ }

    public override void Write(
        Utf8JsonWriter writer, TruncatedCollationProperty value, JsonSerializerOptions options) { /* ... */ }
}

// Register on the client settings at application startup, then build the client from those settings:
var settings = new ElasticsearchClientSettings(/* ... */);
settings.Variants.Register<IProperty, TruncatedCollationProperty>("truncated_collation");
var client = new ElasticsearchClient(settings);

// Subsequent (de)serialization of IProperty values whose discriminator is "truncated_collation"
// resolves to the registered CLR type:
var property = client.RequestResponseSerializer.Deserialize<IProperty>(json)!;
var typed = (TruncatedCollationProperty)property;
```

::::{warning}

A `[JsonConverter]` attribute on the registered type is **required**. Without it, the client falls back to a reflection-based converter driven by its own `JsonSerializerOptions`, which produces incorrect JSON and fails at runtime in AOT-published applications.

Source-generated `JsonSerializerContext` implementations cannot be used. A context registers its type metadata on a specific `JsonSerializerOptions` instance, and there is currently no way to compose a user-supplied context with the client's internal serializer options. Only a hand-written `JsonConverter<TImplementation>` attached via `[JsonConverter]` on the type is supported.

::::


`Variants.Register<TVariantFamily, TImplementation>(string)` is scoped to the settings instance. Calling it again with the same discriminator replaces the previous registration on that instance. On write, the value's `Type` must match the registered discriminator. A registered discriminator overrides the carrier fallback. Discriminators that are not registered continue to land in `UnknownProperty`.

## Container variants [containers]

Container types (for example `Query` or `Aggregation`) don't use a separate `Unknown{Family}` carrier. The container itself holds the variant. Known variants are reached through their typed accessors (`Query.Match`, `Query.Bool`, and so on). For plugin-defined variants the container exposes three additional members:

- `string? VariantName { get; }`: the JSON property name of the variant the container currently holds.
- `T? GetCustomVariant<T>(string variantName)`: returns the stored variant when the container's current name matches, otherwise `null`.
- `void SetCustomVariant<T>(string variantName, T? value)`: sets the container's variant to the given `(name, value)` pair.
- Fluent descriptors expose `CustomVariant<T>(string variantName, T? value)` for writing plugin-defined variants in descriptor-based APIs.

```csharp
using Elastic.Clients.Elasticsearch.QueryDsl;

public sealed class MyCustomQuery
{
    public string Field { get; set; } = "";
    public string Pattern { get; set; } = "";
}

var settings = new ElasticsearchClientSettings(/* ... */);
settings.Variants.RegisterContainer<Query, MyCustomQuery>("my_custom_query");
var client = new ElasticsearchClient(settings);

// Read side
var json = """{ "my_custom_query": { "field": "title", "pattern": "^foo" } }""";
var q = client.RequestResponseSerializer.Deserialize<Query>(json)!;

var typed = q.GetCustomVariant<MyCustomQuery>("my_custom_query");
Console.WriteLine($"name = {q.VariantName}, field = {typed?.Field}");

// Write side
var outgoing = new Query();
outgoing.SetCustomVariant("my_custom_query", new MyCustomQuery { Field = "title", Pattern = "^foo" });

// Descriptor write side
Query descriptorQuery = new QueryDescriptor<object>()
    .CustomVariant("my_custom_query", new MyCustomQuery { Field = "title", Pattern = "^foo" });
```

Register the CLR type for the variant name on the client settings at application startup. Deserialization then produces instances of `MyCustomQuery` rather than a raw `JsonElement`. `GetCustomVariant<T>(variantName)` returns `null` when the container's variant doesn't match the requested name, for example when the container holds a known typed variant instead. `SetCustomVariant<T>(variantName, value)` and the descriptor `CustomVariant<T>(variantName, value)` are intended for plugin-defined variants. Known variants are still set through their typed properties (`outgoing.Match = ...`).

## Limitations [limitations]

The registration mechanism captures the registered CLR type as a generic type argument inside a static delegate. There is no runtime use of `JsonSerializer.Deserialize(Type)` or `JsonSerializer.Serialize(object, Type)`. For this to remain trim-safe in AOT-published applications, the registered type must be discoverable by the client's serializer options. In practice, this means a `[JsonConverter]` attribute on the type is required. A source-generated `JsonSerializerContext` cannot be used, because there is currently no way to compose a user-supplied context with the client's internal serializer options.
