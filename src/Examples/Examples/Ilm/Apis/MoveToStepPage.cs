using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class MoveToStepPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line86()
		{
			// tag::e3c5f93b3c85e8519f801defc20b0ce0[]
			var response0 = new SearchResponse<object>();
			// end::e3c5f93b3c85e8519f801defc20b0ce0[]

			response0.MatchesExample(@"POST _ilm/move/my_index
			{
			  ""current_step"": { \<1>
			    ""phase"": ""new"",
			    ""action"": ""complete"",
			    ""name"": ""complete""
			  },
			  ""next_step"": { \<2>
			    ""phase"": ""warm"",
			    ""action"": ""forcemerge"",
			    ""name"": ""forcemerge""
			  }
			}");
		}
	}
}