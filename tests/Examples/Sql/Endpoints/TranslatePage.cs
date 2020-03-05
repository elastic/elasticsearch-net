using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Sql.Endpoints
{
	public class TranslatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("sql/endpoints/translate.asciidoc:10")]
		public void Line10()
		{
			// tag::8097472be12fcbe8652f03e398e49972[]
			var response0 = new SearchResponse<object>();
			// end::8097472be12fcbe8652f03e398e49972[]

			response0.MatchesExample(@"POST /_sql/translate
			{
			    ""query"": ""SELECT * FROM library ORDER BY page_count DESC"",
			    ""fetch_size"": 10
			}");
		}
	}
}