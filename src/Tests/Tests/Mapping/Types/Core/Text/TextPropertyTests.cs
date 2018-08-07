using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.Text
{
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
					boost = 1.2,
					copy_to = new [] { "other_field" },
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
					include_in_all = false,
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "standard",
					search_quote_analyzer = "standard",
					similarity = "classic",
					store = true,
					norms = false,
					term_vector = "with_positions_offsets"
				}
			}
		};

#pragma warning disable 618 // Usage of IncludeInAll
		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Text(s => s
					.Name(p => p.Name)
					.Analyzer("standard")
					.Boost(1.2)
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
					.IncludeInAll(false)
					.Index(true)
					.IndexOptions(IndexOptions.Offsets)
					.PositionIncrementGap(5)
					.SearchAnalyzer("standard")
					.SearchQuoteAnalyzer("standard")
					.Similarity(SimilarityOption.Classic)
					.Store()
					.Norms(false)
					.TermVector(TermVectorOption.WithPositionsOffsets)
				);
#pragma warning restore 618

		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new TextProperty
				{
					Analyzer = "standard",
					Boost = 1.2,
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
						{ "raw", new KeywordProperty
							{
								IgnoreAbove = 100
							}
						}
					},
#pragma warning disable 618
					IncludeInAll = false,
#pragma warning restore 618
					Index = true,
					IndexOptions = IndexOptions.Offsets,
					PositionIncrementGap = 5,
					SearchAnalyzer = "standard",
					SearchQuoteAnalyzer = "standard",
					Similarity = SimilarityOption.Classic,
					Store = true,
					Norms = false,
					TermVector = TermVectorOption.WithPositionsOffsets
				}
			}
		};
	}
}
