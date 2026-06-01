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
- [Per-family registration surface](#per-family)
- [Container variants](#containers)
- [Native AOT](#aot)
- [Limitations](#limitations)

## Round-trip carrier [carrier]

For every non-container variant family that accepts plugin-defined values, the client emits a public `Unknown{Family}` class that implements the family's interface. When the deserializer encounters a discriminator value that is not part of the closed variant set and that has not been registered, it produces an instance of this carrier instead of throwing.

For internally tagged families such as `IProperty`, `IAnalyzer`, or `ITokenFilter`, the carrier captures the discriminator on a `Type` property and the full JSON object on a `Content` property. The write side emits the discriminator from `Type`, then replays the remaining properties from `Content`, so a value read into the carrier can be written back out without losing fields.

For externally tagged typed-key families such as `IAggregate`, the discriminator comes from the response property name (`type#name`). The carrier still exposes `Type`, but `Content` contains only the JSON value body under that typed key.

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

var property = client.RequestResponseSerializer.Deserialize<IProperty>(json)!; <1>

if (property is UnknownProperty unknown) <2>
{
    Console.WriteLine(unknown.Type);                                 // "truncated_collation"
    Console.WriteLine(unknown.Content.GetProperty("max_length"));     // 64
    Console.WriteLine(unknown.Content.EnumerateObject().Count());     // 3  (type + max_length + rules)
}
```

1. The discriminator (`type: truncated_collation`) is not part of the closed `IProperty` variant set, so the value lands in the carrier.
2. The carrier exposes `Type` (the JSON discriminator) and `Content` (the full JSON object as a `JsonElement`).

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

var filter = new UnknownTokenFilter("sql_normalizer", content); <1>

var json = client.RequestResponseSerializer.SerializeToString<ITokenFilter>(filter); <2>
// {"type":"sql_normalizer","preserve_original":true}
```

1. Construct the carrier with the discriminator the server-side plugin expects and a raw JSON object containing the plugin's configuration.
2. Serialize via the union interface (`ITokenFilter`); the converter writes the discriminator first, then every other property from `Content`.

## Registering a strongly-typed CLR variant [register]

When the same plugin variant appears in several places, working with raw `JsonElement` content can become awkward. For these cases the client settings expose a per-instance registry, `ElasticsearchClientSettings.Variants`, whose `Register<TUnion, TImpl>(string discriminator)` method binds a caller-defined CLR type to a discriminator of the variant family `TUnion`. After registration the deserializer produces instances of the CLR type directly; the serializer writes them through the CLR type's own `JsonConverter`. Because the registry lives on the settings instance, different `ElasticsearchClient` instances in the same process can register different CLR types (or none) independently.

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Mapping;

[JsonConverter(typeof(TruncatedCollationPropertyConverter))]      <1>
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
settings.Variants.Register<IProperty, TruncatedCollationProperty>("truncated_collation");        <2>
var client = new ElasticsearchClient(settings);

// Subsequent (de)serialization of IProperty values whose discriminator is "truncated_collation"
// resolves to the registered CLR type:
var property = client.RequestResponseSerializer.Deserialize<IProperty>(json)!;
var typed = (TruncatedCollationProperty)property;                                                <3>
```

1. The user-defined type carries its own `JsonConverter` attribute. The client never reflects on the type — registration captures a typed delegate, so the converter must be resolvable through the `JsonSerializerOptions` used by the client.
2. `Variants.Register<TUnion, TImpl>(string)` is scoped to the settings instance. Calling it again with the same discriminator replaces the previous registration on that instance. On write, the value's `Type` must match the registered discriminator.
3. A registered discriminator overrides the carrier fallback. Discriminators that are not registered continue to land in `UnknownProperty`.

## Per-family registration surface [per-family]

All registrations are made on `settings.Variants` (an instance of `VariantRegistry`).

| Family | Registration | Carrier |
|---|---|---|
| `IProperty` | `Variants.Register<Mapping.IProperty, T>(string)` | `Mapping.UnknownProperty` |
| `IAnalyzer` | `Variants.Register<Analysis.IAnalyzer, T>(string)` | `Analysis.UnknownAnalyzer` |
| `ITokenFilter` | `Variants.Register<Analysis.ITokenFilter, T>(string)` | `Analysis.UnknownTokenFilter` |
| `ICharFilter` | `Variants.Register<Analysis.ICharFilter, T>(string)` | `Analysis.UnknownCharFilter` |
| `ITokenizer` | `Variants.Register<Analysis.ITokenizer, T>(string)` | `Analysis.UnknownTokenizer` |
| `IAggregate` | `Variants.Register<Aggregations.IAggregate, T>(string)` | `Aggregations.UnknownAggregate` |
| `IRepository` | `Variants.Register<Snapshot.IRepository, T>(string)` | `Snapshot.UnknownRepository` |
| `ISettingsSimilarity` | `Variants.Register<IndexManagement.ISettingsSimilarity, T>(string)` | `IndexManagement.UnknownSettingsSimilarity` |
| `Query` (container) | `Variants.RegisterContainer<QueryDsl.Query, T>(string)` | accessed via container methods (see below) |
| `SpanQuery` (container) | `Variants.RegisterContainer<QueryDsl.SpanQuery, T>(string)` | accessed via container methods |
| `Aggregation` (container) | `Variants.RegisterContainer<Aggregations.Aggregation, T>(string)` | accessed via container methods (see below) |
| `Processor` (container) | `Variants.RegisterContainer<Ingest.Processor, T>(string)` | accessed via container methods |
| `Rescore` (container) | `Variants.RegisterContainer<Core.Search.Rescore, T>(string)` | accessed via container methods |
| `FieldSuggester` (container) | `Variants.RegisterContainer<Core.Search.FieldSuggester, T>(string)` | accessed via container methods |
| `ApiKeyQuery` (container) | `Variants.RegisterContainer<Security.ApiKeyQuery, T>(string)` | accessed via container methods |
| `ApiKeyAggregation` (container) | `Variants.RegisterContainer<Security.ApiKeyAggregation, T>(string)` | accessed via container methods |
| `RoleQuery` (container) | `Variants.RegisterContainer<Security.RoleQuery, T>(string)` | accessed via container methods |
| `UserQuery` (container) | `Variants.RegisterContainer<Security.UserQuery, T>(string)` | accessed via container methods |

Namespaces are abbreviated; each family lives under `Elastic.Clients.Elasticsearch`.

## Container variants [containers]

Container types (`Query`, `Aggregation`) don't use a separate `Unknown{Family}` carrier — the container itself holds the variant. Known variants are reached through their typed accessors (`Query.Match`, `Query.Bool`, ...). For plugin-defined variants the container exposes three additional members:

- `string? VariantName { get; }` — the JSON property name of the variant the container currently holds.
- `T? GetCustomVariant<T>(string variantName)` — returns the stored variant when the container's current name matches, otherwise `null`.
- `void SetCustomVariant<T>(string variantName, T? value)` — sets the container's variant to the given `(name, value)` pair.
- Fluent descriptors expose `CustomVariant<T>(string variantName, T? value)` for writing plugin-defined variants in descriptor-based APIs.

```csharp
using Elastic.Clients.Elasticsearch.QueryDsl;

public sealed class MyCustomQuery
{
    public string Field { get; set; } = "";
    public string Pattern { get; set; } = "";
}

var settings = new ElasticsearchClientSettings(/* ... */);
settings.Variants.RegisterContainer<Query, MyCustomQuery>("my_custom_query"); <1>
var client = new ElasticsearchClient(settings);

// Read side
var json = """{ "my_custom_query": { "field": "title", "pattern": "^foo" } }""";
var q = client.RequestResponseSerializer.Deserialize<Query>(json)!;

var typed = q.GetCustomVariant<MyCustomQuery>("my_custom_query"); <2>
Console.WriteLine($"name = {q.VariantName}, field = {typed?.Field}");

// Write side
var outgoing = new Query();
outgoing.SetCustomVariant("my_custom_query", new MyCustomQuery { Field = "title", Pattern = "^foo" }); <3>

// Descriptor write side
Query descriptorQuery = new QueryDescriptor<object>()
    .CustomVariant("my_custom_query", new MyCustomQuery { Field = "title", Pattern = "^foo" });
```

1. Register the CLR type for the variant name on the client settings at application startup. Deserialization produces instances of `MyCustomQuery` rather than a raw `JsonElement`.
2. `GetCustomVariant<T>(variantName)` returns `null` when the container's variant doesn't match the requested name (for example, when the container holds a known typed variant instead).
3. `SetCustomVariant<T>(variantName, value)` and descriptor `CustomVariant<T>(variantName, value)` are intended for plugin-defined variants. The known variants are still set through their typed properties (`outgoing.Match = ...`).

## Native AOT [aot]

The registration mechanism avoids runtime type-keyed serialization. `Variants.Register<TUnion, TImpl>(string)` (and `Variants.RegisterContainer<TContainer, TImpl>(string)`) captures `TImpl` as a generic context inside a static lambda; the delegate body dispatches through `ReadValue<TImpl>` / `WriteValue<TImpl>`, which resolve their converter through the `JsonSerializerOptions` already configured on the client. There is no runtime use of `JsonSerializer.Deserialize(Type)` or `JsonSerializer.Serialize(object, Type)`.

For the registration to remain trim-safe in published AOT applications, the registered CLR type must be discoverable by the client's serializer options. For request/response variant registration, prefer a `[JsonConverter]` attribute on the type itself, as in the example above. A source-generated `JsonSerializerContext` only helps when it is actually added to the serializer options used for the registered variant type.

## Limitations [limitations]

::::{note}

Registrations are scoped to the `ElasticsearchClientSettings` instance they are made on. A client only sees the variants registered on the settings it was constructed from, so different `ElasticsearchClient` instances in the same process can use different plugin-variant mappings. Register on `settings.Variants` before performing (de)serialization with the client built from those settings.

::::
