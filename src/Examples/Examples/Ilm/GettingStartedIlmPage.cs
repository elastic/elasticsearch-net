using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class GettingStartedIlmPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line29()
		{
			// tag::993a81c69d26d94810172bee4043f0fd[]
			var response0 = new SearchResponse<object>();
			// end::993a81c69d26d94810172bee4043f0fd[]

			response0.MatchesExample(@"PUT _ilm/policy/datastream_policy   \<1>
			{
			  ""policy"": {                       \<2>
			    ""phases"": {
			      ""hot"": {                      \<3>
			        ""actions"": {
			          ""rollover"": {             \<4>
			            ""max_size"": ""50GB"",
			            ""max_age"": ""30d""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""90d"",           \<5>
			        ""actions"": {
			          ""delete"": {}              \<6>
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line80()
		{
			// tag::e3d7b19f993382750719cdfaad2fdd90[]
			var response0 = new SearchResponse<object>();
			// end::e3d7b19f993382750719cdfaad2fdd90[]

			response0.MatchesExample(@"PUT _template/datastream_template
			{
			  ""index_patterns"": [""datastream-*""],                 \<1>
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""number_of_replicas"": 1,
			    ""index.lifecycle.name"": ""datastream_policy"",      \<2>
			    ""index.lifecycle.rollover_alias"": ""datastream""    \<3>
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line113()
		{
			// tag::55ee835d7c28e933ad8fcb9e45af2bf2[]
			var response0 = new SearchResponse<object>();
			// end::55ee835d7c28e933ad8fcb9e45af2bf2[]

			response0.MatchesExample(@"PUT datastream-000001
			{
			  ""aliases"": {
			    ""datastream"": {
			      ""is_write_index"": true
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line157()
		{
			// tag::a1dbaff15cf8166f74c443ca58258d7e[]
			var response0 = new SearchResponse<object>();
			// end::a1dbaff15cf8166f74c443ca58258d7e[]

			response0.MatchesExample(@"GET datastream-*/_ilm/explain");
		}

		[U]
		[SkipExample]
		public void Line168()
		{
			// tag::c0f09a2304109757010e08c8af1f4a5a[]
			var response0 = new SearchResponse<object>();
			// end::c0f09a2304109757010e08c8af1f4a5a[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""datastream-000001"": {
			      ""index"": ""datastream-000001"",
			      ""managed"": true,                           \<1>
			      ""policy"": ""datastream_policy"",             \<2>
			      ""lifecycle_date_millis"": 1538475653281,
			      ""age"": ""30s"",                              \<3>
			      ""phase"": ""hot"",                            \<4>
			      ""phase_time_millis"": 1538475653317,
			      ""action"": ""rollover"",                      \<5>
			      ""action_time_millis"": 1538475653317,
			      ""step"": ""attempt-rollover"",                \<6>
			      ""step_time_millis"": 1538475653317,
			      ""phase_execution"": {
			        ""policy"": ""datastream_policy"",
			        ""phase_definition"": {                    \<7>
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_size"": ""50gb"",
			              ""max_age"": ""30d""
			            }
			          }
			        },
			        ""version"": 1,                            \<8>
			        ""modified_date_in_millis"": 1539609701576
			      }
			    }
			  }
			}");
		}
	}
}