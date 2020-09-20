// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
