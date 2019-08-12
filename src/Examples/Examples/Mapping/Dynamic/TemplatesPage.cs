using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Dynamic
{
	public class TemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line71()
		{
			// tag::bb33e638fdeded7d721d9bbac2305fda[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::bb33e638fdeded7d721d9bbac2305fda[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""integers"": {
			          ""match_mapping_type"": ""long"",
			          ""mapping"": {
			            ""type"": ""integer""
			          }
			        }
			      },
			      {
			        ""strings"": {
			          ""match_mapping_type"": ""string"",
			          ""mapping"": {
			            ""type"": ""text"",
			            ""fields"": {
			              ""raw"": {
			                ""type"":  ""keyword"",
			                ""ignore_above"": 256
			              }
			            }
			          }
			        }
			      }
			    ]
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_integer"": 5, \<1>
			  ""my_string"": ""Some string"" \<2>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line125()
		{
			// tag::4f54b88e05c7a62901062e9e0ed13e5a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4f54b88e05c7a62901062e9e0ed13e5a[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""longs_as_strings"": {
			          ""match_mapping_type"": ""string"",
			          ""match"":   ""long_*"",
			          ""unmatch"": ""*_text"",
			          ""mapping"": {
			            ""type"": ""long""
			          }
			        }
			      }
			    ]
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""long_num"": ""5"", \<1>
			  ""long_text"": ""foo"" \<2>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line179()
		{
			// tag::0b91c082258ce623cc716b679aace653[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0b91c082258ce623cc716b679aace653[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""full_name"": {
			          ""path_match"":   ""name.*"",
			          ""path_unmatch"": ""*.middle"",
			          ""mapping"": {
			            ""type"":       ""text"",
			            ""copy_to"":    ""full_name""
			          }
			        }
			      }
			    ]
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""name"": {
			    ""first"":  ""John"",
			    ""middle"": ""Winston"",
			    ""last"":   ""Lennon""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line215()
		{
			// tag::be51ed37c8425d281a8153abe56b04cb[]
			var response0 = new SearchResponse<object>();
			// end::be51ed37c8425d281a8153abe56b04cb[]

			response0.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""name"": {
			    ""first"":  ""Paul"",
			    ""last"":   ""McCartney"",
			    ""title"": {
			      ""value"": ""Sir"",
			      ""category"": ""order of chivalry""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line241()
		{
			// tag::6873971eb4e4577d76d0a5bd7cd15ef9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::6873971eb4e4577d76d0a5bd7cd15ef9[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""named_analyzers"": {
			          ""match_mapping_type"": ""string"",
			          ""match"": ""*"",
			          ""mapping"": {
			            ""type"": ""text"",
			            ""analyzer"": ""{name}""
			          }
			        }
			      },
			      {
			        ""no_doc_values"": {
			          ""match_mapping_type"":""*"",
			          ""mapping"": {
			            ""type"": ""{dynamic_type}"",
			            ""doc_values"": false
			          }
			        }
			      }
			    ]
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""english"": ""Some English text"", \<1>
			  ""count"":   5 \<2>
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line293()
		{
			// tag::87f85bb49d18f73d0eed0b704e05eb90[]
			var response0 = new SearchResponse<object>();
			// end::87f85bb49d18f73d0eed0b704e05eb90[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""strings_as_keywords"": {
			          ""match_mapping_type"": ""string"",
			          ""mapping"": {
			            ""type"": ""keyword""
			          }
			        }
			      }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line322()
		{
			// tag::1a59fa2708ccb3a24c71e8306b81f17f[]
			var response0 = new SearchResponse<object>();
			// end::1a59fa2708ccb3a24c71e8306b81f17f[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""strings_as_text"": {
			          ""match_mapping_type"": ""string"",
			          ""mapping"": {
			            ""type"": ""text""
			          }
			        }
			      }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line348()
		{
			// tag::3e60c0b29bd3931927e6f2ee7d2ed0ef[]
			var response0 = new SearchResponse<object>();
			// end::3e60c0b29bd3931927e6f2ee7d2ed0ef[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""strings_as_keywords"": {
			          ""match_mapping_type"": ""string"",
			          ""mapping"": {
			            ""type"": ""text"",
			            ""norms"": false,
			            ""fields"": {
			              ""keyword"": {
			                ""type"": ""keyword"",
			                ""ignore_above"": 256
			              }
			            }
			          }
			        }
			      }
			    ]
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line387()
		{
			// tag::9a91f7d0bf52d6c582c62daef5c9d040[]
			var response0 = new SearchResponse<object>();
			// end::9a91f7d0bf52d6c582c62daef5c9d040[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_templates"": [
			      {
			        ""unindexed_longs"": {
			          ""match_mapping_type"": ""long"",
			          ""mapping"": {
			            ""type"": ""long"",
			            ""index"": false
			          }
			        }
			      },
			      {
			        ""unindexed_doubles"": {
			          ""match_mapping_type"": ""double"",
			          ""mapping"": {
			            ""type"": ""float"", \<1>
			            ""index"": false
			          }
			        }
			      }
			    ]
			  }
			}");
		}
	}
}