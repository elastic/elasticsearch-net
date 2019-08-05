using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Scripting
{
	public class UsingPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line24()
		{
			// tag::e62cf588bfc891504bbf933af86eed7c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::e62cf588bfc891504bbf933af86eed7c[]

			response0.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_field"": 5
			}");

			response1.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"": {
			    ""my_doubled_field"": {
			      ""script"": {
			        ""lang"":   ""expression"",
			        ""source"": ""doc['my_field'] * multiplier"",
			        ""params"": {
			          ""multiplier"": 2
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line148()
		{
			// tag::40a2bbc35a887d6c7dda3cca1fe7aa58[]
			var response0 = new SearchResponse<object>();
			// end::40a2bbc35a887d6c7dda3cca1fe7aa58[]

			response0.MatchesExample(@"POST _scripts/calculate-score
			{
			  ""script"": {
			    ""lang"": ""painless"",
			    ""source"": ""Math.log(_score * 2) + params.my_modifier""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line162()
		{
			// tag::08e08feb514b24006e13f258d617d873[]
			var response0 = new SearchResponse<object>();
			// end::08e08feb514b24006e13f258d617d873[]

			response0.MatchesExample(@"GET _scripts/calculate-score");
		}

		[U]
		[SkipExample]
		public void Line171()
		{
			// tag::4484218a06e3bae623250cdaccac5dcb[]
			var response0 = new SearchResponse<object>();
			// end::4484218a06e3bae623250cdaccac5dcb[]

			response0.MatchesExample(@"GET _search
			{
			  ""query"": {
			    ""script"": {
			      ""script"": {
			        ""id"": ""calculate-score"",
			        ""params"": {
			          ""my_modifier"": 2
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line192()
		{
			// tag::4061fd5ba7221ca85805ed14d59a6bc5[]
			var response0 = new SearchResponse<object>();
			// end::4061fd5ba7221ca85805ed14d59a6bc5[]

			response0.MatchesExample(@"DELETE _scripts/calculate-score");
		}
	}
}