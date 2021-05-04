// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Examples.Models;
using System.ComponentModel;
using Nest;

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
			var putIndexTemplateResponse = client.Indices.PutTemplate("template_1", p => p
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
			// tag::9efac5b23bf23de8d81a7455905e2979[]
			var templateResponse1 = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("te*")
				.Order(0)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.SourceField(so => so
						.Enabled(false)
					)
				)
			);

			var templateResponse2 = client.Indices.PutTemplate("template_2", t => t
				.IndexPatterns("tes*")
				.Order(1)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Map(m => m
					.SourceField(so => so
						.Enabled()
					)
				)
			);
			// end::9efac5b23bf23de8d81a7455905e2979[]

			templateResponse1.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""te*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : false }
			    }
			}", e => e.AdjustIndexSettings());

			templateResponse2.MatchesExample(@"PUT /_template/template_2
			{
			    ""index_patterns"" : [""tes*""],
			    ""order"" : 1,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""mappings"" : {
			        ""_source"" : { ""enabled"" : true }
			    }
			}", e => e.AdjustIndexSettings());
		}

		[U]
		[Description("indices/templates.asciidoc:231")]
		public void Line231()
		{
			// tag::8dcc74dc01f26e853e3b3dfa458b1ad7[]
			var templateResponse = client.Indices.PutTemplate("template_1", t => t
				.IndexPatterns("myindex-*")
				.Order(0)
				.Settings(s => s
					.NumberOfShards(1)
				)
				.Version(123)
			);
			// end::8dcc74dc01f26e853e3b3dfa458b1ad7[]

			templateResponse.MatchesExample(@"PUT /_template/template_1
			{
			    ""index_patterns"" : [""myindex-*""],
			    ""order"" : 0,
			    ""settings"" : {
			        ""number_of_shards"" : 1
			    },
			    ""version"": 123
			}", e => e.AdjustIndexSettings());
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
