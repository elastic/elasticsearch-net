using System;
using System.Diagnostics;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface INumberProperty : IDocValuesProperty
	{
		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("null_value")]
		double? NullValue { get; set; }

		[JsonProperty("scaling_factor")]
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

		public TDescriptor Type(NumberType? type) => Assign(type?.GetStringValue(), (a, v) => a.Type = v);

		public TDescriptor Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public TDescriptor Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

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
