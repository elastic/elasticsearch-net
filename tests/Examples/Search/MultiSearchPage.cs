// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class MultiSearchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:10")]
		public void Line10()
		{
			// tag::58a2a71be35e770b50914753740ec91e[]
			var response0 = new SearchResponse<object>();
			// end::58a2a71be35e770b50914753740ec91e[]

			response0.MatchesExample(@"GET twitter/_msearch
			{ }
			{""query"" : {""match"" : { ""message"": ""this is a test""}}}
			{""index"": ""twitter2""}
			{""query"" : {""match_all"" : {}}}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:301")]
		public void Line301()
		{
			// tag::05af5eab63bf98d0078dfe661cd81124[]
			var response0 = new SearchResponse<object>();
			// end::05af5eab63bf98d0078dfe661cd81124[]

			response0.MatchesExample(@"GET twitter/_msearch
			{}
			{""query"" : {""match_all"" : {}}, ""from"" : 0, ""size"" : 10}
			{}
			{""query"" : {""match_all"" : {}}}
			{""index"" : ""twitter2""}
			{""query"" : {""match_all"" : {}}}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:334")]
		public void Line334()
		{
			// tag::a914be2ff7dd0cbdec0257f0ad50b625[]
			var response0 = new SearchResponse<object>();
			// end::a914be2ff7dd0cbdec0257f0ad50b625[]

			response0.MatchesExample(@"GET _msearch/template
			{""index"" : ""twitter""}
			{ ""source"" : ""{ \""query\"": { \""match\"": { \""message\"" : \""{{keywords}}\"" } } } }"", ""params"": { ""query_type"": ""match"", ""keywords"": ""some message"" } }
			{""index"" : ""twitter""}
			{ ""source"" : ""{ \""query\"": { \""match_{{template}}\"": {} } }"", ""params"": { ""template"": ""all"" } }");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:347")]
		public void Line347()
		{
			// tag::28e66ff0ecdd71cb1426880115eab5dd[]
			var response0 = new SearchResponse<object>();
			// end::28e66ff0ecdd71cb1426880115eab5dd[]

			response0.MatchesExample(@"POST /_scripts/my_template_1
			{
			    ""script"": {
			        ""lang"": ""mustache"",
			        ""source"": {
			            ""query"": {
			                ""match"": {
			                    ""message"": ""{{query_string}}""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:366")]
		public void Line366()
		{
			// tag::72e72cb3aa1b10b903d8cadcaddf7d10[]
			var response0 = new SearchResponse<object>();
			// end::72e72cb3aa1b10b903d8cadcaddf7d10[]

			response0.MatchesExample(@"POST /_scripts/my_template_2
			{
			    ""script"": {
			        ""lang"": ""mustache"",
			        ""source"": {
			            ""query"": {
			                ""term"": {
			                    ""{{field}}"": ""{{value}}""
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/multi-search.asciidoc:386")]
		public void Line386()
		{
			// tag::8b4c8f395c0a6f952a42051a0d357154[]
			var response0 = new SearchResponse<object>();
			// end::8b4c8f395c0a6f952a42051a0d357154[]

			response0.MatchesExample(@"GET _msearch/template
			{""index"" : ""main""}
			{ ""id"": ""my_template_1"", ""params"": { ""query_string"": ""some message"" } }
			{""index"" : ""main""}
			{ ""id"": ""my_template_2"", ""params"": { ""field"": ""user"", ""value"": ""test"" } }");
		}
	}
}
