// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class IntervalsQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:28")]
		public void Line28()
		{
			// tag::807c0c9763f8c1114b3c8278c2a0cb56[]
			var response0 = new SearchResponse<object>();
			// end::807c0c9763f8c1114b3c8278c2a0cb56[]

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
			                ""query"" : ""my favorite food"",
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
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:312")]
		public void Line312()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:343")]
		public void Line343()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:374")]
		public void Line374()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:409")]
		public void Line409()
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

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/intervals-query.asciidoc:442")]
		public void Line442()
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
