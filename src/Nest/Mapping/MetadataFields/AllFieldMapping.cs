using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<AllFieldMapping>))]
	public interface IAllFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("store_term_vectors")]
		bool? StoreTermVectors { get; set; }

		[JsonProperty("store_term_vector_offsets")]
		bool? StoreTermVectorOffsets { get; set; }

		[JsonProperty("store_term_vector_positions")]
		bool? StoreTermVectorPositions { get; set; }

		[JsonProperty("store_term_vector_payloads")]
		bool? StoreTermVectorPayloads { get; set; }

		[JsonProperty("omit_norms")]
		bool? OmitNorms { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("index_analyzer")]
		string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("similarity")]
		string Similarity { get; set; }

	}

	public class AllFieldMapping : IAllFieldMapping
	{
		public bool? Enabled { get; set; }
		public bool? Store { get; set; }
		public bool? StoreTermVectors { get; set; }
		public bool? StoreTermVectorOffsets { get; set; }
		public bool? StoreTermVectorPositions { get; set; }
		public bool? StoreTermVectorPayloads { get; set; }
		public TermVectorOption? TermVector { get; set; }
		public bool? OmitNorms { get; set; }
		public string Analyzer { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
		public string Similarity { get; set; }
	}

	public class AllFieldMappingDescriptor : IAllFieldMapping
	{

		private IAllFieldMapping Self => this;

		bool? IAllFieldMapping.Enabled { get; set; }
		
		bool? IAllFieldMapping.Store { get; set; }
		bool? IAllFieldMapping.StoreTermVectors { get; set; }
		bool? IAllFieldMapping.StoreTermVectorOffsets { get; set; }
		bool? IAllFieldMapping.StoreTermVectorPositions { get; set; }
		bool? IAllFieldMapping.StoreTermVectorPayloads { get; set; }

		public bool? OmitNorms { get; set; }

		string IAllFieldMapping.Analyzer { get; set; }

		string IAllFieldMapping.IndexAnalyzer { get; set; }

		string IAllFieldMapping.SearchAnalyzer { get; set; }
		string IAllFieldMapping.Similarity { get; set; }

		public AllFieldMappingDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}

		public AllFieldMappingDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}
		
		public AllFieldMappingDescriptor StoreTermVectors(bool store = true)
		{
			Self.StoreTermVectors = store;
			return this;
		}
		
		public AllFieldMappingDescriptor StoreTermVectorOffsets(bool store = true)
		{
			Self.StoreTermVectorOffsets = store;
			return this;
		}
		
		public AllFieldMappingDescriptor StoreTermVectorPositions(bool store = true)
		{
			Self.StoreTermVectorPositions = store;
			return this;
		}
		
		public AllFieldMappingDescriptor StoreTermVectorPayloads(bool store = true)
		{
			Self.StoreTermVectorPayloads = store;
			return this;
		}

		public AllFieldMappingDescriptor Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public AllFieldMappingDescriptor IndexAnalyzer(string analyzer)
		{
			Self.IndexAnalyzer = analyzer;
			return this;
		}

		public AllFieldMappingDescriptor SearchAnalyzer(string analyzer)
		{
			Self.SearchAnalyzer = analyzer;
			return this;
		}

		public AllFieldMappingDescriptor Similarity(string similarity)
		{
			Self.Similarity = similarity;
			return this;
		}
    }
}