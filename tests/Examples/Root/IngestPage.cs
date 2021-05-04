// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Root
{
	public class IngestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest.asciidoc:32")]
		public void Line32()
		{
			// tag::55704b69b03239fe13293fc7622d27da[]
			var response0 = new SearchResponse<object>();
			// end::55704b69b03239fe13293fc7622d27da[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my_pipeline_id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""new""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest.asciidoc:50")]
		public void Line50()
		{
			// tag::6f3a4b4a01b6fae193897f00cb4855d0[]
			var response0 = new SearchResponse<object>();
			// end::6f3a4b4a01b6fae193897f00cb4855d0[]

			response0.MatchesExample(@"PUT my-index/_doc/my-id?pipeline=my_pipeline_id
			{
			  ""foo"": ""bar""
			}");
		}
	}
}
