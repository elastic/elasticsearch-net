using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class AliasesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::b4392116f2cc57ce8064ccbad30318d5[]
			var response0 = new SearchResponse<object>();
			// end::b4392116f2cc57ce8064ccbad30318d5[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line30()
		{
			// tag::3653567181f43a5f64c74f934aa821c2[]
			var response0 = new SearchResponse<object>();
			// end::3653567181f43a5f64c74f934aa821c2[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""remove"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line46()
		{
			// tag::5a51ead3c0398ecb12bdb5456fd70ab9[]
			var response0 = new SearchResponse<object>();
			// end::5a51ead3c0398ecb12bdb5456fd70ab9[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""remove"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } },
			        { ""add"" : { ""index"" : ""test2"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::f0e21e03a07c8fa0209b0aafdb3791e6[]
			var response0 = new SearchResponse<object>();
			// end::f0e21e03a07c8fa0209b0aafdb3791e6[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } },
			        { ""add"" : { ""index"" : ""test2"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::5f210f74725ea0c9265190346edfa246[]
			var response0 = new SearchResponse<object>();
			// end::5f210f74725ea0c9265190346edfa246[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""indices"" : [""test1"", ""test2""], ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line95()
		{
			// tag::6799d132c1c7ca3970763acde2337ef9[]
			var response0 = new SearchResponse<object>();
			// end::6799d132c1c7ca3970763acde2337ef9[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""test*"", ""alias"" : ""all_test_indices"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line115()
		{
			// tag::de176bc4788ea286fff9e92418a43ea8[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::de176bc4788ea286fff9e92418a43ea8[]

			response0.MatchesExample(@"PUT test     \<1>");

			response1.MatchesExample(@"PUT test_2   \<2>");

			response2.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"":  { ""index"": ""test_2"", ""alias"": ""test"" } },
			        { ""remove_index"": { ""index"": ""test"" } }  \<3>
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line144()
		{
			// tag::23ab0f1023b1b2cd5cdf2a8f9ccfd57b[]
			var response0 = new SearchResponse<object>();
			// end::23ab0f1023b1b2cd5cdf2a8f9ccfd57b[]

			response0.MatchesExample(@"PUT /test1
			{
			  ""mappings"": {
			    ""properties"": {
			      ""user"" : {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line161()
		{
			// tag::7cf71671859be7c1ecf673396db377cd[]
			var response0 = new SearchResponse<object>();
			// end::7cf71671859be7c1ecf673396db377cd[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test1"",
			                 ""alias"" : ""alias2"",
			                 ""filter"" : { ""term"" : { ""user"" : ""kimchy"" } }
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line191()
		{
			// tag::bc1ad5cc6d3eab98e3ce01f209ba7094[]
			var response0 = new SearchResponse<object>();
			// end::bc1ad5cc6d3eab98e3ce01f209ba7094[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""routing"" : ""1""
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line212()
		{
			// tag::fa0f4485cd48f986b7ae8cbb24e331c4[]
			var response0 = new SearchResponse<object>();
			// end::fa0f4485cd48f986b7ae8cbb24e331c4[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias2"",
			                 ""search_routing"" : ""1,2"",
			                 ""index_routing"" : ""2""
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line239()
		{
			// tag::427f6b5c5376cbf0f71f242a60ca3d9e[]
			var response0 = new SearchResponse<object>();
			// end::427f6b5c5376cbf0f71f242a60ca3d9e[]

			response0.MatchesExample(@"GET /alias2/_search?q=user:kimchy&routing=2,3");
		}

		[U(Skip = "Example not implemented")]
		public void Line262()
		{
			// tag::f6d6889667f56b8f49d2858070571a6b[]
			var response0 = new SearchResponse<object>();
			// end::f6d6889667f56b8f49d2858070571a6b[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : true
			            }
			        },
			        {
			            ""add"" : {
			                 ""index"" : ""test2"",
			                 ""alias"" : ""alias1""
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line289()
		{
			// tag::b0ec418bf416c62bed602b0a32a6d5f5[]
			var response0 = new SearchResponse<object>();
			// end::b0ec418bf416c62bed602b0a32a6d5f5[]

			response0.MatchesExample(@"PUT /alias1/_doc/1
			{
			    ""foo"": ""bar""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line302()
		{
			// tag::67bba546d835bca8f31df13e3587c348[]
			var response0 = new SearchResponse<object>();
			// end::67bba546d835bca8f31df13e3587c348[]

			response0.MatchesExample(@"GET /test/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line312()
		{
			// tag::ad79228630684d950fe9792a768d24c5[]
			var response0 = new SearchResponse<object>();
			// end::ad79228630684d950fe9792a768d24c5[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        {
			            ""add"" : {
			                 ""index"" : ""test"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : false
			            }
			        }, {
			            ""add"" : {
			                 ""index"" : ""test2"",
			                 ""alias"" : ""alias1"",
			                 ""is_write_index"" : true
			            }
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line369()
		{
			// tag::591adf1aaf016b9c382990923e37d099[]
			var response0 = new SearchResponse<object>();
			// end::591adf1aaf016b9c382990923e37d099[]

			response0.MatchesExample(@"PUT /logs_201305/_alias/2013");
		}

		[U(Skip = "Example not implemented")]
		public void Line382()
		{
			// tag::890f659cfc10ff8171420809bdcf7c67[]
			var response0 = new SearchResponse<object>();
			// end::890f659cfc10ff8171420809bdcf7c67[]

			response0.MatchesExample(@"PUT /users
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""user_id"" : {""type"" : ""integer""}
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line397()
		{
			// tag::83b2785e63357ab3ade51d8ec0c11917[]
			var response0 = new SearchResponse<object>();
			// end::83b2785e63357ab3ade51d8ec0c11917[]

			response0.MatchesExample(@"PUT /users/_alias/user_12
			{
			    ""routing"" : ""12"",
			    ""filter"" : {
			        ""term"" : {
			            ""user_id"" : 12
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line420()
		{
			// tag::e47c3557fe8ccfc4286155c4b72e7c76[]
			var response0 = new SearchResponse<object>();
			// end::e47c3557fe8ccfc4286155c4b72e7c76[]

			response0.MatchesExample(@"PUT /logs_20162801
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""year"" : {""type"" : ""integer""}
			        }
			    },
			    ""aliases"" : {
			        ""current_day"" : {},
			        ""2016"" : {
			            ""filter"" : {
			                ""term"" : {""year"" : 2016 }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line456()
		{
			// tag::72501997d053c29aa0b66a6bb6fb4105[]
			var response0 = new SearchResponse<object>();
			// end::72501997d053c29aa0b66a6bb6fb4105[]

			response0.MatchesExample(@"DELETE /logs_20162801/_alias/current_day");
		}

		[U(Skip = "Example not implemented")]
		public void Line495()
		{
			// tag::4c0ac18976e4d95d23b6890ea9129a7e[]
			var response0 = new SearchResponse<object>();
			// end::4c0ac18976e4d95d23b6890ea9129a7e[]

			response0.MatchesExample(@"GET /logs_20162801/_alias/*");
		}

		[U(Skip = "Example not implemented")]
		public void Line524()
		{
			// tag::0e98b8cb47ce75336068c6d914b86495[]
			var response0 = new SearchResponse<object>();
			// end::0e98b8cb47ce75336068c6d914b86495[]

			response0.MatchesExample(@"GET /_alias/2016");
		}

		[U(Skip = "Example not implemented")]
		public void Line553()
		{
			// tag::56aa1bff647d1db49dabf175c1e56919[]
			var response0 = new SearchResponse<object>();
			// end::56aa1bff647d1db49dabf175c1e56919[]

			response0.MatchesExample(@"GET /_alias/20*");
		}

		[U(Skip = "Example not implemented")]
		public void Line584()
		{
			// tag::d8aa6ff25f7bb56e32d02df455103e53[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::d8aa6ff25f7bb56e32d02df455103e53[]

			response0.MatchesExample(@"HEAD /_alias/2016");

			response1.MatchesExample(@"HEAD /_alias/20*");

			response2.MatchesExample(@"HEAD /logs_20162801/_alias/*");
		}
	}
}