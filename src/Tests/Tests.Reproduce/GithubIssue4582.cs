using System;
using System.Linq;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue4582
	{
		[U]
		public void DeserializeBucketKeyWithHash()
		{
			var json = @"
{
  ""hits"": {
  },
  ""aggregations"":
  {
    ""some_agg"" : {
      ""buckets"" : {
        ""value1"" : {
          ""doc_count"" : 0
        },
        ""value2"" : {
          ""doc_count"" : 0
        },
        ""value3#something else"" : {
          ""doc_count"" : 0
        }
      }
    }
  }
}
";

			var bytes = Encoding.UTF8.GetBytes(json);
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes));
			var client = new ElasticClient(connectionSettings);
			var response = client.Search<object>(s => s.Index("index"));

			var filters = response.Aggregations
				.Filters("some_agg")
				.Select(x => x.Key)
				.ToList();

			filters[2].Should().Be("value3#something else");
		}
	}
}
