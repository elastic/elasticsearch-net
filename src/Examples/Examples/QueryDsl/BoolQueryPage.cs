using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class BoolQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line36()
		{
			// tag::06afce2955f9094d96d27067ebca32e8[]
			var response0 = new SearchResponse<object>();
			// end::06afce2955f9094d96d27067ebca32e8[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""bool"" : {
			      ""must"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			      },
			      ""filter"": {
			        ""term"" : { ""tag"" : ""tech"" }
			      },
			      ""must_not"" : {
			        ""range"" : {
			          ""age"" : { ""gte"" : 10, ""lte"" : 20 }
			        }
			      },
			      ""should"" : [
			        { ""term"" : { ""tag"" : ""wow"" } },
			        { ""term"" : { ""tag"" : ""elasticsearch"" } }
			      ],
			      ""minimum_should_match"" : 1,
			      ""boost"" : 1.0
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line76()
		{
			// tag::f70a54cd9a9f4811bf962e469f2ca2ea[]
			var response0 = new SearchResponse<object>();
			// end::f70a54cd9a9f4811bf962e469f2ca2ea[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""bool"": {
			      ""filter"": {
			        ""term"": {
			          ""status"": ""active""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line96()
		{
			// tag::fa88f6f5a7d728ec4f1d05244228cb09[]
			var response0 = new SearchResponse<object>();
			// end::fa88f6f5a7d728ec4f1d05244228cb09[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""match_all"": {}
			      },
			      ""filter"": {
			        ""term"": {
			          ""status"": ""active""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line120()
		{
			// tag::162b5b693b713f0bfab1209d59443c46[]
			var response0 = new SearchResponse<object>();
			// end::162b5b693b713f0bfab1209d59443c46[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""term"": {
			          ""status"": ""active""
			        }
			      }
			    }
			  }
			}");
		}
	}
}