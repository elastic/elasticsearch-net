using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class PutMappingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line7()
		{
			// tag::bf6e261ee84680c69d46faa9ee5b2f56[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::bf6e261ee84680c69d46faa9ee5b2f56[]

			response0.MatchesExample(@"PUT twitter \<1>
			{}");

			response1.MatchesExample(@"PUT twitter/_mapping \<2>
			{
			  ""properties"": {
			    ""email"": {
			      ""type"": ""keyword""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line37()
		{
			// tag::88b2e29e3251e48bfb720fa83e9eb6a3[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::88b2e29e3251e48bfb720fa83e9eb6a3[]

			response0.MatchesExample(@"# Create the two indices");

			response1.MatchesExample(@"PUT twitter-1");

			response2.MatchesExample(@"PUT twitter-2");

			response3.MatchesExample(@"# Update both mappings");

			response4.MatchesExample(@"PUT /twitter-1,twitter-2/_mapping \<1>
			{
			  ""properties"": {
			    ""user_name"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line70()
		{
			// tag::9943d52ba0f75fa0eb61e944ed7cbcd9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9943d52ba0f75fa0eb61e944ed7cbcd9[]

			response0.MatchesExample(@"PUT my_index \<1>
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name"": {
			        ""properties"": {
			          ""first"": {
			            ""type"": ""text""
			          }
			        }
			      },
			      ""user_id"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_mapping
			{
			  ""properties"": {
			    ""name"": {
			      ""properties"": {
			        ""last"": { \<2>
			          ""type"": ""text""
			        }
			      }
			    },
			    ""user_id"": {
			      ""type"": ""keyword"",
			      ""ignore_above"": 100 \<3>
			    }
			  }
			}");
		}
	}
}