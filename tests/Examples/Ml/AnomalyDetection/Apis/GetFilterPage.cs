using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class GetFilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line67()
		{
			// tag::800861c15bb33ca01a46fb97dde7537a[]
			var response0 = new SearchResponse<object>();
			// end::800861c15bb33ca01a46fb97dde7537a[]

			response0.MatchesExample(@"GET _ml/filters/safe_domains");
		}
	}
}