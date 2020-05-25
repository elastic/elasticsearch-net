// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class GettingStartedSlmPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line25()
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
		public void Line43()
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

		[U(Skip = "Example not implemented")]
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

		[U(Skip = "Example not implemented")]
		public void Line92()
		{
			// tag::c782b579f1ad15f46a7b9e45219806a5[]
			var response0 = new SearchResponse<object>();
			// end::c782b579f1ad15f46a7b9e45219806a5[]

			response0.MatchesExample(@"PUT /_slm/policy/nightly-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"", <1>
			  ""name"": ""<nightly-snap-{now/d}>"", <2>
			  ""repository"": ""my_repository"", <3>
			  ""config"": { <4>
			    ""indices"": [""*""] <5>
			  },
			  ""retention"": { <6>
			    ""expire_after"": ""30d"", <7>
			    ""min_count"": 5, <8>
			    ""max_count"": 50 <9>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line148()
		{
			// tag::af5ff39759d3af0525d941634a6cdb82[]
			var response0 = new SearchResponse<object>();
			// end::af5ff39759d3af0525d941634a6cdb82[]

			response0.MatchesExample(@"POST /_slm/policy/nightly-snapshots/_execute");
		}

		[U(Skip = "Example not implemented")]
		public void Line160()
		{
			// tag::f1b545d3c3eeedf8ae09c56070c26053[]
			var response0 = new SearchResponse<object>();
			// end::f1b545d3c3eeedf8ae09c56070c26053[]

			response0.MatchesExample(@"GET /_slm/policy/nightly-snapshots?human");
		}
	}
}
