// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Monitoring
{
	public class ProductionPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/production.asciidoc:50")]
		public void Line50()
		{
			// tag::a941fd568f2e20e13df909ab24506073[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a941fd568f2e20e13df909ab24506073[]

			response0.MatchesExample(@"GET _cluster/settings");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.collection.enabled"": false
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("monitoring/production.asciidoc:88")]
		public void Line88()
		{
			// tag::0b47b0bef81b9b5eecfb3775695bd6ad[]
			var response0 = new SearchResponse<object>();
			// end::0b47b0bef81b9b5eecfb3775695bd6ad[]

			response0.MatchesExample(@"POST /_security/user/remote_monitor
			{
			  ""password"" : ""changeme"",
			  ""roles"" : [ ""remote_monitoring_agent""],
			  ""full_name"" : ""Internal Agent For Remote Monitoring""
			}");
		}
	}
}
