// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class TermsQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/terms-query.asciidoc:19")]
		public void Line19()
		{
			// tag::0c4ad860a485fe53d8140ad3ccd11dcf[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.Terms(t => t
						.Field("user")
						.Terms("kimchy", "elasticsearch")
						.Boost(1)
					)
				)
			);
			// end::0c4ad860a485fe53d8140ad3ccd11dcf[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""terms"" : {
			            ""user"" : [""kimchy"", ""elasticsearch""],
			            ""boost"" : 1.0
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/terms-query.asciidoc:127")]
		public void Line127()
		{
			// tag::9e56d79ad9a02b642c361f0b85dd95d7[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map<object>(m => m
					.Properties(p => p
						.Keyword(k => k
							.Name("color")
						)
					)
				)
			);
			// end::9e56d79ad9a02b642c361f0b85dd95d7[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""color"" : { ""type"" : ""keyword"" }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/terms-query.asciidoc:145")]
		public void Line145()
		{
			// tag::d3088d5fa59b3ab110f64fb4f9b0065c[]
			var indexResponse = client.Index(new
			{
				color = new[] { "blue", "green" }
			}, i => i
			.Index("my_index")
			.Id(1)
			);
			// end::d3088d5fa59b3ab110f64fb4f9b0065c[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""color"":   [""blue"", ""green""]
			}");
		}

		[U]
		[Description("query-dsl/terms-query.asciidoc:160")]
		public void Line160()
		{
			// tag::8c5977410335d58217e0626618ce6641[]
			var indexResponse = client.Index(new
			{
				color = "blue"
			}, i => i
			.Index("my_index")
			.Id(2)
			);
			// end::8c5977410335d58217e0626618ce6641[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""color"":   ""blue""
			}");
		}

		[U]
		[Description("query-dsl/terms-query.asciidoc:186")]
		public void Line186()
		{
			// tag::d1bcf2eb63a462bfdcf01a68e68d5b4a[]
			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Pretty()
				.Query(q => q
					.Terms(t => t
						.Field("color")
						.TermsLookup<object>(l => l
							.Index("my_index")
							.Id("2")
							.Path("color")
						)
					)
				)
			);
			// end::d1bcf2eb63a462bfdcf01a68e68d5b4a[]

			searchResponse.MatchesExample(@"GET my_index/_search?pretty
			{
			  ""query"": {
			    ""terms"": {
			        ""color"" : {
			            ""index"" : ""my_index"",
			            ""id"" : ""2"",
			            ""path"" : ""color""
			        }
			    }
			  }
			}");
		}
	}
}
