using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class UpdateSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line9()
		{
			// tag::4029af36cb3f8202549017f7378803b4[]
			var response0 = new SearchResponse<object>();
			// end::4029af36cb3f8202549017f7378803b4[]

			response0.MatchesExample(@"GET /_cluster/settings");
		}

		[U(Skip = "Example not implemented")]
		public void Line20()
		{
			// tag::37f4bd6dd220db648998fc340b3dfa69[]
			var response0 = new SearchResponse<object>();
			// end::37f4bd6dd220db648998fc340b3dfa69[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""persistent"" : {
			        ""indices.recovery.max_bytes_per_sec"" : ""50mb""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line33()
		{
			// tag::8c05281b724106e703c05df661188c4f[]
			var response0 = new SearchResponse<object>();
			// end::8c05281b724106e703c05df661188c4f[]

			response0.MatchesExample(@"PUT /_cluster/settings?flat_settings=true
			{
			    ""transient"" : {
			        ""indices.recovery.max_bytes_per_sec"" : ""20mb""
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line67()
		{
			// tag::1f25c9ef11f574f1ba0ad974bf653cd4[]
			var response0 = new SearchResponse<object>();
			// end::1f25c9ef11f574f1ba0ad974bf653cd4[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""transient"" : {
			        ""indices.recovery.max_bytes_per_sec"" : null
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line93()
		{
			// tag::32496570a397852bece96f4da5d17a7e[]
			var response0 = new SearchResponse<object>();
			// end::32496570a397852bece96f4da5d17a7e[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			    ""transient"" : {
			        ""indices.recovery.*"" : null
			    }
			}");
		}
	}
}