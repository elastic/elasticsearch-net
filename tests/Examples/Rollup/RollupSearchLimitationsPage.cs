// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Rollup
{
	public class RollupSearchLimitationsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("rollup/rollup-search-limitations.asciidoc:45")]
		public void Line45()
		{
			// tag::3d1cea1ad861d1ee62e5f34b84371943[]
			var response0 = new SearchResponse<object>();
			// end::3d1cea1ad861d1ee62e5f34b84371943[]

			response0.MatchesExample(@"GET sensor_rollup/_rollup_search
			{
			    ""size"": 0,
			    ""aggregations"": {
			        ""avg_temperature"": {
			            ""avg"": {
			                ""field"": ""temperature""
			            }
			        }
			    }
			}");
		}
	}
}
