using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class GettingStartedSlmPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line23()
		{
			// tag::718c2afdece55a7de338e668438eac2d[]
			var response0 = new SearchResponse<object>();
			// end::718c2afdece55a7de338e668438eac2d[]

			response0.MatchesExample(@"POST /_security/role/slm-admin
			{
			  ""cluster"": [""manage_slm"", ""create_snapshot""],
			  ""indices"": [
			    {
			      ""names"": ["".slm-history-*""],
			      ""privileges"": [""all""]
			    }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line42()
		{
			// tag::ef76d0e4cdc2881c161a5557a98a3446[]
			var response0 = new SearchResponse<object>();
			// end::ef76d0e4cdc2881c161a5557a98a3446[]

			response0.MatchesExample(@"POST /_security/role/slm-read-only
			{
			  ""cluster"": [""read_slm""],
			  ""indices"": [
			    {
			      ""names"": ["".slm-history-*""],
			      ""privileges"": [""read""]
			    }
			  ]
			}");
		}

		[U]
		[SkipExample]
		public void Line68()
		{
			// tag::89b72dd7f747f6297c2b089e8bc807be[]
			var response0 = new SearchResponse<object>();
			// end::89b72dd7f747f6297c2b089e8bc807be[]

			response0.MatchesExample(@"PUT /_snapshot/my_repository
			{
			  ""type"": ""fs"",
			  ""settings"": {
			    ""location"": ""my_backup_location""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line90()
		{
			// tag::a5bc83a268ea9c8b4368beb6522b5336[]
			var response0 = new SearchResponse<object>();
			// end::a5bc83a268ea9c8b4368beb6522b5336[]

			response0.MatchesExample(@"PUT /_slm/policy/nightly-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"", \<1>
			  ""name"": ""<nightly-snap-{now/d}>"", \<2>
			  ""repository"": ""my_repository"", \<3>
			  ""config"": { \<4>
			    ""indices"": [""*""] \<5>
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line138()
		{
			// tag::5bf4b2d603221fb1df4adb34829e1164[]
			var response0 = new SearchResponse<object>();
			// end::5bf4b2d603221fb1df4adb34829e1164[]

			response0.MatchesExample(@"PUT /_slm/policy/nightly-snapshots/_execute");
		}

		[U]
		[SkipExample]
		public void Line151()
		{
			// tag::f1b545d3c3eeedf8ae09c56070c26053[]
			var response0 = new SearchResponse<object>();
			// end::f1b545d3c3eeedf8ae09c56070c26053[]

			response0.MatchesExample(@"GET /_slm/policy/nightly-snapshots?human");
		}
	}
}