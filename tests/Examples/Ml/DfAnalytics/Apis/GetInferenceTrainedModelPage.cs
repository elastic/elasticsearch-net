using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetInferenceTrainedModelPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/get-inference-trained-model.asciidoc:108")]
		public void Line108()
		{
			// tag::f97c7791a0dd23aad5f96fd38ec7d12e[]
			var response0 = new SearchResponse<object>();
			// end::f97c7791a0dd23aad5f96fd38ec7d12e[]

			response0.MatchesExample(@"GET _ml/inference/");
		}
	}
}