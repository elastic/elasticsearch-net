using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class IndexTemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:36")]
		public void Line36()
		{
			// tag::45266650464f16bc156b9057b733a522[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::45266650464f16bc156b9057b733a522[]

			response0.MatchesExample(@"PUT _component_template/component_template1
			{
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": {
			          ""type"": ""date""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT _component_template/other_component_template
			{
			  ""template"": {
			    ""mappings"": {
			      ""properties"": {
			        ""ip_address"": {
			          ""type"": ""ip""
			        }
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT _index_template/template_1
			{
			  ""index_patterns"": [""te*"", ""bar*""],
			  ""template"": {
			    ""settings"": {
			      ""number_of_shards"": 1
			    },
			    ""mappings"": {
			      ""_source"": {
			        ""enabled"": false
			      },
			      ""properties"": {
			        ""host_name"": {
			          ""type"": ""keyword""
			        },
			        ""created_at"": {
			          ""type"": ""date"",
			          ""format"": ""EEE MMM dd HH:mm:ss Z yyyy""
			        }
			      }
			    },
			    ""aliases"": {
			      ""mydata"": { }
			    }
			  },
			  ""priority"": 10,
			  ""composed_of"": [""component_template1"", ""other_component_template""],
			  ""version"": 3,
			  ""_meta"": {
			    ""description"": ""my custom""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:119")]
		public void Line119()
		{
			// tag::0ac2b0c0cbc8e7799c031b019378bfa9[]
			var response0 = new SearchResponse<object>();
			// end::0ac2b0c0cbc8e7799c031b019378bfa9[]

			response0.MatchesExample(@"POST /_index_template/_simulate_index/myindex");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:126")]
		public void Line126()
		{
			// tag::58581b5088a9cf842241b6c6a561b5cf[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::58581b5088a9cf842241b6c6a561b5cf[]

			response0.MatchesExample(@"POST /_index_template/_simulate/template_1");

			response1.MatchesExample(@"POST /_index_template/_simulate
			{
			  ""index_patterns"": [""foo""],
			  ""template"": {
			    ""settings"": {
			      ""number_of_replicas"": 0
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:144")]
		public void Line144()
		{
			// tag::7d12d490a817493a06146081adee1a18[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();
			// end::7d12d490a817493a06146081adee1a18[]

			response0.MatchesExample(@"PUT /_component_template/ct1 <1>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_shards"": 2
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /_component_template/ct2 <2>
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_replicas"": 0
			    },
			    ""mappings"": {
			      ""properties"": {
			        ""@timestamp"": {
			          ""type"": ""date""
			        }
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"PUT /_index_template/final-template <3>
			{
			  ""index_patterns"": [""logdata-*""],
			  ""composed_of"": [""ct1"", ""ct2""],
			  ""priority"": 5
			}");

			response3.MatchesExample(@"POST /_index_template/_simulate_index/logdata-2019-02-01 <4>");

			response4.MatchesExample(@"POST /_index_template/_simulate/final-template <5>");

			response5.MatchesExample(@"POST /_index_template/_simulate <6>
			{
			  ""index_patterns"": [""logdata-*""],
			  ""composed_of"": [""ct2""],
			  ""priority"": 10,
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_replicas"": 1
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:246")]
		public void Line246()
		{
			// tag::73ecdaeca5885a88f9a4273a462196d0[]
			var response0 = new SearchResponse<object>();
			// end::73ecdaeca5885a88f9a4273a462196d0[]

			response0.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"" : [""te*""],
			  ""priority"" : 1,
			  ""template"": {
			    ""settings"" : {
			      ""number_of_shards"" : 2
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:366")]
		public void Line366()
		{
			// tag::ed8ad81604137f4cc8757bb74636b8b4[]
			var response0 = new SearchResponse<object>();
			// end::ed8ad81604137f4cc8757bb74636b8b4[]

			response0.MatchesExample(@"PUT _index_template/template_1
			{
			  ""index_patterns"" : [""te*""],
			  ""template"": {
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""aliases"" : {
			        ""alias1"" : {},
			        ""alias2"" : {
			            ""filter"" : {
			                ""term"" : {""user"" : ""kimchy"" }
			            },
			            ""routing"" : ""kimchy""
			        },
			        ""{index}-alias"" : {} <1>
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:398")]
		public void Line398()
		{
			// tag::62083da06c2cd4b4b62290eda1ec93e0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::62083da06c2cd4b4b62290eda1ec93e0[]

			response0.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"" : [""t*""],
			  ""priority"" : 0,
			  ""template"": {
			    ""settings"" : {
			      ""number_of_shards"" : 1,
			      ""number_of_replicas"": 0
			    },
			    ""mappings"" : {
			      ""_source"" : { ""enabled"" : false }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /_index_template/template_2
			{
			  ""index_patterns"" : [""te*""],
			  ""priority"" : 1,
			  ""template"": {
			    ""settings"" : {
			      ""number_of_shards"" : 2
			    },
			    ""mappings"" : {
			      ""_source"" : { ""enabled"" : true }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:448")]
		public void Line448()
		{
			// tag::9138550002cb26ab64918cce427963b8[]
			var response0 = new SearchResponse<object>();
			// end::9138550002cb26ab64918cce427963b8[]

			response0.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"" : [""foo"", ""bar""],
			  ""priority"" : 0,
			  ""template"": {
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    }
			  },
			  ""version"": 123
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:476")]
		public void Line476()
		{
			// tag::0d8063b484a18f8672fb5ed8712c5c97[]
			var response0 = new SearchResponse<object>();
			// end::0d8063b484a18f8672fb5ed8712c5c97[]

			response0.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"": [""foo"", ""bar""],
			  ""template"": {
			    ""settings"" : {
			        ""number_of_shards"" : 3
			    }
			  },
			  ""_meta"": {
			    ""description"": ""set number of shards to three"",
			    ""serialization"": {
			      ""class"": ""MyIndexTemplate"",
			      ""id"": 17
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:504")]
		public void Line504()
		{
			// tag::196aed02b11def364bab84e455c1a073[]
			var response0 = new SearchResponse<object>();
			// end::196aed02b11def364bab84e455c1a073[]

			response0.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"": [""logs-*""],
			  ""data_stream"": { }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:524")]
		public void Line524()
		{
			// tag::cd38c601ab293a6ec0e2df71d0c96b58[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::cd38c601ab293a6ec0e2df71d0c96b58[]

			response0.MatchesExample(@"PUT /_component_template/template_with_2_shards
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_shards"": 2
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /_component_template/template_with_3_shards
			{
			  ""template"": {
			    ""settings"": {
			      ""index.number_of_shards"": 3
			    }
			  }
			}");

			response2.MatchesExample(@"PUT /_index_template/template_1
			{
			  ""index_patterns"": [""t*""],
			  ""composed_of"": [""template_with_2_shards"", ""template_with_3_shards""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:572")]
		public void Line572()
		{
			// tag::3759ca688c4bd3c838780a9aad63258b[]
			var response0 = new SearchResponse<object>();
			// end::3759ca688c4bd3c838780a9aad63258b[]

			response0.MatchesExample(@"GET /_index_template/template_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:608")]
		public void Line608()
		{
			// tag::275ec358d5d1e4b9ff06cb4ae7e47650[]
			var response0 = new SearchResponse<object>();
			// end::275ec358d5d1e4b9ff06cb4ae7e47650[]

			response0.MatchesExample(@"GET /_index_template/temp*");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/index-templates.asciidoc:617")]
		public void Line617()
		{
			// tag::3b40db1c5c6b36f087d7a09a4ce285c6[]
			var response0 = new SearchResponse<object>();
			// end::3b40db1c5c6b36f087d7a09a4ce285c6[]

			response0.MatchesExample(@"GET /_index_template");
		}
	}
}