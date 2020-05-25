// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Monitoring
{
	public class EsmsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("monitoring/esms.asciidoc:34")]
		public void Line34()
		{
			// tag::fb2b8d642e16132eebcff4f8b6d592d1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::fb2b8d642e16132eebcff4f8b6d592d1[]

			response0.MatchesExample(@"GET _cluster/settings");

			response1.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.collection.enabled"": true
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("monitoring/esms.asciidoc:43")]
		public void Line43()
		{
			// tag::519603821dc5b883fc2cf50e3d164084[]
			var response0 = new SearchResponse<object>();
			// end::519603821dc5b883fc2cf50e3d164084[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""persistent"": {
			    ""xpack.monitoring.elasticsearch.collection.enabled"": false
			  }
			}");
		}
	}
}
