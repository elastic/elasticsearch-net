using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class DeleteLifecyclePage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line71()
		{
			// tag::af517b6936fa41d124d68b107b2efdc3[]
			var response0 = new SearchResponse<object>();
			// end::af517b6936fa41d124d68b107b2efdc3[]

			response0.MatchesExample(@"DELETE _ilm/policy/my_policy");
		}

		[U]
		[SkipExample]
		public void Line80()
		{
			// tag::bc5fcc40c29087a0df7b5405bb70de5c[]
			var response0 = new SearchResponse<object>();
			// end::bc5fcc40c29087a0df7b5405bb70de5c[]

			response0.MatchesExample(@"{
			  ""acknowledged"": true
			}");
		}
	}
}