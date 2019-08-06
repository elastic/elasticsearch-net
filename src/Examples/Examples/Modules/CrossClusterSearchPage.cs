using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules
{
	public class CrossClusterSearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::a8d39396d741e768083808bb11443e9b[]
			var response0 = new SearchResponse<object>();
			// end::a8d39396d741e768083808bb11443e9b[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster"": {
			      ""remote"": {
			        ""cluster_one"": {
			          ""seeds"": [
			            ""127.0.0.1:9300""
			          ]
			        },
			        ""cluster_two"": {
			          ""seeds"": [
			            ""127.0.0.1:9301""
			          ]
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

		[U(Skip = "Example not implemented")]
		public void Line51()
		{
			// tag::972c0c1b6c0b8327fadd77cc1c71b532[]
			var response0 = new SearchResponse<object>();
			// end::972c0c1b6c0b8327fadd77cc1c71b532[]

			response0.MatchesExample(@"GET /cluster_one:twitter/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line112()
		{
			// tag::475943160dca26fd77e750eb586f72bb[]
			var response0 = new SearchResponse<object>();
			// end::475943160dca26fd77e750eb586f72bb[]

			response0.MatchesExample(@"GET /cluster_one:twitter,twitter/_search
			{
			  ""query"": {
			    ""match"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line197()
		{
			// tag::530326f2f610142a4a314c49c216045b[]
			var response0 = new SearchResponse<object>();
			// end::530326f2f610142a4a314c49c216045b[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""cluster.remote.cluster_two.skip_unavailable"": true \<1>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line210()
		{
			// tag::89a950acef646a65bb947c862743ac61[]
			var response0 = new SearchResponse<object>();
			// end::89a950acef646a65bb947c862743ac61[]

			response0.MatchesExample(@"GET /cluster_one:twitter,cluster_two:twitter,twitter/_search \<1>
			{
			  ""query"": {
			    ""match"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}
	}
}