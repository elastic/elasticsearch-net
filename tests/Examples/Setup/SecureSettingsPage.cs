using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Setup
{
	public class SecureSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/secure-settings.asciidoc:35")]
		public void Line35()
		{
			// tag::32732207b66d0fd661e8e7638aef5176[]
			var response0 = new SearchResponse<object>();
			// end::32732207b66d0fd661e8e7638aef5176[]

			response0.MatchesExample(@"POST _nodes/reload_secure_settings
			{
			  ""reload_secure_settings"": ""s3cr3t"" <1>
			}");
		}
	}
}