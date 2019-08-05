using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class DeleteFilterPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line42()
		{
			// tag::8c5d48252cd6d1ee26a2bb817f89c78e[]
			var response0 = new SearchResponse<object>();
			// end::8c5d48252cd6d1ee26a2bb817f89c78e[]

			response0.MatchesExample(@"DELETE _ml/filters/safe_domains");
		}
	}
}