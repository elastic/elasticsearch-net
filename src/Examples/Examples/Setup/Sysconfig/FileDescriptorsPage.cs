using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Setup.Sysconfig
{
	public class FileDescriptorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line29()
		{
			// tag::c5bc577ff92f889225b0d2617adcb48c[]
			var response0 = new SearchResponse<object>();
			// end::c5bc577ff92f889225b0d2617adcb48c[]

			response0.MatchesExample(@"GET _nodes/stats/process?filter_path=**.max_file_descriptors");
		}
	}
}