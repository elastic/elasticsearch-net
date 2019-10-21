using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search
{
	public class ValidatePage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		public void Line7()
		{
			// tag::6bdf94c025faf346013a70e3473d5f87[]
			var response0 = new SearchResponse<object>();
			// end::6bdf94c025faf346013a70e3473d5f87[]

			response0.MatchesExample(@"GET twitter/_validate/query?q=user:foo");
		}
		[U(Skip = "Example not implemented")]
		public void Line73()
		{
			// tag::a0a6e4abbf0a5d064d06d06ddc585f4c[]
			var response0 = new SearchResponse<object>();
			// end::a0a6e4abbf0a5d064d06d06ddc585f4c[]

			response0.MatchesExample(@"PUT twitter/_bulk?refresh
			{""index"":{""_id"":1}}
			{""user"" : ""kimchy"", ""post_date"" : ""2009-11-15T14:12:12"", ""message"" : ""trying out Elasticsearch""}
			{""index"":{""_id"":2}}
			{""user"" : ""kimchi"", ""post_date"" : ""2009-11-15T14:12:13"", ""message"" : ""My username is similar to @kimchy!""}");
		}

		[U(Skip = "Example not implemented")]
		public void Line102()
		{
			// tag::1a0ce57a5e6d73765601de98a5d60d80[]
			var response0 = new SearchResponse<object>();
			// end::1a0ce57a5e6d73765601de98a5d60d80[]

			response0.MatchesExample(@"GET twitter/_validate/query
			{
			  ""query"" : {
			    ""bool"" : {
			      ""must"" : {
			        ""query_string"" : {
			          ""query"" : ""*:*""
			        }
			      },
			      ""filter"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line129()
		{
			// tag::9989c7860423519c7357936a73c2a5ce[]
			var response0 = new SearchResponse<object>();
			// end::9989c7860423519c7357936a73c2a5ce[]

			response0.MatchesExample(@"GET twitter/_validate/query
			{
			  ""query"": {
			    ""query_string"": {
			      ""query"": ""post_date:foo"",
			      ""lenient"": false
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line153()
		{
			// tag::b5cd0cc45db5f2fba30ac310630ad172[]
			var response0 = new SearchResponse<object>();
			// end::b5cd0cc45db5f2fba30ac310630ad172[]

			response0.MatchesExample(@"GET twitter/_validate/query?explain=true
			{
			  ""query"": {
			    ""query_string"": {
			      ""query"": ""post_date:foo"",
			      ""lenient"": false
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line194()
		{
			// tag::fd74d7518bab5f1dbc1fed588b9bc2a6[]
			var response0 = new SearchResponse<object>();
			// end::fd74d7518bab5f1dbc1fed588b9bc2a6[]

			response0.MatchesExample(@"GET twitter/_validate/query?rewrite=true
			{
			  ""query"": {
			    ""more_like_this"": {
			      ""like"": {
			        ""_id"": ""2""
			      },
			      ""boost_terms"": 1
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line252()
		{
			// tag::d253135ac0a4b3b04531b1a5d2a19279[]
			var response0 = new SearchResponse<object>();
			// end::d253135ac0a4b3b04531b1a5d2a19279[]

			response0.MatchesExample(@"GET twitter/_validate/query?rewrite=true&all_shards=true
			{
			  ""query"": {
			    ""match"": {
			      ""user"": {
			        ""query"": ""kimchy"",
			        ""fuzziness"": ""auto""
			      }
			    }
			  }
			}");
		}
	}
}