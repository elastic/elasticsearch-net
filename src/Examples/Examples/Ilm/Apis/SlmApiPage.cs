using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class SlmApiPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line52()
		{
			// tag::f0f1a2ad8f815d8dfea122420b295a35[]
			var response0 = new SearchResponse<object>();
			// end::f0f1a2ad8f815d8dfea122420b295a35[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"", \<1>
			  ""name"": ""<daily-snap-{now/d}>"", \<2>
			  ""repository"": ""my_repository"", \<3>
			  ""config"": { \<4>
			    ""indices"": [""data-*"", ""important""], \<5>
			    ""ignore_unavailable"": false,
			    ""include_global_state"": false
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line116()
		{
			// tag::b4f9fe8808cb27a210b162e7aaba261d[]
			var response0 = new SearchResponse<object>();
			// end::b4f9fe8808cb27a210b162e7aaba261d[]

			response0.MatchesExample(@"GET /_slm/policy/daily-snapshots?human");
		}

		[U]
		[SkipExample]
		public void Line154()
		{
			// tag::bc2dd9e5ed37f98016ecf53f968d2211[]
			var response0 = new SearchResponse<object>();
			// end::bc2dd9e5ed37f98016ecf53f968d2211[]

			response0.MatchesExample(@"GET /_slm/policy");
		}

		[U]
		[SkipExample]
		public void Line178()
		{
			// tag::c2837666ce06acefbdd575bcc727b370[]
			var response0 = new SearchResponse<object>();
			// end::c2837666ce06acefbdd575bcc727b370[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots/_execute");
		}

		[U]
		[SkipExample]
		public void Line247()
		{
			// tag::b9e0a99932e6f9ee620f5ca7f8588163[]
			var response0 = new SearchResponse<object>();
			// end::b9e0a99932e6f9ee620f5ca7f8588163[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"",
			  ""name"": ""<daily-snap-{now/d}>"",
			  ""repository"": ""my_repository"",
			  ""config"": {
			    ""indices"": [""data-*"", ""important""],
			    ""ignore_unavailable"": true,
			    ""include_global_state"": false
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line346()
		{
			// tag::1a1f3421717ff744ed83232729289bb0[]
			var response0 = new SearchResponse<object>();
			// end::1a1f3421717ff744ed83232729289bb0[]

			response0.MatchesExample(@"DELETE /_slm/policy/daily-snapshots");
		}
	}
}