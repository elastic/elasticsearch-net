// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRangeProperty : IDocValuesProperty
	{
		/// <summary>
		/// Try to convert strings to numbers and truncate fractions for integers. Accepts true (default) and false.
		/// </summary>
		[DataMember(Name ="coerce")]
		bool? Coerce { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[DataMember(Name ="index")]
		bool? Index { get; set; }
	}

	public abstract class RangePropertyBase : DocValuesPropertyBase, IRangeProperty
	{
		protected RangePropertyBase(RangeType rangeType) : base(rangeType.ToFieldType()) { }

		/// <inheritdoc />
		public bool? Coerce { get; set; }

		/// <inheritdoc />
		public bool? Index { get; set; }
	}

	public abstract class RangePropertyDescriptorBase<TDescriptor, TInterface, T>
		: DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, IRangeProperty
		where TDescriptor : RangePropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IRangeProperty
		where T : class
	{
		protected RangePropertyDescriptorBase(RangeType type) : base(type.ToFieldType()) { }

		bool? IRangeProperty.Coerce { get; set; }
		bool? IRangeProperty.Index { get; set; }

		/// <inheritdoc cref="IRangeProperty.Coerce" />
		public TDescriptor Coerce(bool? coerce = true) => Assign(coerce, (a, v) => a.Coerce = v);

		/// <inheritdoc cref="IRangeProperty.Index" />
		public TDescriptor Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);
	}
}
