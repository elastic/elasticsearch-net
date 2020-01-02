using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Indices
{
	public class GetIndexTemplatePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::02f65c6bab8f40bf3ce18160623d1870[]
			var response0 = new SearchResponse<object>();
			// end::02f65c6bab8f40bf3ce18160623d1870[]

			response0.MatchesExample(@"GET /_template/template_1");
		}

		[U(Skip = "Example not implemented")]
		public void Line64()
		{
			// tag::24aee6033bf77a68ced74e3fd9d34283[]
			var response0 = new SearchResponse<object>();
			// end::24aee6033bf77a68ced74e3fd9d34283[]

			response0.MatchesExample(@"GET /_template/template_1,template_2");
		}

		[U(Skip = "Example not implemented")]
		public void Line73()
		{
			// tag::ba6040de55afb2c8fb9e5b24bb038820[]
			var response0 = new SearchResponse<object>();
			// end::ba6040de55afb2c8fb9e5b24bb038820[]

			response0.MatchesExample(@"GET /_template/temp*");
		}

		[U(Skip = "Example not implemented")]
		public void Line82()
		{
			// tag::fd2d289e6b725fcc3cbe8fe7ffe02ea0[]
			var response0 = new SearchResponse<object>();
			// end::fd2d289e6b725fcc3cbe8fe7ffe02ea0[]

			response0.MatchesExample(@"GET /_template");
		}
	}
}