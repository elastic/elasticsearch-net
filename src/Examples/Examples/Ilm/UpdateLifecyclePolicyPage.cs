using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class UpdateLifecyclePolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
		{
			// tag::0c44088f251488432966131135f1bd1c[]
			var response0 = new SearchResponse<object>();
			// end::0c44088f251488432966131135f1bd1c[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
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

		[U(Skip = "Example not implemented")]
		public void Line61()
		{
			// tag::2c37ed0b33658d73a712e7942ea7433a[]
			var response0 = new SearchResponse<object>();
			// end::2c37ed0b33658d73a712e7942ea7433a[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""25GB""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"", \<1>
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line100()
		{
			// tag::7251639b2c1267d7c76ab397bbe43bbd[]
			var response0 = new SearchResponse<object>();
			// end::7251639b2c1267d7c76ab397bbe43bbd[]

			response0.MatchesExample(@"{
			  ""my_policy"": {
			    ""version"": 2, \<1>
			    ""modified_date"": 82392349, \<2>
			    ""policy"": {
			      ""phases"": {
			        ""hot"": {
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_size"": ""25gb""
			            }
			          }
			        },
			        ""delete"": {
			          ""min_age"": ""10d"",
			          ""actions"": {
			            ""delete"": {}
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line144()
		{
			// tag::fc541f5741c1fe052439ededa84ffe8a[]
			var response0 = new SearchResponse<object>();
			// end::fc541f5741c1fe052439ededa84ffe8a[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_docs"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line186()
		{
			// tag::0f6fa3a706a7c17858d3dbe329839ea6[]
			var response0 = new SearchResponse<object>();
			// end::0f6fa3a706a7c17858d3dbe329839ea6[]

			response0.MatchesExample(@"GET my_index/_ilm/explain");
		}

		[U(Skip = "Example not implemented")]
		public void Line193()
		{
			// tag::d5e55676f5242766ebb035b87ce660e2[]
			var response0 = new SearchResponse<object>();
			// end::d5e55676f5242766ebb035b87ce660e2[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""my_index"": {
			      ""index"": ""my_index"",
			      ""managed"": true,
			      ""policy"": ""my_executing_policy"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""age"": ""30s"",
			      ""phase"": ""hot"",
			      ""phase_time_millis"": 1538475653317,
			      ""action"": ""rollover"",
			      ""action_time_millis"": 1538475653317,
			      ""step"": ""check-rollover-ready"",
			      ""step_time_millis"": 1538475653317,
			      ""phase_execution"": {
			        ""policy"": ""my_executing_policy"",
			        ""modified_date_in_millis"": 1538475653317,
			        ""version"": 1,
			        ""phase_definition"": {
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_docs"": 1
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line231()
		{
			// tag::f94601bc9cd640adb939af67116a40c8[]
			var response0 = new SearchResponse<object>();
			// end::f94601bc9cd640adb939af67116a40c8[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""min_age"": ""1d"", \<1>
			        ""actions"": {
			          ""rollover"": {
			            ""max_docs"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line271()
		{
			// tag::aada9dd17e7b08f3c5a279920c84333e[]
			var response0 = new SearchResponse<object>();
			// end::aada9dd17e7b08f3c5a279920c84333e[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""my_index"": {
			      ""index"": ""my_index"",
			      ""managed"": true,
			      ""policy"": ""my_executing_policy"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""age"": ""30s"",
			      ""phase"": ""hot"",
			      ""phase_time_millis"": 1538475653317,
			      ""action"": ""rollover"",
			      ""action_time_millis"": 1538475653317,
			      ""step"": ""check-rollover-ready"",
			      ""step_time_millis"": 1538475653317,
			      ""phase_execution"": {
			        ""policy"": ""my_executing_policy"",
			        ""modified_date_in_millis"": 1538475653317,
			        ""version"": 1, \<1>
			        ""phase_definition"": {
			          ""min_age"": ""0ms"",
			          ""actions"": {
			            ""rollover"": {
			              ""max_docs"": 1
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line311()
		{
			// tag::416c65c55a53d0161426cc09ae999c72[]
			var response0 = new SearchResponse<object>();
			// end::416c65c55a53d0161426cc09ae999c72[]

			response0.MatchesExample(@"PUT _ilm/policy/my_executing_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""1d"",
			        ""actions"": {
			          ""forcemerge"": {
			            ""max_num_segments"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""10d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line406()
		{
			// tag::84935bf612d1aa402a7e16dae1ab99f5[]
			var response0 = new SearchResponse<object>();
			// end::84935bf612d1aa402a7e16dae1ab99f5[]

			response0.MatchesExample(@"{
			  ""indices"": {
			    ""my_index"": {
			      ""index"": ""my_index"",
			      ""managed"": true,
			      ""policy"": ""my_executing_policy"",
			      ""lifecycle_date_millis"": 1538475653281,
			      ""age"": ""30s"",
			      ""phase"": ""warm"",
			      ""phase_time_millis"": 1538475653317,
			      ""action"": ""forcemerge"",
			      ""action_time_millis"": 1538475653317,
			      ""step"": ""forcemerge"",
			      ""step_time_millis"": 1538475653317,
			      ""phase_execution"": {
			        ""policy"": ""my_executing_policy"",
			        ""modified_date_in_millis"": 1538475653317,
			        ""version"": 3, \<1>
			        ""phase_definition"": {
			          ""min_age"": ""1d"",
			          ""actions"": {
			            ""forcemerge"": {
			              ""max_num_segments"": 1
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line505()
		{
			// tag::552b6761ef052efa1e83f8a3c30d6f78[]
			var response0 = new SearchResponse<object>();
			// end::552b6761ef052efa1e83f8a3c30d6f78[]

			response0.MatchesExample(@"PUT my_index/_settings
			{
			  ""lifecycle.name"": ""my_other_policy""
			}");
		}
	}
}