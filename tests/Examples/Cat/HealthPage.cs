using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Cat
{
	public class HealthPage : ExampleBase
	{
		[U]
		public void Line69()
		{
			// tag::f8cc4b331a19ff4df8e4a490f906ee69[]
			var catResponse = client.Cat.Health(h => h.Verbose());
			// end::f8cc4b331a19ff4df8e4a490f906ee69[]

			catResponse.MatchesExample(@"GET /_cat/health?v");
		}

		[U]
		public void Line89()
		{
			// tag::ccd9e2cf7181de67cf9ab0df1a02c575[]
			var catResponse = client.Cat.Health(h => h.Verbose().IncludeTimestamp(false));
			// end::ccd9e2cf7181de67cf9ab0df1a02c575[]

			catResponse.MatchesExample(@"GET /_cat/health?v&ts=false");
		}
	}
}
