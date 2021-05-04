// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-job.asciidoc:90")]
		public void Line90()
		{
			// tag::86280dcb49aa89083be4b2644daf1b7c[]
			var response0 = new SearchResponse<object>();
			// end::86280dcb49aa89083be4b2644daf1b7c[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/high_sum_total_sales");
		}
	}
}
