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

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedApiTests
		: MachineLearningIntegrationTestBase<DeleteDatafeedResponse, IDeleteDatafeedRequest, DeleteDatafeedDescriptor, DeleteDatafeedRequest>
	{
		public DeleteDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteDatafeedRequest Initializer => new DeleteDatafeedRequest(CallIsolatedValue + "-datafeed");
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}-datafeed";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				PutJob(client, callUniqueValue.Value);
				PutDatafeed(client, callUniqueValue.Value);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteDatafeed(CallIsolatedValue + "-datafeed", f),
			(client, f) => client.MachineLearning.DeleteDatafeedAsync(CallIsolatedValue + "-datafeed", f),
			(client, r) => client.MachineLearning.DeleteDatafeed(r),
			(client, r) => client.MachineLearning.DeleteDatafeedAsync(r)
		);

		protected override DeleteDatafeedDescriptor NewDescriptor() => new DeleteDatafeedDescriptor(CallIsolatedValue + "-datafeed");

		protected override void ExpectResponse(DeleteDatafeedResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
