// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.IndexModules
{
	public class SlowlogPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/slowlog.asciidoc:31")]
		public void Line31()
		{
			// tag::45c83ca223d15f758faa61fc42788362[]
			var response0 = new SearchResponse<object>();
			// end::45c83ca223d15f758faa61fc42788362[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index.search.slowlog.threshold.query.warn"": ""10s"",
			    ""index.search.slowlog.threshold.query.info"": ""5s"",
			    ""index.search.slowlog.threshold.query.debug"": ""2s"",
			    ""index.search.slowlog.threshold.query.trace"": ""500ms"",
			    ""index.search.slowlog.threshold.fetch.warn"": ""1s"",
			    ""index.search.slowlog.threshold.fetch.info"": ""800ms"",
			    ""index.search.slowlog.threshold.fetch.debug"": ""500ms"",
			    ""index.search.slowlog.threshold.fetch.trace"": ""200ms""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/slowlog.asciidoc:110")]
		public void Line110()
		{
			// tag::c6226c544951d210c37659e9457d8887[]
			var response0 = new SearchResponse<object>();
			// end::c6226c544951d210c37659e9457d8887[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index.indexing.slowlog.threshold.index.warn"": ""10s"",
			    ""index.indexing.slowlog.threshold.index.info"": ""5s"",
			    ""index.indexing.slowlog.threshold.index.debug"": ""2s"",
			    ""index.indexing.slowlog.threshold.index.trace"": ""500ms"",
			    ""index.indexing.slowlog.source"": ""1000""
			}");
		}
	}
}
