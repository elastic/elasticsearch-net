using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ccr
{
	public class GettingStartedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line94()
		{
			// tag::9ad38ab4d9c3983e97e8c38fec611f10[]
			var response0 = new SearchResponse<object>();
			// end::9ad38ab4d9c3983e97e8c38fec611f10[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""persistent"" : {
			    ""cluster"" : {
			      ""remote"" : {
			        ""leader"" : {
			          ""seeds"" : [
			            ""127.0.0.1:9300"" <1>
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line119()
		{
			// tag::cc0cca5556ec6224c7134c233734beed[]
			var response0 = new SearchResponse<object>();
			// end::cc0cca5556ec6224c7134c233734beed[]

			response0.MatchesExample(@"GET /_remote/info");
		}

		[U(Skip = "Example not implemented")]
		public void Line165()
		{
			// tag::74ed3197e0e77750f5255c6df0cc0fe5[]
			var response0 = new SearchResponse<object>();
			// end::74ed3197e0e77750f5255c6df0cc0fe5[]

			response0.MatchesExample(@"PUT /server-metrics
			{
			  ""settings"" : {
			    ""index"" : {
			      ""number_of_shards"" : 1,
			      ""number_of_replicas"" : 0
			    }
			  },
			  ""mappings"" : {
			    ""properties"" : {
			      ""@timestamp"" : {
			        ""type"" : ""date""
			      },
			      ""accept"" : {
			        ""type"" : ""long""
			      },
			      ""deny"" : {
			        ""type"" : ""long""
			      },
			      ""host"" : {
			        ""type"" : ""keyword""
			      },
			      ""response"" : {
			        ""type"" : ""float""
			      },
			      ""service"" : {
			        ""type"" : ""keyword""
			      },
			      ""total"" : {
			        ""type"" : ""long""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line214()
		{
			// tag::278da0495c9008e6d9f25a5d5f0339b7[]
			var response0 = new SearchResponse<object>();
			// end::278da0495c9008e6d9f25a5d5f0339b7[]

			response0.MatchesExample(@"PUT /server-metrics-copy/_ccr/follow?wait_for_active_shards=1
			{
			  ""remote_cluster"" : ""leader"",
			  ""leader_index"" : ""server-metrics""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line276()
		{
			// tag::6b104a66ab47fc1e1f24a5738f82feb4[]
			var response0 = new SearchResponse<object>();
			// end::6b104a66ab47fc1e1f24a5738f82feb4[]

			response0.MatchesExample(@"PUT /_ccr/auto_follow/beats
			{
			  ""remote_cluster"" : ""leader"",
			  ""leader_index_patterns"" :
			  [
			    ""metricbeat-*"", <1>
			    ""packetbeat-*"" <2>
			  ],
			  ""follow_index_pattern"" : ""{{leader_index}}-copy"" <3>
			}");
		}
	}
}