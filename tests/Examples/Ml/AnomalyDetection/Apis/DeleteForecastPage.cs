// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteForecastPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-forecast.asciidoc:71")]
		public void Line71()
		{
			// tag::eb4e43b47867b54214a8630172dd0e21[]
			var response0 = new SearchResponse<object>();
			// end::eb4e43b47867b54214a8630172dd0e21[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests/_forecast/_all");
		}
	}
}
