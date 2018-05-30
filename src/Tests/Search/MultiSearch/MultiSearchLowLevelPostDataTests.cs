using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Search.MultiSearch
{
	public class MultiSearchLowLevelPostDataTests : IClusterFixture<ReadOnlyCluster>, IClassFixture<EndpointUsage>
	{
		private readonly IElasticClient _client;

		public MultiSearchLowLevelPostDataTests(ReadOnlyCluster cluster, EndpointUsage usage)
		{
			_client = cluster.Client;
		}

		protected static List<object> Search => new object[]
		{
			new {},
			new { from = 0, size = 10, query = new { match_all = new {} } },
			new { search_type = "query_then_fetch" },
			new {},
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } },
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } }
		}.ToList();


		[I] public void PostEnumerableOfObjects()
		{
			var response = this._client.LowLevel.Msearch<DynamicResponse>("project", "project", PostData.MultiJson(Search));
			AssertResponse(response);
		}

		[I] public void PostEnumerableOfStrings()
		{
			var listOfStrings = Search
				.Select(s => this._client.RequestResponseSerializer.SerializeToString(s, SerializationFormatting.None))
				.ToList();

			var response = this._client.LowLevel.Msearch<DynamicResponse>("project", "project", PostData.MultiJson(listOfStrings));
			AssertResponse(response);
		}

		[I] public void PostString()
		{
			var str = Search
				.Select(s => this._client.RequestResponseSerializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var response = this._client.LowLevel.Msearch<DynamicResponse>("project", "project", str);
			AssertResponse(response);
		}

		[I] public void PostByteArray()
		{
			var str = Search
				.Select(s => this._client.RequestResponseSerializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var bytes = Encoding.UTF8.GetBytes(str);

			var response = this._client.LowLevel.Msearch<DynamicResponse>("project", "project", bytes);
			AssertResponse(response);
		}

		private static void AssertResponse(DynamicResponse response)
		{
			response.Success.Should().BeTrue();
			object o = response.Body;
			o.Should().NotBeNull();

			var b = response.Body;
			object[] responses = b.responses;
			responses.Count().Should().Be(4);

			object r = b.responses[0];
			r.Should().NotBeNull();

			object shards = b.responses[0]._shards;
			shards.Should().NotBeNull();

			int totalShards = b.responses[0]._shards.total;
			totalShards.Should().BeGreaterThan(0);
//			JArray responses = r.responses;
//
//			responses.Count().Should().Be(4);

		}
	}
}
