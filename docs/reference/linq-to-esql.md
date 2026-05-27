---
navigation_title: "LINQ to ES|QL"
---

# LINQ to ES|QL [linq-to-esql]

The .NET client includes a LINQ provider that translates standard C# LINQ expressions into [ES|QL](docs-content://explore-analyze/query-filter/languages/esql.md) queries at runtime. Instead of writing ES|QL strings by hand, you compose queries using familiar LINQ operators like `Where`, `Select`, `OrderBy`, `GroupBy`, and `Take`. The provider handles translation, parameterization, and result deserialization automatically.

**Capabilities overview:**

- **Standard LINQ operators**
  
  `Where`, `Select`, `OrderBy`/`ThenBy`, `Take`, `GroupBy`, `First`, `Single`, `Count`, `Any`, and more.

- **ES|QL-specific extensions**
  
  `Keep`, `Drop`, `LookupJoin`, `From`, `Fork`, `Fuse`, `RawEsql`, `Completion`, and `Match` for operations that go beyond standard LINQ.

- **Rich function library**
  
  String, math, date/time, IP, pattern matching, and scoring functions are available directly in expressions.

- **Automatic parameter capturing**
  
  C# variables referenced in query expressions are captured as named parameters, keeping queries safe from injection.

- **Per-row streaming**
  
  Results are materialized one row at a time as `IEnumerable<T>`/`IAsyncEnumerable<T>`, keeping memory usage constant regardless of result set size.

- **Vector search**
  
  KNN search via `EsqlFunctions.Knn`, semantic search via `EsqlFunctions.TextEmbedding`, and exact similarity functions (`VCosine`, `VDotProduct`, `VHamming`, `VL1Norm`, `VL2Norm`).

- **Hybrid search**
  
  `Fork` and `Fuse` combine multiple sub-pipelines — full-text, KNN, or any other query — into a single ranked result set using RRF or linear scoring.

- **Raw response streams**
  
  `ToStream`, `ToStreamAsync`, and `ToPipeReaderAsync` return the response body in any supported wire format (Arrow, CSV, CBOR, etc.) — ready to pipe into columnar consumers like Apache Arrow, DataFusion, or DuckDB, or to do your own zero-copy decoding.

- **Source serialization**
  
  POCO types follow the same `System.Text.Json` rules as the rest of the client. See [Source serialization](source-serialization.md) for details on attribute mapping, custom converters, and naming policies.

## Defining a document type [linq-esql-document-type]

All examples on this page use the following model classes. Property names are mapped to ES|QL column names through `[JsonPropertyName]` attributes. The same rules as regular [source serialization](source-serialization.md) apply.

```csharp
using System.Text.Json.Serialization;

public class Product
{
    [JsonPropertyName("product_id")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    [JsonPropertyName("price_usd")]
    public double Price { get; set; }
    [JsonPropertyName("in_stock")]
    public bool InStock { get; set; }
    [JsonPropertyName("stock_quantity")]
    public int StockQuantity { get; set; }
    public ProductCategory Category { get; set; }
    public List<string> Tags { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<ProductCategory>))]
public enum ProductCategory { Electronics, Clothing, Books, Home, Sports }

public class Order
{
    [JsonPropertyName("order_id")]
    public string Id { get; set; }
    [JsonPropertyName("@timestamp")]
    public DateTime Timestamp { get; set; }
    public OrderStatus Status { get; set; }
    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
public enum OrderStatus { Pending, Confirmed, Shipped, Delivered, Cancelled }
```

## Getting started [linq-esql-getting-started]

The LINQ entry points are on `client.Esql`. There are three main ways to run a query:

### Synchronous

```csharp
var products = client.Esql.Query<Product>(q => q <1>
    .From("products")
    .Where(p => p.InStock)
    .OrderByDescending(p => p.Price)
    .Take(10));

foreach (var product in products)
    Console.WriteLine($"{product.Name}: {product.Price}");
```

1. Returns `IEnumerable<T>`

### Asynchronous

```csharp
await foreach (var product in client.Esql.QueryAsync<Product>(q => q <1>
    .From("products")
    .Where(p => p.InStock)
    .OrderByDescending(p => p.Price)
    .Take(10)))
{
    Console.WriteLine($"{product.Name}: {product.Price}");
}
```

