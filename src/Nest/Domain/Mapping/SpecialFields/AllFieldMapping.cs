using Newtonsoft.Json;

namespace Nest
{
    public class AllFieldMapping
    {

		[JsonProperty("enabled")]
		public bool? _Enabled { get; internal set; }
		
		[JsonProperty("store")]
		public bool? _Store { get; internal set; }

		[JsonProperty("term_vector")]
		public TermVectorOption?  _TermVector { get; internal set; }

		[JsonProperty("analyzer")]
		public string _Analyzer { get; internal set; }

		[JsonProperty("index_analyzer")]
		public string _IndexAnalyzer { get; internal set; }

		[JsonProperty("search_analyzer")]
		public string _SearchAnalyzer { get; internal set; }

		public AllFieldMapping Enabled(bool enabled = true)
		{
			this._Enabled = enabled;
			return this;
		}

		public AllFieldMapping Store(bool store = true)
		{
			this._Store = store;
			return this;
		}

		public AllFieldMapping TermVector(TermVectorOption option)
		{
			this._TermVector = option;
			return this;
		}

		public AllFieldMapping Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}

		public AllFieldMapping IndexAnalyzer(string analyzer)
		{
			this._IndexAnalyzer = analyzer;
			return this;
		}

		public AllFieldMapping SearchAnalyzer(string analyzer)
		{
			this._SearchAnalyzer = analyzer;
			return this;
		}
    }
}