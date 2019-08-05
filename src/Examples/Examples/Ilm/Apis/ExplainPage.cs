using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class ExplainPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line92()
		{
			// tag::0f6fa3a706a7c17858d3dbe329839ea6[]
			var response0 = new SearchResponse<object>();
			// end::0f6fa3a706a7c17858d3dbe329839ea6[]

			response0.MatchesExample(@"GET my_index/_ilm/explain");
		}

		[U]
		[SkipExample]
		public void Line102()
		{
			// tag::bc42b1c517ff1fc6ad4371bae23d1c57[]
			var response0 = new SearchResponse<object>();
			// end::bc42b1c517ff1fc6ad4371bae23d1c57[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""my_index"": {
			      ""index"": ""my_index"",
			      ""managed"": true, \<1>
			      ""policy"": ""my_policy"", \<2>
			      ""lifecycle_date_millis"": 1538475653281, \<3>
			      ""age"": ""15s"", \<4>
			      ""phase"": ""new"",
			      ""phase_time_millis"": 1538475653317, \<5>
			      ""action"": ""complete"",
			      ""action_time_millis"": 1538475653317, \<6>
			      ""step"": ""complete"",
			      ""step_time_millis"": 1538475653317 \<7>
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line138()
		{
			// tag::9cf677738535149f0cdb1796ddafbc8a[]
			var response0 = new SearchResponse<object>();
			// end::9cf677738535149f0cdb1796ddafbc8a[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""test-000069"": {
			      ""index"": ""test-000069"",
			      ""managed"": true,
			      ""policy"": ""my_lifecycle3"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""lifecycle_date"": ""2018-10-15T13:45:21.981Z"",
			      ""age"": ""25.14s"",
			      ""phase"": ""hot"",
			      ""phase_time_millis"": 1538475653317,
			      ""phase_time"": ""2018-10-15T13:45:22.577Z"",
			      ""action"": ""rollover"",
			      ""action_time_millis"": 1538475653317,
			      ""action_time"": ""2018-10-15T13:45:22.577Z"",
			      ""step"": ""attempt-rollover"",
			      ""step_time_millis"": 1538475653317,
			      ""step_time"": ""2018-10-15T13:45:22.577Z"",
			      ""phase_execution"": {
			        ""policy"": ""my_lifecycle3"",
			        ""phase_definition"": { \<1>
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_age"": ""30s""
			            }
			          }
			        },
			        ""version"": 3, \<2>
			        ""modified_date"": ""2018-10-15T13:21:41.576Z"", \<3>
			        ""modified_date_in_millis"": 1539609701576 \<4>
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line187()
		{
			// tag::0a6dcb918e7d6354c4709505f22a786f[]
			var response0 = new SearchResponse<object>();
			// end::0a6dcb918e7d6354c4709505f22a786f[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""test-000020"": {
			      ""index"": ""test-000020"",
			      ""managed"": true,
			      ""policy"": ""my_lifecycle3"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""lifecycle_date"": ""2018-10-15T13:45:21.981Z"",
			      ""age"": ""4.12m"",
			      ""phase"": ""warm"",
			      ""phase_time_millis"": 1538475653317,
			      ""phase_time"": ""2018-10-15T13:45:22.577Z"",
			      ""action"": ""allocate"",
			      ""action_time_millis"": 1538475653317,
			      ""action_time"": ""2018-10-15T13:45:22.577Z"",
			      ""step"": ""check-allocation"",
			      ""step_time_millis"": 1538475653317,
			      ""step_time"": ""2018-10-15T13:45:22.577Z"",
			      ""step_info"": { \<1>
			        ""message"": ""Waiting for all shard copies to be active"",
			        ""shards_left_to_allocate"": -1,
			        ""all_shards_active"": false,
			        ""actual_replicas"": 2
			      },
			      ""phase_execution"": {
			        ""policy"": ""my_lifecycle3"",
			        ""phase_definition"": {
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""allocate"": {
			              ""number_of_replicas"": 2,
			              ""include"": {
			                ""box_type"": ""warm""
			              },
			              ""exclude"": {},
			              ""require"": {}
			            },
			            ""forcemerge"": {
			              ""max_num_segments"": 1
			            }
			          }
			        },
			        ""version"": 2,
			        ""modified_date"": ""2018-10-15T13:20:02.489Z"",
			        ""modified_date_in_millis"": 1539609602489
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line248()
		{
			// tag::f5fc9eb5e7300853a3b93236c72e70e3[]
			var response0 = new SearchResponse<object>();
			// end::f5fc9eb5e7300853a3b93236c72e70e3[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""test-000056"": {
			      ""index"": ""test-000056"",
			      ""managed"": true,
			      ""policy"": ""my_lifecycle3"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""lifecycle_date"": ""2018-10-15T13:45:21.981Z"",
			      ""age"": ""50.1d"",
			      ""phase"": ""hot"",
			      ""phase_time_millis"": 1538475653317,
			      ""phase_time"": ""2018-10-15T13:45:22.577Z"",
			      ""action"": ""rollover"",
			      ""action_time_millis"": 1538475653317,
			      ""action_time"": ""2018-10-15T13:45:22.577Z"",
			      ""step"": ""ERROR"",
			      ""step_time_millis"": 1538475653317,
			      ""step_time"": ""2018-10-15T13:45:22.577Z"",
			      ""failed_step"": ""attempt-rollover"", \<1>
			      ""step_info"": { \<2>
			        ""type"": ""resource_already_exists_exception"",
			        ""reason"": ""index [test-000057/H7lF9n36Rzqa-KfKcnGQMg] already exists"",
			        ""index_uuid"": ""H7lF9n36Rzqa-KfKcnGQMg"",
			        ""index"": ""test-000057""
			      },
			      ""phase_execution"": {
			        ""policy"": ""my_lifecycle3"",
			        ""phase_definition"": {
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_age"": ""30s""
			            }
			          }
			        },
			        ""version"": 3,
			        ""modified_date"": ""2018-10-15T13:21:41.576Z"",
			        ""modified_date_in_millis"": 1539609701576
			      }
			    }
			  }
			}");
		}
	}
}