1. Returns `IAsyncEnumerable<T>`

### Advanced composition with `CreateQuery`

```csharp
var query = client.Esql.CreateQuery<Product>()
    .From("products")
    .Where(p => p.Category == ProductCategory.Electronics)
    .OrderBy(p => p.Price);

// Inspect the generated ES|QL
Console.WriteLine(query.ToEsqlString());

// Execute
await foreach (var product in query.AsAsyncEnumerable())
    Console.WriteLine(product.Name);
```

## Source commands [linq-esql-source-commands]

Every ES|QL query begins with exactly one source command. The LINQ provider currently supports:

- **`FROM`**
  
  Queries documents from one or more indices. This is the most common entry point.

- **`ROW`**
  
  Produces a single row with literal values, useful for testing expressions or generating computed data without hitting an index.

```csharp
using Elastic.Esql.Extensions;

client.Esql.Query<Product>(q => q
    .From("products") <1>
    .Where(p => p.InStock));

client.Esql.Query<Product>(q => q
    .From("products-*") <2>
    .Take(10));

client.Esql.Query<Product>(q => q
    .Row(() => new { name = "Test", price_usd = 9.99 })); <3>
```

1. `FROM`: query an index (or index pattern).
2. `FROM` with a wildcard pattern.
3. `ROW`: produce a synthetic row without querying an index.

If you omit a source command, the provider infers `FROM` using the type name of `T` according to the `ElasticsearchClientSettings` (`IndexMappingFor<T>` or `DefaultIndex`).

## Filtering [linq-esql-filtering]

Use `Where` with standard C# expressions. The provider translates them to ES|QL `WHERE` clauses.

```csharp
// Simple equality.
client.Esql.Query<Product>(q => q.Where(p => p.Brand == "TechCorp"));

// Numeric comparison.
client.Esql.Query<Product>(q => q.Where(p => p.Price > 500));

// Boolean field.
client.Esql.Query<Product>(q => q.Where(p => p.InStock));

// Combined conditions (AND).
client.Esql.Query<Product>(q => q.Where(p => p.InStock && p.Price > 200));

// OR conditions.
client.Esql.Query<Product>(q => q
    .Where(p => p.Brand == "TechCorp" || p.Brand == "StyleMax"));

// Null checks (translates to IS NOT NULL / IS NULL).
client.Esql.Query<Order>(q => q.Where(o => o.Notes != null));

// Enum comparison.
client.Esql.Query<Order>(q => q.Where(o => o.Status == OrderStatus.Delivered));

// Range.
client.Esql.Query<Product>(q => q
    .Where(p => p.Price >= 100 && p.Price <= 300));
```

String methods are also translated:

```csharp
// Contains → LIKE "*value*"
client.Esql.Query<Product>(q => q.Where(p => p.Name.Contains("Pro")));

// StartsWith → LIKE "value*"
client.Esql.Query<Product>(q => q.Where(p => p.Name.StartsWith("Ultra")));
```

Collection-based `Contains` translates to the `IN` operator:

```csharp
var brands = new[] { "TechCorp", "StyleMax", "HomeBase" };
client.Esql.Query<Product>(q => q.Where(p => brands.Contains(p.Brand)));
// → WHERE brand IN ("TechCorp", "StyleMax", "HomeBase")
```

## Projections [linq-esql-projections]

Use `Select` to choose specific fields or create transformed results.

```csharp
// Select specific fields into an anonymous type.
var query = client.Esql.CreateQuery<Product>()
    .Select(p => new { p.Name, p.Price });

// Rename fields in the projection.
var query = client.Esql.CreateQuery<Product>()
    .Select(p => new { ProductName = p.Name, p.Price, p.InStock });
```

## Sorting and pagination [linq-esql-sorting]

```csharp
// Sort ascending.
client.Esql.Query<Product>(q => q.OrderBy(p => p.Price));

// Sort descending.
client.Esql.Query<Product>(q => q.OrderByDescending(p => p.Price));

// Multi-column sort.
client.Esql.Query<Product>(q => q
    .OrderBy(p => p.Brand)
    .ThenByDescending(p => p.Price));

// Pagination with `Take` (translates to LIMIT).
client.Esql.Query<Product>(q => q
    .Where(p => p.InStock)
    .OrderByDescending(p => p.Price)
    .Take(20));
```

