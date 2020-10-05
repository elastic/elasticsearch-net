// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4382
	{
		[U]
		public void CanDeserializeVerifyRepositoryResponse()
		{
			var json = @"{""nodes"":{""1cWG9trDRi--6I-46lOlBw"":{""name"":""DESKTOP-5L01F6I""}}}";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Snapshot.VerifyRepository("repository");
			response.Nodes.Should().NotBeNullOrEmpty();
			response.Nodes["1cWG9trDRi--6I-46lOlBw"].Name.Should().Be("DESKTOP-5L01F6I");
		}
	}
}
