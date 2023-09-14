// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// **********************************
// IMPORTANT: These tests have a secondary use as code snippets used in documentation.
// We disable formatting in sections of this file to ensure the correct indentation when tagged regions are
// included in the asciidocs. While hard to read, this formatting should be left as-is for docs generation.
// We also include using directives that are not required due to global using directives, but remain here
// so that can appear in the documentation.
// **********************************

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
#pragma warning disable IDE0005 // Remove unnecessary using directives
//tag::usings[]
//tag::converter-usings[]
using System;
using System.Text.Json;
//tag::usings-serialization[]
using System.Text.Json.Serialization;
//end::usings-serialization[]
//end::converter-usings[]
using System.Threading.Tasks;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
//end::usings[]
//tag::derived-converter-usings[]
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Serialization;
//end::derived-converter-usings[]
//tag::vanilla-serializer-using-directives[]
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
//end::vanilla-serializer-using-directives[]
using System.Text;
using VerifyXunit;
using System.IO;
using System.Threading;
//tag::querydsl-using-directives[]
using Elastic.Clients.Elasticsearch.QueryDsl;
//end::querydsl-using-directives[]
#pragma warning restore IDE0005 // Remove unnecessary using directives
#pragma warning restore CS0105 // Using directive appeared previously in this namespace

namespace Tests.Documentation.Serialization;

[UsesVerify]
public class CustomSerializationTests : DocumentationTestBase
{
    [U]
    public async Task CustomizingJsonSerializerOptions()
    {
        // This example resets the PropertyNamingPolicy, such that the existing C# Pascal case is sent in the JSON.

#pragma warning disable format
//tag::custom-options-local-function[]
static void ConfigureOptions(JsonSerializerOptions o) => // <1>
    o.PropertyNamingPolicy = null; 
//end::custom-options-local-function[]

//tag::create-client[]
var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new DefaultSourceSerializer(settings, ConfigureOptions)); // <2>
var client = new ElasticsearchClient(settings);
//end::create-client[]
#pragma warning restore format

        // Needed for the test assertion as we should use the in memory connection and disable direct streaming.
        // We don't want to include those in the docs as it may mislead or confuse developers.
        // Any changes to the documentation code needs to be applied here also.
        settings = new ElasticsearchClientSettings(
            nodePool,
            new InMemoryTransportClient(),
            sourceSerializer: (defaultSerializer, settings) =>
                new DefaultSourceSerializer(settings, ConfigureOptions))
            .DisableDirectStreaming();
        client = new ElasticsearchClient(settings);

#pragma warning disable format
//tag::index-person[]
var person = new Person { FirstName = "Steve" };
var indexResponse = await client.IndexAsync(person, "my-index-name");
//end::index-person[]
#pragma warning restore format

        var requestJson = Encoding.UTF8.GetString(indexResponse.ApiCallDetails.RequestBodyInBytes);
        await Verifier.Verify(requestJson);

        var ms = new MemoryStream(indexResponse.ApiCallDetails.RequestBodyInBytes);
        var deserializedPerson = client.SourceSerializer.Deserialize<Person>(ms);
        deserializedPerson.FirstName.Should().Be("Steve");

// Alternative example using an Action
//tag::custom-options-action[]
Action<JsonSerializerOptions> configureOptions = o => o.PropertyNamingPolicy = null;
//end::custom-options-action[]
    }

#pragma warning disable format
//tag::person-class[]
public class Person
{
    public string FirstName { get; set; }
}
//end::person-class[]
#pragma warning restore format
}

[UsesVerify]
public class CustomSerializationTests_WithAttributes : DocumentationTestBase
{
    [U]
    public async Task UsingSystemTextJsonAttributes()
    {
        // This example uses a STJ attribute on the property to provide a specific name to use in the JSON.

#pragma warning disable format
//tag::index-person-with-attributes[]
var person = new Person { FirstName = "Steve", Age = 35 };
var indexResponse = await Client.IndexAsync(person, "my-index-name");
//end::index-person-with-attributes[]
#pragma warning restore format

        var requestJson = Encoding.UTF8.GetString(indexResponse.ApiCallDetails.RequestBodyInBytes);
        await Verifier.Verify(requestJson);
    }

    [U]
    public async Task UsingSystemTextJsonConverterAttributes()
    {
#pragma warning disable format
//tag::index-customer-with-converter[]
var customer = new Customer 
{ 
    CustomerName = "Customer Ltd", 
    CustomerType = CustomerType.Enhanced 
};
var indexResponse = await Client.IndexAsync(customer, "my-index-name");
//end::index-customer-with-converter[]
#pragma warning restore format

        var requestJson = Encoding.UTF8.GetString(indexResponse.ApiCallDetails.RequestBodyInBytes);
        await Verifier.Verify(requestJson);

        var ms = new MemoryStream(indexResponse.ApiCallDetails.RequestBodyInBytes);
        var deserializedCustomer = Client.SourceSerializer.Deserialize<Customer>(ms);
        deserializedCustomer.CustomerName.Should().Be("Customer Ltd");
        deserializedCustomer.CustomerType.Should().Be(CustomerType.Enhanced);
    }

#pragma warning disable format
//tag::person-class-with-attributes[]
public class Person
{
    [JsonPropertyName("forename")] // <1>
    public string FirstName { get; set; }

