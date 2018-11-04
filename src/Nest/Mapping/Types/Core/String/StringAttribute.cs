using System;

namespace Nest
{
	[Obsolete(
		"Only valid for indices created before Elasticsearch 5.0, removed in Elasticsearch 6.0.  For newly created indices, use Text or Keyword attribute instead.")]
	public class StringAttribute : ElasticsearchDocValuesPropertyAttributeBase, IStringProperty
	{
		[Obsolete(
			"Only valid for indices created before Elasticsearch 5.0, removed in Elasticsearch 6.0. For newly created indices, use Text or Keyword attribute instead.")]
		public StringAttribute() : base("string") { }

		public string Analyzer
		{
			get => Self.Analyzer;
			set => Self.Analyzer = value;
		}

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public bool EagerGlobalOrdinals
		{
			get => Self.EagerGlobalOrdinals.GetValueOrDefault(true);
			set => Self.EagerGlobalOrdinals = value;
		}

		public bool Fielddata
		{
			get => Self.FielddataUpgrade.GetValueOrDefault(true);
			set => Self.FielddataUpgrade = value;
		}

		public int IgnoreAbove
		{
			get => Self.IgnoreAbove.GetValueOrDefault();
			set => Self.IgnoreAbove = value;
		}

		/// <remarks>Removed in 6.x</remarks>
		public bool IncludeInAll
		{
			get => Self.IncludeInAll.GetValueOrDefault();
			set => Self.IncludeInAll = value;
		}

		public FieldIndexOption Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public IndexOptions IndexOptions
		{
			get => Self.IndexOptions.GetValueOrDefault();
			set => Self.IndexOptions = value;
		}

		public bool Norms
		{
			get => Self.Norms.GetValueOrDefault(true);
			set => Self.Norms = value;
		}

		public string NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}

		public int PositionIncrementGap
		{
			get => Self.PositionIncrementGap.GetValueOrDefault();
			set => Self.PositionIncrementGap = value;
		}

		public string SearchAnalyzer
		{
			get => Self.SearchAnalyzer;
			set => Self.SearchAnalyzer = value;
		}

		public TermVectorOption TermVector
		{
			get => Self.TermVector.GetValueOrDefault();
			set => Self.TermVector = value;
		}

		string IStringProperty.Analyzer { get; set; }
		double? IStringProperty.Boost { get; set; }
		bool? IStringProperty.EagerGlobalOrdinals { get; set; }

		[Obsolete("Use FielddataUpgrade")]
		IStringFielddata IStringProperty.Fielddata { get; set; }

		IFielddataFrequencyFilter IStringProperty.FielddataFrequencyFilter { get; set; }
		bool? IStringProperty.FielddataUpgrade { get; set; }
		int? IStringProperty.IgnoreAbove { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IStringProperty.IncludeInAll { get; set; }

		FieldIndexOption? IStringProperty.Index { get; set; }
		IndexOptions? IStringProperty.IndexOptions { get; set; }
		bool? IStringProperty.Norms { get; set; }
		string IStringProperty.NullValue { get; set; }
		int? IStringProperty.PositionIncrementGap { get; set; }
		string IStringProperty.SearchAnalyzer { get; set; }
		private IStringProperty Self => this;
		TermVectorOption? IStringProperty.TermVector { get; set; }
	}
}
