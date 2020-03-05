using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class DeleteIndexPage : ExampleBase
	{
		[U]
		[Description("indices/delete-index.asciidoc:10")]
		public void Line10()
		{
			// tag::98f14fddddea54a7d6149ab7b92e099d[]
			var deleteIndexResponse = client.DeleteIndex("twitter");
			// end::98f14fddddea54a7d6149ab7b92e099d[]

			deleteIndexResponse.MatchesExample(@"DELETE /twitter");
		}
	}
}
