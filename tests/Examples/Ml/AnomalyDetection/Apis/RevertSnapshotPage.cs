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
	public class RevertSnapshotPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/anomaly-detection/apis/revert-snapshot.asciidoc:62")]
		public void Line62()
		{
			// tag::b173b1b5bab610668ab74d5b2ab03f78[]
			var response0 = new SearchResponse<object>();
			// end::b173b1b5bab610668ab74d5b2ab03f78[]

			response0.MatchesExample(@"POST _ml/anomaly_detectors/high_sum_total_sales/model_snapshots/1575402237/_revert
			{
			  ""delete_intervening_results"": true
			}");
		}
	}
}
