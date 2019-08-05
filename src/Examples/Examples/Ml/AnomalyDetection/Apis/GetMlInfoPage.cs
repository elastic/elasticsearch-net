using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetMlInfoPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line41()
		{
			// tag::4d7c0b52d3c0a084157428624c543c90[]
			var response0 = new SearchResponse<object>();
			// end::4d7c0b52d3c0a084157428624c543c90[]

			response0.MatchesExample(@"GET _ml/info");
		}
	}
}