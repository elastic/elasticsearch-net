---
navigation_title: "LINQ to ES|QL"
---

# LINQ to ES|QL [linq-to-esql]

The .NET client includes a LINQ provider that translates standard C# LINQ expressions into [ES|QL](docs-content://explore-analyze/query-filter/languages/esql.md) queries at runtime. Instead of writing ES|QL strings by hand, you compose queries using familiar LINQ operators like `Where`, `Select`, `OrderBy`, `GroupBy`, and `Take`. The provider handles translation, parameterization, and result deserialization automatically.

**Capabilities overview:**

- **Standard LINQ operators**
  
  `Where`, `Select`, `OrderBy`/`ThenBy`, `Take`, `GroupBy`, `First`, `Single`, `Count`, `Any`, and more.

- **ES|QL-specific extensions**
  
  `Keep`, `Drop`, `LookupJoin`, `From`, `RawEsql`, `Completion`, and `Match` for operations that go beyond standard LINQ.

- **Rich function library**
  
  String, math, date/time, IP, pattern matching, and scoring functions are available directly in expressions.

- **Automatic parameter capturing**
  
  C# variables referenced in query expressions are captured as named parameters, keeping queries safe from injection.

- **Per-row streaming**
  
  Results are materialized one row at a time as `IEnumerable<T>`/`IAsyncEnumerable<T>`, keeping memory usage constant regardless of result set size.

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

## Raw ES|QL escape hatch [linq-esql-raw]

For ES|QL features not yet covered by the LINQ provider, you can append raw ES|QL fragments to a query:

```csharp
using Elastic.Esql.Extensions;

var results = client.Esql.Query<Product>(q => q
    .Where(p => p.InStock)
    .RawEsql("| EVAL discounted = price_usd * 0.9"));
```

Raw fragments are appended verbatim to the generated ES|QL query.
