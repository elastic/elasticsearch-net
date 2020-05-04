// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
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
