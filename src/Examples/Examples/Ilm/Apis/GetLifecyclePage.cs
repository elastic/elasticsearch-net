using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class GetLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::2e7f4b9be999422a12abb680572b13c8[]
			var response0 = new SearchResponse<object>();
			// end::2e7f4b9be999422a12abb680572b13c8[]

			response0.MatchesExample(@"GET _ilm/policy/my_policy");
		}

		[U(Skip = "Example not implemented")]
		public void Line81()
		{
			// tag::c4c3838c118e037f476ff6eca050fddd[]
			var response0 = new SearchResponse<object>();
			// end::c4c3838c118e037f476ff6eca050fddd[]

			response0.MatchesExample(@"{
			  ""my_policy"": {
			    ""version"": 1, \<1>
			    ""modified_date"": 82392349, \<2>
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
			  }
			}");
		}
	}
}