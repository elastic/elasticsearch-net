using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Params
{
	public class IgnoreMalformedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line16()
		{
			// tag::56af112ba65955f3ca5ef61a199c0daa[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::56af112ba65955f3ca5ef61a199c0daa[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": {
			        ""type"": ""integer"",
			        ""ignore_malformed"": true
			      },
			      ""number_two"": {
			        ""type"": ""integer""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"":       ""Some text value"",
			  ""number_one"": ""foo"" \<1>
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""text"":       ""Some text value"",
			  ""number_two"": ""foo"" \<2>
			}");
		}

		[U]
		[SkipExample]
		public void Line60()
		{
			// tag::835faff0d2e8874b7b9693376fa7fc57[]
			var response0 = new SearchResponse<object>();
			// end::835faff0d2e8874b7b9693376fa7fc57[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""index.mapping.ignore_malformed"": true \<1>
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""number_one"": { \<1>
			        ""type"": ""byte""
			      },
			      ""number_two"": {
			        ""type"": ""integer"",
			        ""ignore_malformed"": false \<2>
			      }
			    }
			  }
			}");
		}
	}
}