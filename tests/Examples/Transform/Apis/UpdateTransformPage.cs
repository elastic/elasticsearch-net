// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform.Apis
{
	public class UpdateTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/apis/update-transform.asciidoc:183")]
		public void Line183()
		{
			// tag::27384266370152add76471dd0332a2f1[]
			var response0 = new SearchResponse<object>();
			// end::27384266370152add76471dd0332a2f1[]

			response0.MatchesExample(@"POST _transform/simple-kibana-ecomm-pivot/_update
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce"",
			    ""query"": {
			      ""term"": {
			        ""geoip.continent_name"": {
			          ""value"": ""Asia""
			        }
			      }
			    }
			  },
			  ""description"": ""Maximum priced ecommerce data by customer_id in Asia"",
			  ""dest"": {
			    ""index"": ""kibana_sample_data_ecommerce_transform_v2"",
			    ""pipeline"": ""add_timestamp_pipeline""
			  },
			  ""frequency"": ""15m"",
			  ""sync"": {
			    ""time"": {
			      ""field"": ""order_date"",
			      ""delay"": ""120s""
			    }
			  }
			}");
		}
	}
}