::::{note}

`Skip` is not supported by ES|QL. Use sorting with a range filter for pagination instead.

::::

## Aggregations [linq-esql-aggregations]

Combine `GroupBy` with aggregate functions in `Select` to produce `STATS ... BY ...` queries.

```csharp
// Count per group.
var brandCounts = client.Esql.Query<Product, object>(q => q
    .GroupBy(p => p.Brand)
    .Select(g => new { Brand = g.Key, Count = g.Count() }));

// Multiple aggregations.
var stats = client.Esql.Query<Product, object>(q => q
    .Where(p => p.InStock)
    .GroupBy(p => p.Brand)
    .Select(g => new
    {
        Brand = g.Key,
        Count = g.Count(),
        AvgPrice = g.Average(p => p.Price),
        MinPrice = g.Min(p => p.Price),
        MaxPrice = g.Max(p => p.Price)
    }));
```

Scalar aggregations work without `GroupBy`:

```csharp
// Count all matching documents.
var count = client.Esql.CreateQuery<Product>()
    .Where(p => p.InStock)
    .Count();
```

## Joins [linq-esql-joins]

Use `LookupJoin` for cross-index lookups (translates to ES|QL `LOOKUP JOIN`).

```csharp
public class CategoryLookup
{
    [JsonPropertyName("category_id")]
    public string CategoryId { get; set; }
    [JsonPropertyName("category_label")]
    public string CategoryLabel { get; set; }
}

var enriched = client.Esql.Query<Product, object>(q => q
    .LookupJoin<Product, CategoryLookup, string, object>(
        "category-lookup-index",
        product => product.Id,
        category => category.CategoryId,
        (product, category) => new
        {
            product.Name,
            product.Price,
            category!.CategoryLabel
        }));
```

## Keep and Drop [linq-esql-keep-drop]

Explicitly include or exclude fields from the result set.

```csharp
using Elastic.Esql.Extensions;

// Keep only specific fields (translates to KEEP command).
client.Esql.Query<Product>(q => q.Keep(p => p.Id, p => p.Name, p => p.Price));

// Drop fields from results (translates to DROP command).
client.Esql.Query<Product>(q => q.Drop(p => p.Tags));

// String-based variants.
client.Esql.Query<Product>(q => q.Keep("product_id", "name", "price_usd"));
```

::::{note}

Projections always emit `KEEP` commands automatically to retain only the required fields.

::::

## Automatic parameter capturing [linq-esql-parameters]

When you reference C# variables in a query expression, the LINQ provider captures them as named parameters. This makes queries safe from injection and enables query plan caching on the server.

```csharp
var minPrice = 100.0;
var brand = "TechCorp";

var query = client.Esql.CreateQuery<Product>()
    .Where(p => p.Price >= minPrice && p.Brand == brand);

// View the parameterized query.
Console.WriteLine(query.ToEsqlString(inlineParameters: false)); <1>

// View the captured parameters.
var parameters = query.GetParameters(); <2>
```

1. `FROM products | WHERE price_usd >= ?minPrice AND brand == ?brand`
2. `{ "minPrice": 100.0, "brand": "TechCorp" }`

By default, `ToEsqlString()` inlines the values for readability:

```csharp
Console.WriteLine(query.ToEsqlString()); <1>
```

1. `FROM products | WHERE price_usd >= 100.0 AND brand == "TechCorp"`

When executing the query through `client.Esql.Query` or `QueryAsync`, parameters are always sent separately from the query string.

## Query options [linq-esql-query-options]

`EsqlQueryOptions` lets you configure per-query settings like filters, timezone, and transport options.

```csharp
using Elastic.Clients.Elasticsearch.Esql;

var options = new EsqlQueryOptions
{
    // Apply a Query DSL filter before the ES|QL query runs.
    Filter = new TermQuery { Field = "environment", Value = "production" },

    // Timezone for date operations (supported for Elasticsearch 9.4 and above).
    TimeZone = "Europe/Berlin",

    // Allow partial results if some shards are unavailable.
    AllowPartialResults = true,

    // Remove entirely null columns from the response.
    DropNullColumns = true
};

var results = client.Esql.Query<Product>(q => q
        .Where(p => p.InStock),
    queryOptions: options); <1>

var results = client.Esql.Query<Product>(q => q
    .Where(p => p.InStock)
    .WithOptions(options) <2>
);
```

