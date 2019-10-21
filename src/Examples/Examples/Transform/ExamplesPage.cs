using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Transform
{
	public class ExamplesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line31()
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
		public void Line117()
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
		public void Line209()
		{
			// tag::18beab68b15a44fc9bcbc5c00700376d[]
			var response0 = new SearchResponse<object>();
			// end::18beab68b15a44fc9bcbc5c00700376d[]

			response0.MatchesExample(@"POST _transform/_preview
			{
			  ""source"": {
			    ""index"": ""kibana_sample_data_logs"",
			    ""query"": { <1>
			      ""range"" : {
			        ""timestamp"" : {
			          ""gte"" : ""now-30d/d""
			        }
			      }
			    }
			  },
			  ""dest"" : { <2>
			    ""index"" : ""sample_weblogs_by_clientip""
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
			      ""responses.counts"": { <4>
			        ""scripted_metric"": {
			          ""init_script"": ""state.responses = ['error':0L,'success':0L,'other':0L]"",
			          ""map_script"": """"""
			            def code = doc['response.keyword'].value;
			            if (code.startsWith('5') || code.startsWith('4')) {
			              state.responses.error += 1 ;
			            } else if(code.startsWith('2')) {
			              state.responses.success += 1;
			            } else {
			              state.responses.other += 1;
			            }
			            """""",
			          ""combine_script"": ""state.responses"",
			          ""reduce_script"": """"""
			            def counts = ['error': 0L, 'success': 0L, 'other': 0L];
			            for (responses in states) {
			              counts.error += responses['error'];
			              counts.success += responses['success'];
			              counts.other += responses['other'];
			            }
			            return counts;
			            """"""
			          }
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
	}
}