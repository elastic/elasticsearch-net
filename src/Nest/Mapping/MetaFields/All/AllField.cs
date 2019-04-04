using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AllField>))]
	public interface IAllField : IFieldMapping
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("enabled")]
		bool? Enabled { get; set; }

		[JsonProperty("omit_norms")]
		bool? OmitNorms { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }

		[JsonProperty("similarity")]
		string Similarity { get; set; }

		[JsonProperty("store")]
		bool? Store { get; set; }

		[JsonProperty("store_term_vector_offsets")]
		bool? StoreTermVectorOffsets { get; set; }

		[JsonProperty("store_term_vector_payloads")]
		bool? StoreTermVectorPayloads { get; set; }

		[JsonProperty("store_term_vector_positions")]
		bool? StoreTermVectorPositions { get; set; }

		[JsonProperty("store_term_vectors")]
		bool? StoreTermVectors { get; set; }
	}

	public class AllField : IAllField
	{
		public string Analyzer { get; set; }
		public bool? Enabled { get; set; }
		public bool? OmitNorms { get; set; }
		public string SearchAnalyzer { get; set; }
		public string Similarity { get; set; }
		public bool? Store { get; set; }
		public bool? StoreTermVectorOffsets { get; set; }
		public bool? StoreTermVectorPayloads { get; set; }
		public bool? StoreTermVectorPositions { get; set; }
		public bool? StoreTermVectors { get; set; }
	}

	//OBSOLETE
	public class AllFieldDescriptor
		: DescriptorBase<AllFieldDescriptor, IAllField>, IAllField
	{
		string IAllField.Analyzer { get; set; }
		bool? IAllField.Enabled { get; set; }
		bool? IAllField.OmitNorms { get; set; }
		string IAllField.SearchAnalyzer { get; set; }
		string IAllField.Similarity { get; set; }
		bool? IAllField.Store { get; set; }
		bool? IAllField.StoreTermVectorOffsets { get; set; }
		bool? IAllField.StoreTermVectorPayloads { get; set; }
		bool? IAllField.StoreTermVectorPositions { get; set; }
		bool? IAllField.StoreTermVectors { get; set; }

		public AllFieldDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);

		public AllFieldDescriptor Store(bool? store = true) => Assign(store, (a, v) => a.Store = v);

		public AllFieldDescriptor StoreTermVectors(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectors = v);

		public AllFieldDescriptor StoreTermVectorOffsets(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorOffsets = v);

		public AllFieldDescriptor StoreTermVectorPositions(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorPositions = v);

		public AllFieldDescriptor StoreTermVectorPayloads(bool? store = true) => Assign(store, (a, v) => a.StoreTermVectorPayloads = v);

		public AllFieldDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public AllFieldDescriptor SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public AllFieldDescriptor Similarity(string similarity) => Assign(similarity, (a, v) => a.Similarity = v);
	}
}
