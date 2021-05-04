// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class TextAttribute : ElasticsearchCorePropertyAttributeBase, ITextProperty {
		public TextAttribute() : base(FieldType.Text) { }
		protected TextAttribute(FieldType fieldType) : base(fieldType) { }

		public string Analyzer
		{
			get => Self.Analyzer;
			set => Self.Analyzer = value;
		}

		public bool EagerGlobalOrdinals
		{
			get => Self.EagerGlobalOrdinals.GetValueOrDefault();
			set => Self.EagerGlobalOrdinals = value;
		}

		public bool Fielddata
		{
			get => Self.Fielddata.GetValueOrDefault();
			set => Self.Fielddata = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public IndexOptions IndexOptions
		{
			get => Self.IndexOptions.GetValueOrDefault();
			set => Self.IndexOptions = value;
		}

		public bool IndexPhrases
		{
			get => Self.IndexPhrases.GetValueOrDefault();
			set => Self.IndexPhrases = value;
		}

		public bool Norms
		{
			get => Self.Norms.GetValueOrDefault(true);
			set => Self.Norms = value;
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

		public string SearchQuoteAnalyzer
		{
			get => Self.SearchQuoteAnalyzer;
			set => Self.SearchQuoteAnalyzer = value;
		}

		public TermVectorOption TermVector
		{
			get => Self.TermVector.GetValueOrDefault();
			set => Self.TermVector = value;
		}

		string ITextProperty.Analyzer { get; set; }
		bool? ITextProperty.EagerGlobalOrdinals { get; set; }
		bool? ITextProperty.Fielddata { get; set; }
		IFielddataFrequencyFilter ITextProperty.FielddataFrequencyFilter { get; set; }
		bool? ITextProperty.Index { get; set; }
		IndexOptions? ITextProperty.IndexOptions { get; set; }
		bool? ITextProperty.IndexPhrases { get; set; }
		ITextIndexPrefixes ITextProperty.IndexPrefixes { get; set; }
		bool? ITextProperty.Norms { get; set; }
		int? ITextProperty.PositionIncrementGap { get; set; }
		string ITextProperty.SearchAnalyzer { get; set; }
		string ITextProperty.SearchQuoteAnalyzer { get; set; }
		private ITextProperty Self => this;
		TermVectorOption? ITextProperty.TermVector { get; set; }
	}
}
