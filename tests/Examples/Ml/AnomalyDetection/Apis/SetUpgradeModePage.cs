// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class SetUpgradeModePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/set-upgrade-mode.asciidoc:76")]
		public void Line76()
		{
			// tag::ae4aa368617637a390074535df86e64b[]
			var response0 = new SearchResponse<object>();
			// end::ae4aa368617637a390074535df86e64b[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=true&timeout=10m");
		}

		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/set-upgrade-mode.asciidoc:98")]
		public void Line98()
		{
			// tag::8e9e7dc5fad2b2b8e74ab4dc225d9c53[]
			var response0 = new SearchResponse<object>();
			// end::8e9e7dc5fad2b2b8e74ab4dc225d9c53[]

			response0.MatchesExample(@"POST _ml/set_upgrade_mode?enabled=false&timeout=10m");
		}
	}
}
