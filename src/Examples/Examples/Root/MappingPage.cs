using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class MappingPage : ExampleBase
	{
		[U]
		public void Line141()
		{
			// tag::b311b42b7dcc69821df1f77bfaf2d50d[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Text(t => t.Name("title"))
						.Text(t => t.Name("name"))
						.Number(n => n.Name("age").Type(NumberType.Integer))
						.Date(d => d
							.Name("created")
							.Format("strict_date_optional_time||epoch_millis")
						)
					)
				)
			);
			// end::b311b42b7dcc69821df1f77bfaf2d50d[]

			createIndexResponse.MatchesExample(@"PUT my_index \<1>
			{
			  ""mappings"": {
			    ""properties"": { \<2>
			      ""title"":    { ""type"": ""text""  }, \<3>
			      ""name"":     { ""type"": ""text""  }, \<4>
			      ""age"":      { ""type"": ""integer"" },  \<5>
			      ""created"":  {
			        ""type"":   ""date"", \<6>
			        ""format"": ""strict_date_optional_time||epoch_millis""
			      }
			    }
			  }
			}");
		}
	}
}
