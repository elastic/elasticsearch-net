using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping
{
	public class RemovalOfTypesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line458()
		{
			// tag::9ee37bb017ab2f08dc870d9b2f937819[]
			var response0 = new SearchResponse<object>();
			// end::9ee37bb017ab2f08dc870d9b2f937819[]

			response0.MatchesExample(@"PUT index?include_type_name=false
			{
			  ""mappings"": {
			    ""properties"": { \<1>
			      ""foo"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line474()
		{
			// tag::21aedb3425f773979be01722661b6a89[]
			var response0 = new SearchResponse<object>();
			// end::21aedb3425f773979be01722661b6a89[]

			response0.MatchesExample(@"PUT index/_mappings?include_type_name=false
			{
			  ""properties"": { \<1>
			    ""bar"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line489()
		{
			// tag::c81959a312a5715c52cacfe01cb0576e[]
			var response0 = new SearchResponse<object>();
			// end::c81959a312a5715c52cacfe01cb0576e[]

			response0.MatchesExample(@"GET index/_mappings?include_type_name=false");
		}

		[U(Skip = "Example not implemented")]
		public void Line524()
		{
			// tag::ab1b1bdda7528003a08d6d5911081483[]
			var response0 = new SearchResponse<object>();
			// end::ab1b1bdda7528003a08d6d5911081483[]

			response0.MatchesExample(@"PUT index/_doc/1
			{
			  ""foo"": ""baz""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line554()
		{
			// tag::de8d7db07c3008039c7691955a553e4c[]
			var response0 = new SearchResponse<object>();
			// end::de8d7db07c3008039c7691955a553e4c[]

			response0.MatchesExample(@"GET index/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line568()
		{
			// tag::f85d1cf4a5b9145632f585cd8c99e49d[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::f85d1cf4a5b9145632f585cd8c99e49d[]

			response0.MatchesExample(@"POST index/_update/1
			{
			    ""doc"" : {
			        ""foo"" : ""qux""
			    }
			}");

			response1.MatchesExample(@"GET /index/_source/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line586()
		{
			// tag::6dce46ae7f4da2467ea1e68cc9b67b31[]
			var response0 = new SearchResponse<object>();
			// end::6dce46ae7f4da2467ea1e68cc9b67b31[]

			response0.MatchesExample(@"POST _bulk
			{ ""index"" : { ""_index"" : ""index"", ""_id"" : ""3"" } }
			{ ""foo"" : ""baz"" }
			{ ""index"" : { ""_index"" : ""index"", ""_id"" : ""4"" } }
			{ ""foo"" : ""qux"" }");
		}

		[U(Skip = "Example not implemented")]
		public void Line617()
		{
			// tag::b479466cd446f8112f491ce8810de43a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b479466cd446f8112f491ce8810de43a[]

			response0.MatchesExample(@"PUT index/my_type/1
			{
			  ""foo"": ""baz""
			}");

			response1.MatchesExample(@"GET index/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		public void Line661()
		{
			// tag::c6f5467904b8182d9203d98414a1bb76[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::c6f5467904b8182d9203d98414a1bb76[]

			response0.MatchesExample(@"PUT _template/template1
			{
			  ""index_patterns"":[ ""index-1-*"" ],
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT _template/template2?include_type_name=true
			{
			  ""index_patterns"":[ ""index-2-*"" ],
			  ""mappings"": {
			    ""type"": {
			      ""properties"": {
			        ""foo"": {
			          ""type"": ""keyword""
			        }
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT index-1-01?include_type_name=true
			{
			  ""mappings"": {
			    ""type"": {
			      ""properties"": {
			        ""bar"": {
			          ""type"": ""long""
			        }
			      }
			    }
			  }
			}");

			response3.MatchesExample(@"PUT index-2-01
			{
			  ""mappings"": {
			    ""properties"": {
			      ""bar"": {
			        ""type"": ""long""
			      }
			    }
			  }
			}");
		}
	}
}