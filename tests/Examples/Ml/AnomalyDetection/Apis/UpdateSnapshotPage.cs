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

namespace Examples.Ml.AnomalyDetection.Apis
{
	public class UpdateSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/update-snapshot.asciidoc:52")]
		public void Line52()
		{
			// tag::3b9c54604535d97e8368d47148aecc6f[]
			var response0 = new SearchResponse<object>();
			// end::3b9c54604535d97e8368d47148aecc6f[]

			response0.MatchesExample(@"POST
			_ml/anomaly_detectors/it_ops_new_logs/model_snapshots/1491852978/_update
			{
			  ""description"": ""Snapshot 1"",
			  ""retain"": true
			}");
		}
	}
}
