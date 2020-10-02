// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.Text
{
	[SkipVersion("<6.4.0", "index_phrases is a new feature")]
	public class TextPropertyIndexPhrasesTests : PropertyTestsBase
	{
		public TextPropertyIndexPhrasesTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "text",
					index_phrases = true
				}
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Text(s => s
				.Name(p => p.Name)
				.IndexPhrases()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new TextProperty { IndexPhrases = true } }
		};
	}

	[SkipVersion("<6.3.0", "index_prefixes is a new feature")]
	public class TextPropertyIndexPrefixesTests : PropertyTestsBase
	{
		public TextPropertyIndexPrefixesTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "text",
					index_prefixes = new
					{
						min_chars = 1,
						max_chars = 10
					}
				}
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Text(s => s
				.Name(p => p.Name)
				.IndexPrefixes(i => i
					.MinCharacters(1)
					.MaxCharacters(10)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new TextProperty
				{
					IndexPrefixes = new TextIndexPrefixes
					{
						MinCharacters = 1,
						MaxCharacters = 10
					}
				}
			}
		};
	}

	public class TextPropertyTests : PropertyTestsBase
	{
		public TextPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "text",
					analyzer = "standard",
					copy_to = new[] { "other_field" },
					eager_global_ordinals = true,
					fielddata = true,
					fielddata_frequency_filter = new
					{
						min = 1.0,
						max = 100.00,
						min_segment_size = 2
					},
					fields = new
					{
						raw = new
						{
							type = "keyword",
							ignore_above = 100
						}
					},
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "standard",
					search_quote_analyzer = "standard",
					similarity = "BM25",
					store = true,
					norms = false,
					term_vector = "with_positions_offsets"
				}
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Text(s => s
				.Name(p => p.Name)
				.Analyzer("standard")
				.CopyTo(c => c
					.Field("other_field")
				)
				.EagerGlobalOrdinals()
				.Fielddata()
				.FielddataFrequencyFilter(ff => ff
					.Min(1)
					.Max(100)
					.MinSegmentSize(2)
				)
				.Fields(fd => fd
					.Keyword(k => k
						.Name("raw")
						.IgnoreAbove(100)
					)
				)
				.Index()
				.IndexOptions(IndexOptions.Offsets)
				.PositionIncrementGap(5)
				.SearchAnalyzer("standard")
				.SearchQuoteAnalyzer("standard")
				.Similarity("BM25")
				.Store()
				.Norms(false)
				.TermVector(TermVectorOption.WithPositionsOffsets)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new TextProperty
				{
					Analyzer = "standard",
					CopyTo = "other_field",
					EagerGlobalOrdinals = true,
					Fielddata = true,
					FielddataFrequencyFilter = new FielddataFrequencyFilter
					{
						Min = 1,
						Max = 100,
						MinSegmentSize = 2
					},
					Fields = new Properties
					{
						{
							"raw", new KeywordProperty
							{
								IgnoreAbove = 100
							}
						}
					},
					Index = true,
					IndexOptions = IndexOptions.Offsets,
					PositionIncrementGap = 5,
					SearchAnalyzer = "standard",
					SearchQuoteAnalyzer = "standard",
					Similarity = "BM25",
					Store = true,
					Norms = false,
					TermVector = TermVectorOption.WithPositionsOffsets
				}
			}
		};
	}
}
