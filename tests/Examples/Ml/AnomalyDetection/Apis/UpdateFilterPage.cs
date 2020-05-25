// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateFilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-filter.asciidoc:46")]
		public void Line46()
		{
			// tag::4d21725453955582ff12b4a1104aa7b6[]
			var response0 = new SearchResponse<object>();
			// end::4d21725453955582ff12b4a1104aa7b6[]

			response0.MatchesExample(@"POST _ml/filters/safe_domains/_update
			{
			  ""description"": ""Updated list of domains"",
			  ""add_items"": [""*.myorg.com""],
			  ""remove_items"": [""wikipedia.org""]
			}");
		}
	}
}
