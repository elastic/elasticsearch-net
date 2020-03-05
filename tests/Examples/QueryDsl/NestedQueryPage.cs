using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class NestedQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/nested-query.asciidoc:23")]
		public void Line23()
		{
			// tag::c612d93e7f682a0d731e385edf9f5d56[]
			var response0 = new SearchResponse<object>();
			// end::c612d93e7f682a0d731e385edf9f5d56[]

			response0.MatchesExample(@"PUT /my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""obj1"" : {
			                ""type"" : ""nested""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/nested-query.asciidoc:41")]
		public void Line41()
		{
			// tag::6be70810d6ebd6f09d8a49f9df847765[]
			var response0 = new SearchResponse<object>();
			// end::6be70810d6ebd6f09d8a49f9df847765[]

			response0.MatchesExample(@"GET /my_index/_search
			{
			    ""query"":  {
			        ""nested"" : {
			            ""path"" : ""obj1"",
			            ""query"" : {
			                ""bool"" : {
			                    ""must"" : [
			                    { ""match"" : {""obj1.name"" : ""blue""} },
			                    { ""range"" : {""obj1.count"" : {""gt"" : 5}} }
			                    ]
			                }
			            },
			            ""score_mode"" : ""avg""
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/nested-query.asciidoc:133")]
		public void Line133()
		{
			// tag::54092c8c646133f5dbbc047990dd458d[]
			var response0 = new SearchResponse<object>();
			// end::54092c8c646133f5dbbc047990dd458d[]

			response0.MatchesExample(@"PUT /drivers
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""driver"" : {
			                ""type"" : ""nested"",
			                ""properties"" : {
			                    ""last_name"" : {
			                        ""type"" : ""text""
			                    },
			                    ""vehicle"" : {
			                        ""type"" : ""nested"",
			                        ""properties"" : {
			                            ""make"" : {
			                                ""type"" : ""text""
			                            },
			                            ""model"" : {
			                                ""type"" : ""text""
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
		[Description("query-dsl/nested-query.asciidoc:165")]
		public void Line165()
		{
			// tag::873fbbc6ab81409058591385fd602736[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::873fbbc6ab81409058591385fd602736[]

			response0.MatchesExample(@"PUT /drivers/_doc/1
			{
			  ""driver"" : {
			        ""last_name"" : ""McQueen"",
			        ""vehicle"" : [
			            {
			                ""make"" : ""Powell Motors"",
			                ""model"" : ""Canyonero""
			            },
			            {
			                ""make"" : ""Miller-Meteor"",
			                ""model"" : ""Ecto-1""
			            }
			        ]
			    }
			}");

			response1.MatchesExample(@"PUT /drivers/_doc/2?refresh
			{
			  ""driver"" : {
			        ""last_name"" : ""Hudson"",
			        ""vehicle"" : [
			            {
			                ""make"" : ""Mifune"",
			                ""model"" : ""Mach Five""
			            },
			            {
			                ""make"" : ""Miller-Meteor"",
			                ""model"" : ""Ecto-1""
			            }
			        ]
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("query-dsl/nested-query.asciidoc:206")]
		public void Line206()
		{
			// tag::0bd3923424a20a4ba860b0774b9991b1[]
			var response0 = new SearchResponse<object>();
			// end::0bd3923424a20a4ba860b0774b9991b1[]

			response0.MatchesExample(@"GET /drivers/_search
			{
			    ""query"" : {
			        ""nested"" : {
			            ""path"" : ""driver"",
			            ""query"" : {
			                ""nested"" : {
			                    ""path"" :  ""driver.vehicle"",
			                    ""query"" :  {
			                        ""bool"" : {
			                            ""must"" : [
			                                { ""match"" : { ""driver.vehicle.make"" : ""Powell Motors"" } },
			                                { ""match"" : { ""driver.vehicle.model"" : ""Canyonero"" } }
			                            ]
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}