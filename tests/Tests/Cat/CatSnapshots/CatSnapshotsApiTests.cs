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
using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatSnapshots
{
	[SkipVersion("<2.1.0", "")]
	public class CatSnapshotsApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, CatResponse<CatSnapshotsRecord>, ICatSnapshotsRequest, CatSnapshotsDescriptor,
			CatSnapshotsRequest>
	{
		private static readonly string RepositoryName = RandomString();
		private static readonly string SnapshotIndexName = RandomString();
		private static readonly string SnapshotName = RandomString();

		public CatSnapshotsApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_cat/snapshots/{RepositoryName}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			if (!TestClient.Configuration.RunIntegrationTests) return;

			var repositoryLocation = Path.Combine(Cluster.FileSystem.RepositoryPath, RandomString());

			var create = Client.Snapshot.CreateRepository(RepositoryName, cr => cr
				.FileSystem(fs => fs
					.Settings(repositoryLocation)
				)
			);

			if (!create.IsValid || !create.Acknowledged)
				throw new Exception("Setup: failed to create snapshot repository");

			Client.Indices.Create(SnapshotIndexName);
			Client.Cluster.Health(SnapshotIndexName, g => g.WaitForStatus(WaitForStatus.Yellow));
			client.Snapshot.Snapshot(RepositoryName, SnapshotName, s => s.WaitForCompletion().Index(SnapshotIndexName));
		}

		protected override CatSnapshotsRequest Initializer => new CatSnapshotsRequest(RepositoryName);
		protected override Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> Fluent => f => f.RepositoryName(RepositoryName);

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Snapshots(f),
			(client, f) => client.Cat.SnapshotsAsync(f),
			(client, r) => client.Cat.Snapshots(r),
			(client, r) => client.Cat.SnapshotsAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatSnapshotsRecord> response) =>
			response.Records.Should().NotBeEmpty().And.OnlyContain(r => r.Status == "SUCCESS");
	}
}
