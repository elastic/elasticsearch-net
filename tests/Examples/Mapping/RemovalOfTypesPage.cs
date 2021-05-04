// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Mapping
{
	public class RemovalOfTypesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/removal_of_types.asciidoc:458")]
		public void Line458()
		{
			// tag::cfb7654fa2e29c83807da58f77b27599[]
			var response0 = new SearchResponse<object>();
			// end::cfb7654fa2e29c83807da58f77b27599[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": { <1>
			      ""foo"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/removal_of_types.asciidoc:474")]
		public void Line474()
		{
			// tag::11c9b5d511fd839f4affeb018d17200d[]
			var response0 = new SearchResponse<object>();
			// end::11c9b5d511fd839f4affeb018d17200d[]

			response0.MatchesExample(@"PUT index/_mappings
			{
			  ""properties"": { <1>
			    ""bar"": {
			      ""type"": ""text""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/removal_of_types.asciidoc:489")]
		public void Line489()
		{
			// tag::0afeb69cc1474ac6d35223e615dc8c91[]
			var response0 = new SearchResponse<object>();
			// end::0afeb69cc1474ac6d35223e615dc8c91[]

			response0.MatchesExample(@"GET index/_mappings");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/removal_of_types.asciidoc:523")]
		public void Line523()
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
		[Description("mapping/removal_of_types.asciidoc:550")]
		public void Line550()
		{
			// tag::de8d7db07c3008039c7691955a553e4c[]
			var response0 = new SearchResponse<object>();
			// end::de8d7db07c3008039c7691955a553e4c[]

			response0.MatchesExample(@"GET index/_doc/1");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/removal_of_types.asciidoc:563")]
		public void Line563()
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
		[Description("mapping/removal_of_types.asciidoc:580")]
		public void Line580()
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
		[Description("mapping/removal_of_types.asciidoc:612")]
		public void Line612()
		{
			// tag::a1114fcb15a01180db9918231e495bbc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::a1114fcb15a01180db9918231e495bbc[]

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

			response1.MatchesExample(@"PUT index-1-01
			{
			  ""mappings"": {
			    ""properties"": {
			      ""bar"": {
			        ""type"": ""long""
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT index-2-01
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
