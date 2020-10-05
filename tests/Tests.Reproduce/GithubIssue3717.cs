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
	public class GithubIssue3717
	{
		[U]
		public void CanDeserializeEmptyAggregation()
		{
			var json = @"{
  ""took"" : 2,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 3,
    ""successful"" : 3,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : {
      ""value"" : 10000,
      ""relation"" : ""gte""
    },
    ""max_score"" : null,
    ""hits"" : [ ]
  },
  ""aggregations"" : {
    ""geo_bounds#viewport"" : { }
  }
}";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);

			Func<ISearchResponse<TestEntity>> response = () => client.Search<TestEntity>(s => s
				.Size(0)
				.Index("issue")
				.Aggregations(q => q
					.GeoBounds("viewport", t => t
						.Field(f => f.TestProperty)))
			);

			response.Should().NotThrow();
		}

		public class TestEntity {
			public string TestProperty { get; set; }
		}
	}
}
