using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Analysis.Charfilters
{
	public class MappingCharfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::f9518803f0368e326ce2f46bd213bde9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::f9518803f0368e326ce2f46bd213bde9[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""char_filter"": [
			            ""my_char_filter""
			          ]
			        }
			      },
			      ""char_filter"": {
			        ""my_char_filter"": {
			          ""type"": ""mapping"",
			          ""mappings"": [
			            ""٠ => 0"",
			            ""١ => 1"",
			            ""٢ => 2"",
			            ""٣ => 3"",
			            ""٤ => 4"",
			            ""٥ => 5"",
			            ""٦ => 6"",
			            ""٧ => 7"",
			            ""٨ => 8"",
			            ""٩ => 9""
			          ]
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""My license plate is ٢٥٠١٥""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line109()
		{
			// tag::8d5c32d86f00cf27d3f52a5fc493ea30[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::8d5c32d86f00cf27d3f52a5fc493ea30[]

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
			          ""type"": ""mapping"",
			          ""mappings"": [
			            "":) => _happy_"",
			            "":( => _sad_""
			          ]
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST my_index/_analyze
			{
			  ""analyzer"": ""my_analyzer"",
			  ""text"": ""I'm delighted about it :(""
			}");
		}
	}
}