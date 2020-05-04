// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Modules.Cluster
{
	public class MiscPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/misc.asciidoc:79")]
		public void Line79()
		{
			// tag::4207219a892339e8f3abe0df8723dd27[]
			var response0 = new SearchResponse<object>();
			// end::4207219a892339e8f3abe0df8723dd27[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""persistent"": {
			    ""cluster.metadata.administrator"": ""sysadmin@example.com""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/cluster/misc.asciidoc:119")]
		public void Line119()
		{
			// tag::c3fa14da3d0b0f93fb59bb5386b7e776[]
			var response0 = new SearchResponse<object>();
			// end::c3fa14da3d0b0f93fb59bb5386b7e776[]

			response0.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""logger.org.elasticsearch.indices.recovery"": ""DEBUG""
			  }
			}");
		}
	}
}