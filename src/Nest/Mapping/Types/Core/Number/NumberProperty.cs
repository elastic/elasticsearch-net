// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elastic.Transport.Extensions;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A numeric mapping that defaults to <c>float</c>.
	/// </summary>
	[InterfaceDataContract]
	public interface INumberProperty : IDocValuesProperty
	{
		[DataMember(Name = "coerce")]
		bool? Coerce { get; set; }

		[DataMember(Name = "fielddata")]
		INumericFielddata Fielddata { get; set; }

		[DataMember(Name = "ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "null_value")]
		double? NullValue { get; set; }

		[DataMember(Name = "scaling_factor")]
		double? ScalingFactor { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class NumberProperty : DocValuesPropertyBase, INumberProperty
	{
		public NumberProperty() : base(FieldType.Float) { }

		public NumberProperty(NumberType type) : base(type.ToFieldType()) { }

		public bool? Coerce { get; set; }
		public INumericFielddata Fielddata { get; set; }
		public bool? IgnoreMalformed { get; set; }

		public bool? Index { get; set; }
		public double? NullValue { get; set; }
		public double? ScalingFactor { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public abstract class NumberPropertyDescriptorBase<TDescriptor, TInterface, T>
		: DocValuesPropertyDescriptorBase<TDescriptor, TInterface, T>, INumberProperty
		where TDescriptor : NumberPropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, INumberProperty
		where T : class
	{
		protected NumberPropertyDescriptorBase() : base(FieldType.Float) { }

		protected NumberPropertyDescriptorBase(FieldType type) : base(type) { }

		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.NullValue { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }

		public TDescriptor Type(NumberType? type) => Assign(type?.GetStringValue(), (a, v) => a.Type = v);

		public TDescriptor Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);


		public TDescriptor NullValue(double? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		public TDescriptor IgnoreMalformed(bool? ignoreMalformed = true) => Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		public TDescriptor Coerce(bool? coerce = true) => Assign(coerce, (a, v) => a.Coerce = v);

		public TDescriptor Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(selector(new NumericFielddataDescriptor()), (a, v) => a.Fielddata = v);

		public TDescriptor ScalingFactor(double? scalingFactor) => Assign(scalingFactor, (a, v) => a.ScalingFactor = v);
	}

	public class NumberPropertyDescriptor<T> : NumberPropertyDescriptorBase<NumberPropertyDescriptor<T>, INumberProperty, T>
		where T : class { }
}
