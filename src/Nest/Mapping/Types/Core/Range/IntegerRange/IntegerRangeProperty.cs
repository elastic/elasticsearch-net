// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A range of signed 32-bit integers with a minimum value of -231 and maximum of 231-1.
	/// </summary>
	[InterfaceDataContract]
	public interface IIntegerRangeProperty : IRangeProperty { }

	/// <inheritdoc cref="IIntegerRangeProperty"/>
	public class IntegerRangeProperty : RangePropertyBase, IIntegerRangeProperty
	{
		public IntegerRangeProperty() : base(RangeType.IntegerRange) { }
	}

	/// <inheritdoc cref="IIntegerRangeProperty"/>
	public class IntegerRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty, T>, IIntegerRangeProperty
		where T : class
	{
		public IntegerRangePropertyDescriptor() : base(RangeType.IntegerRange) { }
	}
}
