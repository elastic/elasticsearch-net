using Elastic.Xunit.XunitPlumbing;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Examples.QueryDsl
{
	public class QueryFilterContextPage : ExampleBase
	{
		[U]
		public void Line62()
		{
			// tag::f29a28fffa7ec604a33a838f48f7ea79[]
			var searchResponse = client.Search<object>(s => s
				.Index("")
				.Query(q => q
					.Match(m => m.Field("title").Query("Search")) && q
					.Match(m => m.Field("content").Query("Elasticsearch")) && +q
					.Term(t => t.Field("status").Value("published")) && +q
					.DateRange(d => d.Field("publish_date").GreaterThanOrEquals("2015-01-01"))
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
			}", example =>
			{
				// client does not support the short form of match and term queries. Expand them to longer form
				var body = JObject.Parse(example.Body);
				body["query"]["bool"]["must"][0]["match"]["title"] = new JObject{{ "query", "Search" }};
				body["query"]["bool"]["must"][1]["match"]["content"] = new JObject{{ "query", "Elasticsearch" }};
				body["query"]["bool"]["filter"][0]["term"]["status"] = new JObject{{ "value", "published" }};
				example.Body = body.ToString();
				return example;
			});
		}
	}
}
