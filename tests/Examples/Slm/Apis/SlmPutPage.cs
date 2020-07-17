// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm.Apis
{
	public class SlmPutPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-put.asciidoc:121")]
		public void Line121()
		{
			// tag::aa7cf5df36b867aee5e3314ac4b4fa68[]
			var response0 = new SearchResponse<object>();
			// end::aa7cf5df36b867aee5e3314ac4b4fa68[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"", <1>
			  ""name"": ""<daily-snap-{now/d}>"", <2>
			  ""repository"": ""my_repository"", <3>
			  ""config"": { <4>
			    ""indices"": [""data-*"", ""important""], <5>
			    ""ignore_unavailable"": false,
			    ""include_global_state"": false
			  },
			  ""retention"": { <6>
			    ""expire_after"": ""30d"", <7>
			    ""min_count"": 5, <8>
			    ""max_count"": 50 <9>
			  }
			}");
		}
	}
}
