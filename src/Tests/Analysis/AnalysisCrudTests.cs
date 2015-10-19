using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;
using System.Collections.Generic;
using Tests.Framework.MockData;
using System.Threading.Tasks;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Analysis
{

	[Collection(IntegrationContext.Indexing)]
	public class AnalysisCrudTests : CrudTestBase<IIndicesOperationResponse, IGetIndexSettingsResponse, IAcknowledgedResponse>
	{
		/**
		* # Analysis crud
		* 
		* In this example we will create an index with analysis settings, read those settings back, update the analysis settings
		* and do another read after the update to assert our new analysis setting is applied.
		* There is NO mechanism to delete an analysis setting in elasticsearch.
		*/
		protected override bool SupportsDeletes => false;

		public AnalysisCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		/**
		* We can create the analysis settings as part of the create index call
		*/
		protected override LazyResponses Create() => Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, IIndicesOperationResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.CreateIndex(s, f),
			fluentAsync: (s, c, f) => c.CreateIndexAsync(s, f),
			request: (s, c, r) => c.CreateIndex(r),
			requestAsync: (s, c, r) => c.CreateIndexAsync(r)
		);

		protected CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = Analyzers.AnalyzerUsageTests.InitializerExample.Analysis.Analyzers,
					CharFilters = CharFilters.CharFilterUsageTests.InitializerExample.Analysis.CharFilters,
					Tokenizers = Tokenizers.TokenizerUsageTests.InitializerExample.Analysis.Tokenizers,
					TokenFilters = TokenFilters.TokenFilterUsageTests.InitializerExample.Analysis.TokenFilters,
				}
			}
		};

		protected ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Analyzers.AnalyzerUsageTests.FluentExample(s).Analysis.Analyzers)
					.CharFilters(t => CharFilters.CharFilterUsageTests.FluentExample(s).Analysis.CharFilters)
					.Tokenizers(t => Tokenizers.TokenizerUsageTests.FluentExample(s).Analysis.Tokenizers)
					.TokenFilters(t => TokenFilters.TokenFilterUsageTests.FluentExample(s).Analysis.TokenFilters)
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
		[I] protected async Task CreatedAnalyisHasCharFilters() => await this.AssertOnGetAfterCreate(r =>
		{
			r.Indices.Should().NotBeNull().And.HaveCount(1);
			var index = r.Indices.Values.First();
			index.Should().NotBeNull();
			index.Settings.Should().NotBeNull();
			var indexSettings = index.Settings;
			indexSettings.Analysis.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().NotBeNull();

			var firstHtmlCharFilter = indexSettings.Analysis.CharFilters["stripMe"];
			firstHtmlCharFilter.Should().NotBeNull();
		});

		/**
		* Elasticsearch has an `UpdateIndexSettings()` call but in order to be able to use it you first need to close the index and reopen it afterwards
		*/
		protected override LazyResponses Update() => Calls<UpdateIndexSettingsDescriptor, UpdateIndexSettingsRequest, IUpdateIndexSettingsRequest, IAcknowledgedResponse>(
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
			Analysis = new Nest.Analysis
			{
				CharFilters = new Nest.CharFilters { { "differentHtml", new HtmlStripCharFilter { } } }
			}
		};


		protected IUpdateIndexSettingsRequest UpdateFluent(string indexName, UpdateIndexSettingsDescriptor u) => u
			//.Index(indexName)
			.Analysis(a => a
				.CharFilters(c => c
					.HtmlStrip("differentHtml")
				)
			);

		/**
		* Here we assert that the `GetIndexSettings()` call after the update sees the newly introduced `differentHmtl` char filter
		*/
		[I] protected async Task UpdatedAnalyisHasNewCharFilter() => await this.AssertOnGetAfterUpdate(r =>
		{
			r.Indices.Should().NotBeNull().And.HaveCount(1);
			var index = r.Indices.Values.First();
			index.Should().NotBeNull();
			index.Settings.Should().NotBeNull();
			var indexSettings = index.Settings;
			indexSettings.Analysis.Should().NotBeNull();
			indexSettings.Analysis.CharFilters.Should().NotBeNull();

			var firstHtmlCharFilter = indexSettings.Analysis.CharFilters["differentHtml"];
			firstHtmlCharFilter.Should().NotBeNull();
		});
	}
}
