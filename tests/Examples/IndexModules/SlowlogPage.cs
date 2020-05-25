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
		[Description("index-modules/slowlog.asciidoc:33")]
		public void Line33()
		{
			// tag::fa0b341d790a4da480b47bf501835359[]
			var response0 = new SearchResponse<object>();
			// end::fa0b341d790a4da480b47bf501835359[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index.search.slowlog.threshold.query.warn"": ""10s"",
			    ""index.search.slowlog.threshold.query.info"": ""5s"",
			    ""index.search.slowlog.threshold.query.debug"": ""2s"",
			    ""index.search.slowlog.threshold.query.trace"": ""500ms"",
			    ""index.search.slowlog.threshold.fetch.warn"": ""1s"",
			    ""index.search.slowlog.threshold.fetch.info"": ""800ms"",
			    ""index.search.slowlog.threshold.fetch.debug"": ""500ms"",
			    ""index.search.slowlog.threshold.fetch.trace"": ""200ms"",
			    ""index.search.slowlog.level"": ""info""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/slowlog.asciidoc:143")]
		public void Line143()
		{
			// tag::44a16db65121edaf099d944819356e2c[]
			var response0 = new SearchResponse<object>();
			// end::44a16db65121edaf099d944819356e2c[]

			response0.MatchesExample(@"PUT /twitter/_settings
			{
			    ""index.indexing.slowlog.threshold.index.warn"": ""10s"",
			    ""index.indexing.slowlog.threshold.index.info"": ""5s"",
			    ""index.indexing.slowlog.threshold.index.debug"": ""2s"",
			    ""index.indexing.slowlog.threshold.index.trace"": ""500ms"",
			    ""index.indexing.slowlog.level"": ""info"",
			    ""index.indexing.slowlog.source"": ""1000""
			}");
		}
	}
}
