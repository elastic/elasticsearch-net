using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Fields
{
	public class FieldNamesFieldPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line24()
		{
			// tag::e4fc720e1f7f2f9a7edf48184fd4a0dd[]
			var response0 = new SearchResponse<object>();
			// end::e4fc720e1f7f2f9a7edf48184fd4a0dd[]

			response0.MatchesExample(@"PUT tweets
			{
			  ""mappings"": {
			    ""_field_names"": {
			      ""enabled"": false
			    }
			  }
			}");
		}
	}
}