    [JsonIgnore] // <2>
    public int Age { get; set; }
}
//end::person-class-with-attributes[]
#pragma warning restore format

#pragma warning disable format
//tag::customer-with-jsonconverter-attribute[]
[JsonConverter(typeof(CustomerConverter))] // <1>
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
//end::customer-with-jsonconverter-attribute[]
#pragma warning restore format

#pragma warning disable format
//tag::customer-converter[]
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

                if (reader.ValueTextEquals("isStandard")) // <1>
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

        if (value.CustomerType == CustomerType.Standard) // <2>
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
//end::customer-converter[]
#pragma warning restore format

    private class CustomerTypeConverter : JsonConverter<CustomerType>
    {
        public override CustomerType Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "basic" => CustomerType.Standard,
                "premium" => CustomerType.Enhanced,
                _ => throw new JsonException(
                    $"Unknown value read when deserializing {nameof(CustomerType)}."),
            };
        }

        public override void Write(Utf8JsonWriter writer, 
            CustomerType value, JsonSerializerOptions options)
        {
            switch (value)
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
}

[UsesVerify]
public class CustomSerializationTestsEnumAttribute : DocumentationTestBase
{
    [U]
    public async Task DerivingFromSystemTextJsonSerializer_ToRegisterACustomEnumConverter_BeforeBuiltInConverters()
    {
        var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
        var settings = new ElasticsearchClientSettings(
            nodePool,
            sourceSerializer: (defaultSerializer, settings) =>
                new MyCustomSerializer(settings)); // <1>
        var client = new ElasticsearchClient(settings);

        // Needed for the test assertion as we should use the in memory connection and disable direct streaming.
        // We don't want to include those in the docs as it may mislead or confuse developers.
        // Any changes to the documentation code needs to be applied here also.
        settings = new ElasticsearchClientSettings(
            nodePool,
            new InMemoryTransportClient(),
            sourceSerializer: (defaultSerializer, settings) =>
                new MyCustomSerializer(settings))
            .DisableDirectStreaming();
        client = new ElasticsearchClient(settings);

#pragma warning disable format
//tag::index-customer-without-jsonconverter-attribute[]
var customer = new Customer
{
    CustomerName = "Customer Ltd",
    CustomerType = CustomerType.Enhanced
};

var indexResponse = await client.IndexAsync(customer, "my-index-name");
//end::index-customer-without-jsonconverter-attribute[]
#pragma warning restore format

        var requestJson = Encoding.UTF8.GetString(indexResponse.ApiCallDetails.RequestBodyInBytes);
        await Verifier.Verify(requestJson);

        var ms = new MemoryStream(indexResponse.ApiCallDetails.RequestBodyInBytes);
        var deserializedCustomer = client.SourceSerializer.Deserialize<Customer>(ms);
        deserializedCustomer.CustomerName.Should().Be("Customer Ltd");
        deserializedCustomer.CustomerType.Should().Be(CustomerType.Enhanced);
    }

#pragma warning disable format
//tag::customer-without-jsonconverter-attribute[]
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
//end::customer-without-jsonconverter-attribute[]
#pragma warning restore format

#pragma warning disable format
//tag::my-custom-serializer[]
public class MyCustomSerializer : SystemTextJsonSerializer // <1>
{
    private readonly JsonSerializerOptions _options;

    public MyCustomSerializer(IElasticsearchClientSettings settings) : base(settings)
    {
        var options = DefaultSourceSerializer.CreateDefaultJsonSerializerOptions(false); // <2>

        options.Converters.Add(new CustomerTypeConverter()); // <3>

        _options = DefaultSourceSerializer.AddDefaultConverters(options); // <4>
    }

    protected override JsonSerializerOptions CreateJsonSerializerOptions() => _options; // <5>
}
//end::my-custom-serializer[]
#pragma warning restore format

#pragma warning disable format
//tag::customer-type-converter[]
public class CustomerTypeConverter : JsonConverter<CustomerType>
{
    public override CustomerType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() switch // <1>
        {
            "basic" => CustomerType.Standard,
            "premium" => CustomerType.Enhanced,
            _ => throw new JsonException(
                $"Unknown value read when deserializing {nameof(CustomerType)}."),
        };
    }

    public override void Write(Utf8JsonWriter writer, CustomerType value, JsonSerializerOptions options)
    {
        switch (value) // <2>
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
//end::customer-type-converter[]
#pragma warning restore format

public void RegisterVanillaSerializer()
{
#pragma warning disable format
//tag::register-vanilla-serializer[]
var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
var settings = new ElasticsearchClientSettings(
    nodePool,
    sourceSerializer: (defaultSerializer, settings) =>
        new VanillaSerializer()); // <1>
var client = new ElasticsearchClient(settings);
//end::register-vanilla-serializer[]
#pragma warning restore format
}

#pragma warning disable format
//tag::vanilla-serializer[]
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
//end::vanilla-serializer[]
#pragma warning restore format

#pragma warning disable format
//tag::percolation-document[]
public class MyPercolationDocument
{
    public Query Query { get; set; }
    public string Category { get; set; }
}
//end::percolation-document[]
#pragma warning restore format
}
