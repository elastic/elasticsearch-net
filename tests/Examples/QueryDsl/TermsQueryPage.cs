using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class TermsQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-query.asciidoc:19")]
		public void Line19()
		{
			// tag::0c4ad860a485fe53d8140ad3ccd11dcf[]
			var response0 = new SearchResponse<object>();
			// end::0c4ad860a485fe53d8140ad3ccd11dcf[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""terms"" : {
			            ""user"" : [""kimchy"", ""elasticsearch""],
			            ""boost"" : 1.0
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-query.asciidoc:127")]
		public void Line127()
		{
			// tag::9e56d79ad9a02b642c361f0b85dd95d7[]
			var response0 = new SearchResponse<object>();
			// end::9e56d79ad9a02b642c361f0b85dd95d7[]

			response0.MatchesExample(@"PUT my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""color"" : { ""type"" : ""keyword"" }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-query.asciidoc:145")]
		public void Line145()
		{
			// tag::d3088d5fa59b3ab110f64fb4f9b0065c[]
			var response0 = new SearchResponse<object>();
			// end::d3088d5fa59b3ab110f64fb4f9b0065c[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""color"":   [""blue"", ""green""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-query.asciidoc:160")]
		public void Line160()
		{
			// tag::8c5977410335d58217e0626618ce6641[]
			var response0 = new SearchResponse<object>();
			// end::8c5977410335d58217e0626618ce6641[]

			response0.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""color"":   ""blue""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/terms-query.asciidoc:186")]
		public void Line186()
		{
			// tag::d1bcf2eb63a462bfdcf01a68e68d5b4a[]
			var response0 = new SearchResponse<object>();
			// end::d1bcf2eb63a462bfdcf01a68e68d5b4a[]

			response0.MatchesExample(@"GET my_index/_search?pretty
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