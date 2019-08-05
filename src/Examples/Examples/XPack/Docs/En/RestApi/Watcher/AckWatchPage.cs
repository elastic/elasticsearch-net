using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class AckWatchPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line47()
		{
			// tag::7a1b94de1cbb90b0f06ff8631a70236a[]
			var response0 = new SearchResponse<object>();
			// end::7a1b94de1cbb90b0f06ff8631a70236a[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch
			{
			  ""trigger"": {
			    ""schedule"": {
			      ""hourly"": {
			        ""minute"": [ 0, 5 ]
			      }
			    }
			  },
			  ""input"": {
			    ""simple"": {
			      ""payload"": {
			        ""send"": ""yes""
			      }
			    }
			  },
			  ""condition"": {
			    ""always"": {}
			  },
			  ""actions"": {
			    ""test_index"": {
			      ""throttle_period"": ""15m"",
			      ""index"": {
			        ""index"": ""test""
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line84()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U]
		[SkipExample]
		public void Line123()
		{
			// tag::bdc1afd2181154bb78797360f9dbb1a0[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::bdc1afd2181154bb78797360f9dbb1a0[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_execute
			{
			  ""record_execution"" : true
			}");

			response1.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U]
		[SkipExample]
		public void Line180()
		{
			// tag::1b0dc9d076bbb58c6a2953ef4323d2fc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1b0dc9d076bbb58c6a2953ef4323d2fc[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch/_ack/test_index");

			response1.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U]
		[SkipExample]
		public void Line236()
		{
			// tag::8051766cadded0892290bc2cc06e145c[]
			var response0 = new SearchResponse<object>();
			// end::8051766cadded0892290bc2cc06e145c[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_ack/action1,action2");
		}

		[U]
		[SkipExample]
		public void Line245()
		{
			// tag::df7dbac966b67404b8bfa9cdda5ef480[]
			var response0 = new SearchResponse<object>();
			// end::df7dbac966b67404b8bfa9cdda5ef480[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_ack");
		}
	}
}