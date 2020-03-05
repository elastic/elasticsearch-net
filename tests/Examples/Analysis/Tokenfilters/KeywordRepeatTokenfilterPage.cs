using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class KeywordRepeatTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-repeat-tokenfilter.asciidoc:19")]
		public void Line19()
		{
			// tag::9da83c9a2149bfc6fe215a612ae0a9aa[]
			var response0 = new SearchResponse<object>();
			// end::9da83c9a2149bfc6fe215a612ae0a9aa[]

			response0.MatchesExample(@"PUT /keyword_repeat_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""stemmed_and_unstemmed"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""lowercase"", ""keyword_repeat"", ""porter_stem"", ""unique_stem""]
			        }
			      },
			      ""filter"": {
			        ""unique_stem"": {
			          ""type"": ""unique"",
			          ""only_on_same_position"": true
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/keyword-repeat-tokenfilter.asciidoc:45")]
		public void Line45()
		{
			// tag::757622a424b8445fee49746862a11b02[]
			var response0 = new SearchResponse<object>();
			// end::757622a424b8445fee49746862a11b02[]

			response0.MatchesExample(@"POST /keyword_repeat_example/_analyze
			{
			  ""analyzer"" : ""stemmed_and_unstemmed"",
			  ""text"" : ""I like cats""
			}");
		}
	}
}