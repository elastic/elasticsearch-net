using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDateProperty : IDocValuesProperty
	{
		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("boost")]
        double? Boost { get; set; }

		[JsonProperty("null_value")]
        DateTime? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}

	public class DateProperty : DocValuesPropertyBase, IDateProperty
	{
		public DateProperty() : base("date") { }

		public bool? Index { get; set; }
		public double? Boost { get; set; }
		public DateTime? NullValue { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? PrecisionStep { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public string Format { get; set; }
		public INumericFielddata Fielddata { get; set; }
	}

	public class DatePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<DatePropertyDescriptor<T>, IDateProperty, T>, IDateProperty
		where T : class
	{
		bool? IDateProperty.Index { get; set; }
		double? IDateProperty.Boost { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		bool? IDateProperty.IncludeInAll { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }
		string IDateProperty.Format { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }

		public DatePropertyDescriptor() : base("date") { }

		public DatePropertyDescriptor<T> Index(bool index) => Assign(a => a.Index = index);
		public DatePropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public DatePropertyDescriptor<T> NullValue(DateTime nullValue) => Assign(a => a.NullValue = nullValue);
		public DatePropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
		public DatePropertyDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);
		public DatePropertyDescriptor<T> Format(string format) => Assign(a => a.Format = format);
		public DatePropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}
