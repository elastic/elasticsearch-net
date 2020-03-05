using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class FingerprintTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:35")]
		public void Line35()
		{
			// tag::df82a9cb21a7557f3ddba2509f76f608[]
			var response0 = new SearchResponse<object>();
			// end::df82a9cb21a7557f3ddba2509f76f608[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""whitespace"",
			  ""filter"" : [""fingerprint""],
			  ""text"" : ""zebra jumps over resting resting dog""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:76")]
		public void Line76()
		{
			// tag::8e09caccab0c7c0f82f06cea45424396[]
			var response0 = new SearchResponse<object>();
			// end::8e09caccab0c7c0f82f06cea45424396[]

			response0.MatchesExample(@"PUT fingerprint_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_fingerprint"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""elision"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/fingerprint-tokenfilter.asciidoc:117")]
		public void Line117()
		{
			// tag::1b0f40959a7a4d124372f2bd3f7eac85[]
			var response0 = new SearchResponse<object>();
			// end::1b0f40959a7a4d124372f2bd3f7eac85[]

			response0.MatchesExample(@"PUT custom_fingerprint_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""whitespace_"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [ ""fingerprint_plus_concat"" ]
			        }
			      },
			      ""filter"": {
			        ""fingerprint_plus_concat"": {
			          ""type"": ""fingerprint"",
			          ""max_output_size"": 100,
			          ""separator"": ""+""
			        }
			      }
			    }
			  }
			}");
		}
	}
}