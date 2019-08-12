using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line105()
		{
			// tag::d51232b6f1be5730519ca7733b3232df[]
			var response0 = new SearchResponse<object>();
			// end::d51232b6f1be5730519ca7733b3232df[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/total-requests/_update
			{
			  ""description"":""An updated job"",
			  ""groups"": [""group1"",""group2""],
			  ""model_plot_config"": {
			    ""enabled"": true
			  },
			  ""analysis_limits"": {
			    ""model_memory_limit"": ""1024mb""
			  },
			  ""renormalization_window_days"": 30,
			  ""background_persist_interval"": ""2h"",
			  ""model_snapshot_retention_days"": 7,
			  ""results_retention_days"": 60,
			  ""custom_settings"": {
			    ""custom_urls"" : [{
			      ""url_name"" : ""Lookup IP"",
			      ""url_value"" : ""http://geoiplookup.net/ip/$clientip$""
			    }]
			  }
			}");
		}
	}
}