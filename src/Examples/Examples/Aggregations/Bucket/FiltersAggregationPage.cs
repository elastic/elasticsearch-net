using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Bucket
{
	public class FiltersAggregationPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line11()
		{
			// tag::188e6208cccb13027a5c1c95440841ee[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::188e6208cccb13027a5c1c95440841ee[]

			response0.MatchesExample(@"PUT /logs/_bulk?refresh
			{ ""index"" : { ""_id"" : 1 } }
			{ ""body"" : ""warning: page could not be rendered"" }
			{ ""index"" : { ""_id"" : 2 } }
			{ ""body"" : ""authentication error"" }
			{ ""index"" : { ""_id"" : 3 } }
			{ ""body"" : ""warning: connection timed out"" }");

			response1.MatchesExample(@"GET logs/_search
			{
			  ""size"": 0,
			  ""aggs"" : {
			    ""messages"" : {
			      ""filters"" : {
			        ""filters"" : {
			          ""errors"" :   { ""match"" : { ""body"" : ""error""   }},
			          ""warnings"" : { ""match"" : { ""body"" : ""warning"" }}
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line74()
		{
			// tag::3cd2f7f9096a8e8180f27b6c30e71840[]
			var response0 = new SearchResponse<object>();
			// end::3cd2f7f9096a8e8180f27b6c30e71840[]

			response0.MatchesExample(@"GET logs/_search
			{
			  ""size"": 0,
			  ""aggs"" : {
			    ""messages"" : {
			      ""filters"" : {
			        ""filters"" : [
			          { ""match"" : { ""body"" : ""error""   }},
			          { ""match"" : { ""body"" : ""warning"" }}
			        ]
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line137()
		{
			// tag::21bb03ca9123de3237c1c76934f9f172[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::21bb03ca9123de3237c1c76934f9f172[]

			response0.MatchesExample(@"PUT logs/_doc/4?refresh
			{
			  ""body"": ""info: user Bob logged out""
			}");

			response1.MatchesExample(@"GET logs/_search
			{
			  ""size"": 0,
			  ""aggs"" : {
			    ""messages"" : {
			      ""filters"" : {
			        ""other_bucket_key"": ""other_messages"",
			        ""filters"" : {
			          ""errors"" :   { ""match"" : { ""body"" : ""error""   }},
			          ""warnings"" : { ""match"" : { ""body"" : ""warning"" }}
			        }
			      }
			    }
			  }
			}");
		}
	}
}