// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class AnalyzePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:11")]
		public void Line11()
		{
			// tag::fa42ae3bf6a300420cd0f77ba006458a[]
			var response0 = new SearchResponse<object>();
			// end::fa42ae3bf6a300420cd0f77ba006458a[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""analyzer"" : ""standard"",
			  ""text"" : ""Quick Brown Foxes!""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:140")]
		public void Line140()
		{
			// tag::76dbdd0b2bd48c3c6b1a8d81e23bafd6[]
			var response0 = new SearchResponse<object>();
			// end::76dbdd0b2bd48c3c6b1a8d81e23bafd6[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""analyzer"" : ""standard"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:154")]
		public void Line154()
		{
			// tag::fd9b668eeb1f117950bd4991c7c03fb1[]
			var response0 = new SearchResponse<object>();
			// end::fd9b668eeb1f117950bd4991c7c03fb1[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""analyzer"" : ""standard"",
			  ""text"" : [""this is a test"", ""the second text""]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:170")]
		public void Line170()
		{
			// tag::ef33b3b373f7040b874146599db5d557[]
			var response0 = new SearchResponse<object>();
			// end::ef33b3b373f7040b874146599db5d557[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""lowercase""],
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:180")]
		public void Line180()
		{
			// tag::dc8c94c9bef1f879282caea5c406f36e[]
			var response0 = new SearchResponse<object>();
			// end::dc8c94c9bef1f879282caea5c406f36e[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""lowercase""],
			  ""char_filter"" : [""html_strip""],
			  ""text"" : ""this is a <b>test</b>""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:195")]
		public void Line195()
		{
			// tag::22dde5fe7ac5d85d52115641a68b3c55[]
			var response0 = new SearchResponse<object>();
			// end::22dde5fe7ac5d85d52115641a68b3c55[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""lowercase"", {""type"": ""stop"", ""stopwords"": [""a"", ""is"", ""this""]}],
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:210")]
		public void Line210()
		{
			// tag::3951d7fcd7f849fa278daf342872125a[]
			var response0 = new SearchResponse<object>();
			// end::3951d7fcd7f849fa278daf342872125a[]

			response0.MatchesExample(@"GET /analyze_sample/_analyze
			{
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:223")]
		public void Line223()
		{
			// tag::71fa652ddea811eb3c8bf8c5db21e549[]
			var response0 = new SearchResponse<object>();
			// end::71fa652ddea811eb3c8bf8c5db21e549[]

			response0.MatchesExample(@"GET /analyze_sample/_analyze
			{
			  ""analyzer"" : ""whitespace"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:238")]
		public void Line238()
		{
			// tag::de2f59887737de3a27716177b60393a2[]
			var response0 = new SearchResponse<object>();
			// end::de2f59887737de3a27716177b60393a2[]

			response0.MatchesExample(@"GET /analyze_sample/_analyze
			{
			  ""field"" : ""obj1.field1"",
			  ""text"" : ""this is a test""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:256")]
		public void Line256()
		{
			// tag::49d87c2eb7314ed34221c5fb4f21dfcc[]
			var response0 = new SearchResponse<object>();
			// end::49d87c2eb7314ed34221c5fb4f21dfcc[]

			response0.MatchesExample(@"GET /analyze_sample/_analyze
			{
			  ""normalizer"" : ""my_normalizer"",
			  ""text"" : ""BaR""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:268")]
		public void Line268()
		{
			// tag::15a34bfe0ef8ef6333c8c7b55c011e5d[]
			var response0 = new SearchResponse<object>();
			// end::15a34bfe0ef8ef6333c8c7b55c011e5d[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""filter"" : [""lowercase""],
			  ""text"" : ""BaR""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:285")]
		public void Line285()
		{
			// tag::89f8eac24f3ec6a7668d580aaf0eeefa[]
			var response0 = new SearchResponse<object>();
			// end::89f8eac24f3ec6a7668d580aaf0eeefa[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"" : ""standard"",
			  ""filter"" : [""snowball""],
			  ""text"" : ""detailed output"",
			  ""explain"" : true,
			  ""attributes"" : [""keyword""] <1>
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/analyze.asciidoc:360")]
		public void Line360()
		{
			// tag::0957bbd535f58c97b12ffba90813d64c[]
			var response0 = new SearchResponse<object>();
			// end::0957bbd535f58c97b12ffba90813d64c[]

			response0.MatchesExample(@"PUT /analyze_sample
			{
			  ""settings"" : {
			    ""index.analyze.max_token_count"" : 20000
			  }
			}");
		}
	}
}