1. Pass options as a method parameter.
2. Or apply options within the LINQ chain using `WithOptions`.

## Multi-field access [linq-esql-multi-field]

ES|QL primarily operates on `keyword` fields for exact filtering and sorting. If a field is for example mapped as `text`, you typically need to reference its `keyword` sub-field. The `MultiField` extension method provides this:

```csharp
using Elastic.Esql.Extensions;

// Access the keyword sub-field of a text field.
client.Esql.Query<Product>(q => q
    .Where(p => p.Name.MultiField("keyword") == "Ultra Pro X1")); <1>
```

1. Translates to `WHERE name.keyword == "Ultra Pro X1"`.

This is especially useful for exact matching, sorting, and aggregations on fields that are mapped as `text` with a `keyword` sub-field in your Elasticsearch index.

## Nested objects [linq-esql-nested-objects]

ES|QL returns flattened column names for nested objects (for example `address.street`, `address.city`). The LINQ provider automatically reassembles these into nested POCO types during deserialization.

Define your models with nested types:

```csharp
public class UserProfile
{
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
    public string Name { get; set; }
    public Address? Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
```

Query and access nested data naturally:

```csharp
await foreach (var user in client.Esql.QueryAsync<UserProfile>(q => q
    .Where(u => u.Name == "Alice")))
{
    // The Address object is reconstructed from flat columns:
    // address.street, address.city, address.country
    Console.WriteLine($"{user.Name} lives in {user.Address?.City}");
}
```

**Null handling:** If all sub-properties of a nested object are null in the response, the nested object itself will be `null`. If at least one sub-property has a value, the object is created with defaults for any missing fields.

## Multi-value fields [linq-esql-multi-value-fields]

ES|QL can return a single scalar value instead of an array when a multi-value field contains exactly one element. For example, a `tags` field with a single tag might be returned as `"electronics"` rather than `["electronics"]`.

The deserializer handles this automatically: scalar values are coerced into single-element collections when the POCO property is a collection type. To ensure correct deserialization, always use collection types for multi-value fields:

```csharp
public class Product
{
    // Correct: handles both scalar and array responses.
    public List<string> Tags { get; set; }

    // Avoid! Will throw if ES|QL returns an array
    // public string Tags { get; set; }
}
```

This applies to any collection type: `List<T>`, `T[]`, `IReadOnlyList<T>`, and so on.

## Document metadata [linq-esql-metadata]

Access Elasticsearch document metadata fields — `_id`, `_score`, `_index`, and others — by requesting them in the `From` command and referencing them through `EsqlMetadata`.

Pass a `MetadataField` flags value to `From` to declare which metadata fields to include:

```csharp
using Elastic.Esql;
using Elastic.Esql.Extensions;

var results = client.Esql.Query<Product>(q => q
    .From("products", MetadataField.Id | MetadataField.Score)
    .Where(p => p.InStock)
    .OrderByDescending(_ => EsqlMetadata.Score));
```

`MetadataField` is a bitfield; combine values with `|`. `MetadataField.All` requests all supported fields.

| Member      | ES\|QL field   | CLR type     | Description                                        |
|-------------|-----------------|--------------|---------------------------------------------------|
| `Id`        | `_id`           | `string`     | Unique document identifier                         |
| `Ignored`   | `_ignored`      | `string[]`   | Fields ignored at index time                       |
| `Index`     | `_index`        | `string`     | Index name                                         |
| `IndexMode` | `_index_mode`   | `string`     | Index mode (`standard`, `lookup`, `logsdb`, …)     |
| `Score`     | `_score`        | `float`      | Query relevance score                              |
| `Size`      | `_size`         | `int`        | Size in bytes of the original `_source` field      |
| `Source`    | `_source`       | `JsonObject` | Original document body as a JSON object            |
| `Version`   | `_version`      | `long`       | Document version number                            |

`EsqlMetadata.Fork` (the `_fork` discriminator added by `Fork`) is always available after a `Fork` command and does not need to be explicitly requested.

## Vector search [linq-esql-vector-search]

The LINQ provider supports dense-vector fields for approximate nearest-neighbour (KNN) search, semantic search via text embeddings, and exact similarity computations.

### DenseVector type [linq-esql-dense-vector]

