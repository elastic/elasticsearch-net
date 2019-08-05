using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Search.Request
{
	public class NamedQueriesAndFiltersPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line7()
		{
			// tag::0aad4321e968effc6e6ef2b98c6c71a5[]
			var response0 = new SearchResponse<object>();
			// end::0aad4321e968effc6e6ef2b98c6c71a5[]

			response0.MatchesExample(@"GET /_search
			{
			    ""query"": {
			        ""bool"" : {
			            ""should"" : [
			                {""match"" : { ""name.first"" : {""query"" : ""shay"", ""_name"" : ""first""} }},
			                {""match"" : { ""name.last"" : {""query"" : ""banon"", ""_name"" : ""last""} }}
			            ],
			            ""filter"" : {
			                ""terms"" : {
			                    ""name.last"" : [""banon"", ""kimchy""],
			                    ""_name"" : ""test""
			                }
			            }
			        }
			    }
			}");
		}
	}
}