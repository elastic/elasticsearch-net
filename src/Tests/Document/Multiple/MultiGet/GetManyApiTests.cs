using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.MultiGet
{
	public class GetManyApiTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;
		private readonly IEnumerable<long> _ids = Developer.Developers.Select(d => (long)d.Id).Take(10);
		private readonly IElasticClient _client;

		public GetManyApiTests(ReadOnlyCluster cluster)
		{
			_cluster = cluster;
			_client = _cluster.Client;
		}

		[I] public void UsesDefaultIndexAndInferredType()
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

		[I] public async Task UsesDefaultIndexAndInferredTypeAsync()
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

		[I] public async Task CanHandleNotFoundResponses()
		{
			var response = await _client.GetManyAsync<Developer>(_ids.Select(i=>i*100));
			response.Count().Should().Be(10);
			foreach (var hit in response)
			{
				hit.Index.Should().NotBeNullOrWhiteSpace();
				hit.Type.Should().NotBeNullOrWhiteSpace();
				hit.Id.Should().NotBeNullOrWhiteSpace();
				hit.Found.Should().BeFalse();
			}
		}
		[I] public void ThrowsExceptionOnConnectionError()
		{
			if (TestClient.RunningFiddler) return; //fiddler meddles here

			var client = new ElasticClient(TestClient.CreateSettings(port: 9500));
			Action response = () => client.GetMany<Developer>(_ids.Select(i=>i*100));
			response.ShouldThrow<ElasticsearchClientException>();
		}
	}
}
