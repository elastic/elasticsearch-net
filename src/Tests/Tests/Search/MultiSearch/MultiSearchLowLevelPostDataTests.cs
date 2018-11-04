using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Search.MultiSearch
{
	public class MultiSearchLowLevelPostDataTests : IClusterFixture<ReadOnlyCluster>, IClassFixture<EndpointUsage>
	{
		private readonly IElasticClient _client;

		public MultiSearchLowLevelPostDataTests(ReadOnlyCluster cluster, EndpointUsage usage) => _client = cluster.Client;

		protected List<object> Search => new object[]
		{
			new { },
			new { from = 0, size = 10, query = new { match_all = new { } } },
			new { search_type = "query_then_fetch" },
			new { },
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new { } } },
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new { } } }
		}.ToList();


		[I] public void PostEnumerableOfObjects()
		{
			var response = _client.LowLevel.Msearch<dynamic>("project", "project", Search);
			AssertResponse(response);
			response = _client.LowLevel.Msearch<dynamic>("project", "project", (object)Search);
			AssertResponse(response);
		}

		[I] public void PostEnumerableOfStrings()
		{
			var listOfStrings = Search
				.Select(s => _client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList();

			var response = _client.LowLevel.Msearch<dynamic>("project", "project", listOfStrings);
			AssertResponse(response);
			response = _client.LowLevel.Msearch<dynamic>("project", "project", (object)listOfStrings);
			AssertResponse(response);
		}

		[I] public void PostString()
		{
			var str = Search
				.Select(s => _client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var response = _client.LowLevel.Msearch<dynamic>("project", "project", str);
			AssertResponse(response);
			response = _client.LowLevel.Msearch<dynamic>("project", "project", (object)str);
			AssertResponse(response);
		}

		[I] public void PostByteArray()
		{
			var str = Search
				.Select(s => _client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var bytes = Encoding.UTF8.GetBytes(str);

			var response = _client.LowLevel.Msearch<dynamic>("project", "project", bytes);
			AssertResponse(response);
			response = _client.LowLevel.Msearch<dynamic>("project", "project", (object)bytes);
			AssertResponse(response);
		}

		public void AssertResponse(ElasticsearchResponse<dynamic> response)
		{
			response.Success.Should().BeTrue();

			var r = response.Body;

			JArray responses = r.responses;

			responses.Count().Should().Be(4);
		}
	}
}
