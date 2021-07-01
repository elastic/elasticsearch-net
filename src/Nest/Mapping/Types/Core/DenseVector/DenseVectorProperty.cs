// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A dense_vector field stores dense vectors of float values. The maximum number of dimensions
	/// that can be in a vector should not exceed 2048. A dense_vector field is a single-valued field.
	/// </summary>
	[InterfaceDataContract]
	public interface IDenseVectorProperty : IProperty
	{
		/// <summary>
		/// The number of dimensions in the vector.
		/// </summary>
		[DataMember(Name = "dims")]
		int? Dimensions { get; set; }
	}

	/// <inheritdoc cref="IDenseVectorProperty" />
	public class DenseVectorProperty : PropertyBase, IDenseVectorProperty
	{
		public DenseVectorProperty() : base(FieldType.DenseVector) { }

		/// <inheritdoc />
		public int? Dimensions { get; set; }
	}

	/// <inheritdoc cref="IDenseVectorProperty" />
	[DebuggerDisplay("{" + nameof(DebugDisplay) + "}")]
	public class DenseVectorPropertyDescriptor<T>
		: PropertyDescriptorBase<DenseVectorPropertyDescriptor<T>, IDenseVectorProperty, T>, IDenseVectorProperty
		where T : class
	{
		public DenseVectorPropertyDescriptor() : base(FieldType.DenseVector) { }

		int? IDenseVectorProperty.Dimensions { get; set; }

		/// <inheritdoc cref="IDenseVectorProperty.Dimensions" />
		public DenseVectorPropertyDescriptor<T> Dimensions(int? dimensions) =>
			Assign(dimensions, (a, v) => a.Dimensions = v);
	}
}
