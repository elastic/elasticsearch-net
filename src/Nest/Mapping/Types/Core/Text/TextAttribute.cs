/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
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
		double? ITextProperty.Boost { get; set; }
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
