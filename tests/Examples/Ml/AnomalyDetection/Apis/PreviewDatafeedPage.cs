// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PreviewDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/preview-datafeed.asciidoc:53")]
		public void Line53()
		{
			// tag::38eed000de433b540116928681c520d3[]
			var response0 = new SearchResponse<object>();
			// end::38eed000de433b540116928681c520d3[]

			response0.MatchesExample(@"GET _ml/datafeeds/datafeed-high_sum_total_sales/_preview");
		}
	}
}
