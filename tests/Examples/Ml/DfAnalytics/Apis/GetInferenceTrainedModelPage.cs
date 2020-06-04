// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetInferenceTrainedModelPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/get-inference-trained-model.asciidoc:170")]
		public void Line170()
		{
			// tag::f97c7791a0dd23aad5f96fd38ec7d12e[]
			var response0 = new SearchResponse<object>();
			// end::f97c7791a0dd23aad5f96fd38ec7d12e[]

			response0.MatchesExample(@"GET _ml/inference/");
		}
	}
}
