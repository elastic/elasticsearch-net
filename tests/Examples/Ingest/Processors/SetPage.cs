// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Processors
{
	public class SetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/set.asciidoc:33")]
		public void Line33()
		{
			// tag::366b29ef910f12c7fbced35f39000953[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::366b29ef910f12c7fbced35f39000953[]

			response0.MatchesExample(@"PUT _ingest/pipeline/set_os
			{
			  ""description"": ""sets the value of host.os.name from the field os"",
			  ""processors"": [
			    {
			      ""set"": {
			        ""field"": ""host.os.name"",
			        ""value"": ""{{os}}""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"POST _ingest/pipeline/set_os/_simulate
			{
			  ""docs"": [
			    {
			      ""_source"": {
			        ""os"": ""Ubuntu""
			      }
			    }
			  ]
			}");
		}
	}
}
