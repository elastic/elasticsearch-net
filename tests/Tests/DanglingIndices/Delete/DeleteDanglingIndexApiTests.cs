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
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.DanglingIndices.Delete
{
	public class DeleteDanglingIndexApiTests
		: ApiTestBase<ReadOnlyCluster, DeleteDanglingIndexResponse, IDeleteDanglingIndexRequest, DeleteDanglingIndexDescriptor, DeleteDanglingIndexRequest>
	{
		private static readonly string IndexUuid = "indexuuid";

		public DeleteDanglingIndexApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<DeleteDanglingIndexDescriptor, IDeleteDanglingIndexRequest> Fluent => d => d;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteDanglingIndexRequest Initializer => new DeleteDanglingIndexRequest(IndexUuid);
		protected override string UrlPath => $"/_dangling/{IndexUuid}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DanglingIndices.DeleteDanglingIndex(IndexUuid, f),
			(client, f) => client.DanglingIndices.DeleteDanglingIndexAsync(IndexUuid, f),
			(client, r) => client.DanglingIndices.DeleteDanglingIndex(r),
			(client, r) => client.DanglingIndices.DeleteDanglingIndexAsync(r)
		);

		protected override DeleteDanglingIndexDescriptor NewDescriptor() => new DeleteDanglingIndexDescriptor(IndexUuid);
	}
}
