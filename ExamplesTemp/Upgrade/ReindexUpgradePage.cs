using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Upgrade
{
	public class ReindexUpgradePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line160()
		{
			// tag::acd65c045139fef38ef5cd20c8c1cfc1[]
			var response0 = new SearchResponse<object>();
			// end::acd65c045139fef38ef5cd20c8c1cfc1[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://oldhost:9200"",
			      ""username"": ""user"",
			      ""password"": ""pass""
			    },
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}
	}
}