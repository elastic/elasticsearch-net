// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The percolator datatype is used to store a query, so that the
	/// <see cref="IPercolateQuery"/> can use it to match provided documents.
	/// </summary>
	[InterfaceDataContract]
	public interface IPercolatorProperty : IProperty { }

	/// <inheritdoc cref="IPercolatorProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class PercolatorProperty : PropertyBase, IPercolatorProperty
	{
		public PercolatorProperty() : base(FieldType.Percolator) { }
	}

	/// <inheritdoc cref="IPercolatorProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class PercolatorPropertyDescriptor<T>
		: PropertyDescriptorBase<PercolatorPropertyDescriptor<T>, IPercolatorProperty, T>, IPercolatorProperty
		where T : class
	{
		public PercolatorPropertyDescriptor() : base(FieldType.Percolator) { }
	}
}
