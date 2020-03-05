using Elastic.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.Slm.Apis
{
	public class SlmExecutePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("slm/apis/slm-execute.asciidoc:45")]
		public void Line45()
		{
			// tag::0ab002c6618af75e1041a23c692327ad[]
			var response0 = new SearchResponse<object>();
			// end::0ab002c6618af75e1041a23c692327ad[]

			response0.MatchesExample(@"POST /_slm/policy/daily-snapshots/_execute");
		}
	}
}