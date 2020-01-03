using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest.Apis.Enrich
{
	public class DeleteEnrichPolicyPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
		{
			// tag::cdd29b01e730b3996de68a2788050021[]
			var response0 = new SearchResponse<object>();
			// end::cdd29b01e730b3996de68a2788050021[]

			response0.MatchesExample(@"DELETE /_enrich/policy/my-policy");
		}
	}
}