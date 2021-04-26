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

namespace Tests.XPack.MachineLearning.SetUpgradeMode
{
	[SkipVersion("<7.7.0", "Introduced in 7.7.0")]
	public class SetUpgradeModeApiTests : MachineLearningIntegrationTestBase<SetUpgradeModeResponse, ISetUpgradeModeRequest, SetUpgradeModeDescriptor, SetUpgradeModeRequest>
	{
		public SetUpgradeModeApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;
		protected override Func<SetUpgradeModeDescriptor, ISetUpgradeModeRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override SetUpgradeModeRequest Initializer => new SetUpgradeModeRequest();
		protected override string UrlPath => $"/_ml/set_upgrade_mode";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.SetUpgradeMode(f),
			(client, f) => client.MachineLearning.SetUpgradeModeAsync(f),
			(client, r) => client.MachineLearning.SetUpgradeMode(r),
			(client, r) => client.MachineLearning.SetUpgradeModeAsync(r)
		);

		protected override SetUpgradeModeDescriptor NewDescriptor() => new SetUpgradeModeDescriptor();

		protected override void ExpectResponse(SetUpgradeModeResponse response)
		{
			response.ShouldBeValid();
			response.Acknowledged.Should().BeTrue();
		}
	}
}
