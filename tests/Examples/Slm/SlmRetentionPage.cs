// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Slm
{
	public class SlmRetentionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/slm-retention.asciidoc:31")]
		public void Line31()
		{
			// tag::1527fe79aa1ae25a155a060bac788e7f[]
			var response0 = new SearchResponse<object>();
			// end::1527fe79aa1ae25a155a060bac788e7f[]

			response0.MatchesExample(@"PUT /_slm/policy/daily-snapshots
			{
			  ""schedule"": ""0 30 1 * * ?"",
			  ""name"": ""<daily-snap-{now/d}>"",
			  ""repository"": ""my_repository"",
			  ""retention"": { <1>
			    ""expire_after"": ""30d"", <2>
			    ""min_count"": 5, <3>
			    ""max_count"": 50 <4>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("slm/slm-retention.asciidoc:80")]
		public void Line80()
		{
			// tag::55e8ddf643726dec51531ada0bec7143[]
			var response0 = new SearchResponse<object>();
			// end::55e8ddf643726dec51531ada0bec7143[]

			response0.MatchesExample(@"GET /_slm/stats");
		}
	}
}
