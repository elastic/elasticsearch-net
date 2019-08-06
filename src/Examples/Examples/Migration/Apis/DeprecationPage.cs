using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Migration.Apis
{
	public class DeprecationPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line35()
		{
			// tag::135819da3a4bde684357c57a49ad8e85[]
			var response0 = new SearchResponse<object>();
			// end::135819da3a4bde684357c57a49ad8e85[]

			response0.MatchesExample(@"GET /_migration/deprecations");
		}

		[U(Skip = "Example not implemented")]
		public void Line114()
		{
			// tag::69f8b0f2a9ba47e11f363d788cee9d6d[]
			var response0 = new SearchResponse<object>();
			// end::69f8b0f2a9ba47e11f363d788cee9d6d[]

			response0.MatchesExample(@"GET /logstash-*/_migration/deprecations");
		}
	}
}