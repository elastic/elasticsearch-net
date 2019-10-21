using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Modules
{
	public class TransportPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line157()
		{
			// tag::939e79dee613238f9512fb9cbf0be816[]
			var response0 = new SearchResponse<object>();
			// end::939e79dee613238f9512fb9cbf0be816[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""logger.org.elasticsearch.transport.TransportService.tracer"" : ""TRACE""
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line170()
		{
			// tag::cecbbd7b4ec1bf82fd84ae96099febcc[]
			var response0 = new SearchResponse<object>();
			// end::cecbbd7b4ec1bf82fd84ae96099febcc[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""transport.tracer.include"" : ""*"",
			      ""transport.tracer.exclude"" : ""internal:coordination/fault_detection/*""
			   }
			}");
		}
	}
}