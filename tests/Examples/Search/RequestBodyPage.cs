using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using Nest;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Examples.Search
{
	public class RequestBodyPage : ExampleBase
	{
		[U]
		[Description("search/request-body.asciidoc:7")]
		public void Line7()
		{
			// tag::0ce3606f1dba490eef83c4317b315b62[]
			var searchResponse = client.Search<Tweet>(s => s
				.Index("twitter")
				.Query(q => q.Term(p => p.User, "kimchy"))
			);
			// end::0ce3606f1dba490eef83c4317b315b62[]

			searchResponse.MatchesExample(@"GET /twitter/_search
			{
			    ""query"" : {
			        ""term"" : { ""user"" : ""kimchy"" }
			    }
			}", e =>
			{
				e.ApplyBodyChanges(b =>
				{
					var value = b["query"]["term"]["user"];
					b["query"]["term"]["user"] = new JObject { ["value"] = value };
				});
				return e;
			});
		}

		[U]
		[Description("search/request-body.asciidoc:156")]
		public void Line156()
		{
			// tag::bfcd65ab85d684d36a8550080032958d[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Size(0)
				.TerminateAfter(1)
				.QueryOnQueryString("message:number")
			);
			// end::bfcd65ab85d684d36a8550080032958d[]

			searchResponse.MatchesExample(@"GET /_search?q=message:number&size=0&terminate_after=1", e =>
			{
				e.Method = HttpMethod.POST;
				e.Uri.Path = "/_all/_search";
				e.MoveQueryStringToBody("size", 0);
				e.MoveQueryStringToBody("terminate_after", 1);
				return e;
			});
		}
	}
}
