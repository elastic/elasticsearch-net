// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class GetTransformStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform-stats.asciidoc:267")]
		public void Line267()
		{
			// tag::53c6256295111524d5ff2885bdcb99a9[]
			var response0 = new SearchResponse<object>();
			// end::53c6256295111524d5ff2885bdcb99a9[]

			response0.MatchesExample(@"GET _transform/_stats?from=5&size=10");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/apis/get-transform-stats.asciidoc:275")]
		public void Line275()
		{
			// tag::0755471d7dce4785d2e7ed0c10182ea3[]
			var response0 = new SearchResponse<object>();
			// end::0755471d7dce4785d2e7ed0c10182ea3[]

			response0.MatchesExample(@"GET _transform/ecommerce-customer-transform/_stats");
		}
	}
}
