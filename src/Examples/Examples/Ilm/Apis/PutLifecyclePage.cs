using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class PutLifecyclePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line46()
		{
			// tag::daa2d4811bec05ac4546b66bd5a615c7[]
			var response0 = new SearchResponse<object>();
			// end::daa2d4811bec05ac4546b66bd5a615c7[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""forcemerge"": {
			            ""max_num_segments"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {}
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
			// tag::bc5fcc40c29087a0df7b5405bb70de5c[]
			var response0 = new SearchResponse<object>();
			// end::bc5fcc40c29087a0df7b5405bb70de5c[]

			response0.MatchesExample(@"{
			  ""acknowledged"": true
			}");
		}
	}
}