using System;
using System.Globalization;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using FluentAssertions.Common;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.Serialization;
using Tests.Domain;

namespace Tests.Reproduce
{
	public class Discuss179634
	{
		[U]
		public void SerializeCompletionSuggesterFieldsCorrectlyWhenDefaultFieldNameInferrerUsed()
		{
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(connectionPool, new InMemoryConnection())
				.DefaultFieldNameInferrer(p => p.ToUpper(CultureInfo.CurrentCulture))
				.DisableDirectStreaming();

			var tester = new SerializationTester(new ElasticClient(settings));

			var suggest = new SearchDescriptor<Project>()
				.Suggest(ss => ss
					.Completion("title", cs => cs
						.Field(f => f.Suggest)
						.Prefix("keyword")
						.Fuzzy(f => f
							.Fuzziness(Fuzziness.Auto)
						)
						.Size(5)
					)
				);

			var expected = @"{
  ""suggest"": {
    ""title"": {
      ""completion"": {
        ""field"": ""SUGGEST"",
        ""size"": 5,
        ""fuzzy"": {
          ""fuzziness"": ""AUTO""
        }
      },
      ""prefix"": ""keyword""
    }
  }
}";

			var result = tester.Serializes(suggest, expected);
			result.Success.Should().Be(true, result.DiffFromExpected);
		}
	}
}
