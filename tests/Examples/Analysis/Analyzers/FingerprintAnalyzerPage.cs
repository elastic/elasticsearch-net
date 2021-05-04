// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Analyzers
{
	public class FingerprintAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/fingerprint-analyzer.asciidoc:19")]
		public void Line19()
		{
			// tag::6490d89a4e43cac5e6b9bc19840d5478[]
			var response0 = new SearchResponse<object>();
			// end::6490d89a4e43cac5e6b9bc19840d5478[]

			response0.MatchesExample(@"POST _analyze
			{
			  ""analyzer"": ""fingerprint"",
			  ""text"": ""Yes yes, Gödel said this sentence is consistent and.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/fingerprint-analyzer.asciidoc:89")]
		public void Line89()
		{
			// tag::2659ccd414867f3a5ee262c9b7cd3f1d[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::2659ccd414867f3a5ee262c9b7cd3f1d[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_fingerprint_analyzer"": {
			          ""type"": ""fingerprint"",
			          ""stopwords"": ""_english_""
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_fingerprint_analyzer"",
			  ""text"": ""Yes yes, Gödel said this sentence is consistent and.""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/fingerprint-analyzer.asciidoc:159")]
		public void Line159()
		{
			// tag::ce725697f93b3eebb3a266314568565a[]
			var response0 = new SearchResponse<object>();
			// end::ce725697f93b3eebb3a266314568565a[]

			response0.MatchesExample(@"PUT /fingerprint_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""rebuilt_fingerprint"": {
			          ""tokenizer"": ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""asciifolding"",
			            ""fingerprint""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