Use `DenseVector<T>` to represent vector field values in document models. Two element types are supported:

- `DenseVector<float>` — for `element_type: "float"` fields
- `DenseVector<byte>` — for `element_type: "byte"` and `element_type: "bit"` fields

```csharp
using Elastic.Esql;

public class Article
{
    public string Title { get; set; }
    public DenseVector<float> Embedding { get; set; }   // element_type: "float"
    public DenseVector<byte> ImageHash { get; set; }    // element_type: "byte"
}
```

Arrays and `ReadOnlyMemory<T>` values are implicitly converted to `DenseVector<T>`, so you can pass them directly in query expressions.

### KNN search [linq-esql-knn]

Call `EsqlFunctions.Knn` inside a `Where` clause to perform approximate KNN search. Request `MetadataField.Score` to access the relevance score:

```csharp
using Elastic.Esql;
using Elastic.Esql.Extensions;
using Elastic.Esql.Functions;

float[] queryVector = await GetEmbeddingAsync("search term");

var results = client.Esql.Query<Article>(q => q
    .From("articles", MetadataField.Score)
    .Where(a => EsqlFunctions.Knn(a.Embedding, queryVector))
    .OrderByDescending(_ => EsqlMetadata.Score)
    .Take(10));
```

Closure-captured arrays and vectors are sent as named parameters, keeping the query safe and enabling server-side plan caching.

### KNN options [linq-esql-knn-options]

Pass a `KnnOptions` record to tune the search behaviour:

```csharp
var results = client.Esql.Query<Article>(q => q
    .From("articles", MetadataField.Score)
    .Where(a => EsqlFunctions.Knn(a.Embedding, queryVector, new KnnOptions
    {
        K = 50,
        Similarity = 0.7,
        RescoreOversample = 2.0
    })));
```

| Property            | ES\|QL key           | Description                                     |
|---------------------|----------------------|-------------------------------------------------|
| `K`                 | `k`                  | Number of nearest neighbours per shard          |
| `MinCandidates`     | `min_candidates`     | Minimum candidates in the approximate search    |
| `Similarity`        | `similarity`         | Minimum similarity threshold                    |
| `Boost`             | `boost`              | Score boost for the KNN clause                  |
| `VisitPercentage`   | `visit_percentage`   | Fraction of vectors visited                     |
| `RescoreOversample` | `rescore_oversample` | Oversampling factor for re-scoring              |

Unset properties are omitted from the generated query.

### Semantic search with text embeddings [linq-esql-text-embedding]

Use `EsqlFunctions.TextEmbedding` to convert a text query to a vector using a serverless inference endpoint, and pass the result directly to `Knn`:

```csharp
var results = client.Esql.Query<Article>(q => q
    .From("articles", MetadataField.Score)
    .Where(a => EsqlFunctions.Knn(
        a.Embedding,
        EsqlFunctions.TextEmbedding("my search query", InferenceEndpoints.TextEmbedding.MultilingualE5Small)))
    .OrderByDescending(_ => EsqlMetadata.Score)
    .Take(10));
```

`InferenceEndpoints` provides constants for Elastic serverless inference endpoints:

| Class                              | Constants                                     | Models                            |
|------------------------------------|-----------------------------------------------|-----------------------------------|
| `InferenceEndpoints.TextEmbedding` | `ElserV2`, `MultilingualE5Small`              | ELSER v2, Multilingual E5 Small   |
| `InferenceEndpoints.Anthropic`     | `Claude46Opus`, `Claude45Sonnet`, …           | Anthropic Claude models           |
| `InferenceEndpoints.Google`        | `Gemini25Pro`, `Gemini25Flash`                | Google Gemini models              |
| `InferenceEndpoints.OpenAi`        | `Gpt41`, `Gpt41Mini`, `Gpt52`, …             | OpenAI GPT models                 |
| `InferenceEndpoints.Elastic`       | `GpLlmV2`                                     | Elastic GP-LLM                    |

### Exact similarity functions [linq-esql-vector-similarity]

For exact (brute-force) similarity, use the vector distance functions in `Where`, `Select`, or `OrderBy` expressions:

