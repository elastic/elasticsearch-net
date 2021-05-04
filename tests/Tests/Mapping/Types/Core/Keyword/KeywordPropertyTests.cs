// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Tests.Framework.Extensions.Promisify;

namespace Tests.Mapping.Types.Core.Keyword
{
	[SkipVersion("<5.2.0", "This uses the normalizer feature introduced in 5.2.0")]
	public class KeywordPropertyTests : PropertyTestsBase
	{
		public KeywordPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				state = new
				{
					type = "keyword",
					doc_values = false,
					eager_global_ordinals = true,
					ignore_above = 50,
					index = false,
					index_options = "freqs",
					null_value = "null",
					norms = false,
					fields = new
					{
						foo = new
						{
							type = "keyword",
							ignore_above = 10
						}
					},
					store = true,
					normalizer = "myCustom",
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Keyword(b => b
				.Name(p => p.State)
				.DocValues(false)
				.EagerGlobalOrdinals()
				.IgnoreAbove(50)
				.Index(false)
				.IndexOptions(IndexOptions.Freqs)
				.NullValue("null")
				.Normalizer("myCustom")
				.Norms(false)
				.Store()
				.Fields(fs => fs
					.Keyword(k => k
						.Name("foo")
						.IgnoreAbove(10)
					)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"state", new KeywordProperty
				{
					DocValues = false,
					EagerGlobalOrdinals = true,
					IgnoreAbove = 50,
					Index = false,
					IndexOptions = IndexOptions.Freqs,
					NullValue = "null",
					Normalizer = "myCustom",
					Norms = false,
					Store = true,
					Fields = new Properties
					{
						{ "foo", new KeywordProperty { IgnoreAbove = 10 } }
					}
				}
			}
		};

		protected override ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create
			.Settings(s => s
				.Analysis(a => a
					.CharFilters(t => Promise(AnalysisUsageTests.CharFiltersFluent.Analysis.CharFilters))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
					.Normalizers(t => Promise(AnalysisUsageTests.NormalizersInitializer.Analysis.Normalizers))
				)
			);

		[SkipVersion("<6.4.0", "split_queries_on_whitespace is a new option https://github.com/elastic/elasticsearch/pull/30691")]
		public class KeywordPropertySplitQueriesOnWhitespaceTests : PropertyTestsBase
		{
			public KeywordPropertySplitQueriesOnWhitespaceTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

			protected override object ExpectJson => new
			{
				properties = new
				{
					state = new
					{
						type = "keyword",
						split_queries_on_whitespace = true
					}
				}
			};

			protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Keyword(b => b
					.Name(p => p.State)
					.SplitQueriesOnWhitespace()
				);


			protected override IProperties InitializerProperties => new Properties
			{
				{ "state", new KeywordProperty { SplitQueriesOnWhitespace = true } }
			};
		}
	}
}
