// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetBucketPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-bucket.asciidoc:178")]
		public void Line178()
		{
			// tag::f96d4614f2fc294339fef325b794355f[]
			var response0 = new SearchResponse<object>();
			// end::f96d4614f2fc294339fef325b794355f[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/results/buckets
			{
			  ""anomaly_score"": 80,
			  ""start"": ""1454530200001""
			}");
		}
	}
}
