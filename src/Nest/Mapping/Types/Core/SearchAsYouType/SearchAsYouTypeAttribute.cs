// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class SearchAsYouTypeAttribute : ElasticsearchCorePropertyAttributeBase, ISearchAsYouTypeProperty
	{
		public SearchAsYouTypeAttribute() : base(FieldType.SearchAsYouType) { }

		public string Analyzer { get; set; }

		public bool Index
		{
			get => Self.Index.GetValueOrDefault(true);
			set => Self.Index = value;
		}

		public IndexOptions IndexOptions
		{
			get => Self.IndexOptions.GetValueOrDefault(IndexOptions.Positions);
			set => Self.IndexOptions = value;
		}

		public int MaxShingleSize
		{
			get => Self.MaxShingleSize.GetValueOrDefault();
			set => Self.MaxShingleSize = value;
		}

		public bool Norms
		{
			get => Self.Norms.GetValueOrDefault(true);
			set => Self.Norms = value;
		}

		public string SearchAnalyzer { get; set; }
		public string SearchQuoteAnalyzer { get; set; }

		public TermVectorOption TermVector
		{
			get => Self.TermVector.GetValueOrDefault(TermVectorOption.No);
			set => Self.TermVector = value;
		}

		bool? ISearchAsYouTypeProperty.Index { get; set; }
		IndexOptions? ISearchAsYouTypeProperty.IndexOptions { get; set; }
		int? ISearchAsYouTypeProperty.MaxShingleSize { get; set; }
		bool? ISearchAsYouTypeProperty.Norms { get; set; }
		private ISearchAsYouTypeProperty Self => this;
		TermVectorOption? ISearchAsYouTypeProperty.TermVector { get; set; }
	}
}
