// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetDfanalyticsStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/get-dfanalytics-stats.asciidoc:538")]
		public void Line538()
		{
			// tag::dbd2834971fedbe36911933d451ae65d[]
			var response0 = new SearchResponse<object>();
			// end::dbd2834971fedbe36911933d451ae65d[]

			response0.MatchesExample(@"GET _ml/data_frame/analytics/ecommerce/_stats");
		}
	}
}
