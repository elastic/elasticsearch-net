using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Processors
{
	public class SetPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line32()
		{
			// tag::366b29ef910f12c7fbced35f39000953[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::366b29ef910f12c7fbced35f39000953[]

			response0.MatchesExample(@"PUT _ingest/pipeline/set_os
			{
			  ""description"": ""sets the value of host.os.name from the field os"",
			  ""processors"": [
			    {
			      ""set"": {
			        ""field"": ""host.os.name"",
			        ""value"": ""{{os}}""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"POST _ingest/pipeline/set_os/_simulate
			{
			  ""docs"": [
			    {
			      ""_source"": {
			        ""os"": ""Ubuntu""
			      }
			    }
			  ]
			}");
		}
	}
}