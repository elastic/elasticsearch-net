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
	/// <inheritdoc cref="IFlattenedProperty" />
	public class FlattenedAttribute : ElasticsearchPropertyAttributeBase, IFlattenedProperty
	{
		public FlattenedAttribute() : base(FieldType.Flattened) { }

		private IFlattenedProperty Self => this;

		double? IFlattenedProperty.Boost { get; set; }
		int? IFlattenedProperty.DepthLimit { get; set; }
		bool? IFlattenedProperty.DocValues { get; set; }
		bool? IFlattenedProperty.EagerGlobalOrdinals { get; set; }
		int? IFlattenedProperty.IgnoreAbove { get; set; }
		bool? IFlattenedProperty.Index { get; set; }
		IndexOptions? IFlattenedProperty.IndexOptions { get; set; }
		bool? IFlattenedProperty.SplitQueriesOnWhitespace { get; set; }

		/// <inheritdoc cref="IFlattenedProperty.Boost" />
		public double Boost
		{
			get => Self.Boost.GetValueOrDefault(1);
			set => Self.Boost = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.DepthLimit" />
		public int DepthLimit
		{
			get => Self.DepthLimit.GetValueOrDefault(20);
			set => Self.DepthLimit = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.DocValues" />
		public bool DocValues
		{
			get => Self.DocValues.GetValueOrDefault(true);
			set => Self.DocValues = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.EagerGlobalOrdinals" />
		public bool EagerGlobalOrdinals
		{
			get => Self.EagerGlobalOrdinals.GetValueOrDefault(false);
			set => Self.EagerGlobalOrdinals = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.IgnoreAbove" />
		public int IgnoreAbove
		{
			get => Self.IgnoreAbove.GetValueOrDefault(-1);
			set => Self.IgnoreAbove = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.Index" />
		public bool Index
		{
			get => Self.Index.GetValueOrDefault(true);
			set => Self.Index = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.IndexOptions" />
		public IndexOptions IndexOptions
		{
			get => Self.IndexOptions.GetValueOrDefault(IndexOptions.Docs);
			set => Self.IndexOptions = value;
		}

		/// <inheritdoc cref="IFlattenedProperty.SplitQueriesOnWhitespace" />
		public bool SplitQueriesOnWhitespace
		{
			get => Self.SplitQueriesOnWhitespace.GetValueOrDefault(false);
			set => Self.SplitQueriesOnWhitespace = value;
		}

		/// <inheritdoc />
		public string NullValue { get; set; }

		/// <inheritdoc />
		public string Similarity { get; set; }
	}
}
