using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search
{
	public class FieldCapsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:7")]
		public void Line7()
		{
			// tag::38f7739f750f1411bccf511a0abaaea3[]
			var response0 = new SearchResponse<object>();
			// end::38f7739f750f1411bccf511a0abaaea3[]

			response0.MatchesExample(@"GET /_field_caps?fields=rating");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:97")]
		public void Line97()
		{
			// tag::614bd49400b6ebf47c5b12839dd1ecb8[]
			var response0 = new SearchResponse<object>();
			// end::614bd49400b6ebf47c5b12839dd1ecb8[]

			response0.MatchesExample(@"GET twitter/_field_caps?fields=rating");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:107")]
		public void Line107()
		{
			// tag::a985e6b7b2ead9c3f30a9bc97d8b598e[]
			var response0 = new SearchResponse<object>();
			// end::a985e6b7b2ead9c3f30a9bc97d8b598e[]

			response0.MatchesExample(@"GET _field_caps?fields=rating,title");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/field-caps.asciidoc:155")]
		public void Line155()
		{
			// tag::4e931cfac74e46e221cf4a9ab88a182d[]
			var response0 = new SearchResponse<object>();
			// end::4e931cfac74e46e221cf4a9ab88a182d[]

			response0.MatchesExample(@"GET _field_caps?fields=rating,title&include_unmapped");
		}
	}
}