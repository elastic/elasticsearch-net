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
	public class AnalysisCrud : CrudExample<IIndicesOperationResponse>
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

		protected override LazyResponses Delete()
		{
			return null;
		}

		protected override LazyResponses Read()
		{
			return null;
		}

		protected override LazyResponses Update()
		{
			return null;
		}
	}
}
