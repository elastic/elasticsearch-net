using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup
{
	public class SecureSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line108()
		{
			// tag::6e87271a5a10dbb8d27b25c7dbfa868a[]
			var response0 = new SearchResponse<object>();
			// end::6e87271a5a10dbb8d27b25c7dbfa868a[]

			response0.MatchesExample(@"POST _nodes/reload_secure_settings");
		}
	}
}