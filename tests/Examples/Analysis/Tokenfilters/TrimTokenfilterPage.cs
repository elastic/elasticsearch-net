using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class TrimTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:32")]
		public void Line32()
		{
			// tag::c318fde926842722825a51e5c9c326a9[]
			var response0 = new SearchResponse<object>();
			// end::c318fde926842722825a51e5c9c326a9[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""text"" : "" fox ""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:62")]
		public void Line62()
		{
			// tag::a3a14f7f0e80725f695a901a7e1d579d[]
			var response0 = new SearchResponse<object>();
			// end::a3a14f7f0e80725f695a901a7e1d579d[]

			response0.MatchesExample(@"GET _analyze
			{
			  ""tokenizer"" : ""keyword"",
			  ""filter"" : [""trim""],
			  ""text"" : "" fox ""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/trim-tokenfilter.asciidoc:96")]
		public void Line96()
		{
			// tag::fd26bfdbe95b2d2db374385d12849f77[]
			var response0 = new SearchResponse<object>();
			// end::fd26bfdbe95b2d2db374385d12849f77[]

			response0.MatchesExample(@"PUT trim_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""keyword_trim"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""trim"" ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}