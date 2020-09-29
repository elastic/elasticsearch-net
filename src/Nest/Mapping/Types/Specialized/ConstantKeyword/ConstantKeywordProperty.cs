// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Constant keyword is a specialization of the keyword field for the case that all documents in the index have the same value.
	/// <para />
	/// Available in Elasticsearch 7.7.0+ with at least a basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IConstantKeywordProperty : IProperty
	{
		/// <summary>
		/// The value to associate with all documents in the index.
		/// If this parameter is not provided, it is set based on the first document that gets indexed.
		/// <para />
		/// Value must be a string or a numeric value
		/// </summary>
		[DataMember(Name ="value")]
		object Value { get; set; }
	}

	/// <inheritdoc cref="IConstantKeywordProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ConstantKeywordProperty : PropertyBase, IConstantKeywordProperty
	{
		public ConstantKeywordProperty() : base(FieldType.ConstantKeyword) { }

		/// <inheritdoc cref="IConstantKeywordProperty.Value" />
		public object Value { get; set; }
	}

	/// <inheritdoc cref="IConstantKeywordProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class ConstantKeywordPropertyDescriptor<T>
		: PropertyDescriptorBase<ConstantKeywordPropertyDescriptor<T>, IConstantKeywordProperty, T>, IConstantKeywordProperty
		where T : class
	{
		public ConstantKeywordPropertyDescriptor() : base(FieldType.ConstantKeyword) { }

		object IConstantKeywordProperty.Value { get; set; }

		/// <inheritdoc cref="IConstantKeywordProperty.Value" />
		public ConstantKeywordPropertyDescriptor<T> Value(object value) => Assign(value, (a, v) => a.Value = v);
	}
}
