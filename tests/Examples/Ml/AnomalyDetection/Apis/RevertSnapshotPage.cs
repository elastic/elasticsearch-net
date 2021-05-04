// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class RevertSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/revert-snapshot.asciidoc:62")]
		public void Line62()
		{
			// tag::b173b1b5bab610668ab74d5b2ab03f78[]
			var response0 = new SearchResponse<object>();
			// end::b173b1b5bab610668ab74d5b2ab03f78[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/high_sum_total_sales/model_snapshots/1575402237/_revert
			{
			  ""delete_intervening_results"": true
			}");
		}
	}
}
