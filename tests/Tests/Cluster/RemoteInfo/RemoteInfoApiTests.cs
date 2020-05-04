// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using M = System.Collections.Generic.Dictionary<string, object>;
using static Nest.Infer;

namespace Tests.Cluster.RemoteInfo
{
	public class RemoteInfoApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, RemoteInfoResponse, IRemoteInfoRequest, RemoteInfoDescriptor, RemoteInfoRequest>
	{
		public RemoteInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_remote/info";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var oldWay = new M
			{
				{
					"search", new M
					{
						{
							"remote", new M
							{
								{
									"cluster_one", new M
									{
										{ "seeds", new[] { "127.0.0.1:9300", "127.0.0.1:9301" } }
									}
								},
								{
									"cluster_two", new M
									{
										{ "seeds", new[] { "127.0.0.1:9300" } }
									}
								}
							}
						}
					}
				}
			};
			/**
			 * As of 6.5.0 you can also use the following helper class which uses
			 * the new way to configure remote clusters.
			 */
			// ReSharper disable once UnusedVariable
			var newWay = new RemoteClusterConfiguration()
			{
				{ "cluster_one", "127.0.0.1:9300", "127.0.0.1:9301" },
				{ "cluster_two", "127.0.0.1:9300" }
			};
			var enableRemoteClusters = client.Cluster.PutSettings(new ClusterPutSettingsRequest
			{
				Transient = oldWay
			});
			enableRemoteClusters.ShouldBeValid();

			var remoteSearch = client.Search<Project>(s => s.Index(Index<Project>("cluster_one").And<Project>("cluster_two")));
			remoteSearch.ShouldBeValid();
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.RemoteInfo(),
			(client, f) => client.Cluster.RemoteInfoAsync(),
			(client, r) => client.Cluster.RemoteInfo(r),
			(client, r) => client.Cluster.RemoteInfoAsync(r)
		);

		protected override void ExpectResponse(RemoteInfoResponse response)
		{
			response.Remotes.Should()
				.NotBeEmpty()
				.And.ContainKey("cluster_one")
				.And.ContainKey("cluster_two");

			foreach (var (name, remote) in response.Remotes)
			{
				if (!name.StartsWith("cluster_")) continue;
				remote.Connected.Should().BeTrue();
				remote.Seeds.Should().NotBeNullOrEmpty();
				remote.InitialConnectTimeout.Should().NotBeNull().And.Be("30s");
				remote.MaxConnectionsPerCluster.Should().BeGreaterThan(0, "max_connections_per_cluster");
				remote.NumNodesConnected.Should().BeGreaterThan(0, "num_nodes_connected");
			}
		}
	}
}
