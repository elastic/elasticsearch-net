// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Properties of a mapping for a property type to a document field that has doc_values in Elasticsearch
	/// </summary>
	[InterfaceDataContract]
	public interface IDocValuesProperty : ICoreProperty
	{
		/// <summary>
		/// Whether to persist the value at index time in a columnar data structure (referred to as doc_values in Lucene)
		/// which makes the value available for efficient sorting and aggregations. Default is <c>true</c>.
		/// </summary>
		[DataMember(Name ="doc_values")]
		bool? DocValues { get; set; }
	}

	/// <inheritdoc cref="IDocValuesProperty" />
	public abstract class DocValuesPropertyBase : CorePropertyBase, IDocValuesProperty
	{
		protected DocValuesPropertyBase(FieldType type) : base(type) { }

		/// <inheritdoc />
		public bool? DocValues { get; set; }
	}
}
