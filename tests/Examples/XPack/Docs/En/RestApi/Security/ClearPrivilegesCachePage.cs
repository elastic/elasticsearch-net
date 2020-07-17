using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Security
{
	public class ClearPrivilegesCachePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/security/clear-privileges-cache.asciidoc:41")]
		public void Line41()
		{
			// tag::f6df4acf3c7a4f85706ff314b21ebcb2[]
			var response0 = new SearchResponse<object>();
			// end::f6df4acf3c7a4f85706ff314b21ebcb2[]

			response0.MatchesExample(@"POST /_security/privilege/myapp/_clear_cache");
		}
	}
}