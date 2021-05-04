// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateDatafeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-datafeed.asciidoc:115")]
		public void Line115()
		{
			// tag::df6d5b5f8e1c8785503269ccb7b34763[]
			var response0 = new SearchResponse<object>();
			// end::df6d5b5f8e1c8785503269ccb7b34763[]

			response0.MatchesExample(@"POST _ml/datafeeds/datafeed-total-requests/_update
			{
			  ""query"": {
			    ""term"": {
			      ""level"": ""error""
			    }
			  }
			}");
		}
	}
}
