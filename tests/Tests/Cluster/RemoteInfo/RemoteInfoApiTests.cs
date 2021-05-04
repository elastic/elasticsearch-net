// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
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
	/*
	 * {
  "error" : {
    "root_cause" : [
      {
        "type" : "illegal_argument_exception",
        "reason" : "transient setting [search.remote.cluster_one.seeds], not recognized",
"stack_trace" : "[transient setting [search.remote.cluster_one.seeds], not recognized]; nested: IllegalArgumentException[transient setting [search.remote.cluster_one.seeds], not recognized];\n\tat org.elasticsearch.ElasticsearchException.guessRootCauses(ElasticsearchException.java:639)\n\tat org.elasticsearch.ElasticsearchException.generateFailureXContent(ElasticsearchException.java:567)\n\tat org.elasticsearch.rest.BytesRestResponse.build(BytesRestResponse.java:138)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:96)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:91)\n\tat org.elasticsearch.rest.action.RestActionListener.onFailure(RestActionListener.java:58)\n\tat org.elasticsearch.action.support.TransportAction$1.onFailure(TransportAction.java:79)\n\tat org.elasticsearch.action.support.master.TransportMasterNodeAction$AsyncSingleAction.lambda$doStart$2(TransportMasterNodeAction.java:155)\n\tat org.elasticsearch.action.ActionListener$2.onFailure(ActionListener.java:93)\n\tat org.elasticsearch.cluster.AckedClusterStateUpdateTask.onFailure(AckedClusterStateUpdateTask.java:79)\n\tat org.elasticsearch.action.admin.cluster.settings.TransportClusterUpdateSettingsAction$1.onFailure(TransportClusterUpdateSettingsAction.java:179)\n\tat org.elasticsearch.cluster.service.MasterService$SafeClusterStateTaskListener.onFailure(MasterService.java:499)\n\tat org.elasticsearch.cluster.service.MasterService$TaskOutputs.notifyFailedTasks(MasterService.java:432)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:211)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\nCaused by: java.lang.IllegalArgumentException: transient setting [search.remote.cluster_one.seeds], not recognized\n\tat org.elasticsearch.common.settings.AbstractScopedSettings.updateSettings(AbstractScopedSettings.java:772)\n\tat org.elasticsearch.common.settings.AbstractScopedSettings.updateDynamicSettings(AbstractScopedSettings.java:718)\n\tat org.elasticsearch.action.admin.cluster.settings.SettingsUpdater.updateSettings(SettingsUpdater.java:79)\n\tat org.elasticsearch.action.admin.cluster.settings.TransportClusterUpdateSettingsAction$1.execute(TransportClusterUpdateSettingsAction.java:185)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\t... 9 more\n"
      }
    ],
    "type" : "illegal_argument_exception",
    "reason" : "transient setting [search.remote.cluster_one.seeds], not recognized",
"stack_trace" : "java.lang.IllegalArgumentException: transient setting [search.remote.cluster_one.seeds], not recognized\n\tat org.elasticsearch.common.settings.AbstractScopedSettings.updateSettings(AbstractScopedSettings.java:772)\n\tat org.elasticsearch.common.settings.AbstractScopedSettings.updateDynamicSettings(AbstractScopedSettings.java:718)\n\tat org.elasticsearch.action.admin.cluster.settings.SettingsUpdater.updateSettings(SettingsUpdater.java:79)\n\tat org.elasticsearch.action.admin.cluster.settings.TransportClusterUpdateSettingsAction$1.execute(TransportClusterUpdateSettingsAction.java:185)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
  },
  "status" : 400
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO broken in snapshot")]
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
