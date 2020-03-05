using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Monitoring
{
	public class ConfiguringFilebeatPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/configuring-filebeat.asciidoc:39")]
		public void Line39()
		{
			// tag::fb2b8d642e16132eebcff4f8b6d592d1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fb2b8d642e16132eebcff4f8b6d592d1[]

			response0.MatchesExample(@"GET _cluster/settings");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.collection.enabled"": true
			  }
			}");
		}
	}
}