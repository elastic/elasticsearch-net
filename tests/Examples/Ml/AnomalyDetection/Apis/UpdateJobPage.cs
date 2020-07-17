// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-job.asciidoc:238")]
		public void Line238()
		{
			// tag::421e68e2b9789f0e8c08760d9e685d1c[]
			var response0 = new SearchResponse<object>();
			// end::421e68e2b9789f0e8c08760d9e685d1c[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_update
			{
			  ""description"":""An updated job"",
			  ""detectors"": {
			    ""detector_index"": 0,
			    ""description"": ""An updated detector description""
			  },
			  ""groups"": [""kibana_sample_data"",""kibana_sample_web_logs""],
			  ""model_plot_config"": {
			    ""enabled"": true
			  },
			  ""renormalization_window_days"": 30,
			  ""background_persist_interval"": ""2h"",
			  ""model_snapshot_retention_days"": 7,
			  ""results_retention_days"": 60
			}");
		}
	}
}
