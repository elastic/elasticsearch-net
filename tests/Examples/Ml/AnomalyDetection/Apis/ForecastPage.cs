// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class ForecastPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/forecast.asciidoc:76")]
		public void Line76()
		{
			// tag::591c7fb7451069829a14bba593136f1f[]
			var response0 = new SearchResponse<object>();
			// end::591c7fb7451069829a14bba593136f1f[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/low_request_rate/_forecast
			{
			  ""duration"": ""10d""
			}");
		}
	}
}
