// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class PreviewTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::681a67458b6ee3b0ec96ca017c363770[]
			var response0 = new SearchResponse<object>();
			// end::681a67458b6ee3b0ec96ca017c363770[]

			response0.MatchesExample(@"POST _data_frame/transforms/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce""
			  },
			  ""pivot"": {
			    ""group_by"": {
			      ""customer_id"": {
			        ""terms"": {
			          ""field"": ""customer_id""
			        }
			      }
			    },
			    ""aggregations"": {
			      ""max_price"": {
			        ""max"": {
			          ""field"": ""taxful_total_price""
			        }
			      }
			    }
			  }
			}");
		}
	}
}
