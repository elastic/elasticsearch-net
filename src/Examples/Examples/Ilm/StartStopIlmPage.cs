using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class StartStopIlmPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line57()
		{
			// tag::182df084f028479ecbe8d7648ddad892[]
			var response0 = new SearchResponse<object>();
			// end::182df084f028479ecbe8d7648ddad892[]

			response0.MatchesExample(@"GET _ilm/status");
		}

		[U]
		[SkipExample]
		public void Line65()
		{
			// tag::99e0bec31e49636bc0053ac66bc29352[]
			var response0 = new SearchResponse<object>();
			// end::99e0bec31e49636bc0053ac66bc29352[]

			response0.MatchesExample(@"{
			  ""operation_mode"": ""RUNNING""
			}");
		}

		[U]
		[SkipExample]
		public void Line92()
		{
			// tag::585a34ad79aee16678b37da785933ac8[]
			var response0 = new SearchResponse<object>();
			// end::585a34ad79aee16678b37da785933ac8[]

			response0.MatchesExample(@"POST _ilm/stop");
		}

		[U]
		[SkipExample]
		public void Line111()
		{
			// tag::8de1c258461189d65cba97dbc94600cd[]
			var response0 = new SearchResponse<object>();
			// end::8de1c258461189d65cba97dbc94600cd[]

			response0.MatchesExample(@"{
			  ""operation_mode"": ""STOPPING""
			}");
		}

		[U]
		[SkipExample]
		public void Line135()
		{
			// tag::db8563ab7fe37081a9bb66c91d65d673[]
			var response0 = new SearchResponse<object>();
			// end::db8563ab7fe37081a9bb66c91d65d673[]

			response0.MatchesExample(@"{
			  ""operation_mode"": ""STOPPED""
			}");
		}

		[U]
		[SkipExample]
		public void Line150()
		{
			// tag::72ae3851160fcf02b8e2cdfd4e57d238[]
			var response0 = new SearchResponse<object>();
			// end::72ae3851160fcf02b8e2cdfd4e57d238[]

			response0.MatchesExample(@"POST _ilm/start");
		}
	}
}