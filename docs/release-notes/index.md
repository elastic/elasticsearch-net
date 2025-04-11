---
navigation_title: "Elasticsearch .NET Client"
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/release-notes.html
---

# Elasticsearch .NET Client release notes [elasticsearch-net-client-release-notes]

Review the changes, fixes, and more in each version of Elasticsearch .NET Client.

To check for security updates, go to [Security announcements for the Elastic stack](https://discuss.elastic.co/c/announcements/security-announcements/31).

% Release notes include only features, enhancements, and fixes. Add breaking changes, deprecations, and known issues to the applicable release notes sections.

% ## version.next [felasticsearch-net-client-next-release-notes]

% ### Features and enhancements [elasticsearch-net-client-next-features-enhancements]
% *

% ### Fixes [elasticsearch-net-client-next-fixes]
% *

## 9.0.0 [elasticsearch-net-client-900-release-notes]

### 1. Request Method/API Changes

#### 1.1. Synchronous Request APIs

Synchronous request APIs are no longer marked as `obsolete`. We received some feedback about this deprecation and decided to revert it.

#### 1.2. Separate Type Arguments for Request/Response

It is now possible to specify separate type arguments for requests/responses when executing request methods:

```csharp
var response = await client.SearchAsync<Person, JsonObject>(x => x
    .Query(x => x.Term(x => x.Field(x => x.FirstName).Value("Florian")))
);

var documents = response.Documents; <1>
```

1. `IReadOnlyCollection<JsonObject>`

The regular APIs with merged type arguments are still available.

### 2. Improved Fluent API

The enhanced fluent API generation is likely the most notable change in the 9.0 client.

This section describes the main syntax constructs generated based on the type of the property in the corresponding object.

#### 2.1. `ICollection<E>`

Note: This syntax already existed in 8.x.

```csharp
new SearchRequestDescriptor<Person>()
    .Query(q => q
        .Bool(b => b
            .Must(new Query())                           // Scalar: Single element.
            .Must(new Query(), new Query())              // Scalar: Multiple elements (params).
            .Must(m => m.MatchAll())                     // Fluent: Single element.
            .Must(m => m.MatchAll(), m => m.MatchNone()) // Fluent: Multiple elements (params).
        )
    );
```

#### 2.2. `IDictionary<K, V>`

The 9.0 client introduces full fluent API support for dictionary types.

```csharp
new SearchRequestDescriptor<Person>()
    .Aggregations(new Dictionary<string, Aggregation>()) // Scalar.
    .Aggregations(aggs => aggs                           // Fluent: Nested.
        .Add("key", new MaxAggregation())                // Scalar: Key + Value.
        .Add("key", x => x.Max())                        // Fluent: Key + Value.
    )
    .AddAggregation("key", new MaxAggregation())         // Scalar.
    .AddAggregation("key", x => x.Max());                // Fluent.
```

:::{warning}

The `Add{Element}` methods have different semantics compared to the standard setter methods.

Standard fluent setters set or **replace** a value.

In contrast, the new additive methods append new elements to the dictionary.

:::

For dictionaries where the value type does not contain required properties that must be initialized, another syntax is generated that allows easy addition of new entries by just specifying the key:

```csharp
// Dictionary<Name, Alias>()

new CreateIndexRequestDescriptor("index")
    // ... all previous overloads ...
    .Aliases(aliases => aliases // Fluent: Nested.
        .Add("key")             // Key only.
    )
    .Aliases("key")             // Key only: Single element.
    .Aliases("first", "second") // Key only: Multiple elements (params).
```

If the value type in the dictionary is a collection, additional `params` overloads are generated:

```csharp
// Dictionary<Field, ICollection<CompletionContext>>

new CompletionSuggesterDescriptor<Person>()
    // ... all previous overloads ...
    .AddContext("key", 
        new CompletionContext{ Context = new Context("first") },
        new CompletionContext{ Context = new Context("second") }
    )
    .AddContext("key",
        x => x.Context(x => x.Category("first")),
        x => x.Context(x => x.Category("second"))
    );
```

#### 2.3. `ICollection<KeyValuePair<K, V>>`

Elasticsearch often uses `ICollection<KeyValuePair<K, V>>` types for ordered dictionaries.

The 9.0 client abstracts this implementation detail by providing a fluent API that can be used exactly like the one for `IDictionary<K, V>` types:

```csharp
new PutMappingRequestDescriptor<Person>("index")
    .DynamicTemplates(new List<KeyValuePair<string, DynamicTemplate>>()) // Scalar.
    .DynamicTemplates(x => x                                             // Fluent: Nested.
        .Add("key", new DynamicTemplate())                               // Scalar: Key + Value.
        .Add("key", x => x.Mapping(new TextProperty()))                  // Fluent: Key + Value.
    )
    .AddDynamicTemplate("key", new DynamicTemplate())                    // Scalar: Key + Value.
    .AddDynamicTemplate("key", x => x.Runtime(x => x.Format("123")));    // Fluent: Key + Value.
```

#### 2.4. Union Types

Fluent syntax is now as well available for all auto-generated union- and variant-types.

```csharp
// TermsQueryField : Union<ICollection<FieldValue>, TermsLookup>

new TermsQueryDescriptor()
    .Terms(x => x.Value("a", "b", "c"))                    <1>
    .Terms(x => x.Lookup(x => x.Index("index").Id("id"))); <2>
```

1. `ICollection<FieldValue>`
2. `TermsLookup`

### 3. Improved Descriptor Design

The 9.0 release features a completely overhauled descriptor design.

Descriptors now wrap the object representation. This brings several internal quality-of-life improvements as well as noticeable benefits to end-users.

#### 3.1. Wrap

Use the wrap constructor to create a new descriptor for an existing object:

```csharp
var request = new SearchRequest();

// Wrap.
var descriptor = new SearchRequestDescriptor(request);
```

All fluent methods of the descriptor will mutate the existing `request` passed to the wrap constructor.

:::{note}

Descriptors are now implemented as `struct` instead of `class`, reducing allocation overhead as much as possible.

:::

#### 3.2. Unwrap / Inspect

Descriptor values can now be inspected by unwrapping the object using an implicit conversion operator:

```csharp
var descriptor = new SearchRequestDescriptor();

// Unwrap.
SearchRequest request = descriptor;
```

Unwrapping does not allocate or copy.

#### 3.3. Removal of Side Effects

In 8.x, execution of (most but not all) lambda actions passed to descriptors was deferred until the actual request was made. It was never clear to the user when, and how often an action would be executed.

In 9.0, descriptor actions are always executed immediately. This ensures no unforeseen side effects occur if the user-provided lambda action mutates external state (it is still recommended to exclusively use pure/invariant actions). Consequently, the effects of all changes performed by a descriptor method are immediately applied to the wrapped object.

### 4. Request Path Parameter Properties

In 8.x, request path parameters like `Index`, `Id`, etc. could only be set by calling the corresponding constructor of the request. Afterwards, there was no way to read or change the current value.

In the 9.0 client, all request path parameters are exposed as `get/set` properties, allowing for easy access:

```csharp
// 8.x and 9.0
var request = new SearchRequest(Indices.All);

// 9.0
var request = new SearchRequest { Indices = Indices.All };
var indices = request.Indices;
request.Indices = "my_index";
```

### 5. Field Name Inference

The `Field` type and especially its implicit conversion operations allowed for `null` return values. This led to a poor developer experience, as the null-forgiveness operator (`!`) had to be used frequently without good reason.

This is no longer required in 9.0:

```csharp
// 8.x
Field field = "field"!;

// 9.0
Field field = "field";
```

### 6. Uniform Date/Time/Duration Types

The encoding of date, time and duration values in Elasticsearch often varies depending on the context. In addition to string representations in ISO 8601 and RFC 3339 format (always UTC), also Unix timestamps (in seconds, milliseconds, nanoseconds) or simply seconds, milliseconds, nanoseconds are frequently used.

In 8.x, some date/time values are already mapped as `DateTimeOffset`, but the various non-ISO/RFC representations were not.

9.0 now represents all date/time values uniformly as `DateTimeOffset` and also uses the native `TimeSpan` type for all durations.

:::{note}

There are some places where the Elasticsearch custom date/time/duration types are continued to be used. This is always the case when the type has special semantics and/or offers functionality that goes beyond that of the native date/time/duration types (e.g. `Duration`, `DateMath`).

:::

### 7. Improved Container Design

In 8.x, container types like `Query` or `Aggregation` had to be initialized using static factory methods.

```csharp
// 8.x
var agg = Aggregation.Max(new MaxAggregation { Field = "my_field" });
```

This made it mandatory to assign the created container to a temporary variable if additional properties of the container (not the contained variant) needed to be set:

```csharp
// 8.x
var agg = Aggregation.Max(new MaxAggregation { Field = "my_field" });
agg.Aggregations ??= new Dictionary<string, Aggregation>();
agg.Aggregations.Add("my_sub_agg", Aggregation.Terms(new TermsAggregation()));
```

Additionally, it was not possible to inspect the contained variant.

In 9.0, each possible container variant is represented as a regular property of the container. This allows for determining and inspecting the contained variant and initializing container properties in one go when using an object initializer:

```csharp
// 9.0
var agg = new Aggregation
{
    Max = new MaxAggregation { Field = "my_field" },
    Aggregations = new Dictionary<string, Aggregation>
    {
        { "my_sub_agg", new Aggregation{ Terms = new TermsAggregation() } }
    }
};
```

:::{warning}

A container can still only contain a single variant. Setting multiple variants at once is invalid.

Consecutive assignments of variant properties (e.g., first setting `Max`, then `Min`) will cause the previous variant to be replaced.

:::

### 8. Sorting

Applying a sort order to a search request using the fluent API is now more convenient:

```csharp
var search = new SearchRequestDescriptor<Person>()
    .Sort(
        x => x.Score(),
        x => x.Score(x => x.Order(SortOrder.Desc)),
        x => x.Field(x => x.FirstName),
        x => x.Field(x => x.Age, x => x.Order(SortOrder.Desc)),
        x => x.Field(x => x.Age, SortOrder.Desc)
        // 7.x syntax
        x => x.Field(x => x.Field(x => x.FirstName).Order(SortOrder.Desc))
    );
```

The improvements are even more evident when specifying a sort order for aggregations:

```csharp
new SearchRequestDescriptor<Person>()
    .Aggregations(aggs => aggs
        .Add("my_terms", agg => agg
            .Terms(terms => terms
                // 8.x syntax.
                .Order(new List<KeyValuePair<Field, SortOrder>>
                {
                    new KeyValuePair<Field, SortOrder>("_key", SortOrder.Desc)
                })
                // 9.0 fluent syntax.
                .Order(x => x
                    .Add(x => x.Age, SortOrder.Asc)
                    .Add("_key", SortOrder.Desc)
                )
                // 9.0 fluent add syntax (valid for all dictionary-like values).
                .AddOrder("_key", SortOrder.Desc)
            )
        )
    );
```

### 9. Safer Object Creation

In version 9.0, users are better guided to correctly initialize objects and thus prevent invalid requests.

For this purpose, at least one constructor is now created that enforces the initialization of all required properties. Existing parameterless constructors or constructor variants that allow the creation of incomplete objects are preserved for backwards compatibility reasons, but are marked as obsolete.

For NET7+ TFMs, required properties are marked with the `required` keyword, and a non-deprecated parameterless constructor is unconditionally generated.

:::{note}

Please note that the use of descriptors still provides the chance to create incomplete objects/requests, as descriptors do not enforce the initialization of all required properties for usability reasons.

:::

### 10. Serialization

Serialization in version 9.0 has been completely overhauled, with a primary focus on robustness and performance. Additionally, initial milestones have been set for future support of native AOT.

In 9.0, round-trip serialization is now supported for all types (limited to all JSON serializable types).

```csharp
var request = new SearchRequest{ /* ... */ };

var json = client.ElasticsearchClientSettings.RequestResponseSerializer.SerializeToString(
    request, 
    SerializationFormatting.Indented
);

var searchRequestBody = client.ElasticsearchClientSettings.RequestResponseSerializer.Deserialize<SearchRequest>(json)!;
```

:::{warning}

Note that only the body is serialized for request types. Path- and query properties must be handled manually.

:::

:::{note}

It is important to use the `RequestResponseSerializer` when (de-)serializing client internal types. Direct use of `JsonSerializer` will not work.

:::
