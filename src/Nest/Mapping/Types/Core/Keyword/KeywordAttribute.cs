// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class KeywordAttribute : ElasticsearchDocValuesPropertyAttributeBase, IKeywordProperty
	{
		public KeywordAttribute() : base(FieldType.Keyword) { }

		bool? IKeywordProperty.EagerGlobalOrdinals { get; set; }
		int? IKeywordProperty.IgnoreAbove { get; set; }
		bool? IKeywordProperty.Index { get; set; }
		IndexOptions? IKeywordProperty.IndexOptions { get; set; }
		string IKeywordProperty.Normalizer { get; set; }
		bool? IKeywordProperty.Norms { get; set; }
		string IKeywordProperty.NullValue { get; set; }
		private IKeywordProperty Self => this;
		bool? IKeywordProperty.SplitQueriesOnWhitespace { get; set; }

		public bool EagerGlobalOrdinals
		{
			get => Self.EagerGlobalOrdinals.GetValueOrDefault();
			set => Self.EagerGlobalOrdinals = value;
		}

		public int IgnoreAbove
		{
			get => Self.IgnoreAbove.GetValueOrDefault();
			set => Self.IgnoreAbove = value;
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

		public string NullValue
		{
			get => Self.NullValue;
			set => Self.NullValue = value;
		}

		public bool Norms
		{
			get => Self.Norms.GetValueOrDefault(true);
			set => Self.Norms = value;
		}

		public bool SplitQueriesOnWhitespace
		{
			get => Self.SplitQueriesOnWhitespace.GetValueOrDefault(false);
			set => Self.SplitQueriesOnWhitespace = value;
		}

		public string Normalizer
		{
			get => Self.Normalizer;
			set => Self.Normalizer = value;
		}
		// ReSharper restore ArrangeThisQualifier
	}
}
