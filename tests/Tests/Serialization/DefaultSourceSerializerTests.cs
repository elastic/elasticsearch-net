// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Serialization;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class DefaultSourceSerializerTests : SerializerTestBase
{
	private readonly ElasticsearchClient _client = new(
		new ElasticsearchClientSettings(new InMemoryTransportClient())
			.DefaultMappingFor<MyDocument>(m => m.IndexName("index"))
			.DefaultMappingFor<MyChild>(m => m.IndexName("index"))
			.DefaultMappingFor<MyParent>(m => m.IndexName("index").RelationName("my_parent"))
			.DisableDirectStreaming());

	[U]
	public async Task SourceSerialization_WithBuiltInDefaultSourceSerializer_CorrectlySerializes_TypesUsingAJoinField()
	{
		var parent = new MyParent
		{
			Id = 1,
			ParentProperty = "A parent property",
			MyJoinField = JoinField.Root<MyParent>()
		};

		var child = new MyChild
		{
			Id = 2,
			MyJoinField = JoinField.Link<MyChild, MyParent>(parent)
		};

		var parentResponse = _client.Index(parent);
		var childResponse = _client.Index(child);

		var parentRequestJson = Encoding.UTF8.GetString(parentResponse.ApiCallDetails.RequestBodyInBytes);
		var childRequestJson = Encoding.UTF8.GetString(childResponse.ApiCallDetails.RequestBodyInBytes);

		var requests = parentRequestJson + Environment.NewLine + childRequestJson;

		await Verifier.Verify(requests);

		var ms = new MemoryStream(parentResponse.ApiCallDetails.RequestBodyInBytes);
		var deserializedParent = _client.SourceSerializer.Deserialize<MyParent>(ms);
		deserializedParent.Id.Should().Be(1);
		deserializedParent.ParentProperty.Should().Be("A parent property");
		deserializedParent.MyJoinField.Match(p => p.Name.Should().Be("my_parent"), c => c.Should().BeNull());

		ms = new MemoryStream(childResponse.ApiCallDetails.RequestBodyInBytes);
		var deserializedChild = _client.SourceSerializer.Deserialize<MyChild>(ms);
		deserializedChild.Id.Should().Be(2);
		deserializedChild.MyJoinField.Match(p => p.Should().BeNull(), c => c.Name.Should().Be("mychild"));
	}
	
	[U]
	public async Task SourceSerialization_WithBuiltInDefaultSourceSerializer_CorrectlySerializes_TypesUsingQuery()
	{
		var response = _client.Index(new MyQuery { Query = Query.MatchAll(new() { Boost = 2.1f }) }, "test-index");

		var requestJson = Encoding.UTF8.GetString(response.ApiCallDetails.RequestBodyInBytes);

		await Verifier.Verify(requestJson);
	}

	[U]
	public async Task SourceSerialization_WithCustomSerializer_CorrectlySerializes_TypesUsingQuery()
	{
		var nodePool = new SingleNodePool(new Uri("http://localhost:9200"));

		var settings = new ElasticsearchClientSettings(
			nodePool,
			new InMemoryTransportClient(),
			sourceSerializer: (defaultSerializer, settings) =>
				new MyCustomSerializer(settings))
			.DisableDirectStreaming()
			.DefaultMappingFor<MyDocument>(m => m.IndexName("index"))
			.DefaultMappingFor<MyChild>(m => m.IndexName("index"))
			.DefaultMappingFor<MyParent>(m => m.IndexName("index").RelationName("my_parent"));

		var client = new ElasticsearchClient(settings);

		var parent = new MyParent
		{
			Id = 1,
			ParentProperty = "A parent property",
			MyJoinField = JoinField.Root<MyParent>()
		};

		var child = new MyChild
		{
			Id = 2,
			MyJoinField = JoinField.Link<MyChild, MyParent>(parent)
		};

		var parentResponse = client.Index(parent);
		var childResponse = client.Index(child);

		var parentRequestJson = Encoding.UTF8.GetString(parentResponse.ApiCallDetails.RequestBodyInBytes);
		var childRequestJson = Encoding.UTF8.GetString(childResponse.ApiCallDetails.RequestBodyInBytes);

		var requests = parentRequestJson + Environment.NewLine + childRequestJson;

		await Verifier.Verify(requests);

		var ms = new MemoryStream(parentResponse.ApiCallDetails.RequestBodyInBytes);
		var deserializedParent = client.SourceSerializer.Deserialize<MyParent>(ms);
		deserializedParent.Id.Should().Be(1);
		deserializedParent.ParentProperty.Should().Be("A parent property");
		deserializedParent.MyJoinField.Match(p => p.Name.Should().Be("my_parent"), c => c.Should().BeNull());

		ms = new MemoryStream(childResponse.ApiCallDetails.RequestBodyInBytes);
		var deserializedChild = client.SourceSerializer.Deserialize<MyChild>(ms);
		deserializedChild.Id.Should().Be(2);
		deserializedChild.MyJoinField.Match(p => p.Should().BeNull(), c => c.Name.Should().Be("mychild"));
	}

	private abstract class MyDocument
	{
		public int Id { get; set; }
		public JoinField MyJoinField { get; set; }
	}

	private class MyParent : MyDocument
	{
		public string ParentProperty { get; set; }
	}

	private class MyChild : MyDocument
	{
		public string ChildProperty { get; set; }
	}

	private class MyQuery
	{
		public Query Query { get; set; }
	}

	private class MyCustomSerializer : SystemTextJsonSerializer
	{
		private readonly JsonSerializerOptions _options;

		public MyCustomSerializer(IElasticsearchClientSettings settings) : base(settings)
		{
			var options = DefaultSourceSerializer.CreateDefaultJsonSerializerOptions(false);

			// At this point, a custom converter could be added, before our default ones. That's not what we're testing here.

			// This is necessary for custom serializers if they want to support serializing our types, such as Query.
			_options = DefaultSourceSerializer.AddDefaultConverters(options);
		}

		protected override JsonSerializerOptions CreateJsonSerializerOptions() => _options;
	}
}
