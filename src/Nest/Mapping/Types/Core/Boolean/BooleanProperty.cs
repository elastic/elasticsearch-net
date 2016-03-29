using System;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization.OptIn)]
	public interface IBooleanProperty : IProperty
	{
		[JsonProperty("index")]
		NonStringIndexOption? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		bool? NullValue { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}
	
	public class BooleanProperty : PropertyBase, IBooleanProperty
	{
		public BooleanProperty() : base("boolean") { }

		public NonStringIndexOption? Index { get; set; }
		public double? Boost { get; set; }
		public bool? NullValue { get; set; }
		public INumericFielddata Fielddata { get; set; }
	}

	public class BooleanPropertyDescriptor<T>
		: PropertyDescriptorBase<BooleanPropertyDescriptor<T>, IBooleanProperty, T>, IBooleanProperty
		where T : class
	{
		double? IBooleanProperty.Boost { get; set; }
		NonStringIndexOption? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }

		public BooleanPropertyDescriptor() : base("boolean") { }

		public BooleanPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public BooleanPropertyDescriptor<T> Index(NonStringIndexOption index) => Assign(a => a.Index = index);
		public BooleanPropertyDescriptor<T> NullValue(bool nullValue) => Assign(a => a.NullValue = nullValue);
		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}