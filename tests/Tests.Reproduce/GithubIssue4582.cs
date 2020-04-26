using System.Linq;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Client;

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
			var client = TestClient.FixedInMemoryClient(bytes);
			var response = client.Search<object>();

			var filters = response.Aggregations
				.Filters("some_agg")
				.Select(x => x.Key)
				.ToList();

			filters[2].Should().Be("value3#something else");
		}
	}
}
