using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm.Apis
{
	public class StartPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line72()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}

		[U]
		[SkipExample]
		public void Line81()
		{
			// tag::bc5fcc40c29087a0df7b5405bb70de5c[]
			var response0 = new SearchResponse<object>();
			// end::bc5fcc40c29087a0df7b5405bb70de5c[]

			response0.MatchesExample(@"{
			  ""acknowledged"": true
			}");
		}
	}
}