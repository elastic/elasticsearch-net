using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Analysis.Tokenizers;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Tests.Framework.Promisify;

namespace Tests.Analysis
{

	[SkipVersion("<5.2.0", "This tests contains analyzers/tokenfilters not found in previous versions, need a clean way to seperate these out")]
	public class AnalysisCrudTests
		: CrudWithNoDeleteTestBase<ICreateIndexResponse, IGetIndexSettingsResponse, IUpdateIndexSettingsResponse>
	{
		/**
		* == Analysis crud
		*
		* In this example we will create an index with analysis settings, read those settings back, update the analysis settings
		* and do another read after the update to assert our new analysis setting is applied.
		* There is NO mechanism to delete an analysis setting in elasticsearch.
		*/
		protected override bool SupportsDeletes => false;

		public AnalysisCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		/**
		* We can create the analysis settings as part of the create index call
		*/
		protected override LazyResponses Create() => Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, ICreateIndexResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.CreateIndex(s, f),
			fluentAsync: (s, c, f) => c.CreateIndexAsync(s, f),
			request: (s, c, r) => c.CreateIndex(r),
			requestAsync: (s, c, r) => c.CreateIndexAsync(r)
		);

		protected virtual CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = Analyzers.AnalyzerUsageTests.InitializerExample.Analysis.Analyzers,
					CharFilters = CharFilters.CharFilterUsageTests.InitializerExample.Analysis.CharFilters,
					Tokenizers = AnalysisUsageTests.TokenizersInitializer.Analysis.Tokenizers,
					TokenFilters = AnalysisUsageTests.TokenFiltersInitializer.Analysis.TokenFilters,
				}
			}
		};

		protected virtual ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Promise(Analyzers.AnalyzerUsageTests.FluentExample(s).Value.Analysis.Analyzers))
					.CharFilters(t => Promise(CharFilters.CharFilterUsageTests.FluentExample(s).Value.Analysis.CharFilters))
					.Tokenizers(t => Promise(AnalysisUsageTests.TokenizersFluent.Analysis.Tokenizers))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
				)
			);


		/**
		* We then read back the analysis settings using `GetIndexSettings()`, you can use this method to get the settings for 1, or many indices in one go
		*/
		protected override LazyResponses Read() => Calls<GetIndexSettingsDescriptor, GetIndexSettingsRequest, IGetIndexSettingsRequest, IGetIndexSettingsResponse>(
			GetInitializer,
			GetFluent,
			fluent: (s, c, f) => c.GetIndexSettings(f),
			fluentAsync: (s, c, f) => c.GetIndexSettingsAsync(f),
			request: (s, c, r) => c.GetIndexSettings(r),
			requestAsync: (s, c, r) => c.GetIndexSettingsAsync(r)
		);

		protected GetIndexSettingsRequest GetInitializer(string indexName) => new GetIndexSettingsRequest(Nest.Indices.Index(indexName)) { };
		protected IGetIndexSettingsRequest GetFluent(string indexName, GetIndexSettingsDescriptor u) => u.Index(indexName);

		/**
		* Here we assert over the response from `GetIndexSettings()` after the index creation to make sure our analysis chain did infact
		* store our html char filter called `stripMe`
		*/
		protected override void ExpectAfterCreate(IGetIndexSettingsResponse response)
		{
			response.Indices.Should().NotBeNull().And.HaveCount(1);
			var index = response.Indices.Values.First();
			index.Should().NotBeNull();
			index.Settings.Should().NotBeNull();
			var indexSettings = index.Settings;
			indexSettings.Analysis.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().NotBeNull();

			var firstHtmlCharFilter = indexSettings.Analysis.CharFilters["stripMe"];
			firstHtmlCharFilter.Should().NotBeNull();
		}

		/**
		* Elasticsearch has an `UpdateIndexSettings()` call but in order to be able to use it you first need to close the index and reopen it afterwards
		*/
		protected override LazyResponses Update() => Calls<UpdateIndexSettingsDescriptor, UpdateIndexSettingsRequest, IUpdateIndexSettingsRequest, IUpdateIndexSettingsResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) =>
			{
				c.CloseIndex(s);
				var response = c.UpdateIndexSettings(s, f);
				c.OpenIndex(s);
				return response;
			}
			,
			fluentAsync: async (s, c, f) =>
			{
				c.CloseIndex(s);
				var response = await c.UpdateIndexSettingsAsync(s, f);
				c.OpenIndex(s);
				return response;
			},
			request: (s, c, r) =>
			{
				c.CloseIndex(s);
				var response = c.UpdateIndexSettings(r);
				c.OpenIndex(s);
				return response;
			},
			requestAsync: async (s, c, r) =>
			{
				c.CloseIndex(s);
				var response = await c.UpdateIndexSettingsAsync(r);
				c.OpenIndex(s);
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
					CharFilters = new Nest.CharFilters {{"differentHtml", new HtmlStripCharFilter {}}}
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
		protected override void ExpectAfterUpdate(IGetIndexSettingsResponse response)
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
