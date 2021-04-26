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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.DocumentationTests;

namespace Tests.Indices.AliasManagement.GetIndicesPointingToAlias
{
	public class GetIndicesPointingToAliasTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
	{
		private static readonly string Unique = RandomString();
		private static readonly string Alias = "alias-" + Unique;

		private static readonly string[] Indices =
		{
			$"alias-index-{Unique}-1",
			$"alias-index-{Unique}-2",
			$"alias-index-{Unique}-3"
		};

		private readonly IElasticClient _client;

		public GetIndicesPointingToAliasTests(WritableCluster cluster) : base(cluster)
		{
			_client = cluster.Client;

			foreach (var index in Indices)
			{
				if (_client.Indices.Exists(index).Exists) continue;

				lock (Unique)
				{
					if (_client.Indices.Exists(index).Exists) continue;

					var createResponse = _client.Indices.Create(index, c => c
						.Settings(s => s
							.NumberOfShards(1)
							.NumberOfReplicas(0)
						)
						.Aliases(a => a
							.Alias(Alias)
						)
					);
					createResponse.ShouldBeValid();
				}
			}
		}

		[I]
		public void ShouldGetAliasesPointingToIndex()
		{
			var indicesPointingToAlias = _client.GetIndicesPointingToAlias(Alias);

			indicesPointingToAlias.Should().NotBeEmpty().And.HaveCount(3);
			indicesPointingToAlias.Should().Contain(Indices);
		}

		[I]
		public async Task ShouldGetAliasesPointingToIndexAsync()
		{
			var indicesPointingToAlias = await _client.GetIndicesPointingToAliasAsync(Alias);

			indicesPointingToAlias.Should().NotBeEmpty().And.HaveCount(3);
			indicesPointingToAlias.Should().Contain(Indices);
		}
	}
}
