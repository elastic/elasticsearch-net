// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace AOT;

public static class Program
{
	public static void Main(string[] args)
	{
		var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));
		var settings = new ElasticsearchClientSettings(
			nodePool,
			sourceSerializer: (_, settings) =>
				new DefaultSourceSerializer(settings, UserTypeSerializerContext.Default)
			)
			.DefaultMappingFor<Person>(x => x.IndexName("index"));

		var client = new ElasticsearchClient(settings);

		var person = new Person
		{
			Id = 1234,
			FirstName = "Florian",
			LastName = "Bernd"
		};

		Trace.Assert(client.Infer.Id(person) == "1234");

		var indexRequest = new IndexRequest<Person>(person);
		var indexRequestBody = client.ElasticsearchClientSettings.RequestResponseSerializer.SerializeToString(indexRequest);
		var indexRequest2 = client.ElasticsearchClientSettings.RequestResponseSerializer.Deserialize<IndexRequest<Person>>(indexRequestBody)!;

		Trace.Assert(indexRequest.Document == indexRequest2.Document);
	}
}

internal sealed record Person
{
	public long? Id { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
	public DateTimeOffset? BirthDate { get; init; }
}

[JsonSerializable(typeof(Person), GenerationMode = JsonSourceGenerationMode.Default)]
internal sealed partial class UserTypeSerializerContext :
	JsonSerializerContext
{
}