```csharp
// Filter by cosine similarity threshold.
var results = client.Esql.Query<Article>(q => q
    .Where(a => EsqlFunctions.VCosine(a.Embedding, queryVector) >= 0.8));

// Order by dot product (for unit-normalised vectors).
var results = client.Esql.Query<Article>(q => q
    .OrderByDescending(a => EsqlFunctions.VDotProduct(a.Embedding, queryVector)));
```

| Method        | ES\|QL function  | Input type           | Description                   |
|---------------|------------------|----------------------|-------------------------------|
| `VCosine`     | `V_COSINE`       | `DenseVector<float>` | Cosine similarity             |
| `VDotProduct` | `V_DOT_PRODUCT`  | `DenseVector<float>` | Dot product                   |
| `VL1Norm`     | `V_L1_NORM`      | `DenseVector<float>` | L1 (Manhattan) distance       |
| `VL2Norm`     | `V_L2_NORM`      | `DenseVector<float>` | L2 (Euclidean) distance       |
| `VHamming`    | `V_HAMMING`      | `DenseVector<byte>`  | Hamming distance (byte / bit) |

## Hybrid search [linq-esql-hybrid-search]

`Fork` and `Fuse` let you run multiple sub-pipelines over the same input — for example a full-text query and a KNN search — and merge their results into a single ranked list.

### Basic Fork + Fuse [linq-esql-fork-fuse]

```csharp
using Elastic.Esql.Extensions;
using Elastic.Esql.Functions;

float[] queryVector = await GetEmbeddingAsync("search term");

var results = client.Esql.Query<Article>(q => q
    .From("articles", MetadataField.Score)
    .Fork(
        branch => branch.Where(a => EsqlFunctions.Match(a.Title, "search term")).Take(50), // <1>
        branch => branch.Where(a => EsqlFunctions.Knn(a.Embedding, queryVector)).Take(50)) // <2>
    .Fuse()
    .OrderByDescending(_ => EsqlMetadata.Score)
    .Take(10));
```

1. Full-text branch.
2. KNN branch.

`Fork` translates to an ES|QL `FORK` command and adds a `_fork` discriminator column with values `fork1`, `fork2`, … indicating which branch produced each row. `Fuse()` with no arguments applies Reciprocal Rank Fusion (RRF) with the default rank constant of 60. Each branch that feeds a `Fuse` must include a `Take(...)` (LIMIT).

### Fuse options [linq-esql-fuse-options]

```csharp
// RRF with a custom rank constant.
.Fuse(method: FuseMethod.Rrf, rankConstant: 20)

// Linear combination with per-branch weights and min-max normalisation.
.Fuse(method: FuseMethod.Linear, weights: [0.3, 0.7], normalizer: ScoreNormalizer.MinMax)
```

| Parameter      | Type              | Description                                                         |
|----------------|-------------------|---------------------------------------------------------------------|
| `method`       | `FuseMethod`      | `Rrf` (default) or `Linear`                                         |
| `rankConstant` | `int?`            | RRF rank constant (default: 60)                                     |
| `normalizer`   | `ScoreNormalizer` | `None` (default) or `MinMax` — min-max normalisation (linear only)  |
| `weights`      | `double[]?`       | Per-branch weights, aligned to branch declaration order             |
| `score`        | `Expression`      | Custom scoring column selector                                      |
| `group`        | `Expression`      | Custom grouping column selector                                     |
| `key`          | `Expression`      | Custom key column selector                                          |

`Fork` supports up to 8 branches.

## Server-side async queries [linq-esql-async-queries]

For long-running queries, you can submit them as server-side async queries. The server processes the query in the background and you poll for results.

```csharp
await using var asyncQuery = await client.Esql.SubmitAsyncQueryAsync<Product>(
    q => q.Where(p => p.InStock),
    asyncQueryOptions: new EsqlAsyncQueryOptions
    {
        WaitForCompletionTimeout = TimeSpan.FromSeconds(5),
        KeepAlive = TimeSpan.FromMinutes(10),
        KeepOnCompletion = true
    });

await asyncQuery.WaitForCompletionAsync(); <1>

await foreach (var product in asyncQuery.AsAsyncEnumerable()) <2>
    Console.WriteLine(product.Name);

foreach (var product in asyncQuery.AsEnumerable()) <3>
    Console.WriteLine(product.Name);
```

1. Wait for the query to complete.
2. Stream the results.
3. Or consume synchronously.

The `EsqlAsyncQuery<T>` is disposable. Disposing it automatically sends a delete request to clean up the server-side query.

