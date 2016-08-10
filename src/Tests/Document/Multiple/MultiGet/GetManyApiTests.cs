using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Tests.Document.Multiple.MultiGet
{
	[Collection(TypeOfCluster.ReadOnly)]
	public class GetManyApiTests
	{
		private readonly ReadOnlyCluster _cluster;
		private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => (long)d.Id).Take(10);
		private readonly IElasticClient _client;

		public GetManyApiTests(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client();
		}

		[I]
		public void UsesDefaultIndexAndInferredType()
		{
			var response = _client.GetMany<Developer>(_ids);
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		}

		[I]
		public async Task UsesDefaultIndexAndInferredTypeAsync()
		{
			var response = await _client.GetManyAsync<Developer>(_ids);
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeTrue();
			}
		}

		[U]
		public void UsesCustomSerializationSettings()
		{
			const string expectedDateString = "2015-02-06T23:45:05Z";
			var jsonResponse = $@"{{ ""docs"": [ {{ ""_id"": ""1"", ""_source"": {{ ""dateString"": ""{expectedDateString}"" }}}}]}}";

			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(jsonResponse));
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, connection, settings => new LocalJsonNetSerializer(settings)).DefaultIndex("default-index");
			var client = new ElasticClient(connectionSettings);

			var hit = client.GetMany<HasDateString>(new[] { "1" })?.FirstOrDefault();
			hit.Should().NotBeNull();
			hit.Source.Should().NotBeNull();
			hit.Source.DateString.Should().Be(expectedDateString);
		}

		private sealed class HasDateString
		{
			public string DateString { get; set; }
		}

		private sealed class LocalJsonNetSerializer : JsonNetSerializer
		{
			public LocalJsonNetSerializer(IConnectionSettingsValues settings) : base(settings) { }

			protected override void ModifyJsonSerializerSettings(JsonSerializerSettings settings)
			{
				settings.DateParseHandling = DateParseHandling.None;
			}
		}
	}
}
