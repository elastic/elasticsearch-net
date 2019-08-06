using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class GetStatusPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line38()
		{
			// tag::182df084f028479ecbe8d7648ddad892[]
			var response0 = new SearchResponse<object>();
			// end::182df084f028479ecbe8d7648ddad892[]

			response0.MatchesExample(@"GET _ilm/status");
		}

		[U(Skip = "Example not implemented")]
		public void Line46()
		{
			// tag::99e0bec31e49636bc0053ac66bc29352[]
			var response0 = new SearchResponse<object>();
			// end::99e0bec31e49636bc0053ac66bc29352[]

			response0.MatchesExample(@"{
			  ""operation_mode"": ""RUNNING""
			}");
		}
	}
}