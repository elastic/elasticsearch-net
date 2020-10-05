// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
