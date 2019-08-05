using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.QueryDsl
{
	public class IntervalsQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line20()
		{
			// tag::5d59e61b35103a17e262a625503f896b[]
			var response0 = new SearchResponse<object>();
			// end::5d59e61b35103a17e262a625503f896b[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""all_of"" : {
			          ""ordered"" : true,
			          ""intervals"" : [
			            {
			              ""match"" : {
			                ""query"" : ""my favourite food"",
			                ""max_gaps"" : 0,
			                ""ordered"" : true
			              }
			            },
			            {
			              ""any_of"" : {
			                ""intervals"" : [
			                  { ""match"" : { ""query"" : ""hot water"" } },
			                  { ""match"" : { ""query"" : ""cold porridge"" } }
			                ]
			              }
			            }
			          ]
			        },
			        ""_name"" : ""favourite_food""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line175()
		{
			// tag::7471e97aaaf21c3a200abdd89f15c3cc[]
			var response0 = new SearchResponse<object>();
			// end::7471e97aaaf21c3a200abdd89f15c3cc[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""match"" : {
			          ""query"" : ""hot porridge"",
			          ""max_gaps"" : 10,
			          ""filter"" : {
			            ""not_containing"" : {
			              ""match"" : {
			                ""query"" : ""salty""
			              }
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line226()
		{
			// tag::2de6885bacb8769b8f22dce253c96b0c[]
			var response0 = new SearchResponse<object>();
			// end::2de6885bacb8769b8f22dce253c96b0c[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""match"" : {
			          ""query"" : ""hot porridge"",
			          ""filter"" : {
			            ""script"" : {
			              ""source"" : ""interval.start > 10 && interval.end < 20 && interval.gaps == 0""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line257()
		{
			// tag::e2a22c6fd58cc0becf4c383134a08f8b[]
			var response0 = new SearchResponse<object>();
			// end::e2a22c6fd58cc0becf4c383134a08f8b[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""match"" : {
			          ""query"" : ""salty"",
			          ""filter"" : {
			            ""contained_by"" : {
			              ""match"" : {
			                ""query"" : ""hot porridge""
			              }
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line293()
		{
			// tag::5f79c42b0f74fdf71359cef82843fad3[]
			var response0 = new SearchResponse<object>();
			// end::5f79c42b0f74fdf71359cef82843fad3[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""all_of"" : {
			          ""intervals"" : [
			            { ""match"" : { ""query"" : ""the"" } },
			            { ""any_of"" : {
			                ""intervals"" : [
			                    { ""match"" : { ""query"" : ""big"" } },
			                    { ""match"" : { ""query"" : ""big bad"" } }
			                ] } },
			            { ""match"" : { ""query"" : ""wolf"" } }
			          ],
			          ""max_gaps"" : 0,
			          ""ordered"" : true
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line327()
		{
			// tag::e7811867397b305efbbe8925d8a01c1a[]
			var response0 = new SearchResponse<object>();
			// end::e7811867397b305efbbe8925d8a01c1a[]

			response0.MatchesExample(@"POST _search
			{
			  ""query"": {
			    ""intervals"" : {
			      ""my_text"" : {
			        ""any_of"" : {
			          ""intervals"" : [
			            { ""match"" : {
			                ""query"" : ""the big bad wolf"",
			                ""ordered"" : true,
			                ""max_gaps"" : 0 } },
			            { ""match"" : {
			                ""query"" : ""the big wolf"",
			                ""ordered"" : true,
			                ""max_gaps"" : 0 } }
			           ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}