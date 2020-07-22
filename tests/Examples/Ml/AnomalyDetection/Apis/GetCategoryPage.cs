// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetCategoryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-category.asciidoc:130")]
		public void Line130()
		{
			// tag::e8f1c9ee003d115ec8f55e57990df6e4[]
			var response0 = new SearchResponse<object>();
			// end::e8f1c9ee003d115ec8f55e57990df6e4[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/esxi_log/results/categories
			{
			  ""page"":{
			    ""size"": 1
			  }
			}");
		}
	}
}
