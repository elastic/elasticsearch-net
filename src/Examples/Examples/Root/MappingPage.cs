using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Root
{
	public class MappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line144()
		{
			// tag::d8b2a88b5eca99d3691ad3cd40266736[]
			var response0 = new SearchResponse<object>();
			// end::d8b2a88b5eca99d3691ad3cd40266736[]

			response0.MatchesExample(@"PUT /my-index
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

		[U(Skip = "Example not implemented")]
		public void Line173()
		{
			// tag::71ba9033107882f61cdc3b32fc73568d[]
			var response0 = new SearchResponse<object>();
			// end::71ba9033107882f61cdc3b32fc73568d[]

			response0.MatchesExample(@"PUT /my-index/_mapping
			{
			  ""properties"": {
			    ""employee-id"": {
			      ""type"": ""keyword"",
			      ""index"": false
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line211()
		{
			// tag::609260ad1d5998be2ca09ff1fe237efa[]
			var response0 = new SearchResponse<object>();
			// end::609260ad1d5998be2ca09ff1fe237efa[]

			response0.MatchesExample(@"GET /my-index/_mapping");
		}

		[U(Skip = "Example not implemented")]
		public void Line257()
		{
			// tag::99a52be903945b17e734a1d02a57e958[]
			var response0 = new SearchResponse<object>();
			// end::99a52be903945b17e734a1d02a57e958[]

			response0.MatchesExample(@"GET /my-index/_mapping/field/employee-id");
		}
	}
}
