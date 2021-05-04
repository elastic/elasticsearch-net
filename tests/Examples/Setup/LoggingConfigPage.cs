// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Setup
{
	public class LoggingConfigPage : ExampleBase
	{
		[U]
		[Description("setup/logging-config.asciidoc:155")]
		public void Line155()
		{
			// tag::8e6bfb4441ffa15c86d5dc20fa083571[]
			var settingsResponse = client.Cluster.PutSettings(s => s
				.Transient(t => t
					.Add("logger.org.elasticsearch.transport", "trace")
				)
			);
			// end::8e6bfb4441ffa15c86d5dc20fa083571[]

			settingsResponse.MatchesExample(@"PUT /_cluster/settings
			{
			  ""transient"": {
			    ""logger.org.elasticsearch.transport"": ""trace""
			  }
			}");
		}
	}
}
