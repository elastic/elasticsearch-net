using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.Security.Authorization
{
	public class RoleTemplatesPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line16()
		{
			// tag::fa154ca3d40df55e3f40d6636fe805de[]
			var response0 = new SearchResponse<object>();
			// end::fa154ca3d40df55e3f40d6636fe805de[]

			response0.MatchesExample(@"POST /_security/role/example1
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""my_index"" ],
			      ""privileges"" : [ ""read"" ],
			      ""query"" : {
			        ""template"" : {
			          ""source"" : {
			            ""term"" : { ""acl.username"" : ""{{_user.username}}"" }
			          }
			        }
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line52()
		{
			// tag::91b0ce11b58f1d3d8bdfe11d38b820fa[]
			var response0 = new SearchResponse<object>();
			// end::91b0ce11b58f1d3d8bdfe11d38b820fa[]

			response0.MatchesExample(@"POST /_security/role/example2
			{
			  ""indices"" : [
			    {
			      ""names"" : [ ""my_index"" ],
			      ""privileges"" : [ ""read"" ],
			      ""query"" : {
			        ""template"" : {
			          ""source"" : {
			            ""term"" : { ""group.id"" : ""{{_user.metadata.group_id}}"" }
			          }
			        }
			      }
			    }
			  ]
			}");
		}
	}
}