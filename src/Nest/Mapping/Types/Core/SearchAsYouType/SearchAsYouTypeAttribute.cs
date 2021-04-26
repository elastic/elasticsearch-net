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
