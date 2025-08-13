// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Playground;

var pool = new SingleNodePool(new Uri("https://primary.es.europe-west3.gcp.cloud.es.io"));
var settings = new ElasticsearchClientSettings(pool,
		sourceSerializer: (_, settings) =>
			new DefaultSourceSerializer(settings, PlaygroundJsonSerializerContext.Default)
		)
	.Authentication(new BasicAuthentication("elastic", "Oov35Wtxj5DzpZNzYAzFb0KZ"))
	.DisableDirectStreaming()
	.EnableDebugMode(cd =>
	{
		//var request = System.Text.Encoding.Default.GetString(cd.RequestBodyInBytes);
		Console.WriteLine(cd.DebugInformation);
	});

var client = new ElasticsearchClient(settings);

var person = new Person
{
	FirstName = "Steve",
	LastName = "Jobs",
	Age = 35,
	IsDeleted = false,
	Routing = "1234567890",
	Id = 1,
	Enum = DateTimeKind.Utc,
};

var id = client.Infer.Id(person);
var idByType = IdByType(person.GetType(), person);
Console.WriteLine(id);
Console.WriteLine(idByType);
// This still errors on AOT compilation
Console.WriteLine(client.SourceSerializer.SerializeToString(person));

[UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "Can only annotate our implementation")]
[UnconditionalSuppressMessage("Trimming", "IL2067", Justification = "Can only annotate our implementation")]
string? IdByType(Type type, object instance) => client.Infer.Id(type, instance);
