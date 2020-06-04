// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;

namespace Examples.Indices
{
	public class TemplatesPage : ExampleBase
	{
		[U]
		[Description("indices/templates.asciidoc:20")]
		public void Line20()
		{
			// tag::e5f50b31f165462d883ecbff45f74985[]
			var putIndexTemplateResponse = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("te*", "bar*")
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.SourceField(s => s.Enabled(false))
					.Properties(p => p
						.Keyword(k => k.Name("host_name"))
						.Date(d => d.Name("created_at").Format("EEE MMM dd HH:mm:ss Z yyyy"))
					)
				)
			);
			// end::e5f50b31f165462d883ecbff45f74985[]

			putIndexTemplateResponse.MatchesExample(@"PUT _template/template_1
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
			}", e =>
			{
				e.AdjustIndexSettings();
			});
		}

		[U]
		[Description("indices/templates.asciidoc:146")]
		public void Line146()
		{
			// tag::1b8caf0a6741126c6d0ad83b56fce290[]
			var putIndexTemplateResponse = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("te*")
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Aliases(a => a
					.Alias("alias1")
					.Alias("alias2", aa => aa
						.Filter<Tweet>(f => f
							.Term(t => t.User, "kimchy")
						)
						.Routing("kimchy")
					)
					.Alias("{index}-alias")
				)
			);
			// end::1b8caf0a6741126c6d0ad83b56fce290[]

			putIndexTemplateResponse.MatchesExample(@"PUT _template/template_1
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
			}", e =>
			{
				e.AdjustIndexSettings();
				e.ApplyBodyChanges(o => o["aliases"]["alias2"]["filter"]["term"]["user"].ToLongFormTermQuery());
			});
		}

		[U]
		[Description("indices/templates.asciidoc:180")]
		public void Line180()
		{
			// tag::b5f95bc097a201b29c7200fc8d3d31c1[]
			var putIndexTemplateResponse1 = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("*")
				.Order(0)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.SourceField(s => s.Enabled(false))
				)
			);

			var putIndexTemplateResponse2 = client.Indices.PutTemplate("template_2", t => t
				.IndexPatterns("te*")
				.Order(1)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.SourceField(s => s.Enabled(true))
				)
			);
			// end::b5f95bc097a201b29c7200fc8d3d31c1[]

			putIndexTemplateResponse1.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : false }
			    }
			}", e =>
			{
				e.AdjustIndexSettings();
			});

			putIndexTemplateResponse2.MatchesExample(@"PUT /_template/template_2
			{
			    ""index_patterns"" : [""te*""],
			    ""order"" : 1,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : true }
			    }
			}", e =>
			{
				e.AdjustIndexSettings();
			});
		}

		[U]
		[Description("indices/templates.asciidoc:231")]
		public void Line231()
		{
			// tag::9166cf38427d5cde5d2ec12a2012b669[]
			var putIndexTemplateResponse1 = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("*")
				.Order(0)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Version(123)
			);
			// end::9166cf38427d5cde5d2ec12a2012b669[]

			putIndexTemplateResponse1.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""version"": 123
			}", e =>
			{
				e.AdjustIndexSettings();
			});
		}

		[U]
		[Description("indices/templates.asciidoc:249")]
		public void Line249()
		{
			// tag::46658f00edc4865dfe472a392374cd0f[]
			var getIndexTemplateResponse = client.Indices.GetTemplate("template_1", t => t.FilterPath(new[] { "*.version" }));
			// end::46658f00edc4865dfe472a392374cd0f[]

			getIndexTemplateResponse.MatchesExample(@"GET /_template/template_1?filter_path=*.version");
		}
	}
}
