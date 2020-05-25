// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Examples.DataFrames.Apis
{
	public class PutTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line143()
		{
			// tag::6e2c969573581cc1c4ae83f59ef8d2f0[]
			var response0 = new SearchResponse<object>();
			// end::6e2c969573581cc1c4ae83f59ef8d2f0[]

			response0.MatchesExample(@"PUT _data_frame/transforms/ecommerce_transform
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
			  },
			  ""description"": ""Maximum priced ecommerce data by customer_id in Asia"",
			  ""dest"": {
			    ""index"": ""kibana_sample_data_ecommerce_transform"",
			    ""pipeline"": ""add_timestamp_pipeline""
			  },
			  ""frequency"": ""5m"",
			  ""sync"": {
			    ""time"": {
			      ""field"": ""order_date"",
			      ""delay"": ""60s""
			    }
			  }
			}");
		}
	}
}
