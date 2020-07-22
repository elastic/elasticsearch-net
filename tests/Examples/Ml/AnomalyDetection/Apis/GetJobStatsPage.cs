// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetJobStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-job-stats.asciidoc:370")]
		public void Line370()
		{
			// tag::9298aaf8232a819e79b3bf8471245e98[]
			var response0 = new SearchResponse<object>();
			// end::9298aaf8232a819e79b3bf8471245e98[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/_stats");
		}
	}
}
