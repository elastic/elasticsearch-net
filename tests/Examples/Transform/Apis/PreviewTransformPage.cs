// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class PreviewTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/preview-transform.asciidoc:196")]
		public void Line196()
		{
			// tag::a5ee3f40c34bd913a12e0069b6e42611[]
			var response0 = new SearchResponse<object>();
			// end::a5ee3f40c34bd913a12e0069b6e42611[]

			response0.MatchesExample(@"POST _transform/_preview
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
