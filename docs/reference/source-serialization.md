---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/source-serialization.html
---

# Source serialization [source-serialization]

Source serialization refers to the process of (de)serializing POCO types in consumer applications as source documents indexed and retrieved from {{es}}. A source serializer implementation handles serialization, with the default implementation using the `System.Text.Json` library. As a result, you may use `System.Text.Json` attributes and converters to control the serialization behavior.

- [Modelling documents with types](#modeling-documents-with-types)
  - [Default behavior](#default-behaviour)
  - [Using Elasticsearch types in documents](#elasticsearch-types-in-documents)
- [Customizing source serialization](#customizing-source-serialization)
  - [Using `System.Text.Json` attributes](#system-text-json-attributes)
  - [Configuring custom `JsonSerializerOptions`](#configuring-custom-jsonserializeroptions)
  - [Registering custom `System.Text.Json` converters](#registering-custom-converters)
  - [Creating a custom `Serializer`](#creating-custom-serializers)
- [Native AOT](#native-aot)

## Modeling documents with types [modeling-documents-with-types]

{{es}} provides search and aggregation capabilities on the documents that it is sent and indexes. These documents are sent as JSON objects within the request body of a HTTP request. It is natural to model documents within the {{es}} .NET client using [POCOs (*Plain Old CLR Objects*)](https://en.wikipedia.org/wiki/Plain_Old_CLR_Object).

This section provides an overview of how types and type hierarchies can be used to model documents.

### Default behaviour [default-behaviour]

The default behaviour is to serialize type property names as camelcase JSON object members.

We can model documents using a regular class (POCO).

```csharp
public class MyDocument
{
    public string StringProperty { get; set; }
}
```

We can then index the an instance of the document into {{es}}.

```csharp
using Elastic.Clients.Elasticsearch;

var document = new MyDocument
{
    StringProperty = "value"
};

var indexResponse = await Client.IndexAsync(document);
```

The index request is serialized, with the source serializer handling the `MyDocument` type, serializing the POCO property named `StringProperty` to the JSON object member named `stringProperty`.

```javascript
{
  "stringProperty": "value"
}
```

### Using Elasticsearch types in documents [elasticsearch-types-in-documents]

There are various cases where you might have a POCO type that contains an `Elastic.Clients.Elasticsearch` type as one of its properties.

For example, consider if you want to use percolation; you need to store {{es}} queries as part of the `_source` of your document, which means you need to have a POCO that looks like this:

```csharp
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Serialization;

public class MyPercolationDocument
{
    [JsonConverter(typeof(RequestResponseConverter<Query>))] <2>
    public Query Query { get; set; } <2>

    public string Category { get; set; }
}
```

1. The `Query` property uses the `Elastic.Clients.Elasticsearch.QueryDsl.Query` Elasticsearch type.
2. The `JsonConverter` attribute specifies the `RequestResponseConverter<T>` to be used for the `Query` property. This special converter instructs the `DefaultSourceSerializer` to delegate (de-)serialization to the `RequestResponseSerializer`.

::::{warning}

Failure to strictly use `RequestResponseConverter<T>` for `Elastic.Clients.Elasticsearch` types will most likely result in problems with document (de-)serialization.

::::

## Customizing source serialization [customizing-source-serialization]

The built-in source serializer handles most POCO document models correctly. Sometimes, you may need further control over how your types are serialized.

::::{note}

The built-in source serializer uses the [Microsoft `System.Text.Json` library](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) internally. You can apply `System.Text.Json` attributes and converters to control the serialization of your document types.

::::

### Using `System.Text.Json` attributes [system-text-json-attributes]

`System.Text.Json` includes attributes that can be applied to types and properties to control their serialization. These can be applied to your POCO document types to perform actions such as controlling the name of a property or ignoring a property entirely. Visit the [Microsoft documentation for further examples](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview).

We can model a document to represent data about a person using a regular class (POCO), applying `System.Text.Json` attributes as necessary.

```csharp
using System.Text.Json.Serialization;

public class Person
{
    [JsonPropertyName("forename")] <1>
    public string FirstName { get; set; }

    [JsonIgnore] <2>
    public int Age { get; set; }
}
```

1. The `JsonPropertyName` attribute ensures the `FirstName` property uses the JSON name `forename` when serialized.
2. The `JsonIgnore` attribute prevents the `Age` property from appearing in the serialized JSON.

We can then index an instance of the document into {{es}}.

```csharp
var person = new Person { FirstName = "Steve", Age = 35 };
var indexResponse = await Client.IndexAsync(person);
```

The index request is serialized, with the source serializer handling the `Person` type, serializing the POCO property named `FirstName` to the JSON object member named `forename`. The `Age` property is ignored and does not appear in the JSON.

```javascript
{
  "forename": "Steve"
}
```

### Configuring custom `JsonSerializerOptions` [configuring-custom-jsonserializeroptions]

The default source serializer applies a set of standard `JsonSerializerOptions` when serializing source document types. In some circumstances, you may need to override some of our defaults. This is achievable by creating an instance of `DefaultSourceSerializer` and passing an `Action<JsonSerializerOptions>`, which is applied after our defaults have been set. This mechanism allows you to apply additional settings or change the value of our defaults.

The `DefaultSourceSerializer` includes a constructor that accepts the current `IElasticsearchClientSettings` and a `configureOptions` `Action`.

```csharp
public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions>? configureOptions = null);
```

Our application defines the following `Person` class, which models a document we will index to {{es}}.

```csharp
public class Person
{
    public string FirstName { get; set; }
}
```

We want to serialize our source document using Pascal Casing for the JSON properties. Since the options applied in the `DefaultSouceSerializer` set the `PropertyNamingPolicy` to `JsonNamingPolicy.CamelCase`, we must override this setting. After configuring the `ElasticsearchClientSettings`, we index our document to {{es}}.

```csharp
using System.Text.Json;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

static void ConfigureOptions(JsonSerializerOptions o) <1>
{
    o.PropertyNamingPolicy = null;
}

var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new DefaultSourceSerializer(settings, ConfigureOptions)); <2>
var client = new ElasticsearchClient(settings);

var person = new Person { FirstName = "Steve" };
var indexResponse = await client.IndexAsync(person);
```

1. A local function can be defined, accepting a `JsonSerializerOptions` parameter. Here, we set `PropertyNamingPolicy` to `null`. This returns to the default behavior for `System.Text.Json`, which uses Pascal Case.
2. When creating the `ElasticsearchClientSettings`, we supply a `SourceSerializerFactory` using a lambda. The factory function creates a new instance of `DefaultSourceSerializer`, passing in the `settings` and our `ConfigureOptions` local function. We have now configured the settings with a custom instance of the source serializer.

The `Person` instance is serialized, with the source serializer serializing the POCO property named `FirstName` using Pascal Case.

```javascript
{
  "FirstName": "Steve"
}
```

As an alternative to using a local function, we could store an `Action<JsonSerializerOptions>` into a variable instead, which can be passed to the `DefaultSouceSerializer` constructor.

```csharp
Action<JsonSerializerOptions> configureOptions = o =>
{
    o.PropertyNamingPolicy = null;
}
```

### Registering custom `System.Text.Json` converters [registering-custom-converters]

In certain more advanced situations, you may have types which require further customization during serialization than is possible using `System.Text.Json` property attributes. In these cases, the recommendation from Microsoft is to leverage a custom `JsonConverter`. Source document types serialized using the  `DefaultSourceSerializer` can leverage the power of custom converters.

For this example, our application has a document class that should use a legacy JSON structure to continue operating with existing indexed documents. Several options are available, but we’ll apply a custom converter in this case.

Our class is defined, and the `JsonConverter` attribute is applied to the class type, specifying the type of a custom converter.

```csharp
using System.Text.Json.Serialization;

[JsonConverter(typeof(CustomerConverter))] <1>
public class Customer
{
    public string CustomerName { get; set; }
    public CustomerType CustomerType { get; set; }
}

public enum CustomerType
{
    Standard,
    Enhanced
}
```

1. The `JsonConverter` attribute signals to `System.Text.Json` that it should use a converter of type `CustomerConverter` when serializing instances of this class.

When serializing this class, rather than include a string value representing the value of the `CustomerType` property, we must send a boolean property named `isStandard`. This requirement can be achieved with a custom JsonConverter implementation.

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomerConverter : JsonConverter<Customer>
{
    public override Customer Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        var customer = new Customer();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                if (reader.ValueTextEquals("customerName"))
                {
                    reader.Read();
                    customer.CustomerName = reader.GetString();
                    continue;
                }

                if (reader.ValueTextEquals("isStandard")) <1>
                {
                    reader.Read();
                    var isStandard = reader.GetBoolean();

                    if (isStandard)
                    {
                        customer.CustomerType = CustomerType.Standard;
                    }
                    else
                    {
                        customer.CustomerType = CustomerType.Enhanced;
                    }

                    continue;
                }
            }
        }

        return customer;
    }

    public override void Write(Utf8JsonWriter writer,
        Customer value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        if (!string.IsNullOrEmpty(value.CustomerName))
        {
            writer.WritePropertyName("customerName");
            writer.WriteStringValue(value.CustomerName);
        }

        writer.WritePropertyName("isStandard");

        if (value.CustomerType == CustomerType.Standard) <2>
        {
            writer.WriteBooleanValue(true);
        }
        else
        {
            writer.WriteBooleanValue(false);
        }

        writer.WriteEndObject();
    }
}
```

1. When reading, this converter reads the `isStandard` boolean and translate this to the correct `CustomerType` enum value.
2. When writing, this converter translates the `CustomerType` enum value to an `isStandard` boolean property.

We can then index a customer document into {{es}}.

```csharp
var customer = new Customer
{
    CustomerName = "Customer Ltd",
    CustomerType = CustomerType.Enhanced
};

var indexResponse = await Client.IndexAsync(customer);
```

The `Customer` instance is serialized using the custom converter, creating the following JSON document.

```javascript
{
  "customerName": "Customer Ltd",
  "isStandard": false
}
```

### Creating a custom `Serializer` [creating-custom-serializers]

Suppose you prefer using an alternative JSON serialization library for your source types. In that case, you can inject an isolated serializer only to be called for the serialization of `_source`, `_fields`, or wherever a user-provided value is expected to be written and returned.

Implementing `Elastic.Transport.Serializer` is technically enough to create a custom source serializer.

```csharp
using Elastic.Transport;

public class VanillaSerializer : Serializer
{
    public override object? Deserialize(Type type, Stream stream) =>
        throw new NotImplementedException();

    public override T Deserialize<T>(Stream stream) =>
        throw new NotImplementedException();

    public override ValueTask<object?> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override void Serialize(object? data, Type type, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None) =>
        throw new NotImplementedException();

    public override Task SerializeAsync(object? data, Type type, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}
```

Registering up the serializer is performed in the `ConnectionSettings` constructor.

```csharp
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;

var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new VanillaSerializer()); <1>
var client = new ElasticsearchClient(settings);
```

1. If implementing `Serializer` is enough, why must we provide an instance wrapped in a factory `Func`?

There are various cases where you might have a POCO type that contains an `Elastic.Clients.Elasticsearch` type as one of its properties (see [Elasticsearch types in documents](#elasticsearch-types-in-documents)). The `SourceSerializerFactory` delegate provides access to the default built-in serializer so you can access it when necessary. For example, consider if you want to use percolation; you need to store {{es}} queries as part of the `_source` of your document, which means you need to have a POCO that looks like this.

```csharp
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Serialization;

public class MyPercolationDocument
{
    [JsonConverter(typeof(RequestResponseConverter<Query>))]
    public Query Query { get; set; }

    public string Category { get; set; }
}
```

A custom serializer would not know how to serialize `Query` or other `Elastic.Clients.Elasticsearch` types that could appear as part of the `_source` of a document. Therefore, your custom `Serializer` would need to store a reference to our built-in serializer and delegate serialization of Elastic types back to it.

::::{note}

Depending on the use-case, it might be easier to instead derive from `SystemTextJsonSerializer` and/or create a custom `IJsonSerializerOptionsProvider` implementation.

::::

## Native AOT [native-aot]

To use the Elasticsearch client in a [native AOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot) application, `System.Text.Json` must be configured to use [source generation](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation).

Source generation is always for internal `Elastic.Clients.Elasticsearch` types, but additional steps are required to also enable source generation for user defined types.

```csharp
using System.Text.Json.Serialization;

[JsonSerializable(typeof(Person), GenerationMode = JsonSourceGenerationMode.Default)] <2>
[JsonSerializable(typeof(...), GenerationMode = JsonSourceGenerationMode.Default)]
public sealed partial class UserTypeSerializerContext :
    JsonSerializerContext <1>
{
}
```

1. Create a new `partial` class that derives from `JsonSerializerContext`.
2. Annotate the serializer context with `JsonSerializable` attributes for all source types that are (de-)serialized using the Elasticsearch client.

```csharp
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (_, settings) =>
        new DefaultSourceSerializer(settings, UserTypeSerializerContext.Default)); <1>
var client = new ElasticsearchClient(settings);

var person = new Person { FirstName = "Steve" };
var indexResponse = await client.IndexAsync(person);
```

1. When creating the `ElasticsearchClientSettings`, we supply a `SourceSerializerFactory` using a lambda. The factory function creates a new instance of `DefaultSourceSerializer`, passing in the `settings` and our `UserTypeSerializerContext`. We have now configured the settings with a custom instance of the source serializer.

As an alternative, the `UserTypeSerializerContext` can as well be set when configuring the `JsonSerializerOptions` as described in [Configuring custom `JsonSerializerOptions`](#configuring-custom-jsonserializeroptions):

```csharp
static void ConfigureOptions(JsonSerializerOptions o)
{
    o.TypeInfoResolver = UserTypeSerializerContext.Default;
}
```
