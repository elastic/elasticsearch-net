using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Sql
{
	public class GettingStartedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line10()
		{
			// tag::15f293688537c82d2bdebda916769fa4[]
			var response0 = new SearchResponse<object>();
			// end::15f293688537c82d2bdebda916769fa4[]

			response0.MatchesExample(@"PUT /library/book/_bulk?refresh
			{""index"":{""_id"": ""Leviathan Wakes""}}
			{""name"": ""Leviathan Wakes"", ""author"": ""James S.A. Corey"", ""release_date"": ""2011-06-02"", ""page_count"": 561}
			{""index"":{""_id"": ""Hyperion""}}
			{""name"": ""Hyperion"", ""author"": ""Dan Simmons"", ""release_date"": ""1989-05-26"", ""page_count"": 482}
			{""index"":{""_id"": ""Dune""}}
			{""name"": ""Dune"", ""author"": ""Frank Herbert"", ""release_date"": ""1965-06-01"", ""page_count"": 604}");
		}

		[U(Skip = "Example not implemented")]
		public void Line24()
		{
			// tag::53b14d640c4c48a5e7ea86ddc26bee64[]
			var response0 = new SearchResponse<object>();
			// end::53b14d640c4c48a5e7ea86ddc26bee64[]

			response0.MatchesExample(@"POST /_sql?format=txt
			{
			    ""query"": ""SELECT * FROM library WHERE release_date < '2000-01-01'""
			}");
		}
	}
}