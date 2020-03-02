using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class DeleteLifecyclePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line77()
		{
			// tag::af517b6936fa41d124d68b107b2efdc3[]
			var response0 = new SearchResponse<object>();
			// end::af517b6936fa41d124d68b107b2efdc3[]

			response0.MatchesExample(@"DELETE _ilm/policy/my_policy");
		}
	}
}