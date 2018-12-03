using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBooleanProperty : IDocValuesProperty
	{
		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		[DataMember(Name = "fielddata")]
		INumericFielddata Fielddata { get; set; }

		[DataMember(Name = "index")]
		bool? Index { get; set; }

		[DataMember(Name = "null_value")]
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

		public BooleanPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public BooleanPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);

		public BooleanPropertyDescriptor<T> NullValue(bool? nullValue) => Assign(a => a.NullValue = nullValue);

		public BooleanPropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}
