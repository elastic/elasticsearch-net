// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.DfAnalytics.Apis
{
	public class GetInferenceTrainedModelStatsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/get-inference-trained-model-stats.asciidoc:152")]
		public void Line152()
		{
			// tag::2dde95ba98c5d5e19725fbb10435d283[]
			var response0 = new SearchResponse<object>();
			// end::2dde95ba98c5d5e19725fbb10435d283[]

			response0.MatchesExample(@"GET _ml/inference/_stats");
		}
	}
}
