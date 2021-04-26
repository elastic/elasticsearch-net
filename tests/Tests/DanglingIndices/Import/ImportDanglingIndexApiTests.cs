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
using Elasticsearch.Net;
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
