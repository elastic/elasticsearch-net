using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm
{
	public class IndexPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/index.asciidoc:38")]
		public void Line38()
		{
			// tag::1d7abeea98f6ed64cc8371794c90a921[]
			var response0 = new SearchResponse<object>();
			// end::1d7abeea98f6ed64cc8371794c90a921[]

			response0.MatchesExample(@"POST /_security/role/slm-admin
			{
			  ""cluster"": [""manage_slm"", ""cluster:admin/snapshot/*""],
			  ""indices"": [
			    {
			      ""names"": ["".slm-history-*""],
			      ""privileges"": [""all""]
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/index.asciidoc:56")]
		public void Line56()
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
	}
}