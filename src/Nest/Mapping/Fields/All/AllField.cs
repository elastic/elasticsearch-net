using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<AllField>))]
	public interface IAllField : ISpecialField
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

	public class AllField : IAllField
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

	public class AllFieldDescriptor : IAllField
	{
		private IAllField Self => this;

		bool? IAllField.Enabled { get; set; }
		bool? IAllField.Store { get; set; }
		bool? IAllField.StoreTermVectors { get; set; }
		bool? IAllField.StoreTermVectorOffsets { get; set; }
		bool? IAllField.StoreTermVectorPositions { get; set; }
		bool? IAllField.StoreTermVectorPayloads { get; set; }

		public bool? OmitNorms { get; set; }

		string IAllField.Analyzer { get; set; }

		string IAllField.IndexAnalyzer { get; set; }

		string IAllField.SearchAnalyzer { get; set; }
		string IAllField.Similarity { get; set; }

		public AllFieldDescriptor Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}

		public AllFieldDescriptor Store(bool store = true)
		{
			Self.Store = store;
			return this;
		}
		
		public AllFieldDescriptor StoreTermVectors(bool store = true)
		{
			Self.StoreTermVectors = store;
			return this;
		}
		
		public AllFieldDescriptor StoreTermVectorOffsets(bool store = true)
		{
			Self.StoreTermVectorOffsets = store;
			return this;
		}
		
		public AllFieldDescriptor StoreTermVectorPositions(bool store = true)
		{
			Self.StoreTermVectorPositions = store;
			return this;
		}
		
		public AllFieldDescriptor StoreTermVectorPayloads(bool store = true)
		{
			Self.StoreTermVectorPayloads = store;
			return this;
		}

		public AllFieldDescriptor Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public AllFieldDescriptor IndexAnalyzer(string analyzer)
		{
			Self.IndexAnalyzer = analyzer;
			return this;
		}

		public AllFieldDescriptor SearchAnalyzer(string analyzer)
		{
			Self.SearchAnalyzer = analyzer;
			return this;
		}

		public AllFieldDescriptor Similarity(string similarity)
		{
			Self.Similarity = similarity;
			return this;
		}
    }
}