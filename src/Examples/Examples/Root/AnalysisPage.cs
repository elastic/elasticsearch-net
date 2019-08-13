using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class AnalysisPage : ExampleBase
	{
		[U]
		public void Line42()
		{
			// tag::7ffee3c2a5581994fc0ea59dd106d39f[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t
							.Name("title")
							.Analyzer("standard")
						)
					)
				)
			);
			// end::7ffee3c2a5581994fc0ea59dd106d39f[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"":     ""text"",
			        ""analyzer"": ""standard""
			      }
			    }
			  }
			}");
		}
	}
}
