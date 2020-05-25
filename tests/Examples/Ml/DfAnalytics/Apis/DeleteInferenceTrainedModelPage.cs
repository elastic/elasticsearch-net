// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DeleteInferenceTrainedModelPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/delete-inference-trained-model.asciidoc:57")]
		public void Line57()
		{
			// tag::334e28ff99f12b721b9942bad3a78f94[]
			var response0 = new SearchResponse<object>();
			// end::334e28ff99f12b721b9942bad3a78f94[]

			response0.MatchesExample(@"DELETE _ml/inference/regression-job-one-1574775307356");
		}
	}
}
