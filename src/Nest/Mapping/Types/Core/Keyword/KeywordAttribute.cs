using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class KeywordAttribute : ElasticsearchPropertyAttributeBase, IKeywordProperty
	{
		IKeywordProperty Self => this;

		double? IKeywordProperty.Boost { get; set; }
		bool? IKeywordProperty.EagerGlobalOrdinals { get; set; }
		int? IKeywordProperty.IgnoreAbove { get; set; }
		bool? IKeywordProperty.IncludeInAll { get; set; }
		bool? IKeywordProperty.Index { get; set; }
		IndexOptions? IKeywordProperty.IndexOptions { get; set; }
		INorms IKeywordProperty.Norms { get; set; }
		string IKeywordProperty.NullValue { get; set; }
		string IKeywordProperty.SearchAnalyzer { get; set; }

		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public bool EagerGlobalOrdinals { get { return Self.EagerGlobalOrdinals.GetValueOrDefault(); } set { Self.EagerGlobalOrdinals = value; } }
		public int IgnoreAbove { get { return Self.IgnoreAbove.GetValueOrDefault(); } set { Self.IgnoreAbove = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public bool Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public IndexOptions IndexOptions { get { return Self.IndexOptions.GetValueOrDefault(); } set { Self.IndexOptions = value; } }
		public string NullValue { get { return Self.NullValue; } set { Self.NullValue = value; } }
		public string SearchAnalyzer { get { return Self.SearchAnalyzer; } set { Self.SearchAnalyzer = value; } }

		public KeywordAttribute() : base("keyword") { }
	}
}
