using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class TemplatesPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line18()
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

		[U]
		[SkipExample]
		public void Line54()
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

		[U]
		[SkipExample]
		public void Line87()
		{
			// tag::0f0fba0061d26602cd5f401ca4a19be3[]
			var response0 = new SearchResponse<object>();
			// end::0f0fba0061d26602cd5f401ca4a19be3[]

			response0.MatchesExample(@"DELETE /_template/template_1");
		}

		[U]
		[SkipExample]
		public void Line100()
		{
			// tag::02f65c6bab8f40bf3ce18160623d1870[]
			var response0 = new SearchResponse<object>();
			// end::02f65c6bab8f40bf3ce18160623d1870[]

			response0.MatchesExample(@"GET /_template/template_1");
		}

		[U]
		[SkipExample]
		public void Line108()
		{
			// tag::f1a1ce2bbd82b7b2a8df2796cd2f0c98[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::f1a1ce2bbd82b7b2a8df2796cd2f0c98[]

			response0.MatchesExample(@"GET /_template/temp*");

			response1.MatchesExample(@"GET /_template/template_1,template_2");
		}

		[U]
		[SkipExample]
		public void Line117()
		{
			// tag::fd2d289e6b725fcc3cbe8fe7ffe02ea0[]
			var response0 = new SearchResponse<object>();
			// end::fd2d289e6b725fcc3cbe8fe7ffe02ea0[]

			response0.MatchesExample(@"GET /_template");
		}

		[U]
		[SkipExample]
		public void Line129()
		{
			// tag::aea94bf2da993bfde1c73bd552eee2ae[]
			var response0 = new SearchResponse<object>();
			// end::aea94bf2da993bfde1c73bd552eee2ae[]

			response0.MatchesExample(@"HEAD _template/template_1");
		}

		[U]
		[SkipExample]
		public void Line153()
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

		[U]
		[SkipExample]
		public void Line201()
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

		[U]
		[SkipExample]
		public void Line219()
		{
			// tag::46658f00edc4865dfe472a392374cd0f[]
			var response0 = new SearchResponse<object>();
			// end::46658f00edc4865dfe472a392374cd0f[]

			response0.MatchesExample(@"GET /_template/template_1?filter_path=*.version");
		}
	}
}