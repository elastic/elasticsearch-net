using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class PorterstemTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/porterstem-tokenfilter.asciidoc:28")]
		public void Line28()
		{
			// tag::0d54ddad2bf6f76aa5c35f53ba77748a[]
			var response0 = new SearchResponse<object>();
			// end::0d54ddad2bf6f76aa5c35f53ba77748a[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""standard"",
			  ""filter"": [ ""porter_stem"" ],
			  ""text"": ""the foxes jumping quickly""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/porterstem-tokenfilter.asciidoc:97")]
		public void Line97()
		{
			// tag::1badc80ac8097713f8fc51872615a042[]
			var response0 = new SearchResponse<object>();
			// end::1badc80ac8097713f8fc51872615a042[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [
			            ""lowercase"",
			            ""porter_stem""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}