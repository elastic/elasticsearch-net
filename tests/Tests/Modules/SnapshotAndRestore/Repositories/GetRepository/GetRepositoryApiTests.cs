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

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests
		: ApiTestBase<WritableCluster, GetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		public GetRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(CallIsolatedValue);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.GetRepository(f),
			(client, f) => client.Snapshot.GetRepositoryAsync(f),
			(client, r) => client.Snapshot.GetRepository(r),
			(client, r) => client.Snapshot.GetRepositoryAsync(r)
		);

	}
}
