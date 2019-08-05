using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Mapping.Types
{
	public class FlattenedPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line37()
		{
			// tag::8aa74aee3dcf4b34028e4c5e1c1ed27b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::8aa74aee3dcf4b34028e4c5e1c1ed27b[]

			response0.MatchesExample(@"PUT bug_reports
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text""
			      },
			      ""labels"": {
			        ""type"": ""flattened""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST bug_reports/_doc/1
			{
			  ""title"": ""Results are not sorted correctly."",
			  ""labels"": {
			    ""priority"": ""urgent"",
			    ""release"": [""v1.2.5"", ""v1.3.0""],
			    ""timestamp"": {
			      ""created"": 1541458026,
			      ""closed"": 1541457010
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line76()
		{
			// tag::169b39bb889ecd47541bed3e48725488[]
			var response0 = new SearchResponse<object>();
			// end::169b39bb889ecd47541bed3e48725488[]

			response0.MatchesExample(@"POST bug_reports/_search
			{
			  ""query"": {
			    ""term"": {""labels"": ""urgent""}
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line88()
		{
			// tag::2f4a55dfeba8851b306ef9c1b216ef54[]
			var response0 = new SearchResponse<object>();
			// end::2f4a55dfeba8851b306ef9c1b216ef54[]

			response0.MatchesExample(@"POST bug_reports/_search
			{
			  ""query"": {
			    ""term"": {""labels.release"": ""v1.3.0""}
			  }
			}");
		}
	}
}