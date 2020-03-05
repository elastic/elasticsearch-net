using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.IndexModules.Allocation
{
	public class FilteringPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line54()
		{
			// tag::dad2d4add751fde5c39475ca709cc14b[]
			var response0 = new SearchResponse<object>();
			// end::dad2d4add751fde5c39475ca709cc14b[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include.size"": ""big,medium""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line67()
		{
			// tag::b8b198ede3d08f315348e2a857e47773[]
			var response0 = new SearchResponse<object>();
			// end::b8b198ede3d08f315348e2a857e47773[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include.size"": ""big"",
			  ""index.routing.allocation.include.rack"": ""rack1""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line109()
		{
			// tag::28ac880057135e46b3b00c7f3976538c[]
			var response0 = new SearchResponse<object>();
			// end::28ac880057135e46b3b00c7f3976538c[]

			response0.MatchesExample(@"PUT test/_settings
			{
			  ""index.routing.allocation.include._ip"": ""192.168.2.*""
			}");
		}
	}
}