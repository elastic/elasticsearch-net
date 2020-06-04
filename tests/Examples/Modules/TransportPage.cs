// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Modules
{
	public class TransportPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/transport.asciidoc:155")]
		public void Line155()
		{
			// tag::939e79dee613238f9512fb9cbf0be816[]
			var response0 = new SearchResponse<object>();
			// end::939e79dee613238f9512fb9cbf0be816[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""logger.org.elasticsearch.transport.TransportService.tracer"" : ""TRACE""
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/transport.asciidoc:168")]
		public void Line168()
		{
			// tag::cecbbd7b4ec1bf82fd84ae96099febcc[]
			var response0 = new SearchResponse<object>();
			// end::cecbbd7b4ec1bf82fd84ae96099febcc[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""transport.tracer.include"" : ""*"",
			      ""transport.tracer.exclude"" : ""internal:coordination/fault_detection/*""
			   }
			}");
		}
	}
}
