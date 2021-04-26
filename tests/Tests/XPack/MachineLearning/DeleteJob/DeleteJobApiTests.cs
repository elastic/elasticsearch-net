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

namespace Tests.XPack.MachineLearning.DeleteJob
{
	public class DeleteJobApiTests : MachineLearningIntegrationTestBase<DeleteJobResponse, IDeleteJobRequest, DeleteJobDescriptor, DeleteJobRequest>
	{
		public DeleteJobApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteJobDescriptor, IDeleteJobRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteJobRequest Initializer => new DeleteJobRequest(CallIsolatedValue);
		protected override string UrlPath => $"_ml/anomaly_detectors/{CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteJob(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.DeleteJobAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.DeleteJob(r),
			(client, r) => client.MachineLearning.DeleteJobAsync(r)
		);

		protected override DeleteJobDescriptor NewDescriptor() => new DeleteJobDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteJobResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
