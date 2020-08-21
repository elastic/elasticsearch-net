// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.SearchAsYouType
{
	[SkipVersion("<7.2.0", "Implemented in 7.2.0")]
	public class SearchAsYouTypePropertyTests : PropertyTestsBase
	{
		public SearchAsYouTypePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					max_shingle_size = 4,
					type = "search_as_you_type",
					analyzer = "standard",
					copy_to = new[] { "other_field" },
					index = true,
					index_options = "offsets",
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
			.SearchAsYouType(s => s
				.MaxShingleSize(4)
				.Name(p => p.Name)
				.Analyzer("standard")
				.CopyTo(c => c
					.Field("other_field")
				)
				.Index()
				.IndexOptions(IndexOptions.Offsets)
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
				"name", new SearchAsYouTypeProperty
				{
					MaxShingleSize = 4,
					Analyzer = "standard",
					CopyTo = "other_field",
					Index = true,
					IndexOptions = IndexOptions.Offsets,
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
