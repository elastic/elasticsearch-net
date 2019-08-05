using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class QueryFilterContextPage : ExampleBase
	{
		[U]
		[SkipExample] // match query long form in client
		public void Line62()
		{
			// tag::f29a28fffa7ec604a33a838f48f7ea79[]
			var searchResponse = client.Search<object>(s => s
				.Index("").TypedKeys(false)
				.Query(q => q
					.Match(m => m.Field("title").Query("Search")) && q
					.Match(m => m.Field("content").Query("Elasticsearch")) && +q
					.Term(t => t.Field("status").Value("published")) && +q
					.DateRange(d => d.Field("publish_date").GreaterThan("2015-01-01"))
				)
			);
			// end::f29a28fffa7ec604a33a838f48f7ea79[]

			searchResponse.MatchesExample(@"GET /_search
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
