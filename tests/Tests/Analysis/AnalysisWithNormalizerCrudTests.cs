// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Framework.Extensions.Promisify;

namespace Tests.Analysis
{
	/*
	 * # Response:
{
  "error" : {
    "root_cause" : [
      {
        "type" : "illegal_argument_exception",
        "reason" : "Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]",
"stack_trace" : "[Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]]; nested: IllegalArgumentException[Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]];\n\tat org.elasticsearch.ElasticsearchException.guessRootCauses(ElasticsearchException.java:639)\n\tat org.elasticsearch.ElasticsearchException.generateFailureXContent(ElasticsearchException.java:567)\n\tat org.elasticsearch.rest.BytesRestResponse.build(BytesRestResponse.java:138)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:96)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:91)\n\tat org.elasticsearch.rest.action.RestActionListener.onFailure(RestActionListener.java:58)\n\tat org.elasticsearch.action.support.TransportAction$1.onFailure(TransportAction.java:79)\n\tat org.elasticsearch.action.support.master.TransportMasterNodeAction$AsyncSingleAction.lambda$doStart$2(TransportMasterNodeAction.java:155)\n\tat org.elasticsearch.action.ActionListener$2.onFailure(ActionListener.java:93)\n\tat org.elasticsearch.action.admin.indices.settings.put.TransportUpdateSettingsAction$1.onFailure(TransportUpdateSettingsAction.java:105)\n\tat org.elasticsearch.action.support.ContextPreservingActionListener.onFailure(ContextPreservingActionListener.java:50)\n\tat org.elasticsearch.cluster.AckedClusterStateUpdateTask.onFailure(AckedClusterStateUpdateTask.java:79)\n\tat org.elasticsearch.cluster.service.MasterService$SafeClusterStateTaskListener.onFailure(MasterService.java:499)\n\tat org.elasticsearch.cluster.service.MasterService$TaskOutputs.notifyFailedTasks(MasterService.java:432)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:211)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\nCaused by: java.lang.IllegalArgumentException: Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]\n\tat org.elasticsearch.cluster.metadata.MetaDataUpdateSettingsService$1.execute(MetaDataUpdateSettingsService.java:145)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\t... 9 more\n"
      }
    ],
    "type" : "illegal_argument_exception",
    "reason" : "Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]",
"stack_trace" : "java.lang.IllegalArgumentException: Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysiswithnormalizer/Ey_VkDnMRtGHAZi06oU2hw]]\n\tat org.elasticsearch.cluster.metadata.MetaDataUpdateSettingsService$1.execute(MetaDataUpdateSettingsService.java:145)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
  },
  "status" : 400
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "")]
	public class AnalysisWithNormalizerCrudTests : AnalysisCrudTests
	{
		public AnalysisWithNormalizerCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = AnalysisUsageTests.AnalyzersInitializer.Analysis.Analyzers,
					CharFilters = AnalysisUsageTests.CharFiltersInitializer.Analysis.CharFilters,
					Tokenizers = AnalysisUsageTests.TokenizersInitializer.Analysis.Tokenizers,
					TokenFilters = AnalysisUsageTests.TokenFiltersInitializer.Analysis.TokenFilters,
					Normalizers = AnalysisUsageTests.NormalizersInitializer.Analysis.Normalizers,
				}
			}
		};

		protected override ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Promise(AnalysisUsageTests.AnalyzersFluent.Analysis.Analyzers))
					.CharFilters(t => Promise(AnalysisUsageTests.CharFiltersFluent.Analysis.CharFilters))
					.Tokenizers(t => Promise(AnalysisUsageTests.TokenizersFluent.Analysis.Tokenizers))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
					.Normalizers(t => Promise(AnalysisUsageTests.NormalizersFluent.Analysis.Normalizers))
				)
			);
	}
}
