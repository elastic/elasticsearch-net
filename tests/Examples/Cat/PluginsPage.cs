using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class PluginsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/plugins.asciidoc:38")]
		public void Line38()
		{
			// tag::3796d69e8339bab58e70fdde9f9c09ad[]
			var response0 = new SearchResponse<object>();
			// end::3796d69e8339bab58e70fdde9f9c09ad[]

			response0.MatchesExample(@"GET /_cat/plugins?v&s=component&h=name,component,version,description");
		}
	}
}