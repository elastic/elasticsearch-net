using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Aggregations.Pipeline
{
	public class MovfnAggregationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line42()
		{
			// tag::5903a75a28cec4b60c54662457c6d405[]
			var response0 = new SearchResponse<object>();
			// end::5903a75a28cec4b60c54662457c6d405[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{                \<1>
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" } \<2>
			                },
			                ""the_movfn"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"", \<3>
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.unweightedAvg(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line144()
		{
			// tag::93c9711ee6c0554cd775c013c3837f13[]
			var response0 = new SearchResponse<object>();
			// end::93c9711ee6c0554cd775c013c3837f13[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""return values.length > 0 ? values[0] : Double.NaN""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line215()
		{
			// tag::13fd394e3e9a3398cac21ac1064fc154[]
			var response0 = new SearchResponse<object>();
			// end::13fd394e3e9a3398cac21ac1064fc154[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_moving_max"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.max(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line258()
		{
			// tag::c8bebf3c45fc9e75e161bf4e516a957a[]
			var response0 = new SearchResponse<object>();
			// end::c8bebf3c45fc9e75e161bf4e516a957a[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_moving_min"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.min(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line301()
		{
			// tag::d0897840a5702b4ec0616e6c90acfe1e[]
			var response0 = new SearchResponse<object>();
			// end::d0897840a5702b4ec0616e6c90acfe1e[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_moving_sum"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.sum(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line346()
		{
			// tag::46c4d95fc06cd0eb0401caa1e0bdc8f0[]
			var response0 = new SearchResponse<object>();
			// end::46c4d95fc06cd0eb0401caa1e0bdc8f0[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_moving_sum"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.stdDev(values, MovingFunctions.unweightedAvg(values))""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line398()
		{
			// tag::beea9d59a7cbe53d5d4c4ec2a49487b2[]
			var response0 = new SearchResponse<object>();
			// end::beea9d59a7cbe53d5d4c4ec2a49487b2[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.unweightedAvg(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line444()
		{
			// tag::bbbbe980b6dcd2a77ff16cc8a081e472[]
			var response0 = new SearchResponse<object>();
			// end::bbbbe980b6dcd2a77ff16cc8a081e472[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.linearWeightedAvg(values)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line496()
		{
			// tag::d84ea140bbe8abfb156a72c1c963ea00[]
			var response0 = new SearchResponse<object>();
			// end::d84ea140bbe8abfb156a72c1c963ea00[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.ewma(values, 0.3)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line554()
		{
			// tag::76fc9f5a879772ffcc4ec0c99bf74277[]
			var response0 = new SearchResponse<object>();
			// end::76fc9f5a879772ffcc4ec0c99bf74277[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""MovingFunctions.holt(values, 0.3, 0.1)""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line620()
		{
			// tag::af25b173c8bcc73a3bfbfddacb218478[]
			var response0 = new SearchResponse<object>();
			// end::af25b173c8bcc73a3bfbfddacb218478[]

			response0.MatchesExample(@"POST /_search
			{
			    ""size"": 0,
			    ""aggs"": {
			        ""my_date_histo"":{
			            ""date_histogram"":{
			                ""field"":""date"",
			                ""calendar_interval"":""1M""
			            },
			            ""aggs"":{
			                ""the_sum"":{
			                    ""sum"":{ ""field"": ""price"" }
			                },
			                ""the_movavg"": {
			                    ""moving_fn"": {
			                        ""buckets_path"": ""the_sum"",
			                        ""window"": 10,
			                        ""script"": ""if (values.length > 5*2) {MovingFunctions.holtWinters(values, 0.3, 0.1, 0.1, 5, false)}""
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}