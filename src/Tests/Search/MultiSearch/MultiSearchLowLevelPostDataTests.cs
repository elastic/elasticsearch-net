using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.MultiSearch
{
	[Collection(IntegrationContext.ReadOnly)]
	public class MultiSearchLowLevelPostDataTests
	{
		private IElasticClient _client;

		public MultiSearchLowLevelPostDataTests(ReadOnlyCluster cluster, EndpointUsage usage)
		{
			_client = cluster.Client();
		}

		protected List<object> Search => new object[]
		{
			new {},
			new { from = 0, size = 10, query = new { match_all = new {} } },
			new { search_type = "query_and_fetch" },
			new {},
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } },
			new { index = "devs", type = "developer" },
			new { from = 0, size = 5, query = new { match_all = new {} } }
		}.ToList();


		[I] public void PostEnumerableOfObjects()
		{
			var response = this._client.LowLevel.Msearch<dynamic>("project", "project", this.Search);
			AssertResponse(response);
			response = this._client.LowLevel.Msearch<dynamic>("project", "project", (object)this.Search);
			AssertResponse(response);
		}

		[I] public void PostEnumerableOfStrings()
		{
			var listOfStrings = Search
				.Select(s => this._client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList();

			var response = this._client.LowLevel.Msearch<dynamic>("project", "project", listOfStrings);
			AssertResponse(response);
			response = this._client.LowLevel.Msearch<dynamic>("project", "project", (object)listOfStrings);
			AssertResponse(response);
		}

		[I] public void PostString()
		{
			var str = Search
				.Select(s => this._client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var response = this._client.LowLevel.Msearch<dynamic>("project", "project", str);
			AssertResponse(response);
			response = this._client.LowLevel.Msearch<dynamic>("project", "project", (object)str);
			AssertResponse(response);
		}

		[I] public void PostByteArray()
		{
			var str = Search
				.Select(s => this._client.Serializer.SerializeToString(s, SerializationFormatting.None))
				.ToList()
				.Aggregate(new StringBuilder(), (sb, s) => sb.Append(s + "\n"), sb => sb.ToString());

			var bytes = Encoding.UTF8.GetBytes(str);

			var response = this._client.LowLevel.Msearch<dynamic>("project", "project", bytes);
			AssertResponse(response);
			response = this._client.LowLevel.Msearch<dynamic>("project", "project", (object)bytes);
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
