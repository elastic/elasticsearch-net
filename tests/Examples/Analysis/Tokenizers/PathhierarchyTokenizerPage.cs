// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenizers
{
	public class PathhierarchyTokenizerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:15")]
		public void Line15()
		{
			// tag::dc4dcfeae8a5f248639335c2c9809549[]
			var response0 = new SearchResponse<object>();
			// end::dc4dcfeae8a5f248639335c2c9809549[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""tokenizer"": ""path_hierarchy"",
			  ""text"": ""/one/two/three""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:96")]
		public void Line96()
		{
			// tag::fcc35d56dff0291bcf3663830ce99254[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fcc35d56dff0291bcf3663830ce99254[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""my_tokenizer""
			        }
			      },
			      ""tokenizer"": {
			        ""my_tokenizer"": {
			          ""type"": ""path_hierarchy"",
			          ""delimiter"": ""-"",
			          ""replacement"": ""/"",
			          ""skip"": 2
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""one-two-three-four-five""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:191")]
		public void Line191()
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
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:269")]
		public void Line269()
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
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:285")]
		public void Line285()
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
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:305")]
		public void Line305()
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
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:324")]
		public void Line324()
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
		[Description("analysis/tokenizers/pathhierarchy-tokenizer.asciidoc:345")]
		public void Line345()
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
