// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-snapshot.asciidoc:204")]
		public void Line204()
		{
			// tag::c873f9cd093e26515148f052e28c7805[]
			var response0 = new SearchResponse<object>();
			// end::c873f9cd093e26515148f052e28c7805[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/high_sum_total_sales/model_snapshots
			{
			  ""start"": ""1575402236000""
			}");
		}
	}
}
