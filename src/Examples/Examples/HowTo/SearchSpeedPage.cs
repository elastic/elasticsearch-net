using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.HowTo
{
	public class SearchSpeedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line52()
		{
			// tag::12facf3617a41551ce2f0c4d005cb1c7[]
			var response0 = new SearchResponse<object>();
			// end::12facf3617a41551ce2f0c4d005cb1c7[]

			response0.MatchesExample(@"PUT movies
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name_and_plot"": {
			        ""type"": ""text""
			      },
			      ""name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""name_and_plot""
			      },
			      ""plot"": {
			        ""type"": ""text"",
			        ""copy_to"": ""name_and_plot""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line87()
		{
			// tag::a008f42379930edc354b4074e0a33344[]
			var response0 = new SearchResponse<object>();
			// end::a008f42379930edc354b4074e0a33344[]

			response0.MatchesExample(@"PUT index/_doc/1
			{
			  ""designation"": ""spoon"",
			  ""price"": 13
			}");
		}

		[U]
		[SkipExample]
		public void Line99()
		{
			// tag::a0a7557bb7e2aff7918557cd648f41af[]
			var response0 = new SearchResponse<object>();
			// end::a0a7557bb7e2aff7918557cd648f41af[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""aggs"": {
			    ""price_ranges"": {
			      ""range"": {
			        ""field"": ""price"",
			        ""ranges"": [
			          { ""to"": 10 },
			          { ""from"": 10, ""to"": 100 },
			          { ""from"": 100 }
			        ]
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line123()
		{
			// tag::a4bae4d956bc0a663f42cfec36bf8e0b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a4bae4d956bc0a663f42cfec36bf8e0b[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""price_range"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT index/_doc/1
			{
			  ""designation"": ""spoon"",
			  ""price"": 13,
			  ""price_range"": ""10-100""
			}");
		}

		[U]
		[SkipExample]
		public void Line148()
		{
			// tag::7dedb148ff74912de81b8f8275f0d7f3[]
			var response0 = new SearchResponse<object>();
			// end::7dedb148ff74912de81b8f8275f0d7f3[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""aggs"": {
			    ""price_ranges"": {
			      ""terms"": {
			        ""field"": ""price_range""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line192()
		{
			// tag::102c7de25d13c87cf28839ada9f63c95[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::102c7de25d13c87cf28839ada9f63c95[]

			response0.MatchesExample(@"PUT index/_doc/1
			{
			  ""my_date"": ""2016-05-11T16:30:55.328Z""
			}");

			response1.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""range"": {
			          ""my_date"": {
			            ""gte"": ""now-1h"",
			            ""lte"": ""now""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line219()
		{
			// tag::17dd67a66c49f7eb618dd17430e48dfa[]
			var response0 = new SearchResponse<object>();
			// end::17dd67a66c49f7eb618dd17430e48dfa[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""range"": {
			          ""my_date"": {
			            ""gte"": ""now-1h/m"",
			            ""lte"": ""now/m""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line253()
		{
			// tag::abc7a670a47516b58b6b07d7497b140c[]
			var response0 = new SearchResponse<object>();
			// end::abc7a670a47516b58b6b07d7497b140c[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""bool"": {
			          ""should"": [
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gte"": ""now-1h"",
			                  ""lte"": ""now-1h/m""
			                }
			              }
			            },
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gt"": ""now-1h/m"",
			                  ""lt"": ""now/m""
			                }
			              }
			            },
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gte"": ""now/m"",
			                  ""lte"": ""now""
			                }
			              }
			            }
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line326()
		{
			// tag::971c7a36ee79f2b3aa82c64ea338de70[]
			var response0 = new SearchResponse<object>();
			// end::971c7a36ee79f2b3aa82c64ea338de70[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""keyword"",
			        ""eager_global_ordinals"": true
			      }
			    }
			  }
			}");
		}
	}
}