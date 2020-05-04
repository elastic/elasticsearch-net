// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Framework.Extensions.Promisify;

namespace Tests.Analysis
{
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
