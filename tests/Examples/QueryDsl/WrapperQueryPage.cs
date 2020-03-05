using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.QueryDsl
{
	public class WrapperQueryPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("query-dsl/wrapper-query.asciidoc:10")]
		public void Line10()
		{
			// tag::6159a7d56e93e14a31fc06644c803a38[]
			var response0 = new SearchResponse<object>();
			// end::6159a7d56e93e14a31fc06644c803a38[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""wrapper"": {
			            ""query"" : ""eyJ0ZXJtIiA6IHsgInVzZXIiIDogIktpbWNoeSIgfX0="" \<1>
			        }
			    }
			}");
		}
	}
}