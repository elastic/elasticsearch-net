// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetOverallBucketsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-overall-buckets.asciidoc:124")]
		public void Line124()
		{
			// tag::e48e7da65c2b32d724fd7e3bfa175c6f[]
			var response0 = new SearchResponse<object>();
			// end::e48e7da65c2b32d724fd7e3bfa175c6f[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/job-*/results/overall_buckets
			{
			  ""overall_score"": 80,
			  ""start"": ""1403532000000""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-overall-buckets.asciidoc:169")]
		public void Line169()
		{
			// tag::405db6f3a01eceacfaa8b0ed3e4b3ac2[]
			var response0 = new SearchResponse<object>();
			// end::405db6f3a01eceacfaa8b0ed3e4b3ac2[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/job-*/results/overall_buckets
			{
			  ""top_n"": 2,
			  ""overall_score"": 50.0,
			  ""start"": ""1403532000000""
			}");
		}
	}
}
