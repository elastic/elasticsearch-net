using System.IO;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{

	[TestFixture]
	public class Reproduce860Tests : BaseJsonTests
	{
		[Test]
		public void ConvenienceExtensionShouldGenerateExactBodyAsMethodOnClient()
		{
			var connection = new InMemoryConnection(this._settings) { RecordRequests = true };
			var client = new ElasticClient(this._settings, connection);

			var response = client.GetMany<ElasticsearchProject>(new[] { "123" });
            var response2 = client.MultiGet(s => s.GetMany<ElasticsearchProject>(new[] { "123" }));
			
			connection.Requests.Should().NotBeEmpty().And.HaveCount(2);

			var getManyRequest = connection.Requests[0];
			var multiGetRequest = connection.Requests[1];

			getManyRequest.Item3.Should().NotBeNull()
				.And.BeEquivalentTo(multiGetRequest.Item3);

			getManyRequest.Item3.Utf8String().Should().Contain("_index\":");
			getManyRequest.Item3.Utf8String().Should().Contain("_type\":");
				

		}
	}
}
