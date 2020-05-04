// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field to index structured content such as IDs, email addresses, hostnames, status codes, zip codes or tags.
	/// Used for filtering, sorting, and for aggregations.
	/// <para />
	/// Keyword fields are only searchable by their exact value.
	/// </summary>
	[InterfaceDataContract]
	public interface IKeywordProperty : IDocValuesProperty
	{
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		[DataMember(Name ="ignore_above")]
		int? IgnoreAbove { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name ="normalizer")]
		string Normalizer { get; set; }

		[DataMember(Name ="norms")]
		bool? Norms { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }

		/// <summary> Whether full text queries should split the input on whitespace when building a query for this field. </summary>
		[DataMember(Name ="split_queries_on_whitespace")]
		bool? SplitQueriesOnWhitespace { get; set; }
	}

	/// <inheritdoc cref="IKeywordProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordProperty : DocValuesPropertyBase, IKeywordProperty
	{
		public KeywordProperty() : base(FieldType.Keyword) { }

		public double? Boost { get; set; }
		public bool? EagerGlobalOrdinals { get; set; }
		public int? IgnoreAbove { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public string Normalizer { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }

		/// <inheritdoc cref="IKeywordProperty.SplitQueriesOnWhitespace" />
		public bool? SplitQueriesOnWhitespace { get; set; }
	}

	/// <inheritdoc cref="IKeywordProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class KeywordPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<KeywordPropertyDescriptor<T>, IKeywordProperty, T>, IKeywordProperty
		where T : class
	{
		public KeywordPropertyDescriptor() : base(FieldType.Keyword) { }

		double? IKeywordProperty.Boost { get; set; }
		bool? IKeywordProperty.EagerGlobalOrdinals { get; set; }
		int? IKeywordProperty.IgnoreAbove { get; set; }
		bool? IKeywordProperty.Index { get; set; }
		IndexOptions? IKeywordProperty.IndexOptions { get; set; }
		string IKeywordProperty.Normalizer { get; set; }
		bool? IKeywordProperty.Norms { get; set; }
		string IKeywordProperty.NullValue { get; set; }
		bool? IKeywordProperty.SplitQueriesOnWhitespace { get; set; }

		public KeywordPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public KeywordPropertyDescriptor<T> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) =>
			Assign(eagerGlobalOrdinals, (a, v) => a.EagerGlobalOrdinals = v);

		public KeywordPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(ignoreAbove, (a, v) => a.IgnoreAbove = v);

		public KeywordPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public KeywordPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		public KeywordPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		/// <inheritdoc cref="IKeywordProperty.SplitQueriesOnWhitespace" />
		public KeywordPropertyDescriptor<T> SplitQueriesOnWhitespace(bool? split = true) => Assign(split, (a, v) => a.SplitQueriesOnWhitespace = v);

		public KeywordPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		public KeywordPropertyDescriptor<T> Normalizer(string normalizer) => Assign(normalizer, (a, v) => a.Normalizer = v);
	}
}
