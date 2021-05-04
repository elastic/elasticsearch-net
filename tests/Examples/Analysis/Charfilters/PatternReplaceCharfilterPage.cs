// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Charfilters
{
	public class PatternReplaceCharfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/pattern-replace-charfilter.asciidoc:54")]
		public void Line54()
		{
			// tag::2b8ba109999fc87712433cea92c99ebe[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2b8ba109999fc87712433cea92c99ebe[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""char_filter"": [
			            ""my_char_filter""
			          ]
			        }
			      },
			      ""char_filter"": {
			        ""my_char_filter"": {
			          ""type"": ""pattern_replace"",
			          ""pattern"": ""(\\d+)-(?=\\d)"",
			          ""replacement"": ""$1_""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""My credit card is 123-456-789""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/pattern-replace-charfilter.asciidoc:104")]
		public void Line104()
		{
			// tag::1e1f0d83b1ca672396341af5dcfd2603[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1e1f0d83b1ca672396341af5dcfd2603[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""standard"",
			          ""char_filter"": [
			            ""my_char_filter""
			          ],
			          ""filter"": [
			            ""lowercase""
			          ]
			        }
			      },
			      ""char_filter"": {
			        ""my_char_filter"": {
			          ""type"": ""pattern_replace"",
			          ""pattern"": ""(?<=\\p{Lower})(?=\\p{Upper})"",
			          ""replacement"": "" ""
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""text"": {
			        ""type"": ""text"",
			        ""analyzer"": ""my_analyzer""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""The fooBarBaz method""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/charfilters/pattern-replace-charfilter.asciidoc:205")]
		public void Line205()
		{
			// tag::32afaee3f1326785b4009ff48576d42f[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::32afaee3f1326785b4009ff48576d42f[]

			response0.MatchesExample(@"PUT my_index/_doc/1?refresh
			{
			  ""text"": ""The fooBarBaz method""
			}");

			response1.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""match"": {
			      ""text"": ""bar""
			    }
			  },
			  ""highlight"": {
			    ""fields"": {
			      ""text"": {}
			    }
			  }
			}");
		}
	}
}
