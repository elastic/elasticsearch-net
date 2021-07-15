// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GitHubIssue5892
	{
		private static readonly byte[] ResponseBytes = Encoding.UTF8.GetBytes(@"{
    ""took"": 2,
    ""timed_out"": false,
    ""_shards"": {
        ""total"": 1,
        ""successful"": 1,
        ""skipped"": 0,
        ""failed"": 0
    },
    ""hits"": {
        ""max_score"": null,
        ""hits"": []
    }
}");

		[U] public void SearchResponseTotalShouldNotThrowWhenTrackTotalHitsIsFalse()
		{
			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(ResponseBytes));
			var client = new ElasticClient(settings);

			var response = client.Search<Project>(s => s.Index("test").MatchAll());

			response.Total.Should().Be(-1);
		}
	}
}
