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

namespace Tests.XPack.MachineLearning.DeleteExpiredData
{
	public class DeleteExpiredDataApiTests
		: MachineLearningIntegrationTestBase<DeleteExpiredDataResponse, IDeleteExpiredDataRequest, DeleteExpiredDataDescriptor,
			DeleteExpiredDataRequest>
	{
		public DeleteExpiredDataApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override DeleteExpiredDataRequest Initializer => new DeleteExpiredDataRequest();
		protected override string UrlPath => $"_ml/_delete_expired_data";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteExpiredData(f),
			(client, f) => client.MachineLearning.DeleteExpiredDataAsync(f),
			(client, r) => client.MachineLearning.DeleteExpiredData(r),
			(client, r) => client.MachineLearning.DeleteExpiredDataAsync(r)
		);

		protected override DeleteExpiredDataDescriptor NewDescriptor() => new DeleteExpiredDataDescriptor();

		protected override void ExpectResponse(DeleteExpiredDataResponse response) => response.Deleted.Should().BeTrue();
	}
}
