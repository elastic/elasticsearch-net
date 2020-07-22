// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class PutJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/put-job.asciidoc:298")]
		public void Line298()
		{
			// tag::9c11e238772d67dbc9d273776de9916c[]
			var response0 = new SearchResponse<object>();
			// end::9c11e238772d67dbc9d273776de9916c[]

			response0.MatchesExample(@"PUT _ml/anomaly_detectors/total-requests
			{
			  ""description"" : ""Total sum of requests"",
			  ""analysis_config"" : {
			    ""bucket_span"":""10m"",
			    ""detectors"": [
			      {
			        ""detector_description"": ""Sum of total"",
			        ""function"": ""sum"",
			        ""field_name"": ""total""
			      }
			    ]
			  },
			  ""data_description"" : {
			    ""time_field"":""timestamp"",
			    ""time_format"": ""epoch_ms""
			  }
			}");
		}
	}
}
