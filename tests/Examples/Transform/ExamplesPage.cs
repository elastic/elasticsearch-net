// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Transform
{
	public class ExamplesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("transform/examples.asciidoc:29")]
		public void Line29()
		{
			// tag::88341b4eba71ec722f3e38fa1696fe87[]
			var response0 = new SearchResponse<object>();
			// end::88341b4eba71ec722f3e38fa1696fe87[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_ecommerce""
			  },
			  ""dest"" : { <1>
			    ""index"" : ""sample_ecommerce_orders_by_customer""
			  },
			  ""pivot"": {
			    ""group_by"": { <2>
			      ""user"": { ""terms"": { ""field"": ""user"" }},
			      ""customer_id"": { ""terms"": { ""field"": ""customer_id"" }}
			    },
			    ""aggregations"": {
			      ""order_count"": { ""value_count"": { ""field"": ""order_id"" }},
			      ""total_order_amt"": { ""sum"": { ""field"": ""taxful_total_price"" }},
			      ""avg_amt_per_order"": { ""avg"": { ""field"": ""taxful_total_price"" }},
			      ""avg_unique_products_per_order"": { ""avg"": { ""field"": ""total_unique_products"" }},
			      ""total_unique_products"": { ""cardinality"": { ""field"": ""products.product_id"" }}
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/examples.asciidoc:115")]
		public void Line115()
		{
			// tag::be9376b1e354ad9c6bdad83f6a0ce5ad[]
			var response0 = new SearchResponse<object>();
			// end::be9376b1e354ad9c6bdad83f6a0ce5ad[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_flights"",
			    ""query"": { <1>
			      ""bool"": {
			        ""filter"": [
			          { ""term"":  { ""Cancelled"": false } }
			        ]
			      }
			    }
			  },
			  ""dest"" : { <2>
			    ""index"" : ""sample_flight_delays_by_carrier""
			  },
			  ""pivot"": {
			    ""group_by"": { <3>
			      ""carrier"": { ""terms"": { ""field"": ""Carrier"" }}
			    },
			    ""aggregations"": {
			      ""flights_count"": { ""value_count"": { ""field"": ""FlightNum"" }},
			      ""delay_mins_total"": { ""sum"": { ""field"": ""FlightDelayMin"" }},
			      ""flight_mins_total"": { ""sum"": { ""field"": ""FlightTimeMin"" }},
			      ""delay_time_percentage"": { <4>
			        ""bucket_script"": {
			          ""buckets_path"": {
			            ""delay_time"": ""delay_mins_total.value"",
			            ""flight_time"": ""flight_mins_total.value""
			          },
			          ""script"": ""(params.delay_time / params.flight_time) * 100""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/examples.asciidoc:202")]
		public void Line202()
		{
			// tag::7bc36ea4a24937de6c009397861baf05[]
			var response0 = new SearchResponse<object>();
			// end::7bc36ea4a24937de6c009397861baf05[]

			response0.MatchesExample(@"PUT _transform/suspicious_client_ips
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_logs""
			  },
			  ""dest"" : { <1>
			    ""index"" : ""sample_weblogs_by_clientip""
			  },
			  ""sync"" : { <2>
			    ""time"": {
			      ""field"": ""timestamp"",
			      ""delay"": ""60s""
			    }
			  },
			  ""pivot"": {
			    ""group_by"": {  <3>
			      ""clientip"": { ""terms"": { ""field"": ""clientip"" } }
			      },
			    ""aggregations"": {
			      ""url_dc"": { ""cardinality"": { ""field"": ""url.keyword"" }},
			      ""bytes_sum"": { ""sum"": { ""field"": ""bytes"" }},
			      ""geo.src_dc"": { ""cardinality"": { ""field"": ""geo.src"" }},
			      ""agent_dc"": { ""cardinality"": { ""field"": ""agent.keyword"" }},
			      ""geo.dest_dc"": { ""cardinality"": { ""field"": ""geo.dest"" }},
			      ""responses.total"": { ""value_count"": { ""field"": ""timestamp"" }},
			      ""success"" : { <4>
			         ""filter"": {
			            ""term"": { ""response"" : ""200""}}
			        },
			      ""error404"" : {
			         ""filter"": {
			            ""term"": { ""response"" : ""404""}}
			        },
			      ""error503"" : {
			         ""filter"": {
			            ""term"": { ""response"" : ""503""}}
			        },
			      ""timestamp.min"": { ""min"": { ""field"": ""timestamp"" }},
			      ""timestamp.max"": { ""max"": { ""field"": ""timestamp"" }},
			      ""timestamp.duration_ms"": { <5>
			        ""bucket_script"": {
			          ""buckets_path"": {
			            ""min_time"": ""timestamp.min.value"",
			            ""max_time"": ""timestamp.max.value""
			          },
			          ""script"": ""(params.max_time - params.min_time)""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/examples.asciidoc:272")]
		public void Line272()
		{
			// tag::4bd42e31ac4a5cf237777f1a0e97aba8[]
			var response0 = new SearchResponse<object>();
			// end::4bd42e31ac4a5cf237777f1a0e97aba8[]

			response0.MatchesExample(@"POST _transform/suspicious_client_ips/_start");
		}

		[U(Skip = "Example not implemented")]
		[Description("transform/examples.asciidoc:282")]
		public void Line282()
		{
			// tag::14f2dab0583c5a9fcc39931d33194872[]
			var response0 = new SearchResponse<object>();
			// end::14f2dab0583c5a9fcc39931d33194872[]

			response0.MatchesExample(@"GET sample_weblogs_by_clientip/_search");
		}
	}
}
