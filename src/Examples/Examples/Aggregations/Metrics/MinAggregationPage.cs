using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Metrics
{
	public class MinAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::bbd52c02b078e650f1a871f7fe7ff343[]
			var response0 = new SearchResponse<object>();
			// end::bbd52c02b078e650f1a871f7fe7ff343[]

			response0.MatchesExample(@"POST /sales/_search?size=0
			{
			    ""aggs"" : {
			        ""min_price"" : { ""min"" : { ""field"" : ""price"" } }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line52()
		{
			// tag::27cf2556b606f91d1fe3db3d7b6fd21a[]
			var response0 = new SearchResponse<object>();
			// end::27cf2556b606f91d1fe3db3d7b6fd21a[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""min_price"" : {
			            ""min"" : {
			                ""script"" : {
			                    ""source"" : ""doc.price.value""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line72()
		{
			// tag::f76eb7821cb7855339ffcaab3460d934[]
			var response0 = new SearchResponse<object>();
			// end::f76eb7821cb7855339ffcaab3460d934[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""min_price"" : {
			            ""min"" : {
			                ""script"" : {
			                    ""id"": ""my_script"",
			                    ""params"": {
			                        ""field"": ""price""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line99()
		{
			// tag::57ec3af2f4b3ce90722de51efc9d2cf1[]
			var response0 = new SearchResponse<object>();
			// end::57ec3af2f4b3ce90722de51efc9d2cf1[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""min_price_in_euros"" : {
			            ""min"" : {
			                ""field"" : ""price"",
			                ""script"" : {
			                    ""source"" : ""_value * params.conversion_rate"",
			                    ""params"" : {
			                        ""conversion_rate"" : 1.2
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line126()
		{
			// tag::05161bf816a98dd2a57b8cd2a3d39db4[]
			var response0 = new SearchResponse<object>();
			// end::05161bf816a98dd2a57b8cd2a3d39db4[]

			response0.MatchesExample(@"POST /sales/_search
			{
			    ""aggs"" : {
			        ""grade_min"" : {
			            ""min"" : {
			                ""field"" : ""grade"",
			                ""missing"": 10 \<1>
			            }
			        }
			    }
			}");
		}
	}
}