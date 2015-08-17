using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDateType : IElasticType
	{
		[JsonProperty("index")]
		NonStringIndexOption? Index { get; set; }

		[JsonProperty("boost")]
        double? Boost { get; set; }

		[JsonProperty("null_value")]
        DateTime? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }

		[JsonProperty("precision_step")]
		int? PrecisionStep { get; set; }
		
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
		
		[JsonProperty("format")]
		string Format { get; set; }
		
		[JsonProperty("numeric_resolution")]
        NumericResolutionUnit? NumericResolution { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}

	public class DateType : ElasticType, IDateType
	{
		public DateType() : base("date") { }

		internal DateType(DateAttribute attribute)
			: base("date", attribute)
		{
			Index = attribute.Index;
			Boost = attribute.Boost;
			NullValue = attribute.NullValue;
			IncludeInAll = attribute.IncludeInAll;
			PrecisionStep = attribute.PrecisionStep;
			IgnoreMalformed = attribute.IgnoreMalformed;
			Format = attribute.Format;
			NumericResolution = attribute.NumericResolution;
		}

		public NonStringIndexOption? Index { get; set; }
		public double? Boost { get; set; }
		public DateTime? NullValue { get; set; }
		public bool? IncludeInAll { get; set; }
		public int? PrecisionStep { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public string Format { get; set; }
		public NumericResolutionUnit? NumericResolution { get; set; }
		public INumericFielddata Fielddata { get; set; }
	}

	public class DateTypeDescriptor<T> 
		: TypeDescriptorBase<DateTypeDescriptor<T>, IDateType, T>, IDateType
		where T : class
	{
		NonStringIndexOption? IDateType.Index { get; set; }
		double? IDateType.Boost { get; set; }
		DateTime? IDateType.NullValue { get; set; }
		bool? IDateType.IncludeInAll { get; set; }
		int? IDateType.PrecisionStep { get; set; }
		bool? IDateType.IgnoreMalformed { get; set; }
		string IDateType.Format { get; set; }
		NumericResolutionUnit? IDateType.NumericResolution { get; set; }
		INumericFielddata IDateType.Fielddata { get; set; }

		public DateTypeDescriptor<T> Index(NonStringIndexOption index = NonStringIndexOption.No) => Assign(a => a.Index = index);
		public DateTypeDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);
		public DateTypeDescriptor<T> NullValue(DateTime nullValue) => Assign(a => a.NullValue = nullValue);
		public DateTypeDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
		public DateTypeDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);
		public DateTypeDescriptor<T> IgnoreMalformed(bool ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);
		public DateTypeDescriptor<T> Format(string format) => Assign(a => a.Format = format);
		public DateTypeDescriptor<T> NumericResolution(NumericResolutionUnit unit) => Assign(a => a.NumericResolution = unit);
		public DateTypeDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}