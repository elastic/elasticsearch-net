using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class DeleteIndexPage : ExampleBase
	{
		[U]
		public void Line10()
		{
			// tag::98f14fddddea54a7d6149ab7b92e099d[]
			var deleteIndexResponse = client.DeleteIndex("twitter");
			// end::98f14fddddea54a7d6149ab7b92e099d[]

			deleteIndexResponse.MatchesExample(@"DELETE /twitter");
		}
	}
}
