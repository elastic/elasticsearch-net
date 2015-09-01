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

namespace Tests
{
	[Collection(IntegrationContext.Indexing)]
	public class AnalysisCrud : CrudExample<IIndicesOperationResponse, IIndexSettingsResponse, IIndicesOperationResponse>
	{
		public AnalysisCrud(IndexingCluster cluster, ApiUsage usage) : base (cluster, usage) { }

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
				Analysis = new Analysis
				{
					Analyzers = AnalyzersTests.Usage.InitializerExample.Analysis.Analyzers,
					CharFilters = CharFilterTests.Usage.InitializerExample.Analysis.CharFilters,
					Tokenizers = TokenizerTests.Usage.InitializerExample.Analysis.Tokenizers,
					TokenFilters = TokenFiltersTests.Usage.InitializerExample.Analysis.TokenFilters,
				}
			}
		};

		protected ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s=>s
				.Analysis(a=>a
					.Analyzers(t=>AnalyzersTests.Usage.FluentExample(s).Analysis.Analyzers)
					.CharFilters(t=>CharFilterTests.Usage.FluentExample(s).Analysis.CharFilters)
					.Tokenizers(t=>TokenizerTests.Usage.FluentExample(s).Analysis.Tokenizers)
					.TokenFilters(t=>TokenFiltersTests.Usage.FluentExample(s).Analysis.TokenFilters)
				)
			);


		protected override LazyResponses Read() => Calls<GetIndexSettingsDescriptor, GetIndexSettingsRequest, IGetIndexSettingsRequest, IIndexSettingsResponse>(
			GetInitializer,
			GetFluent,
			fluent: (s, c, f) => c.GetIndexSettings(f),
			fluentAsync: (s, c, f) => c.GetIndexSettingsAsync(f),
			request: (s, c, r) => c.GetIndexSettings(r),
			requestAsync: (s, c, r) => c.GetIndexSettingsAsync(r)
		);

		protected GetIndexSettingsRequest GetInitializer(string indexName) => new GetIndexSettingsRequest(indexName) { };
		protected IGetIndexSettingsRequest GetFluent(string indexName, GetIndexSettingsDescriptor u) => u.Index(indexName);

		protected override LazyResponses Update() => Calls<UpdateSettingsDescriptor, UpdateSettingsRequest, IUpdateSettingsRequest, IAcknowledgedResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.UpdateSettings(f),
			fluentAsync: (s, c, f) => c.UpdateSettingsAsync(f),
			request: (s, c, r) => c.UpdateSettings(r),
			requestAsync: (s, c, r) => c.UpdateSettingsAsync(r)
		);

		protected UpdateSettingsRequest UpdateInitializer(string indexName) => new UpdateSettingsRequest(indexName)
		{
			Analysis = new Analysis
			{
				CharFilters = new CharFilters { { "differentHtml", new HtmlStripCharFilter { } } }
			}
		};
		[I] protected async Task CreatedAnalyisHasCharFilters() => await this.AssertOnGetAfterCreate(r =>
		{
			r.IndexSettings.Should().NotBeNull();
			r.IndexSettings.Settings.Should().NotBeNull();
			r.IndexSettings.Settings.Analysis.Should().NotBeNull();
			r.IndexSettings.Settings.Analysis.CharFilters.Should().NotBeNull();
		});

		protected IUpdateSettingsRequest UpdateFluent(string indexName, UpdateSettingsDescriptor u) => u
			.Index(indexName)
			.Analysis(a => a
				.CharFilters(c => c
					.HtmlStrip("differentHtml")
				)
			);

		protected override bool SupportsDeletes => false;
	}
}
