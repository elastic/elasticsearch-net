// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.XPack.Docs.En.Security.Auditing
{
	public class OutputLogfilePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("../../x-pack/docs/en/security/auditing/output-logfile.asciidoc:27")]
		public void Line27()
		{
			// tag::a465177ff9450120097e7f6cf13dbc33[]
			var response0 = new SearchResponse<object>();
			// end::a465177ff9450120097e7f6cf13dbc33[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""persistent"": {
			    ""logger.org.elasticsearch.xpack.security.audit.logfile.DeprecatedLoggingAuditTrail"": ""off""
			  }
			}");
		}
	}
}
