using System;
using System.Diagnostics;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface INumberProperty : IDocValuesProperty
	{
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="coerce")]
		bool? Coerce { get; set; }

		[DataMember(Name ="fielddata")]
		INumericFielddata Fielddata { get; set; }

		[DataMember(Name ="ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="null_value")]
		double? NullValue { get; set; }

		[DataMember(Name ="scaling_factor")]
		double? ScalingFactor { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class NumberProperty : DocValuesPropertyBase, INumberProperty
	{
		public NumberProperty() : base(FieldType.Float) { }

		public NumberProperty(NumberType type) : base(type.ToFieldType()) { }

		public double? Boost { get; set; }
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

		double? INumberProperty.Boost { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.NullValue { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }

		public TDescriptor Type(NumberType? type) => Assign(a => a.Type = type?.GetStringValue());

		public TDescriptor Index(bool? index = true) => Assign(a => a.Index = index);

		public TDescriptor Boost(double? boost) => Assign(a => a.Boost = boost);

		public TDescriptor NullValue(double? nullValue) => Assign(a => a.NullValue = nullValue);

		public TDescriptor IgnoreMalformed(bool? ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);

		public TDescriptor Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		public TDescriptor Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));

		public TDescriptor ScalingFactor(double? scalingFactor) => Assign(a => a.ScalingFactor = scalingFactor);
	}

	public class NumberPropertyDescriptor<T> : NumberPropertyDescriptorBase<NumberPropertyDescriptor<T>, INumberProperty, T>
		where T : class { }
}
