// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-snapshot.asciidoc:44")]
		public void Line44()
		{
			// tag::1e08e054c761353f99211cd18e8ca47b[]
			var response0 = new SearchResponse<object>();
			// end::1e08e054c761353f99211cd18e8ca47b[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/farequote/model_snapshots/1491948163");
		}
	}
}
