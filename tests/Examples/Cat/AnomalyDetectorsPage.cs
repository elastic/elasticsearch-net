using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Cat
{
	public class AnomalyDetectorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("cat/anomaly-detectors.asciidoc:267")]
		public void Line267()
		{
			// tag::bb03aeaba69c110f86505fa0e4e5035d[]
			var response0 = new SearchResponse<object>();
			// end::bb03aeaba69c110f86505fa0e4e5035d[]

			response0.MatchesExample(@"GET _cat/ml/anomaly_detectors?h=id,s,dpr,mb&v");
		}
	}
}