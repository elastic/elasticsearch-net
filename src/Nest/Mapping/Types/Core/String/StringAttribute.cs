using System;

namespace Nest
{
	[Obsolete("Only valid for indices created before Elasticsearch 5.0, removed in NEST 6.x.  For newly created indices, use Text or Keyword attribute instead.")]
	public class StringAttribute : ElasticsearchDocValuesPropertyAttributeBase, IStringProperty
	{
		IStringProperty Self => this;

		FieldIndexOption? IStringProperty.Index { get; set; }
		TermVectorOption? IStringProperty.TermVector { get; set; }
		double? IStringProperty.Boost { get; set; }
		string IStringProperty.NullValue { get; set; }
		bool? IStringProperty.Norms { get; set; }
		IndexOptions? IStringProperty.IndexOptions { get; set; }
		string IStringProperty.Analyzer { get; set; }
		string IStringProperty.SearchAnalyzer { get; set; }
		[Obsolete("Scheduled to be removed in 6.0")]
		bool? IStringProperty.IncludeInAll { get; set; }
		int? IStringProperty.IgnoreAbove { get; set; }
		int? IStringProperty.PositionIncrementGap { get; set; }
		[Obsolete("Use FielddataUpgrade")]
		IStringFielddata IStringProperty.Fielddata { get; set; }
		bool? IStringProperty.FielddataUpgrade { get; set; }
		IFielddataFrequencyFilter IStringProperty.FielddataFrequencyFilter { get; set; }
		bool? IStringProperty.EagerGlobalOrdinals { get; set; }

		public string Analyzer { get { return Self.Analyzer; } set { Self.Analyzer = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public int IgnoreAbove { get { return Self.IgnoreAbove.GetValueOrDefault(); } set { Self.IgnoreAbove = value; } }

		[Obsolete("Scheduled to be removed in 6.0")]
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public FieldIndexOption Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public IndexOptions IndexOptions { get { return Self.IndexOptions.GetValueOrDefault(); } set { Self.IndexOptions = value; } }
		public string NullValue { get { return Self.NullValue; } set { Self.NullValue = value; } }
		public int PositionIncrementGap { get { return Self.PositionIncrementGap.GetValueOrDefault(); } set { Self.PositionIncrementGap = value; } }
		public string SearchAnalyzer { get { return Self.SearchAnalyzer; } set { Self.SearchAnalyzer = value; } }
		public TermVectorOption TermVector { get { return Self.TermVector.GetValueOrDefault(); } set { Self.TermVector = value; } }
		public bool Norms { get { return Self.Norms.GetValueOrDefault(true); } set { Self.Norms = value; } }
		public bool Fielddata { get { return Self.FielddataUpgrade.GetValueOrDefault(true); } set { Self.FielddataUpgrade = value; } }
		public bool EagerGlobalOrdinals { get { return Self.EagerGlobalOrdinals.GetValueOrDefault(true); } set { Self.EagerGlobalOrdinals = value; } }

		[Obsolete("Only valid for indices created before Elasticsearch 5.0, removed in NEST 6.x.  For newly created indices, use Text or Keyword attribute instead.")]
		public StringAttribute() : base("string") { }
	}
}
