using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Modules
{
	public class HttpPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/http.asciidoc:119")]
		public void Line119()
		{
			// tag::45df8177c5f8a3cc4e36867742e8250c[]
			var response0 = new SearchResponse<object>();
			// end::45df8177c5f8a3cc4e36867742e8250c[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""logger.org.elasticsearch.http.HttpTracer"" : ""TRACE""
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/http.asciidoc:132")]
		public void Line132()
		{
			// tag::fa4e5b5cd144dd03cd507ffa9dec5b7e[]
			var response0 = new SearchResponse<object>();
			// end::fa4e5b5cd144dd03cd507ffa9dec5b7e[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""http.tracer.include"" : ""*"",
			      ""http.tracer.exclude"" : """"
			   }
			}");
		}
	}
}