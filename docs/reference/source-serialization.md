---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/source-serialization.html
---

# Source serialization [source-serialization]

Source serialization refers to the process of (de)serializing POCO types in consumer applications as source documents indexed and retrieved from {{es}}. A source serializer implementation handles serialization, with the default implementation using the `System.Text.Json` library. As a result, you may use `System.Text.Json` attributes and converters to control the serialization behavior.

* [Modelling documents with types](#modeling-documents-with-types)
* [Customizing source serialization](#customizing-source-serialization)

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
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;

var document = new MyDocument
{
    StringProperty = "value"
};

var indexResponse = await Client
    .IndexAsync(document, "my-index-name");
```

The index request is serialized, with the source serializer handling the `MyDocument` type, serializing the POCO property named `StringProperty` to the JSON object member named `stringProperty`.

```javascript
{
  "stringProperty": "value"
}
```



## Customizing source serialization [customizing-source-serialization]

The built-in source serializer handles most POCO document models correctly. Sometimes, you may need further control over how your types are serialized.

::::{note}
The built-in source serializer uses the [Microsoft `System.Text.Json` library](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview) internally. You can apply `System.Text.Json` attributes and converters to control the serialization of your document types.
::::



#### Using `System.Text.Json` attributes [system-text-json-attributes]

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
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

var person = new Person { FirstName = "Steve", Age = 35 };
var indexResponse = await Client.IndexAsync(person, "my-index-name");
```

The index request is serialized, with the source serializer handling the `Person` type, serializing the POCO property named `FirstName` to the JSON object member named `forename`. The `Age` property is ignored and does not appear in the JSON.

```javascript
{
  "forename": "Steve"
}
```


#### Configuring custom `JsonSerializerOptions` [configuring-custom-jsonserializeroptions]

The default source serializer applies a set of standard `JsonSerializerOptions` when serializing source document types. In some circumstances, you may need to override some of our defaults. This is achievable by creating an instance of `DefaultSourceSerializer` and passing an `Action<JsonSerializerOptions>`, which is applied after our defaults have been set. This mechanism allows you to apply additional settings or change the value of our defaults.

The `DefaultSourceSerializer` includes a constructor that accepts the current `IElasticsearchClientSettings` and a `configureOptions` `Action`.

```csharp
public DefaultSourceSerializer(IElasticsearchClientSettings settings, Action<JsonSerializerOptions> configureOptions);
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
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

static void ConfigureOptions(JsonSerializerOptions o) => <1>
    o.PropertyNamingPolicy = null;

var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new DefaultSourceSerializer(settings, ConfigureOptions)); <2>
var client = new ElasticsearchClient(settings);

var person = new Person { FirstName = "Steve" };
var indexResponse = await client.IndexAsync(person, "my-index-name");
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
Action<JsonSerializerOptions> configureOptions = o => o.PropertyNamingPolicy = null;
```


#### Registering custom `System.Text.Json` converters [registering-custom-converters]

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
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

var customer = new Customer
{
    CustomerName = "Customer Ltd",
    CustomerType = CustomerType.Enhanced
};
var indexResponse = await Client.IndexAsync(customer, "my-index-name");
```

The `Customer` instance is serialized using the custom converter, creating the following JSON document.

```javascript
{
  "customerName": "Customer Ltd",
  "isStandard": false
}
```


#### Creating a custom `SystemTextJsonSerializer` [creating-custom-system-text-json-serializer]

The built-in `DefaultSourceSerializer` includes the registration of `JsonConverter` instances which apply during source serialization. In most cases, these provide the proper behavior for serializing source documents, including those which use `Elastic.Clients.Elasticsearch` types on their properties.

An example of a situation where you may require more control over the converter registration order is for serializing `enum` types. The `DefaultSourceSerializer` registers the `System.Text.Json.Serialization.JsonStringEnumConverter`, so enum values are serialized using their string representation. Generally, this is the preferred option for types used to index documents to {{es}}.

In some scenarios, you may need to control the string value sent for an enumeration value. That is not directly supported in `System.Text.Json` but can be achieved by creating a custom `JsonConverter` for the `enum` type you wish to customize. In this situation, it is not sufficient to use the `JsonConverterAttribute` on the `enum` type to register the converter. `System.Text.Json` will prefer the converters added to the `Converters` collection on the `JsonSerializerOptions` over an attribute applied to an `enum` type. It is, therefore, necessary to either remove the `JsonStringEnumConverter` from the `Converters` collection or register a specialized converter for your `enum` type before the `JsonStringEnumConverter`.

The latter is possible via several techniques. When using the {{es}} .NET library, we can achieve this by deriving from the abstract `SystemTextJsonSerializer` class.

Here we have a POCO which uses the `CustomerType` enum as the type for a property.

```csharp
using System.Text.Json.Serialization;

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

To customize the strings used during serialization of the `CustomerType`, we define a custom `JsonConverter` specific to our `enum` type.

```csharp
using System.Text.Json.Serialization;

public class CustomerTypeConverter : JsonConverter<CustomerType>
{
    public override CustomerType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch <1>
        {
            "basic" => CustomerType.Standard,
            "premium" => CustomerType.Enhanced,
            _ => throw new JsonException(
                $"Unknown value read when deserializing {nameof(CustomerType)}."),
        };
    }

    public override void Write(Utf8JsonWriter writer, CustomerType value, JsonSerializerOptions options)
    {
        switch (value) <2>
        {
            case CustomerType.Standard:
                writer.WriteStringValue("basic");
                return;
            case CustomerType.Enhanced:
                writer.WriteStringValue("premium");
                return;
        }

        writer.WriteNullValue();
    }
}
```

1. When reading, this converter translates the string used in the JSON to the matching enum value.
2. When writing, this converter translates the `CustomerType` enum value to a custom string value written to the JSON.


We create a serializer derived from `SystemTextJsonSerializer` to give us complete control of converter registration order.

```csharp
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Serialization;

public class MyCustomSerializer : SystemTextJsonSerializer <1>
{
    private readonly JsonSerializerOptions _options;

    public MyCustomSerializer(IElasticsearchClientSettings settings) : base(settings)
    {
        var options = DefaultSourceSerializer.CreateDefaultJsonSerializerOptions(false); <2>

        options.Converters.Add(new CustomerTypeConverter()); <3>

        _options = DefaultSourceSerializer.AddDefaultConverters(options); <4>
    }

    protected override JsonSerializerOptions CreateJsonSerializerOptions() => _options; <5>
}
```

1. Inherit from `SystemTextJsonSerializer`.
2. In the constructor, use the factory method `DefaultSourceSerializer.CreateDefaultJsonSerializerOptions` to create default options for serialization. No default converters are registered at this stage because we pass `false` as an argument.
3. Register our `CustomerTypeConverter` as the first converter.
4. To apply any default converters, call the `DefaultSourceSerializer.AddDefaultConverters` helper method, passing the options to modify.
5. Implement the `CreateJsonSerializerOptions` method returning the stored `JsonSerializerOptions`.


Because we have registered our `CustomerTypeConverter` before the default converters (which include the `JsonStringEnumConverter`), our converter takes precedence when serializing `CustomerType` instances on source documents.

The base `SystemTextJsonSerializer` class handles the implementation details of binding, which is required to ensure that the built-in converters can access the `IElasticsearchClientSettings` where needed.

We can then index a customer document into {{es}}.

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

var customer = new Customer
{
    CustomerName = "Customer Ltd",
    CustomerType = CustomerType.Enhanced
};

var indexResponse = await client.IndexAsync(customer, "my-index-name");
```

The `Customer` instance is serialized using the custom `enum` converter, creating the following JSON document.

```javascript
{
  "customerName": "Customer Ltd",
  "customerType": "premium" <1>
}
```

1. The string value applied during serialization is provided by our custom converter.



#### Creating a custom `Serializer` [creating-custom-serializers]

Suppose you prefer using an alternative JSON serialization library for your source types. In that case, you can inject an isolated serializer only to be called for the serialization of `_source`, `_fields`, or wherever a user-provided value is expected to be written and returned.

Implementing `Elastic.Transport.Serializer` is technically enough to create a custom source serializer.

```csharp
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

public class VanillaSerializer : Serializer
{
    public override object Deserialize(Type type, Stream stream) =>
        throw new NotImplementedException();

    public override T Deserialize<T>(Stream stream) =>
        throw new NotImplementedException();

    public override ValueTask<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public override void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None) =>
        throw new NotImplementedException();

    public override Task SerializeAsync<T>(T data, Stream stream,
        SerializationFormatting formatting = SerializationFormatting.None, CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();
}
```

Registering up the serializer is performed in the `ConnectionSettings` constructor.

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;

var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new VanillaSerializer()); <1>
var client = new ElasticsearchClient(settings);
```

1. If implementing `Serializer` is enough, why must we provide an instance wrapped in a factory `Func`?


There are various cases where you might have a POCO type that contains an `Elastic.Clients.Elasticsearch` type as one of its properties. The `SourceSerializerFactory` delegate provides access to the default built-in serializer so you can access it when necessary. For example, consider if you want to use percolation; you need to store {{es}} queries as part of the `_source` of your document, which means you need to have a POCO that looks like this.

```csharp
using Elastic.Clients.Elasticsearch.QueryDsl;

public class MyPercolationDocument
{
    public Query Query { get; set; }
    public string Category { get; set; }
}
```

A custom serializer would not know how to serialize `Query` or other `Elastic.Clients.Elasticsearch` types that could appear as part of the `_source` of a document. Therefore, your custom `Serializer` would need to store a reference to our built-in serializer and delegate serialization of Elastic types back to it.


