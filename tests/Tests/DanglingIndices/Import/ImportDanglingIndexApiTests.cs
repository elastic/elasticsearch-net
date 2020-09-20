// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.DanglingIndices.Import
{
	public class ImportDanglingIndexApiTests
		: ApiTestBase<ReadOnlyCluster, ImportDanglingIndexResponse, IImportDanglingIndexRequest, ImportDanglingIndexDescriptor, ImportDanglingIndexRequest>
	{
		private static readonly string IndexUuid = "indexuuid";

		public ImportDanglingIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<ImportDanglingIndexDescriptor, IImportDanglingIndexRequest> Fluent => d => d
			.AcceptDataLoss();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ImportDanglingIndexRequest Initializer => new ImportDanglingIndexRequest(IndexUuid) { AcceptDataLoss = true };
		protected override string UrlPath => $"/_dangling/{IndexUuid}?accept_data_loss=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DanglingIndices.ImportDanglingIndex(IndexUuid, f),
			(client, f) => client.DanglingIndices.ImportDanglingIndexAsync(IndexUuid, f),
			(client, r) => client.DanglingIndices.ImportDanglingIndex(r),
			(client, r) => client.DanglingIndices.ImportDanglingIndexAsync(r)
		);

		protected override ImportDanglingIndexDescriptor NewDescriptor() => new ImportDanglingIndexDescriptor(IndexUuid);
	}
}
