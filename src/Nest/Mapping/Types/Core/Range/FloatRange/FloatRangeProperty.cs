// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of single-precision 32-bit IEEE 754 floating point values.
	/// </summary>
	[InterfaceDataContract]
	public interface IFloatRangeProperty : IRangeProperty { }

	/// <inheritdoc cref="IFloatRangeProperty"/>
	public class FloatRangeProperty : RangePropertyBase, IFloatRangeProperty
	{
		public FloatRangeProperty() : base(RangeType.FloatRange) { }
	}

	/// <inheritdoc cref="IFloatRangeProperty"/>
	public class FloatRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<FloatRangePropertyDescriptor<T>, IFloatRangeProperty, T>, IFloatRangeProperty
		where T : class
	{
		public FloatRangePropertyDescriptor() : base(RangeType.FloatRange) { }
	}
}
