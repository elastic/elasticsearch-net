using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Indices
{
	public class ComponentTemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:12")]
		public void Line12()
		{
			// tag::e784fc00894635470adfd78a0c46b427[]
			var response0 = new SearchResponse<object>();
			// end::e784fc00894635470adfd78a0c46b427[]

			response0.MatchesExample(@"PUT _component_template/template_1
			{
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
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:88")]
		public void Line88()
		{
			// tag::827b7e9308ea288f18aea00a5accc38e[]
			var response0 = new SearchResponse<object>();
			// end::827b7e9308ea288f18aea00a5accc38e[]

			response0.MatchesExample(@"GET /_component_template/template_1");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:124")]
		public void Line124()
		{
			// tag::6c72460570307f23478100db04a84c8e[]
			var response0 = new SearchResponse<object>();
			// end::6c72460570307f23478100db04a84c8e[]

			response0.MatchesExample(@"GET /_component_template/temp*");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:133")]
		public void Line133()
		{
			// tag::dd4f051ab62f0507e3b6e3d6f333e85f[]
			var response0 = new SearchResponse<object>();
			// end::dd4f051ab62f0507e3b6e3d6f333e85f[]

			response0.MatchesExample(@"GET /_component_template");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:187")]
		public void Line187()
		{
			// tag::07694ab6343fcb4fea2859d17ae5ae7e[]
			var response0 = new SearchResponse<object>();
			// end::07694ab6343fcb4fea2859d17ae5ae7e[]

			response0.MatchesExample(@"PUT _component_template/template_1
			{
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
		[Description("indices/component-templates.asciidoc:228")]
		public void Line228()
		{
			// tag::c6339d09f85000a6432304b0ec63b8f6[]
			var response0 = new SearchResponse<object>();
			// end::c6339d09f85000a6432304b0ec63b8f6[]

			response0.MatchesExample(@"PUT /_component_template/template_1
			{
			  ""template"": {
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    }
			  },
			  ""version"": 123
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/component-templates.asciidoc:254")]
		public void Line254()
		{
			// tag::5b2a13366bd4e1ab4b25d04d360570dc[]
			var response0 = new SearchResponse<object>();
			// end::5b2a13366bd4e1ab4b25d04d360570dc[]

			response0.MatchesExample(@"PUT /_component_template/template_1
			{
			  ""template"": {
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    }
			  },
			  ""_meta"": {
			    ""description"": ""set number of shards to one"",
			    ""serialization"": {
			      ""class"": ""MyComponentTemplate"",
			      ""id"": 10
			    }
			  }
			}");
		}
	}
}