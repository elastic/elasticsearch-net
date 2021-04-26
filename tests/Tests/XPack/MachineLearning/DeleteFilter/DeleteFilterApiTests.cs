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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	[SkipVersion("<6.4.0", "Filter functions for machine learning stabilised in 6.4.0")]
	public class DeleteFilterApiTests : MachineLearningIntegrationTestBase<DeleteFilterResponse, IDeleteFilterRequest, DeleteFilterDescriptor, DeleteFilterRequest>
	{
		public DeleteFilterApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
				PutFilter(client, callUniqueValue.Value);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteFilterDescriptor, IDeleteFilterRequest> Fluent => f => f;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteFilterRequest Initializer => new DeleteFilterRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/filters/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.DeleteFilter(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.DeleteFilterAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.DeleteFilter(r),
			(client, r) => client.MachineLearning.DeleteFilterAsync(r)
		);

		protected override DeleteFilterDescriptor NewDescriptor() => new DeleteFilterDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(DeleteFilterResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
