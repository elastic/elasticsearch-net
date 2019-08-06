using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class ErrorHandlingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::9d211c6226d0b4434f01cceb76ab6ffa[]
			var response0 = new SearchResponse<object>();
			// end::9d211c6226d0b4434f01cceb76ab6ffa[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-the-index
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""5d"",
			        ""actions"": {
			          ""shrink"": {
			            ""number_of_shards"": 4
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line43()
		{
			// tag::3d0b9acdacc7ecec380c57e814256472[]
			var response0 = new SearchResponse<object>();
			// end::3d0b9acdacc7ecec380c57e814256472[]

			response0.MatchesExample(@"PUT /myindex
			{
			  ""settings"": {
			    ""index.number_of_shards"": 2,
			    ""index.lifecycle.name"": ""shrink-the-index""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::943f92e1d3fa566ef23659be2d96f222[]
			var response0 = new SearchResponse<object>();
			// end::943f92e1d3fa566ef23659be2d96f222[]

			response0.MatchesExample(@"GET /myindex/_ilm/explain");
		}

		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::e29f69a4bcfe27332cb2bb994a2cb5bf[]
			var response0 = new SearchResponse<object>();
			// end::e29f69a4bcfe27332cb2bb994a2cb5bf[]

			response0.MatchesExample(@"{
			  ""indices"" : {
			    ""myindex"" : {
			      ""index"" : ""myindex"",
			      ""managed"" : true,                         \<1>
			      ""policy"" : ""shrink-the-index"",            \<2>
			      ""lifecycle_date_millis"" : 1541717265865,
			      ""age"": ""5.1d"",                            \<3>
			      ""phase"" : ""warm"",                         \<4>
			      ""phase_time_millis"" : 1541717272601,
			      ""action"" : ""shrink"",                      \<5>
			      ""action_time_millis"" : 1541717272601,
			      ""step"" : ""ERROR"",                         \<6>
			      ""step_time_millis"" : 1541717272688,
			      ""failed_step"" : ""shrink"",                 \<7>
			      ""step_info"" : {
			        ""type"" : ""illegal_argument_exception"",   \<8>
			        ""reason"" : ""the number of target shards [4] must be less that the number of source shards [2]"" \<9>
			      },
			      ""phase_execution"" : {
			        ""policy"" : ""shrink-the-index"",
			        ""phase_definition"" : {                   <10>
			          ""min_age"" : ""5d"",
			          ""actions"" : {
			            ""shrink"" : {
			              ""number_of_shards"" : 4
			            }
			          }
			        },
			        ""version"" : 1,
			        ""modified_date_in_millis"" : 1541717264230
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line127()
		{
			// tag::7bee02e8962e355a23559b6eaa6678f2[]
			var response0 = new SearchResponse<object>();
			// end::7bee02e8962e355a23559b6eaa6678f2[]

			response0.MatchesExample(@"PUT _ilm/policy/shrink-the-index
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""5d"",
			        ""actions"": {
			          ""shrink"": {
			            ""number_of_shards"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line155()
		{
			// tag::235513edcb5ce3fe2e38a781eeefa6a0[]
			var response0 = new SearchResponse<object>();
			// end::235513edcb5ce3fe2e38a781eeefa6a0[]

			response0.MatchesExample(@"POST /myindex/_ilm/retry");
		}
	}
}