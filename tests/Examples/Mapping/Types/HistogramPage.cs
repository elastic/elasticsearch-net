using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Mapping.Types
{
	public class HistogramPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/histogram.asciidoc:74")]
		public void Line74()
		{
			// tag::7d6b1797b1178e96d287831a94bb9658[]
			var response0 = new SearchResponse<object>();
			// end::7d6b1797b1178e96d287831a94bb9658[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_histogram"": {
			        ""type"" : ""histogram""
			      },
			      ""my_text"" : {
			        ""type"" : ""keyword""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/histogram.asciidoc:94")]
		public void Line94()
		{
			// tag::09774dd1a8613672844caadb2bc8dc1e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::09774dd1a8613672844caadb2bc8dc1e[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_text"" : ""histogram_1"",
			  ""my_histogram"" : {
			      ""values"" : [0.1, 0.2, 0.3, 0.4, 0.5], <1>
			      ""counts"" : [3, 7, 23, 12, 6] <2>
			   }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_text"" : ""histogram_2"",
			  ""my_histogram"" : {
			      ""values"" : [0.1, 0.25, 0.35, 0.4, 0.45, 0.5], <1>
			      ""counts"" : [8, 17, 8, 7, 6, 2] <2>
			   }
			}");
		}
	}
}