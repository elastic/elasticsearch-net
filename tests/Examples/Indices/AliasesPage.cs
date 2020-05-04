// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class AliasesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/aliases.asciidoc:12")]
		public void Line12()
		{
			// tag::d3016e4e8025362ad9a05ee86bb2061f[]
			var response0 = new SearchResponse<object>();
			// end::d3016e4e8025362ad9a05ee86bb2061f[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""add"" : { ""index"" : ""twitter"", ""alias"" : ""alias1"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/aliases.asciidoc:162")]
		public void Line162()
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
		[Description("indices/aliases.asciidoc:178")]
		public void Line178()
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
		[Description("indices/aliases.asciidoc:196")]
		public void Line196()
		{
			// tag::af3fb9fa5691a7b37a6dc2a69ff66e64[]
			var response0 = new SearchResponse<object>();
			// end::af3fb9fa5691a7b37a6dc2a69ff66e64[]

			response0.MatchesExample(@"POST /_aliases
			{
			    ""actions"" : [
			        { ""remove"" : { ""index"" : ""test1"", ""alias"" : ""alias1"" } },
			        { ""add"" : { ""index"" : ""test1"", ""alias"" : ""alias2"" } }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/aliases.asciidoc:214")]
		public void Line214()
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
		[Description("indices/aliases.asciidoc:228")]
		public void Line228()
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
		[Description("indices/aliases.asciidoc:245")]
		public void Line245()
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
		[Description("indices/aliases.asciidoc:264")]
		public void Line264()
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
		[Description("indices/aliases.asciidoc:292")]
		public void Line292()
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
		[Description("indices/aliases.asciidoc:308")]
		public void Line308()
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
		[Description("indices/aliases.asciidoc:336")]
		public void Line336()
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
		[Description("indices/aliases.asciidoc:356")]
		public void Line356()
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
		[Description("indices/aliases.asciidoc:382")]
		public void Line382()
		{
			// tag::427f6b5c5376cbf0f71f242a60ca3d9e[]
			var response0 = new SearchResponse<object>();
			// end::427f6b5c5376cbf0f71f242a60ca3d9e[]

			response0.MatchesExample(@"GET /alias2/_search?q=user:kimchy&routing=2,3");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/aliases.asciidoc:403")]
		public void Line403()
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
		[Description("indices/aliases.asciidoc:429")]
		public void Line429()
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
		[Description("indices/aliases.asciidoc:441")]
		public void Line441()
		{
			// tag::67bba546d835bca8f31df13e3587c348[]
			var response0 = new SearchResponse<object>();
			// end::67bba546d835bca8f31df13e3587c348[]

			response0.MatchesExample(@"GET /test/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/aliases.asciidoc:450")]
		public void Line450()
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
	}
}