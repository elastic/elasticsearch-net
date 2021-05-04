// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-datafeed.asciidoc:45")]
		public void Line45()
		{
			// tag::8a12cd824404d74f098d854716a26899[]
			var response0 = new SearchResponse<object>();
			// end::8a12cd824404d74f098d854716a26899[]

			response0.MatchesExample(@"DELETE _ml/datafeeds/datafeed-total-requests");
		}
	}
}
