// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// By default, each subfield in an object is mapped and indexed separately.
	/// If the names or types of the subfields are not known in advance, then they are mapped dynamically.
	/// The flattened type provides an alternative approach, where the entire object is mapped as a single field.
	/// Given an object, the flattened mapping will parse out its leaf values and index them into one
	/// field as keywords. The object's contents can then be searched through simple queries and aggregations.
	/// <para></para>
	/// Available in Elasticsearch 7.3.0+ with at least basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IFlattenedProperty : IProperty
	{
		/// <summary>
		/// The maximum allowed depth of the flattened object field,
		/// in terms of nested inner objects. If a flattened object field exceeds this limit,
		/// then an error will be thrown. Defaults to <c>20</c>.
		/// </summary>
		[DataMember(Name = "depth_limit")]
		int? DepthLimit { get; set; }

		/// <summary>
		/// Whether to persist the value at index time in a columnar data structure (referred to as doc_values in Lucene)
		/// which makes the value available for efficient sorting and aggregations. Default is <c>true</c>.
		/// </summary>
		[DataMember(Name = "doc_values")]
		bool? DocValues { get; set; }

		/// <summary>
		/// Should global ordinals be loaded eagerly on refresh? Accepts true or false (default).
		/// Enabling this is a good idea on fields that are frequently used for terms aggregations.
		/// </summary>
		[DataMember(Name = "eager_global_ordinals")]
		bool? EagerGlobalOrdinals { get; set; }

		/// <summary>
		/// Leaf values longer than this limit will not be indexed. By default, there is no limit and all values will be indexed.
		/// Note that this limit applies to the leaf values within the flattened object field, and not the length of the entire
		/// field.
		/// </summary>
		[DataMember(Name = "ignore_above")]
		int? IgnoreAbove { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts <c>true</c> (default) and <c>false</c>.
		/// </summary>
		[DataMember(Name = "index")]
		bool? Index { get; set; }

		/// <summary>
		/// What information should be stored in the index for scoring purposes.
		/// Defaults to docs but can also be set to freqs to take term frequency into account when computing scores.
		/// </summary>
		[DataMember(Name = "index_options")]
		IndexOptions? IndexOptions { get; set; }

		/// <summary>
		/// A string value which is substituted for any explicit null values within the flattened
		/// object field. Defaults to null, which means null fields are treated as if it were missing.
		/// </summary>
		[DataMember(Name = "null_value")]
		string NullValue { get; set; }

		/// <summary>
		/// Which relevancy scoring algorithm or similarity should be used.
		/// Defaults to BM25
		/// </summary>
		[DataMember(Name = "similarity")]
		string Similarity { get; set; }

		/// <summary> Whether full text queries should split the input on whitespace when building a query for this field. </summary>
		[DataMember(Name = "split_queries_on_whitespace")]
		bool? SplitQueriesOnWhitespace { get; set; }
	}

	/// <inheritdoc cref="IFlattenedProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class FlattenedProperty : PropertyBase, IFlattenedProperty
	{
		public FlattenedProperty() : base(FieldType.Flattened) { }

		/// <inheritdoc />
		public int? DepthLimit { get; set; }

		/// <inheritdoc />
		public bool? DocValues { get; set; }

		/// <inheritdoc />
		public bool? EagerGlobalOrdinals { get; set; }

		/// <inheritdoc />
		public int? IgnoreAbove { get; set; }

		/// <inheritdoc />
		public bool? Index { get; set; }

		/// <inheritdoc />
		public IndexOptions? IndexOptions { get; set; }

		/// <inheritdoc />
		public string NullValue { get; set; }

		/// <inheritdoc />
		public string Similarity { get; set; }

		/// <inheritdoc />
		public bool? SplitQueriesOnWhitespace { get; set; }
	}

	/// <inheritdoc cref="IFlattenedProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class FlattenedPropertyDescriptor<T>
		: PropertyDescriptorBase<FlattenedPropertyDescriptor<T>, IFlattenedProperty, T>, IFlattenedProperty
		where T : class
	{
		public FlattenedPropertyDescriptor() : base(FieldType.Flattened) { }

		int? IFlattenedProperty.DepthLimit { get; set; }
		bool? IFlattenedProperty.DocValues { get; set; }
		bool? IFlattenedProperty.EagerGlobalOrdinals { get; set; }
		int? IFlattenedProperty.IgnoreAbove { get; set; }
		bool? IFlattenedProperty.Index { get; set; }
		IndexOptions? IFlattenedProperty.IndexOptions { get; set; }
		string IFlattenedProperty.NullValue { get; set; }
		string IFlattenedProperty.Similarity { get; set; }
		bool? IFlattenedProperty.SplitQueriesOnWhitespace { get; set; }

		/// <inheritdoc cref="IFlattenedProperty.DepthLimit" />
		public FlattenedPropertyDescriptor<T> DepthLimit(int? depthLimit) => Assign(depthLimit, (a, v) => a.DepthLimit = v);

		/// <inheritdoc cref="IFlattenedProperty.DepthLimit" />
		public FlattenedPropertyDescriptor<T> DocValues(bool? docValues = true) => Assign(docValues, (a, v) => a.DocValues = v);

		/// <inheritdoc cref="IFlattenedProperty.EagerGlobalOrdinals" />
		public FlattenedPropertyDescriptor<T> EagerGlobalOrdinals(bool? eagerGlobalOrdinals = true) =>
			Assign(eagerGlobalOrdinals, (a, v) => a.EagerGlobalOrdinals = v);

		/// <inheritdoc cref="IFlattenedProperty.IgnoreAbove" />
		public FlattenedPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(ignoreAbove, (a, v) => a.IgnoreAbove = v);

		/// <inheritdoc cref="IFlattenedProperty.Index" />
		public FlattenedPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="IFlattenedProperty.IndexOptions" />
		public FlattenedPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		/// <inheritdoc cref="IFlattenedProperty.NullValue" />
		public FlattenedPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		/// <inheritdoc cref="IFlattenedProperty.SplitQueriesOnWhitespace" />
		public FlattenedPropertyDescriptor<T> SplitQueriesOnWhitespace(bool? splitQueriesOnWhitespace = true) =>
			Assign(splitQueriesOnWhitespace, (a, v) => a.SplitQueriesOnWhitespace = v);

		/// <inheritdoc cref="IFlattenedProperty.Similarity" />
		public FlattenedPropertyDescriptor<T> Similarity(string similarity) => Assign(similarity, (a, v) => a.Similarity = v);
	}
}
