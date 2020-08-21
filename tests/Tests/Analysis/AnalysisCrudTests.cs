// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Framework.Extensions.Promisify;

namespace Tests.Analysis
{

	/*
	 * {
  "error" : {
    "root_cause" : [
      {
        "type" : "illegal_argument_exception",
        "reason" : "Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]",
"stack_trace" : "[Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]]; nested: IllegalArgumentException[Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]];\n\tat org.elasticsearch.ElasticsearchException.guessRootCauses(ElasticsearchException.java:639)\n\tat org.elasticsearch.ElasticsearchException.generateFailureXContent(ElasticsearchException.java:567)\n\tat org.elasticsearch.rest.BytesRestResponse.build(BytesRestResponse.java:138)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:96)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:91)\n\tat org.elasticsearch.rest.action.RestActionListener.onFailure(RestActionListener.java:58)\n\tat org.elasticsearch.action.support.TransportAction$1.onFailure(TransportAction.java:79)\n\tat org.elasticsearch.action.support.master.TransportMasterNodeAction$AsyncSingleAction.lambda$doStart$2(TransportMasterNodeAction.java:155)\n\tat org.elasticsearch.action.ActionListener$2.onFailure(ActionListener.java:93)\n\tat org.elasticsearch.action.admin.indices.settings.put.TransportUpdateSettingsAction$1.onFailure(TransportUpdateSettingsAction.java:105)\n\tat org.elasticsearch.action.support.ContextPreservingActionListener.onFailure(ContextPreservingActionListener.java:50)\n\tat org.elasticsearch.cluster.AckedClusterStateUpdateTask.onFailure(AckedClusterStateUpdateTask.java:79)\n\tat org.elasticsearch.cluster.service.MasterService$SafeClusterStateTaskListener.onFailure(MasterService.java:499)\n\tat org.elasticsearch.cluster.service.MasterService$TaskOutputs.notifyFailedTasks(MasterService.java:432)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:211)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\nCaused by: java.lang.IllegalArgumentException: Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]\n\tat org.elasticsearch.cluster.metadata.MetaDataUpdateSettingsService$1.execute(MetaDataUpdateSettingsService.java:145)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\t... 9 more\n"
      }
    ],
    "type" : "illegal_argument_exception",
    "reason" : "Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]",
"stack_trace" : "java.lang.IllegalArgumentException: Can't update non dynamic settings [[index.analysis.char_filter.differentHtml.type]] for open indices [[fluent-ff26e1e7-analysis/dQISjWvqSRmZv2CqqLRFIA]]\n\tat org.elasticsearch.cluster.metadata.MetaDataUpdateSettingsService$1.execute(MetaDataUpdateSettingsService.java:145)\n\tat org.elasticsearch.cluster.ClusterStateUpdateTask.execute(ClusterStateUpdateTask.java:47)\n\tat org.elasticsearch.cluster.service.MasterService.executeTasks(MasterService.java:687)\n\tat org.elasticsearch.cluster.service.MasterService.calculateTaskOutputs(MasterService.java:310)\n\tat org.elasticsearch.cluster.service.MasterService.runTasks(MasterService.java:210)\n\tat org.elasticsearch.cluster.service.MasterService$Batcher.run(MasterService.java:142)\n\tat org.elasticsearch.cluster.service.TaskBatcher.runIfNotProcessed(TaskBatcher.java:150)\n\tat org.elasticsearch.cluster.service.TaskBatcher$BatchedTask.run(TaskBatcher.java:188)\n\tat org.elasticsearch.common.util.concurrent.ThreadContext$ContextPreservingRunnable.run(ThreadContext.java:699)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.runAndClean(PrioritizedEsThreadPoolExecutor.java:252)\n\tat org.elasticsearch.common.util.concurrent.PrioritizedEsThreadPoolExecutor$TieBreakingPrioritizedRunnable.run(PrioritizedEsThreadPoolExecutor.java:215)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1128)\n\tat java.base/java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:628)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
  },
  "status" : 400
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "Skip while we fix this snapshot failure later")]
	public class AnalysisCrudTests
		: CrudWithNoDeleteTestBase<CreateIndexResponse, GetIndexSettingsResponse, UpdateIndexSettingsResponse>
	{
		public AnalysisCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		/**
		* == Analysis crud
		*
		* In this example we will create an index with analysis settings, read those settings back, update the analysis settings
		* and do another read after the update to assert our new analysis setting is applied.
		* There is NO mechanism to delete an analysis setting in Elasticsearch.
		*/
		protected override bool SupportsDeletes => false;

