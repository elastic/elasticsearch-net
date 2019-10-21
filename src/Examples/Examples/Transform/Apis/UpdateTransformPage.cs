using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Transform.Apis
{
	public class UpdateTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line124()
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