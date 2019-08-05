using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class LowercaseTokenfilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line12()
		{
			// tag::5be6349f5da1a7a5658df1d7fdf542db[]
			var response0 = new SearchResponse<object>();
			// end::5be6349f5da1a7a5658df1d7fdf542db[]

			response0.MatchesExample(@"PUT /lowercase_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""standard_lowercase_example"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""lowercase""]
			        },
			        ""greek_lowercase_example"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""standard"",
			          ""filter"": [""greek_lowercase""]
			        }
			      },
			      ""filter"": {
			        ""greek_lowercase"": {
			          ""type"": ""lowercase"",
			          ""language"": ""greek""
			        }
			      }
			    }
			  }
			}");
		}
	}
}