		/**
		* We can create the analysis settings as part of the create index call
		*/
		protected override LazyResponses Create() => Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
			CreateInitializer,
			CreateFluent,
			(s, c, f) => c.Indices.Create(s, f),
			(s, c, f) => c.Indices.CreateAsync(s, f),
			(s, c, r) => c.Indices.Create(r),
			(s, c, r) => c.Indices.CreateAsync(r)
		);

		protected virtual CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = AnalysisUsageTests.AnalyzersInitializer.Analysis.Analyzers,
					CharFilters = AnalysisUsageTests.CharFiltersInitializer.Analysis.CharFilters,
					Tokenizers = AnalysisUsageTests.TokenizersInitializer.Analysis.Tokenizers,
					TokenFilters = AnalysisUsageTests.TokenFiltersInitializer.Analysis.TokenFilters,
				}
			}
		};

		protected virtual ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Promise(AnalysisUsageTests.AnalyzersFluent.Analysis.Analyzers))
					.CharFilters(t => Promise(AnalysisUsageTests.CharFiltersFluent.Analysis.CharFilters))
					.Tokenizers(t => Promise(AnalysisUsageTests.TokenizersFluent.Analysis.Tokenizers))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
				)
			);


		/**
		* We then read back the analysis settings using `GetIndexSettings()`, you can use this method to get the settings for 1, or many indices in one go
		*/
		protected override LazyResponses Read() =>
			Calls<GetIndexSettingsDescriptor, GetIndexSettingsRequest, IGetIndexSettingsRequest, GetIndexSettingsResponse>(
				GetInitializer,
				GetFluent,
				(s, c, f) => c.Indices.GetSettings(s, f),
				(s, c, f) => c.Indices.GetSettingsAsync(s, f),
				(s, c, r) => c.Indices.GetSettings(r),
				(s, c, r) => c.Indices.GetSettingsAsync(r)
			);

		protected GetIndexSettingsRequest GetInitializer(string indexName) => new GetIndexSettingsRequest(Nest.Indices.Index((IndexName)indexName));

		protected IGetIndexSettingsRequest GetFluent(string indexName, GetIndexSettingsDescriptor u) => u;

		/**
		* Here we assert over the response from `GetIndexSettings()` after the index creation to make sure our analysis chain did infact
		* store our html char filter called `htmls`
		*/
		protected override void ExpectAfterCreate(GetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeNull().And.HaveCount(1);
			var index = response.Indices.Values.First();
			index.Should().NotBeNull();
			index.Settings.Should().NotBeNull();
			var indexSettings = index.Settings;
			indexSettings.Analysis.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().NotBeNull();

			var firstHtmlCharFilter = indexSettings.Analysis.CharFilters["htmls"];
			firstHtmlCharFilter.Should().NotBeNull();
		}

		/**
		* Elasticsearch has an `UpdateIndexSettings()` call but in order to be able to use it you first need to close the index and reopen it afterwards
		*/
		protected override LazyResponses Update() =>
			Calls<UpdateIndexSettingsDescriptor, UpdateIndexSettingsRequest, IUpdateIndexSettingsRequest, UpdateIndexSettingsResponse>(
				UpdateInitializer,
				UpdateFluent,
				(s, c, f) =>
				{
					c.Indices.Close(s);
					var response = c.Indices.UpdateSettings(s, f);
					c.Indices.Open(s);
					return response;
				}
				,
				async (s, c, f) =>
				{
					c.Indices.Close(s);
					var response = await c.Indices.UpdateSettingsAsync(s, f);
					c.Indices.Open(s);
					return response;
				},
				(s, c, r) =>
				{
					c.Indices.Close(s);
					var response = c.Indices.UpdateSettings(r);
					c.Indices.Open(s);
					return response;
				},
				async (s, c, r) =>
				{
					c.Indices.Close(s);
					var response = await c.Indices.UpdateSettingsAsync(r);
					c.Indices.Open(s);
					return response;
				}
			);

		/**
		* Here we add a new `HtmlStripCharFilter` called `differentHtml`
		*/

		protected UpdateIndexSettingsRequest UpdateInitializer(string indexName) => new UpdateIndexSettingsRequest(indexName)
		{
			IndexSettings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					CharFilters = new Nest.CharFilters { { "differentHtml", new HtmlStripCharFilter() } }
				}
			}
		};

		protected IUpdateIndexSettingsRequest UpdateFluent(string indexName, UpdateIndexSettingsDescriptor u) => u
			.IndexSettings(s => s
				.Analysis(a => a
					.CharFilters(c => c
						.HtmlStrip("differentHtml")
					)
				)
			);

		/**
		* Here we assert that the `GetIndexSettings()` call after the update sees the newly introduced `differentHmtl` char filter
		*/
		protected override void ExpectAfterUpdate(GetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeNull().And.HaveCount(1);
			var index = response.Indices.Values.First();
			index.Should().NotBeNull();
			index.Settings.Should().NotBeNull();
			var indexSettings = index.Settings;
			indexSettings.Analysis.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().ContainKey("differentHtml");
			var firstHtmlCharFilter = indexSettings.Analysis.CharFilters["differentHtml"];
			firstHtmlCharFilter.Should().NotBeNull();
		}
	}
}
