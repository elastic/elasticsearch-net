using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class TemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::e5f50b31f165462d883ecbff45f74985[]
			var response0 = new SearchResponse<object>();
			// end::e5f50b31f165462d883ecbff45f74985[]

			response0.MatchesExample(@"PUT _template/template_1
			{
			  ""index_patterns"": [""te*"", ""bar*""],
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
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line138()
		{
			// tag::1b8caf0a6741126c6d0ad83b56fce290[]
			var response0 = new SearchResponse<object>();
			// end::1b8caf0a6741126c6d0ad83b56fce290[]

			response0.MatchesExample(@"PUT _template/template_1
			{
			    ""index_patterns"" : [""te*""],
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
			        ""{index}-alias"" : {} \<1>
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line172()
		{
			// tag::b5f95bc097a201b29c7200fc8d3d31c1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::b5f95bc097a201b29c7200fc8d3d31c1[]

			response0.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : false }
			    }
			}");

			response1.MatchesExample(@"PUT /_template/template_2
			{
			    ""index_patterns"" : [""te*""],
			    ""order"" : 1,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : true }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line223()
		{
			// tag::9166cf38427d5cde5d2ec12a2012b669[]
			var response0 = new SearchResponse<object>();
			// end::9166cf38427d5cde5d2ec12a2012b669[]

			response0.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""version"": 123
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line241()
		{
			// tag::46658f00edc4865dfe472a392374cd0f[]
			var response0 = new SearchResponse<object>();
			// end::46658f00edc4865dfe472a392374cd0f[]

			response0.MatchesExample(@"GET /_template/template_1?filter_path=*.version");
		}
	}
}