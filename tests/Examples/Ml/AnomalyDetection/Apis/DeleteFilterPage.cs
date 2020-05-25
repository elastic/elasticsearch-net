// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteFilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/delete-filter.asciidoc:41")]
		public void Line41()
		{
			// tag::8c5d48252cd6d1ee26a2bb817f89c78e[]
			var response0 = new SearchResponse<object>();
			// end::8c5d48252cd6d1ee26a2bb817f89c78e[]

			response0.MatchesExample(@"DELETE _ml/filters/safe_domains");
		}
	}
}
