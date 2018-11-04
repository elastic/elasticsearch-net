using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IBooleanProperty : IDocValuesProperty
	{
		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }

		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("null_value")]
		bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanProperty : DocValuesPropertyBase, IBooleanProperty
	{
		public BooleanProperty() : base(FieldType.Boolean) { }

		public double? Boost { get; set; }
		public INumericFielddata Fielddata { get; set; }

		public bool? Index { get; set; }
		public bool? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class BooleanPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<BooleanPropertyDescriptor<T>, IBooleanProperty, T>, IBooleanProperty
		where T : class
	{
		public BooleanPropertyDescriptor() : base(FieldType.Boolean) { }

		double? IBooleanProperty.Boost { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }
		bool? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }

		public BooleanPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public BooleanPropertyDescriptor<T> Index(bool index) => Assign(a => a.Index = index);

		public BooleanPropertyDescriptor<T> NullValue(bool nullValue) => Assign(a => a.NullValue = nullValue);

		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}
