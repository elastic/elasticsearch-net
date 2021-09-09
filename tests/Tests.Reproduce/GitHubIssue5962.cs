// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class GitHubIssue5962
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
        ""total"": {
            ""value"": 6,
            ""relation"": ""eq""
        },
        ""max_score"": null,
        ""hits"": []
    },
    ""aggregations"": {
        ""multi_terms#multi-terms"": {
            ""doc_count_error_upper_bound"": 0,
            ""sum_other_doc_count"": 0,
            ""buckets"": [
                {
                    ""key"": [
                        ""A title 1"",
                        true
                    ],
                    ""key_as_string"": ""A title 1|true"",
                    ""doc_count"": 3
                },
                {
                    ""key"": [
                        ""A title 1"",
                        false
                    ],
                    ""key_as_string"": ""A title 1|false"",
                    ""doc_count"": 1
                }
            ]
        }
    }
}");

		[U] public void MultiTermsShouldHandleBooleanValues()
		{
			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(ResponseBytes));
			var client = new ElasticClient(settings);

			var response = client.Search<TestData>(s => s
				.Size(0)
				.Index("test")
				.Aggregations(a => a
					.MultiTerms("multi-terms", mt => mt.Terms(t1 => t1.Field("title.keyword"), t2 => t2.Field(f2 => f2.IsEnabled)))));

			response.Aggregations.MultiTerms("multi-terms").Buckets.Should().HaveCount(2);
		}

		private class TestData
		{
			public bool IsEnabled { get; set; }
			public string SubTitle { get; set; }
			public string Title { get; set; }
		}
	}
}
