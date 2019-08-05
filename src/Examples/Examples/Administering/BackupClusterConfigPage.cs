using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Administering
{
	public class BackupClusterConfigPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line41()
		{
			// tag::ee79edafcbc80dfda496e3a26506dcbc[]
			var response0 = new SearchResponse<object>();
			// end::ee79edafcbc80dfda496e3a26506dcbc[]

			response0.MatchesExample(@"GET _cluster/settings?pretty&flat_settings&filter_path=persistent");
		}
	}
}