using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class MappingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line141()
		{
			// tag::b311b42b7dcc69821df1f77bfaf2d50d[]
			var response0 = new SearchResponse<object>();
			// end::b311b42b7dcc69821df1f77bfaf2d50d[]

			response0.MatchesExample(@"PUT my_index \<1>
			{
			  ""mappings"": {
			    ""properties"": { \<2>
			      ""title"":    { ""type"": ""text""  }, \<3>
			      ""name"":     { ""type"": ""text""  }, \<4>
			      ""age"":      { ""type"": ""integer"" },  \<5>
			      ""created"":  {
			        ""type"":   ""date"", \<6>
			        ""format"": ""strict_date_optional_time||epoch_millis""
			      }
			    }
			  }
			}");
		}
	}
}