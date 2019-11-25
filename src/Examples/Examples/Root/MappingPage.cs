using Elastic.Xunit.XunitPlumbing;
using Examples.Models;
using static Nest.Infer;

namespace Examples.Root
{
	public class MappingPage : ExampleBase
	{
		[U]
		public void Line144()
		{
			// tag::d8b2a88b5eca99d3691ad3cd40266736[]
			var mapResponse = client.Indices.Create("my-index", c => c
				.Map<Employee>(m => m
					.Properties(props => props
						.Scalar(p => p.Age)
						.Keyword(k => k.Name(p => p.Email))
						.Text(k => k.Name(p => p.Name))
					)
				)
			);
			// end::d8b2a88b5eca99d3691ad3cd40266736[]

			mapResponse.MatchesExample(@"PUT /my-index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""age"":    { ""type"": ""integer"" },  <1>
			      ""email"":  { ""type"": ""keyword""  }, <2>
			      ""name"":   { ""type"": ""text""  }     <3>
			    }
			  }
			}");
		}

		[U]
		public void Line173()
		{
			// tag::71ba9033107882f61cdc3b32fc73568d[]
			var mapResponse = client.Map<Employee>(m => m
				.Index("my-index")
				.Properties(props => props
					.Keyword(k => k
						.Name(p => p.EmployeeId)
						.Index(false)
					)
				)
			);
			// end::71ba9033107882f61cdc3b32fc73568d[]

			mapResponse.MatchesExample(@"PUT /my-index/_mapping
			{
			  ""properties"": {
			    ""employee-id"": {
			      ""type"": ""keyword"",
			      ""index"": false
			    }
			  }
			}");
		}

		[U]
		public void Line211()
		{
			// tag::609260ad1d5998be2ca09ff1fe237efa[]
			var getMappingResponse = client.Indices.GetMapping<Employee>(m => m.Index("my-index"));
			// end::609260ad1d5998be2ca09ff1fe237efa[]

			getMappingResponse.MatchesExample(@"GET /my-index/_mapping");
		}

		[U]
		public void Line257()
		{
			// tag::99a52be903945b17e734a1d02a57e958[]
			var getMappingResponse = client.Indices.GetFieldMapping<Employee>(
				Field<Employee>(p=>p.EmployeeId),
				m => m.Index("my-index")
			);
			// end::99a52be903945b17e734a1d02a57e958[]

			getMappingResponse.MatchesExample(@"GET /my-index/_mapping/field/employee-id");
		}
	}
}
