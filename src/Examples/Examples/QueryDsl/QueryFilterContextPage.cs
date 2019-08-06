using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class QueryFilterContextPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::f29a28fffa7ec604a33a838f48f7ea79[]
			var response0 = new SearchResponse<object>();
			// end::f29a28fffa7ec604a33a838f48f7ea79[]

			response0.MatchesExample(@"GET /_search
			{
			  ""query"": { \<1>
			    ""bool"": { \<2>
			      ""must"": [
			        { ""match"": { ""title"":   ""Search""        }},
			        { ""match"": { ""content"": ""Elasticsearch"" }}
			      ],
			      ""filter"": [ \<3>
			        { ""term"":  { ""status"": ""published"" }},
			        { ""range"": { ""publish_date"": { ""gte"": ""2015-01-01"" }}}
			      ]
			    }
			  }
			}");
		}
	}
}