## Raw format response streams [linq-esql-raw-streams]

By default, every query path materializes rows into POCOs by streaming the JSON response through the typed reader. For scenarios where you want the unparsed, server-formatted bytes — piping to a file, feeding columnar consumers like Apache Arrow / DataFusion / DuckDB, or doing your own zero-copy decoding — every queryable also exposes raw-stream terminal methods.

### Supported formats [linq-esql-formats]

Pick the wire format with the `EsqlFormat` enum:

| Value   | Media type                              | Description                     |
|---------|-----------------------------------------|---------------------------------|
| `Json`  | `application/json`                      | JSON (default)                  |
| `Csv`   | `text/csv`                              | Comma-separated values          |
| `Tsv`   | `text/tab-separated-values`             | Tab-separated values            |
| `Txt`   | `text/plain`                            | Human-readable table            |
| `Arrow` | `application/vnd.apache.arrow.stream`   | Apache Arrow IPC stream         |
| `Smile` | `application/smile`                     | SMILE (binary JSON variant)     |
| `Cbor`  | `application/cbor`                      | CBOR (binary)                   |
| `Yaml`  | `application/yaml`                      | YAML                            |

### Synchronous stream [linq-esql-to-stream]

```csharp
using Elastic.Esql;
using Elastic.Esql.Extensions;

// The returned Stream owns the HTTP response — dispose it to release the connection.
using var stream = client.Esql.CreateQuery<Product>()
    .Where(p => p.InStock)
    .AsEsqlQueryable() // <1>
    .ToStream(EsqlFormat.Arrow);

// Pipe into any consumer that accepts a Stream.
await ProcessArrowStreamAsync(stream);
```

1. Standard LINQ operators like `Where` return `IQueryable<T>`. Call `AsEsqlQueryable()` before any raw-stream terminal method.

### Asynchronous stream [linq-esql-to-stream-async]

```csharp
await using var stream = await client.Esql.CreateQuery<Product>()
    .Where(p => p.InStock)
    .AsEsqlQueryable()
    .ToStreamAsync(EsqlFormat.Csv);

using var reader = new StreamReader(stream);
Console.WriteLine(await reader.ReadToEndAsync());
```

### PipeReader (.NET 10+) [linq-esql-to-pipe-reader]

On .NET 10 and later, use `ToPipeReaderAsync` for zero-copy access:

```csharp
var pipeReader = await client.Esql.CreateQuery<Product>()
    .Where(p => p.InStock)
    .AsEsqlQueryable()
    .ToPipeReaderAsync(EsqlFormat.Arrow);
```

### Raw format async queries [linq-esql-raw-async]

For long-running queries where you need both server-side async execution and raw format output, use `ToAsyncQuery` or `ToAsyncQueryAsync`:

```csharp
await using var asyncQuery = await client.Esql.CreateQuery<Product>()
    .Where(p => p.InStock)
    .AsEsqlQueryable()
    .ToAsyncQueryAsync(EsqlFormat.Arrow, new EsqlAsyncQueryOptions
    {
        WaitForCompletionTimeout = TimeSpan.FromSeconds(5),
        KeepAlive = TimeSpan.FromMinutes(10)
    });

await asyncQuery.WaitForCompletionAsync();

// Hand the Arrow IPC stream to the Apache.Arrow.Ipc reader (separate NuGet).
using var stream = asyncQuery.GetResponseStream();
using var reader = new ArrowStreamReader(stream);
while (await reader.ReadNextRecordBatchAsync() is { } batch)
    Console.WriteLine($"Batch: {batch.Length} rows, {batch.ColumnCount} cols");

// On .NET 10+, access as a PipeReader instead.
// var pipeReader = asyncQuery.GetResponsePipeReader();
```

Disposing the `EsqlAsyncQuery` automatically deletes the server-side query.

## Raw ES|QL escape hatch [linq-esql-raw]

For ES|QL features not yet covered by the LINQ provider, you can append raw ES|QL fragments to a query:

```csharp
using Elastic.Esql.Extensions;

var results = client.Esql.Query<Product>(q => q
    .Where(p => p.InStock)
    .RawEsql("| EVAL discounted = price_usd * 0.9"));
```

Raw fragments are appended verbatim to the generated ES|QL query.
