using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBooleanProperty : IDocValuesProperty
	{
		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		bool? NullValue { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanProperty : DocValuesPropertyBase, IBooleanProperty
	{
		public BooleanProperty() : base(FieldType.Boolean) { }

		public bool? Index { get; set; }
		public double? Boost { get; set; }
		public bool? NullValue { get; set; }
		public INumericFielddata Fielddata { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BooleanPropertyDescriptor<T>, IBooleanProperty, T>, IBooleanProperty
		where T : class
	{
		double? IBooleanProperty.Boost { get; set; }
		bool? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }

		public BooleanPropertyDescriptor() : base(FieldType.Boolean) { }

		public BooleanPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);
		public BooleanPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);
		public BooleanPropertyDescriptor<T> NullValue(bool? nullValue) => Assign(a => a.NullValue = nullValue);
		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}
