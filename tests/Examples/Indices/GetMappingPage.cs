using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class GetMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:10")]
		public void Line10()
		{
			// tag::a8fba09a46b2c3524428aa3259b7124f[]
			var response0 = new SearchResponse<object>();
			// end::a8fba09a46b2c3524428aa3259b7124f[]

			response0.MatchesExample(@"GET /twitter/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:60")]
		public void Line60()
		{
			// tag::cf02e3d8b371bd59f0224967c36330da[]
			var response0 = new SearchResponse<object>();
			// end::cf02e3d8b371bd59f0224967c36330da[]

			response0.MatchesExample(@"GET /twitter,kimchy/_mapping");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-mapping.asciidoc:70")]
		public void Line70()
		{
			// tag::09cdd5ae8114c49886026fef8d00a19c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::09cdd5ae8114c49886026fef8d00a19c[]

			response0.MatchesExample(@"GET /_all/_mapping");

			response1.MatchesExample(@"GET /_mapping");
		}
	}
}