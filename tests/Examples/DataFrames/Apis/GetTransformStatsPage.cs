// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class GetTransformStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line105()
		{
			// tag::148bef7f7b2a9c1c2011e4d018c4ae50[]
			var response0 = new SearchResponse<object>();
			// end::148bef7f7b2a9c1c2011e4d018c4ae50[]

			response0.MatchesExample(@"GET _data_frame/transforms/_stats?from=5&size=10");
		}

		[U(Skip = "Example not implemented")]
		public void Line115()
		{
			// tag::d4a2862678b5ef99ec596de1927c3944[]
			var response0 = new SearchResponse<object>();
			// end::d4a2862678b5ef99ec596de1927c3944[]

			response0.MatchesExample(@"GET _data_frame/transforms/ecommerce_transform/_stats");
		}
	}
}
