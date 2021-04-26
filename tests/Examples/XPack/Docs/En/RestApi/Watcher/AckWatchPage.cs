/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.RestApi.Watcher
{
	public class AckWatchPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:62")]
		public void Line62()
		{
			// tag::9116ee8a5b00cc877291ed5559563f24[]
			var response0 = new SearchResponse<object>();
			// end::9116ee8a5b00cc877291ed5559563f24[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch
			{
			  ""trigger"" : {
			    ""schedule"" : {
			      ""yearly"" : { ""in"" : ""february"", ""on"" : 29, ""at"" : ""noon"" }
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

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:96")]
		public void Line96()
		{
			// tag::e827a9040e137410d62d10bb3b3cbb71[]
			var response0 = new SearchResponse<object>();
			// end::e827a9040e137410d62d10bb3b3cbb71[]

			response0.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:134")]
		public void Line134()
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

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:190")]
		public void Line190()
		{
			// tag::1b0dc9d076bbb58c6a2953ef4323d2fc[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::1b0dc9d076bbb58c6a2953ef4323d2fc[]

			response0.MatchesExample(@"PUT _watcher/watch/my_watch/_ack/test_index");

			response1.MatchesExample(@"GET _watcher/watch/my_watch");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:245")]
		public void Line245()
		{
			// tag::8051766cadded0892290bc2cc06e145c[]
			var response0 = new SearchResponse<object>();
			// end::8051766cadded0892290bc2cc06e145c[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_ack/action1,action2");
		}

		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/rest-api/watcher/ack-watch.asciidoc:253")]
		public void Line253()
		{
			// tag::df7dbac966b67404b8bfa9cdda5ef480[]
			var response0 = new SearchResponse<object>();
			// end::df7dbac966b67404b8bfa9cdda5ef480[]

			response0.MatchesExample(@"POST _watcher/watch/my_watch/_ack");
		}
	}
}
