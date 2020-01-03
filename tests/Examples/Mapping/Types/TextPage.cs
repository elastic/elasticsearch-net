using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class TextPage : ExampleBase
	{
		[U]
		public void Line22()
		{
			// tag::24ea1c6cdf10165228951e562b7ec0ef[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p.Text(t => t.Name("full_name")))
				)
			);
			// end::24ea1c6cdf10165228951e562b7ec0ef[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""full_name"": {
			        ""type"":  ""text""
			      }
			    }
			  }
			}");
		}
	}
}
