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

namespace Tests.Cat.CatRepositories
{
	[SkipVersion("<2.1.0", "")]
	public class CatRepositoriesApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, CatResponse<CatRepositoriesRecord>, ICatRepositoriesRequest, CatRepositoriesDescriptor,
			CatRepositoriesRequest>
	{
		private static readonly string RepositoryName = RandomString();

		public CatRepositoriesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_cat/repositories";

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
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Repositories(f),
			(client, f) => client.Cat.RepositoriesAsync(f),
			(client, r) => client.Cat.Repositories(r),
			(client, r) => client.Cat.RepositoriesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatRepositoriesRecord> response) => response.Records.Should()
			.NotBeEmpty()
			.And.OnlyContain(r =>
				!string.IsNullOrEmpty(r.Id)
				&& !string.IsNullOrEmpty(r.Type)
			);
	}
}
