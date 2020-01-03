using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class PutLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
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
	}
}