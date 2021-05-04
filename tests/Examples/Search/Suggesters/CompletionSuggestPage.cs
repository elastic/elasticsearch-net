// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Suggesters
{
	public class CompletionSuggestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:28")]
		public void Line28()
		{
			// tag::b8718ca915bbb848925a5fb593a03e70[]
			var response0 = new SearchResponse<object>();
			// end::b8718ca915bbb848925a5fb593a03e70[]

			response0.MatchesExample(@"PUT music
			{
			    ""mappings"": {
			        ""properties"" : {
			            ""suggest"" : {
			                ""type"" : ""completion""
			            },
			            ""title"" : {
			                ""type"": ""keyword""
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:84")]
		public void Line84()
		{
			// tag::223787a2b80e132a22548768ccf7052d[]
			var response0 = new SearchResponse<object>();
			// end::223787a2b80e132a22548768ccf7052d[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			    ""suggest"" : {
			        ""input"": [ ""Nevermind"", ""Nirvana"" ],
			        ""weight"" : 34
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:120")]
		public void Line120()
		{
			// tag::5e9f3b7246f4549624fa5b9dd3719d75[]
			var response0 = new SearchResponse<object>();
			// end::5e9f3b7246f4549624fa5b9dd3719d75[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			    ""suggest"" : [
			        {
			            ""input"": ""Nevermind"",
			            ""weight"" : 10
			        },
			        {
			            ""input"": ""Nirvana"",
			            ""weight"" : 3
			        }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:141")]
		public void Line141()
		{
			// tag::7c3414279d47e9c29105d061ed316ef8[]
			var response0 = new SearchResponse<object>();
			// end::7c3414279d47e9c29105d061ed316ef8[]

			response0.MatchesExample(@"PUT music/_doc/1?refresh
			{
			  ""suggest"" : [ ""Nevermind"", ""Nirvana"" ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:158")]
		public void Line158()
		{
			// tag::7f951981bd8ed09e56aebeb13adb96ce[]
			var response0 = new SearchResponse<object>();
			// end::7f951981bd8ed09e56aebeb13adb96ce[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nir"", \<1>
			            ""completion"" : { \<2>
			                ""field"" : ""suggest"" \<3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:226")]
		public void Line226()
		{
			// tag::565ef4aad0c7765879325cc5d2e3c530[]
			var response0 = new SearchResponse<object>();
			// end::565ef4aad0c7765879325cc5d2e3c530[]

			response0.MatchesExample(@"POST music/_search
			{
			    ""_source"": ""suggest"", \<1>
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nir"",
			            ""completion"" : {
			                ""field"" : ""suggest"", \<2>
			                ""size"" : 5 \<3>
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:316")]
		public void Line316()
		{
			// tag::b2a6fb1a94dd10bf594dafe727647e1d[]
			var response0 = new SearchResponse<object>();
			// end::b2a6fb1a94dd10bf594dafe727647e1d[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nor"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""skip_duplicates"": true
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:341")]
		public void Line341()
		{
			// tag::a4eac3c0bac550247e8c7d3f9bcaac1c[]
			var response0 = new SearchResponse<object>();
			// end::a4eac3c0bac550247e8c7d3f9bcaac1c[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""prefix"" : ""nor"",
			            ""completion"" : {
			                ""field"" : ""suggest"",
			                ""fuzzy"" : {
			                    ""fuzziness"" : 2
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/completion-suggest.asciidoc:399")]
		public void Line399()
		{
			// tag::62280b8a1ec0c214b3110a2c42a55fce[]
			var response0 = new SearchResponse<object>();
			// end::62280b8a1ec0c214b3110a2c42a55fce[]

			response0.MatchesExample(@"POST music/_search?pretty
			{
			    ""suggest"": {
			        ""song-suggest"" : {
			            ""regex"" : ""n[ever|i]r"",
			            ""completion"" : {
			                ""field"" : ""suggest""
			            }
			        }
			    }
			}");
		}
	}
}
