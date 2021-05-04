// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetRecordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/get-record.asciidoc:202")]
		public void Line202()
		{
			// tag::20e3b181114e00c943a27a9bbcf85f15[]
			var response0 = new SearchResponse<object>();
			// end::20e3b181114e00c943a27a9bbcf85f15[]

			response0.MatchesExample(@"GET _ml/anomaly_detectors/low_request_rate/results/records
			{
			  ""sort"": ""record_score"",
			  ""desc"": true,
			  ""start"": ""1454944100000""
			}");
		}
	}
}
