// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetMlInfoPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-ml-info.asciidoc:41")]
		public void Line41()
		{
			// tag::4d7c0b52d3c0a084157428624c543c90[]
			var response0 = new SearchResponse<object>();
			// end::4d7c0b52d3c0a084157428624c543c90[]

			response0.MatchesExample(@"GET _ml/info");
		}
	}
}
