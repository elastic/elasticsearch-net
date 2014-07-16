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

		[JsonProperty("term_vector")]
		TermVectorOption? TermVector { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("index_analyzer")]
		string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		string SearchAnalyzer { get; set; }
	}

	public class AllFieldMapping : IAllFieldMapping
	{
		public bool? Enabled { get; set; }
		public bool? Store { get; set; }
		public TermVectorOption? TermVector { get; set; }
		public string Analyzer { get; set; }
		public string IndexAnalyzer { get; set; }
		public string SearchAnalyzer { get; set; }
	}

	public class AllFieldMappingDescriptor : IAllFieldMapping
	{

		private IAllFieldMapping Self { get { return this; } }

		bool? IAllFieldMapping.Enabled { get; set; }
		
		bool? IAllFieldMapping.Store { get; set; }

		TermVectorOption?  IAllFieldMapping.TermVector { get; set; }

		string IAllFieldMapping.Analyzer { get; set; }

		string IAllFieldMapping.IndexAnalyzer { get; set; }

		string IAllFieldMapping.SearchAnalyzer { get; set; }

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

		public AllFieldMappingDescriptor TermVector(TermVectorOption option)
		{
			Self.TermVector = option;
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
    }
}