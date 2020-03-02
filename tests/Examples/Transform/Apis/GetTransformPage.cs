using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Transform.Apis
{
	public class GetTransformPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line92()
		{
			// tag::c65b00a285f510dcd2865aa3539b4e03[]
			var response0 = new SearchResponse<object>();
			// end::c65b00a285f510dcd2865aa3539b4e03[]

			response0.MatchesExample(@"GET _transform?size=10");
		}

		[U(Skip = "Example not implemented")]
		public void Line101()
		{
			// tag::c8ebbecc372bcfa5f4a6e7242395ab5e[]
			var response0 = new SearchResponse<object>();
			// end::c8ebbecc372bcfa5f4a6e7242395ab5e[]

			response0.MatchesExample(@"GET _transform/ecommerce_transform");
		}
	}
}