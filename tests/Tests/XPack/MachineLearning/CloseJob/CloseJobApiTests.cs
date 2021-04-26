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

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.CloseJob
{
	public class CloseJobApiTests : MachineLearningIntegrationTestBase<CloseJobResponse, ICloseJobRequest, CloseJobDescriptor, CloseJobRequest>
	{
		public CloseJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<CloseJobDescriptor, ICloseJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override CloseJobRequest Initializer => new CloseJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"/_ml/anomaly_detectors/{CallIsolatedValue}/_close";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				OpenJob(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.CloseJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.CloseJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.CloseJob(r),
			(client, r) => client.MachineLearning.CloseJobAsync(r)
		);

		protected override CloseJobDescriptor NewDescriptor() => new CloseJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(CloseJobResponse response) => response.Closed.Should().BeTrue();
	}
}
