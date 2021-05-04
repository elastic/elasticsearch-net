// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class PathhierarchyTokenizerExamplesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:18")]
		public void Line18()
		{
			// tag::840b6c5c3d9c56aed854cfab8da04486[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();
			// end::840b6c5c3d9c56aed854cfab8da04486[]

			response0.MatchesExample(@"PUT file-path-test
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""custom_path_tree"": {
			          ""tokenizer"": ""custom_hierarchy""
			        },
			        ""custom_path_tree_reversed"": {
			          ""tokenizer"": ""custom_hierarchy_reversed""
			        }
			      },
			      ""tokenizer"": {
			        ""custom_hierarchy"": {
			          ""type"": ""path_hierarchy"",
			          ""delimiter"": ""/""
			        },
			        ""custom_hierarchy_reversed"": {
			          ""type"": ""path_hierarchy"",
			          ""delimiter"": ""/"",
			          ""reverse"": ""true""
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""file_path"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""tree"": {
			            ""type"": ""text"",
			            ""analyzer"": ""custom_path_tree""
			          },
			          ""tree_reversed"": {
			            ""type"": ""text"",
			            ""analyzer"": ""custom_path_tree_reversed""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST file-path-test/_doc/1
			{
			  ""file_path"": ""/User/alice/photos/2017/05/16/my_photo1.jpg""
			}");

			response2.MatchesExample(@"POST file-path-test/_doc/2
			{
			  ""file_path"": ""/User/alice/photos/2017/05/16/my_photo2.jpg""
			}");

			response3.MatchesExample(@"POST file-path-test/_doc/3
			{
			  ""file_path"": ""/User/alice/photos/2017/05/16/my_photo3.jpg""
			}");

			response4.MatchesExample(@"POST file-path-test/_doc/4
			{
			  ""file_path"": ""/User/alice/photos/2017/05/15/my_photo1.jpg""
			}");

			response5.MatchesExample(@"POST file-path-test/_doc/5
			{
			  ""file_path"": ""/User/bob/photos/2017/05/16/my_photo1.jpg""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:97")]
		public void Line97()
		{
			// tag::bd767ea03171fe71c73f58f16d5da92f[]
			var response0 = new SearchResponse<object>();
			// end::bd767ea03171fe71c73f58f16d5da92f[]

			response0.MatchesExample(@"GET file-path-test/_search
			{
			  ""query"": {
			    ""match"": {
			      ""file_path"": ""/User/bob/photos/2017/05""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:112")]
		public void Line112()
		{
			// tag::b724f547c5d67e95bbc0a9920e47033c[]
			var response0 = new SearchResponse<object>();
			// end::b724f547c5d67e95bbc0a9920e47033c[]

			response0.MatchesExample(@"GET file-path-test/_search
			{
			  ""query"": {
			    ""term"": {
			      ""file_path.tree"": ""/User/alice/photos/2017/05/16""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:131")]
		public void Line131()
		{
			// tag::f1dc6f69453867ffafe86e998dd464d9[]
			var response0 = new SearchResponse<object>();
			// end::f1dc6f69453867ffafe86e998dd464d9[]

			response0.MatchesExample(@"GET file-path-test/_search
			{
			  ""query"": {
			    ""term"": {
			      ""file_path.tree_reversed"": {
			        ""value"": ""my_photo1.jpg""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:149")]
		public void Line149()
		{
			// tag::acc52da725a996ae696b00d9f818dfde[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::acc52da725a996ae696b00d9f818dfde[]

			response0.MatchesExample(@"POST file-path-test/_analyze
			{
			  ""analyzer"": ""custom_path_tree"",
			  ""text"": ""/User/alice/photos/2017/05/16/my_photo1.jpg""
			}");

			response1.MatchesExample(@"POST file-path-test/_analyze
			{
			  ""analyzer"": ""custom_path_tree_reversed"",
			  ""text"": ""/User/alice/photos/2017/05/16/my_photo1.jpg""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer-examples.asciidoc:169")]
		public void Line169()
		{
			// tag::4bba59cf745ac7b996bf90308bc26957[]
			var response0 = new SearchResponse<object>();
			// end::4bba59cf745ac7b996bf90308bc26957[]

			response0.MatchesExample(@"GET file-path-test/_search
			{
			  ""query"": {
			    ""bool"" : {
			      ""must"" : {
			        ""match"" : { ""file_path"" : ""16"" }
			      },
			      ""filter"": {
			        ""term"" : { ""file_path.tree"" : ""/User/alice"" }
			      }
			    }
			  }
			}");
		}
	}
}
