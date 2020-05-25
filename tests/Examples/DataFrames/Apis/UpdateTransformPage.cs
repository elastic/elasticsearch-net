// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class UpdateTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line125()
		{
			// tag::f61fdfcbebde925d253f455e23f4bf25[]
			var response0 = new SearchResponse<object>();
			// end::f61fdfcbebde925d253f455e23f4bf25[]

			response0.MatchesExample(@"POST _data_frame/transforms/simple-kibana-ecomm-pivot/_update
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
