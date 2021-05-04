// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.ClusterReroute
{
	public class ClusterRerouteApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ClusterRerouteResponse, IClusterRerouteRequest, ClusterRerouteDescriptor,
			ClusterRerouteRequest>
	{
		public ClusterRerouteApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// get a suitable load of projects in order to get a decent task status out
			foreach (var (_, index) in values)
			{
				var createIndex = client.Indices.Create(index, i => i
					.Settings(settings => settings.Analysis(DefaultSeeder.ProjectAnalysisSettings))
					.Map<Project>(DefaultSeeder.ProjectTypeMappings)
				);
				createIndex.ShouldBeValid();
				client.IndexMany(Project.Generator.Generate(100), index).ShouldBeValid();
				client.Indices.Refresh(index).ShouldBeValid();
			}

		}
		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => new
		{
			commands = new[]
			{
				new Dictionary<string, object>
				{
					{
						"allocate_empty_primary", new
						{
							index = CallIsolatedValue,
							node = "x",
							shard = 0,
							accept_data_loss = true
						}
					}
				},
				new Dictionary<string, object>
				{
					{
						"allocate_stale_primary", new
						{
							index = CallIsolatedValue,
							node = "x",
							shard = 0,
							accept_data_loss = true
						}
					}
				},
				new Dictionary<string, object>
				{
					{
						"allocate_replica", new
						{
							index = CallIsolatedValue,
							node = "x",
							shard = 0
						}
					}
				},
				new Dictionary<string, object>
				{
					{
						"move", new
						{
							to_node = "y",
							from_node = "x",
							index = CallIsolatedValue,
							shard = 0
						}
					}
				},
				new Dictionary<string, object>
				{
					{
						"cancel", new
						{
							index = CallIsolatedValue,
							node = "x",
							shard = 1
						}
					}
				},
			}
		};

		protected override int ExpectStatusCode => 400;

		protected override Func<ClusterRerouteDescriptor, IClusterRerouteRequest> Fluent => c => c
			.AllocateEmptyPrimary(a => a
				.Index(CallIsolatedValue)
				.Node("x")
				.Shard(0)
				.AcceptDataLoss()
			)
			.AllocateStalePrimary(a => a
				.Index(CallIsolatedValue)
				.Node("x")
				.Shard(0)
				.AcceptDataLoss()
			)
			.AllocateReplica(a => a
				.Index(CallIsolatedValue)
				.Node("x")
				.Shard(0)
			)
			.Move(a => a
				.ToNode("y")
				.FromNode("x")
				.Index(CallIsolatedValue)
				.Shard(0)
			)
			.Cancel(a => a
				.Index(CallIsolatedValue)
				.Node("x")
				.Shard(1)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClusterRerouteRequest Initializer => new ClusterRerouteRequest
		{
			Commands = new List<IClusterRerouteCommand>
			{
				new AllocateEmptyPrimaryRerouteCommand { Index = CallIsolatedValue, Node = "x", Shard = 0, AcceptDataLoss = true },
				new AllocateStalePrimaryRerouteCommand { Index = CallIsolatedValue, Node = "x", Shard = 0, AcceptDataLoss = true },
				new AllocateReplicaClusterRerouteCommand { Index = CallIsolatedValue, Node = "x", Shard = 0 },
				new MoveClusterRerouteCommand { Index = CallIsolatedValue, FromNode = "x", ToNode = "y", Shard = 0 },
				new CancelClusterRerouteCommand() { Index = CallIsolatedValue, Node = "x", Shard = 1 }
			}
		};

		protected override string UrlPath => "/_cluster/reroute";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.Reroute(f),
			(client, f) => client.Cluster.RerouteAsync(f),
			(client, r) => client.Cluster.Reroute(r),
			(client, r) => client.Cluster.RerouteAsync(r)
		);

		protected override void ExpectResponse(ClusterRerouteResponse response)
		{
			response.ShouldNotBeValid();
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			//targetting unknown node x
			response.ServerError.Error.Reason.Should().Contain("No data for shard [0] of index");
			response.ServerError.Error.Type.Should().Contain("illegal_argument_exception");
		}
	}


	//TODO simple integration test against isolated index to test happy flow
}
