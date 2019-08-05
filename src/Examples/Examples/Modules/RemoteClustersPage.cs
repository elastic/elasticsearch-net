using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules
{
	public class RemoteClustersPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line103()
		{
			// tag::d4ce5a9672f85094e6d833d08debc018[]
			var response0 = new SearchResponse<object>();
			// end::d4ce5a9672f85094e6d833d08debc018[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ],
			          ""transport.ping_schedule"": ""30s""
			        },
			        ""cluster_two"": {
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ],
			          ""transport.compress"": true,
			          ""skip_unavailable"": true
			        },
			        ""cluster_three"": {
			          ""seeds"": [
			            ""127.0.0.1:9302""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line140()
		{
			// tag::328b7b4d0de6fac3a91205251de6e9b5[]
			var response0 = new SearchResponse<object>();
			// end::328b7b4d0de6fac3a91205251de6e9b5[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ],
			          ""transport.ping_schedule"": ""60s""
			        },
			        ""cluster_two"": {
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ],
			          ""transport.compress"": false
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line173()
		{
			// tag::2a0d451f9e13aca39467883b16270cc2[]
			var response0 = new SearchResponse<object>();
			// end::2a0d451f9e13aca39467883b16270cc2[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_two"": { \<1>
			          ""seeds"": null,
			          ""skip_unavailable"": null,
			          ""transport"": {
			            ""compress"": null
			          }
			        }
			      }
			    }
			  }
			}");
		}
	}
}