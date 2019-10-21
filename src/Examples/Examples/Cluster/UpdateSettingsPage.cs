using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cluster
{
	public class UpdateSettingsPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		public void Line62()
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
		public void Line75()
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
		public void Line104()
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
		public void Line131()
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