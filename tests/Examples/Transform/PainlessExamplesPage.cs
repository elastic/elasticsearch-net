// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Transform
{
	public class PainlessExamplesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/painless-examples.asciidoc:163")]
		public void Line163()
		{
			// tag::8ce484cceef334f0a8ad3a40570b3425[]
			var response0 = new SearchResponse<object>();
			// end::8ce484cceef334f0a8ad3a40570b3425[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": [ <1>
			      ""kibana_sample_data_logs""
			    ]
			  },
			  ""pivot"": {
			    ""group_by"": {
			      ""agent"": {
			        ""terms"": {
			          ""script"": { <2>
			            ""source"": """"""String agent = doc['agent.keyword'].value;
			            if (agent.contains(""MSIE"")) {
			              return ""internet explorer"";
			            } else if (agent.contains(""AppleWebKit"")) {
			              return ""safari"";
			            } else if (agent.contains('Firefox')) {
			              return ""firefox"";
			            } else { return agent }"""""",
			            ""lang"": ""painless""
			          }
			        }
			      }
			    },
			    ""aggregations"": { <3>
			      ""200"": {
			        ""filter"": {
			          ""term"": {
			            ""response"": ""200""
			          }
			        }
			      },
			      ""404"": {
			        ""filter"": {
			          ""term"": {
			            ""response"": ""404""
			          }
			        }
			      },
			      ""503"": {
			        ""filter"": {
			          ""term"": {
			            ""response"": ""503""
			          }
			        }
			      }
			    }
			  },
			  ""dest"": { <4>
			    ""index"": ""pivot_logs""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/painless-examples.asciidoc:301")]
		public void Line301()
		{
			// tag::e4b64b8277af259a52c8d3940157b5fa[]
			var response0 = new SearchResponse<object>();
			// end::e4b64b8277af259a52c8d3940157b5fa[]

			response0.MatchesExample(@"PUT _transform/data_log
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_logs""
			  },
			  ""dest"": {
			    ""index"": ""data-logs-by-client""
			  },
			  ""pivot"": {
			    ""group_by"": {
			      ""machine.os"": {""terms"": {""field"": ""machine.os.keyword""}},
			      ""machine.ip"": {""terms"": {""field"": ""clientip""}}
			    },
			    ""aggregations"": {
			      ""time_frame.lte"": {
			        ""max"": {
			          ""field"": ""timestamp""
			        }
			      },
			      ""time_frame.gte"": {
			        ""min"": {
			          ""field"": ""timestamp""
			        }
			      },
			      ""time_length"": { <1>
			        ""bucket_script"": {
			          ""buckets_path"": { <2>
			            ""min"": ""time_frame.gte.value"",
			            ""max"": ""time_frame.lte.value""
			          },
			          ""script"": ""params.max - params.min"" <3>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/painless-examples.asciidoc:417")]
		public void Line417()
		{
			// tag::22908e2f01460917d3145a53e3fb7546[]
			var response0 = new SearchResponse<object>();
			// end::22908e2f01460917d3145a53e3fb7546[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""id"" : ""index_compare"",
			  ""source"" : { <1>
			    ""index"" : [
			      ""index1"",
			      ""index2""
			    ],
			    ""query"" : {
			      ""match_all"" : { }
			    }
			  },
			  ""dest"" : { <2>
			    ""index"" : ""compare""
			  },
			  ""pivot"" : {
			    ""group_by"" : {
			      ""unique-id"" : {
			        ""terms"" : {
			          ""field"" : ""<unique-id-field>"" <3>
			        }
			      }
			    },
			    ""aggregations"" : {
			      ""compare"" : { <4>
			        ""scripted_metric"" : {
			          ""init_script"" : """",
			          ""map_script"" : ""state.doc = new HashMap(params['_source'])"", <5>
			          ""combine_script"" : ""return state"", <6>
			          ""reduce_script"" : """""" <7>
			            if (states.size() != 2) {
			              return ""count_mismatch""
			            }
			            if (states.get(0).equals(states.get(1))) {
			              return ""match""
			            } else {
			              return ""mismatch""
			            }
			            """"""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
