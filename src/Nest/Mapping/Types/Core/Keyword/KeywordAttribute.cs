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
