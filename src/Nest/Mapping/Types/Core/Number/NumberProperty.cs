using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface INumberProperty : IProperty
	{
		[JsonProperty("index")]
		NonStringIndexOption? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		double? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("precision_step")]
		int? PrecisionStep { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}

	public class NumberProperty : Property, INumberProperty
	{
		public NumberProperty() : base(NumberType.Double.GetStringValue()) { }
		public NumberProperty(NumberType type) : base(type.GetStringValue()) { }
		protected NumberProperty(string type) : base(type) { }

		public NonStringIndexOption? Index { get; set; }
		public double? Boost { get; set; }
		public double? NullValue { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? PrecisionStep { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public bool? Coerce { get; set; }
		public INumericFielddata Fielddata { get; set; }
	}

	public class NumberPropertyDescriptor<T> 
		: PropertyDescriptorBase<NumberPropertyDescriptor<T>, INumberProperty, T>, INumberProperty
		where T : class
	{
		public NumberPropertyDescriptor() : base("double") { }

		NonStringIndexOption? INumberProperty.Index { get; set; }
		double? INumberProperty.Boost { get; set; }
		double? INumberProperty.NullValue { get; set; }
		bool? INumberProperty.IncludeInAll { get; set; }
		int? INumberProperty.PrecisionStep { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }

		public NumberPropertyDescriptor<T> Type(NumberType type) => Assign(a => a.Type = type.GetStringValue());

		public NumberPropertyDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No) => Assign(a => a.Index = index);

		public NumberPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public NumberPropertyDescriptor<T> NullValue(double nullValue) => Assign(a => a.NullValue = nullValue);

		public NumberPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);

		public NumberPropertyDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);

		public NumberPropertyDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);

		public NumberPropertyDescriptor<T> Coerce(bool coerce = true) => Assign(a => a.Coerce = coerce);

		public NumberPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}