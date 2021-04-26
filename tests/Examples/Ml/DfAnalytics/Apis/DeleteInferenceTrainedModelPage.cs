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

namespace Examples.Ml.DfAnalytics.Apis
{
	public class DeleteInferenceTrainedModelPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ml/df-analytics/apis/delete-inference-trained-model.asciidoc:57")]
		public void Line57()
		{
			// tag::334e28ff99f12b721b9942bad3a78f94[]
			var response0 = new SearchResponse<object>();
			// end::334e28ff99f12b721b9942bad3a78f94[]

			response0.MatchesExample(@"DELETE _ml/inference/regression-job-one-1574775307356");
		}
	}
}
