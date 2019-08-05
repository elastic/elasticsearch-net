using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup
{
	public class LoggingConfigPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line155()
		{
			// tag::8e6bfb4441ffa15c86d5dc20fa083571[]
			var response0 = new SearchResponse<object>();
			// end::8e6bfb4441ffa15c86d5dc20fa083571[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""logger.org.elasticsearch.transport"": ""trace""
			  }
			}");
		}
	}
}