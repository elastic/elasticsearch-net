// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Setup
{
	public class SecureSettingsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/secure-settings.asciidoc:35")]
		public void Line35()
		{
			// tag::eb7e3aaed0c8f3f8e3462bf3df9a7a5c[]
			var response0 = new SearchResponse<object>();
			// end::eb7e3aaed0c8f3f8e3462bf3df9a7a5c[]

			response0.MatchesExample(@"POST _nodes/reload_secure_settings
			{
			  ""secure_settings_password"": ""s3cr3t"" <1>
			}");
		}
	}
}
