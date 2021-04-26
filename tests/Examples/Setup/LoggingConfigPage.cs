/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
