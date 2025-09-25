---
navigation_title: "Breaking changes"
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/breaking-changes-policy.html
---

# Elasticsearch .NET Client breaking changes [elasticsearch-net-client-breaking-changes]

Breaking changes can impact your Elastic applications, potentially disrupting normal operations. Before you upgrade, carefully review the Elasticsearch .NET Client breaking changes and take the necessary steps to mitigate any issues. To learn how to upgrade, check [Upgrade](docs-content://deploy-manage/upgrade.md).

% ## Next version [elasticsearch-net-client-nextversion-breaking-changes]

% ::::{dropdown} Title of breaking change
%
% **Impact**: High/Low.
%
% Description of the breaking change.
% For more information, check [PR #](PR link).
%
% ::::

## 9.1.8 [elasticsearch-net-client-918-breaking-changes]

### Overview

- [1. Improved usability of `CompositeAggregation`](#1-composite-aggregation)

### Breaking changes

#### 1. Improved usability of `CompositeAggregation` [#1-composite-aggregation]

**Impact**: Low.

The type of the `Sources` property has been changed from `ICollection<IDictionary<string, CompositeAggregationSource>>` to `ICollection<KeyValuePair<string, CompositeAggregationSource>>`. This corresponds to the Elasticsearch standard for modeling ordered dictionaries in the REST API.

`CompositeAggregationSource` is now also a container (similar to `Aggregation`, `Query`, etc.). This change improves usability due to specialized code generation. For example, implicit conversion operators from all existing variants (`CompositeTermsAggregation`, `CompositeHistogramAggregation`, etc.) to `CompositeAggregationSource` are now generated.

As a consequence, the object initializer syntax changes as follows:

```csharp
// 9.1.7 and below

var request = new SearchRequest
{
    Aggregations = new Dictionary<string, Aggregation>
    {
        { "my_composite", new CompositeAggregation
        {
            Sources =
            [
                new Dictionary<string, CompositeAggregationSource>
                {
                    { "my_terms", new CompositeAggregationSource
                    {
                        Terms = new CompositeTermsAggregation
                        {
                            // ...
                        }
                    }}
                },
                new Dictionary<string, CompositeAggregationSource>
                {
                    { "my_histo", new CompositeAggregationSource
                    {
                        Histogram = new CompositeHistogramAggregation(0.5)
                        {
                            // ...
                        }
                    }}
                }
            ]
        }}
    }
};

// 9.1.8 and above

var request = new SearchRequest
{
    Aggregations = new Dictionary<string, Aggregation>
    {
        { "my_composite", new CompositeAggregation
        {
            Sources =
            [
                new KeyValuePair<string, CompositeAggregationSource>(
                    "my_terms",
                    new CompositeTermsAggregation <1>
                    {
                        // ...
                    }
                ),
                new KeyValuePair<string, CompositeAggregationSource>(
                    "my_histo",
                    new CompositeHistogramAggregation(0.5) <2>
                    {
                        // ...
                    }
                )
            ]
        }}
    }
};
```

1. Implicit conversion from `CompositeTermsAggregation` to `CompositeAggregationSource`.
2. Implicit conversion from `CompositeHistogramAggregation` to `CompositeAggregationSource`.

In addition, this change allows optimized Fluent syntax to be generated, which ultimately avoids a previously existing ambiguity:

```csharp
// 9.1.7 and below

var descriptor = new SearchRequestDescriptor()
    .Aggregations(aggs => aggs
        .Add("my_composize", agg => agg
            .Composite(composite => composite
                .Sources( <1>
                    x => x.Add("my_terms", x => x.Terms(/* ... */)), <2>
                    x => x.Add("my_histo", x => x.Histogram(/* ... */)) <3>
                )
            )
        )
    );

// 9.1.8 and above

var descriptor = new SearchRequestDescriptor()
    .Aggregations(aggs => aggs
        .Add("my_composize", agg => agg
            .Composite(composite => composite
                .Sources(sources => sources
                    .Add("my_terms", x => x.Terms(/* ... */))
                    .Add("my_histo", x => x.Histogram(/* ... */))
                )
            )
        )
    );
```

1. This is the `params` overload that initializes the `Sources` collection with multiple entries.
2. Add dictionary item 1 that contains a single `Terms` aggregation entry.
3. Add dictionary item 2 that contains a single `Histogram` aggregation entry.

The old syntax was tricky because the 9.1.8 example also compiled successfully, but the `.Add` referred to the first dictionary both times. This ultimately resulted in a list with only one dictionary, which had multiple entries, and thus an invalid request.

## 9.1.1 [elasticsearch-net-client-911-breaking-changes]

### Overview

- [1. Improved usability of `Percentiles` aggregation results](#1-percentiles-aggregate)

### Breaking changes

#### 1. Improved usability of `Percentiles` aggregation results [#1-percentiles-aggregate]

**Impact**: Low.

The type of the `Values` property for the following classes got changed from the `Percentiles` union to a simple `PercentilesItem` collection:

- `HdrPercentilesAggregate`
- `HdrPercentileRanksAggregate`
- `TDigestPercentilesAggregate`
- `TDigestPercentileRanksAggregate`
- `PercentilesBucketAggregate`

The `Percentiles` union type got removed as it's no longer used.

## 9.0.0 [elasticsearch-net-client-900-breaking-changes]

### Overview

- [1. Container types](#1-container-types)
- [2. Removal of certain generic request descriptors](#2-removal-of-certain-generic-request-descriptors)
- [3. Removal of certain descriptor constructors and related request APIs](#3-removal-of-certain-descriptor-constructors-and-related-request-apis)
- [4. Date / Time / Duration values](#4-date-time-duration-values)
- [5. `ExtendedBounds`](#5-extendedbounds)
- [6. `Field.Format`](#6-fieldformat)
- [7. `Field`/`Fields` semantics](#7-fieldfields-semantics)
- [8. `FieldValue`](#8-fieldvalue)
- [9. `FieldSort`](#9-fieldsort)
- [10. Descriptor types `class` -\> `struct`](#10-descriptor-types-class-struct)

### Breaking changes

#### 1. Container types [1-container-types]

**Impact**: High.

Container types now use regular properties for their variants instead of static factory methods ([read more](index.md#7-improved-container-design)).

This change primarily affects the `Query` and `Aggregation` types.

```csharp
// 8.x
new SearchRequest
{
    Query = Query.MatchAll(
        new MatchAllQuery
        {
        }
    )
};

// 9.0
new SearchRequest
{
    Query = new Query
    {
        MatchAll = new MatchAllQuery
        {
        }
    }
};
```

Previously required methods like e.g. `TryGet<TVariant>(out)` have been removed.

The new recommended way of inspecting container types is to use simple pattern matching:

```csharp
var query = new Query();

if (query.Nested is { } nested)
{
    // We have a nested query.
}
```

#### 2. Removal of certain generic request descriptors [2-removal-of-certain-generic-request-descriptors]

**Impact**: High.

Removed the generic version of some request descriptors for which the corresponding requests do not contain inferrable properties.

These descriptors were generated unintentionally.

When migrating, the generic type parameter must be removed from the type, e.g., `AsyncSearchStatusRequestDescriptor<TDocument>` should become just `AsyncSearchStatusRequestDescriptor`.

List of affected descriptors:

- `AsyncQueryDeleteRequestDescriptor<TDocument>`
- `AsyncQueryGetRequestDescriptor<TDocument>`
- `AsyncSearchStatusRequestDescriptor<TDocument>`
- `DatabaseConfigurationDescriptor<TDocument>`
- `DatabaseConfigurationFullDescriptor<TDocument>`
- `DeleteAsyncRequestDescriptor<TDocument>`
- `DeleteAsyncSearchRequestDescriptor<TDocument>`
- `DeleteDataFrameAnalyticsRequestDescriptor<TDocument>`
- `DeleteGeoipDatabaseRequestDescriptor<TDocument>`
- `DeleteIpLocationDatabaseRequestDescriptor<TDocument>`
- `DeleteJobRequestDescriptor<TDocument>`
- `DeletePipelineRequestDescriptor<TDocument>`
- `DeleteScriptRequestDescriptor<TDocument>`
- `DeleteSynonymRequestDescriptor<TDocument>`
- `EqlDeleteRequestDescriptor<TDocument>`
- `EqlGetRequestDescriptor<TDocument>`
- `GetAsyncRequestDescriptor<TDocument>`
- `GetAsyncSearchRequestDescriptor<TDocument>`
- `GetAsyncStatusRequestDescriptor<TDocument>`
- `GetDataFrameAnalyticsRequestDescriptor<TDocument>`
- `GetDataFrameAnalyticsStatsRequestDescriptor<TDocument>`
- `GetEqlStatusRequestDescriptor<TDocument>`
- `GetGeoipDatabaseRequestDescriptor<TDocument>`
- `GetIpLocationDatabaseRequestDescriptor<TDocument>`
- `GetJobsRequestDescriptor<TDocument>`
- `GetPipelineRequestDescriptor<TDocument>`
- `GetRollupCapsRequestDescriptor<TDocument>`
- `GetRollupIndexCapsRequestDescriptor<TDocument>`
- `GetScriptRequestDescriptor<TDocument>`
- `GetSynonymRequestDescriptor<TDocument>`
- `IndexModifyDataStreamActionDescriptor<TDocument>`
- `PreprocessorDescriptor<TDocument>`
- `PutGeoipDatabaseRequestDescriptor<TDocument>`
- `PutIpLocationDatabaseRequestDescriptor<TDocument>`
- `PutScriptRequestDescriptor<TDocument>`
- `PutSynonymRequestDescriptor<TDocument>`
- `QueryVectorBuilderDescriptor<TDocument>`
- `RankDescriptor<TDocument>`
- `RenderSearchTemplateRequestDescriptor<TDocument>`
- `SmoothingModelDescriptor<TDocument>`
- `StartDataFrameAnalyticsRequestDescriptor<TDocument>`
- `StartJobRequestDescriptor<TDocument>`
- `StopDataFrameAnalyticsRequestDescriptor<TDocument>`
- `StopJobRequestDescriptor<TDocument>`
- `TokenizationConfigDescriptor<TDocument>`
- `UpdateDataFrameAnalyticsRequestDescriptor<TDocument>`

#### 3. Removal of certain descriptor constructors and related request APIs [3-removal-of-certain-descriptor-constructors-and-related-request-apis]

**Impact**: High.

Removed `(TDocument, IndexName)` descriptor constructors and related request APIs for all requests with `IndexName` and `Id` path parameters.

For example:

```csharp
// 8.x
public IndexRequestDescriptor(TDocument document, IndexName index, Id? id) { }
public IndexRequestDescriptor(TDocument document, IndexName index) { }
public IndexRequestDescriptor(TDocument document, Id? id) { }
public IndexRequestDescriptor(TDocument document) { }

// 9.0
public IndexRequestDescriptor(TDocument document, IndexName index, Id? id) { }
public IndexRequestDescriptor(TDocument document, Id? id) { }
public IndexRequestDescriptor(TDocument document) { }
```

These overloads caused invocation ambiguities since both, `IndexName` and `Id` implement implicit conversion operators from `string`.

Alternative with same semantics:

```csharp
// Descriptor constructor.
new IndexRequestDescriptor(document, "my_index", Id.From(document));

// Request API method.
await client.IndexAsync(document, "my_index", Id.From(document), ...);
```

#### 4. Date / Time / Duration values [4-date-time-duration-values]

**Impact**: High.

In places where previously `long` or `double` was used to represent a date/time/duration value, `DateTimeOffset` or `TimeSpan` is now used instead.

#### 5. `ExtendedBounds` [5-extendedbounds]

**Impact**: High.

Removed `ExtendedBoundsDate`/`ExtendedBoundsDateDescriptor`, `ExtendedBoundsFloat`/`ExtendedBoundsFloatDescriptor`.

Replaced by `ExtendedBounds<T>`, `ExtendedBoundsOfFieldDateMathDescriptor`, and `ExtendedBoundsOfDoubleDescriptor`.

#### 6. `Field.Format` [6-fieldformat]

**Impact**: Low.

Removed `Field.Format` property and corresponding constructor and inferrer overloads.

This property has not been used for some time (replaced by the `FieldAndFormat` type).

#### 7. `Field`/`Fields` semantics [7-fieldfields-semantics]

**Impact**: Low.

`Field`/`Fields` static factory methods and conversion operators no longer return nullable references but throw exceptions instead (`Field`) if the input `string`/`Expression`/`PropertyInfo` argument is `null`.

This makes implicit conversions to `Field` more user-friendly without requiring the null-forgiveness operator (`!`) ([read more](index.md#5-field-name-inference)).

#### 8. `FieldValue` [8-fieldvalue]

**Impact**: Low.

Removed `FieldValue.IsLazyDocument`, `FieldValue.IsComposite`, and the corresponding members in the `FieldValue.ValueKind` enum.

These values have not been used for some time.

#### 9. `FieldSort` [9-fieldsort]

**Impact**: High.

Removed `FieldSort` parameterless constructor.

Please use the new constructor instead:

```csharp
public FieldSort(Elastic.Clients.Elasticsearch.Field field)
```

**Impact**: Low.

Removed static `FieldSort.Empty` member.

Sorting got reworked which makes this member obsolete ([read more](index.md#8-sorting)).

#### 10. Descriptor types `class` -> `struct` [10-descriptor-types-class-struct]

**Impact**: Low.

All descriptor types are now implemented as `struct` instead of `class`.
