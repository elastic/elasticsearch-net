using System;

namespace Nest
{
	[Obsolete("Only valid for indices created before Elasticsearch 5.0 and will be removed in the next major version.  For newly created indices, use Text or Keyword attribute instead.")]
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
		int? IStringProperty.IgnoreAbove { get; set; }
		int? IStringProperty.PositionIncrementGap { get; set; }
		IStringFielddata IStringProperty.Fielddata { get; set; }

		public string Analyzer { get => Self.Analyzer; set => Self.Analyzer = value; }
		public double Boost { get => Self.Boost.GetValueOrDefault(); set => Self.Boost = value; }
		public int IgnoreAbove { get => Self.IgnoreAbove.GetValueOrDefault(); set => Self.IgnoreAbove = value; }
		public FieldIndexOption Index { get => Self.Index.GetValueOrDefault(); set => Self.Index = value; }
		public IndexOptions IndexOptions { get => Self.IndexOptions.GetValueOrDefault(); set => Self.IndexOptions = value; }
		public string NullValue { get => Self.NullValue; set => Self.NullValue = value; }
		public int PositionIncrementGap { get => Self.PositionIncrementGap.GetValueOrDefault(); set => Self.PositionIncrementGap = value; }
		public string SearchAnalyzer { get => Self.SearchAnalyzer; set => Self.SearchAnalyzer = value; }
		public TermVectorOption TermVector { get => Self.TermVector.GetValueOrDefault(); set => Self.TermVector = value; }
		public bool Norms { get => Self.Norms.GetValueOrDefault(true); set => Self.Norms = value; }

		[Obsolete("Only valid for indices created before Elasticsearch 5.0 and will be removed in the next major version.  For newly created indices, use Text or Keyword attribute instead.")]
		public StringAttribute() : base(FieldType.String) { }
	}
}
