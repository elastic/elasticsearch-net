using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.IndexModules
{
	public class IndexSortingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line16()
		{
			// tag::fea339c85b60ccefa6a163a70b86ca82[]
			var response0 = new SearchResponse<object>();
			// end::fea339c85b60ccefa6a163a70b86ca82[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : ""date"", \<1>
			            ""sort.order"" : ""desc"" \<2>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""date"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line42()
		{
			// tag::a69f1a67cdc141e8dde5abb437c76959[]
			var response0 = new SearchResponse<object>();
			// end::a69f1a67cdc141e8dde5abb437c76959[]

			response0.MatchesExample(@"PUT twitter
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : [""username"", ""date""], \<1>
			            ""sort.order"" : [""asc"", ""desc""] \<2>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""username"": {
			                ""type"": ""keyword"",
			                ""doc_values"": true
			            },
			            ""date"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line116()
		{
			// tag::e01a82a0a809a4770ddc84c2cfc1ec85[]
			var response0 = new SearchResponse<object>();
			// end::e01a82a0a809a4770ddc84c2cfc1ec85[]

			response0.MatchesExample(@"PUT events
			{
			    ""settings"" : {
			        ""index"" : {
			            ""sort.field"" : ""timestamp"",
			            ""sort.order"" : ""desc"" \<1>
			        }
			    },
			    ""mappings"": {
			        ""properties"": {
			            ""timestamp"": {
			                ""type"": ""date""
			            }
			        }
			    }
			}");
		}

		[U]
		[SkipExample]
		public void Line141()
		{
			// tag::46a3694ee4a7bbd4973565e5886782bb[]
			var response0 = new SearchResponse<object>();
			// end::46a3694ee4a7bbd4973565e5886782bb[]

			response0.MatchesExample(@"GET /events/_search
			{
			    ""size"": 10,
			    ""sort"": [
			        { ""timestamp"": ""desc"" }
			    ]
			}");
		}

		[U]
		[SkipExample]
		public void Line163()
		{
			// tag::2e8ba1e0b2a18dd276bbbe64f2b86338[]
			var response0 = new SearchResponse<object>();
			// end::2e8ba1e0b2a18dd276bbbe64f2b86338[]

			response0.MatchesExample(@"GET /events/_search
			{
			    ""size"": 10,
			    ""sort"": [ \<1>
			        { ""timestamp"": ""desc"" }
			    ],
			    ""track_total_hits"": false
			}");
		}
	}
}