using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Cat
{
	public class TrainedmodelPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/trainedmodel.asciidoc:113")]
		public void Line113()
		{
			// tag::1d56d3532d30d60c1d93d75b6a377a49[]
			var response0 = new SearchResponse<object>();
			// end::1d56d3532d30d60c1d93d75b6a377a49[]

			response0.MatchesExample(@"GET _cat/ml/trained_models?h=c,o,l,ct,v&v");
		}
	}
}