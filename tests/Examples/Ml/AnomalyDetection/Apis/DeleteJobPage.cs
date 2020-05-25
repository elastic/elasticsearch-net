// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteJobPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-job.asciidoc:60")]
		public void Line60()
		{
			// tag::3ac8b5234e9d53859245cf8ab0094ca5[]
			var response0 = new SearchResponse<object>();
			// end::3ac8b5234e9d53859245cf8ab0094ca5[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-job.asciidoc:77")]
		public void Line77()
		{
			// tag::ccec66fb20d5ede6c691e0890cfe402a[]
			var response0 = new SearchResponse<object>();
			// end::ccec66fb20d5ede6c691e0890cfe402a[]

			response0.MatchesExample(@"DELETE _ml/anomaly_detectors/total-requests?wait_for_completion=false");
		}
	}
}
