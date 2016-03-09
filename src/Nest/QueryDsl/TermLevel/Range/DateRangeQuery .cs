using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<DateRangeQuery>))]
	public interface IDateRangeQuery :  IRangeQuery
	{
		[JsonProperty("gte")]
		DateMath GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		DateMath LessThanOrEqualTo { get; set; }
		
		[JsonProperty("gt")]
		DateMath GreaterThan { get; set; }

		[JsonProperty("lt")]
		DateMath LessThan { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }
	}

	public class DateRangeQuery : FieldNameQueryBase, IDateRangeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		public DateMath GreaterThanOrEqualTo { get; set; }
		public DateMath LessThanOrEqualTo { get; set; }
		public DateMath GreaterThan { get; set; }
		public DateMath LessThan { get; set; }
		public string TimeZone { get; set; }
		public string Format { get; set; }


		internal static bool IsConditionless(IDateRangeQuery q)
		{
			return q.Field.IsConditionless() 
				|| (q.GreaterThanOrEqualTo == null
				&& q.LessThanOrEqualTo == null
				&& q.GreaterThan == null
				&& q.LessThan == null);
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DateRangeQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<DateRangeQueryDescriptor<T>, IDateRangeQuery, T>
		, IDateRangeQuery where T : class
	{
		protected override bool Conditionless => DateRangeQuery.IsConditionless(this);
		DateMath IDateRangeQuery.GreaterThanOrEqualTo { get; set; }
		DateMath IDateRangeQuery.LessThanOrEqualTo { get; set; }
		DateMath IDateRangeQuery.GreaterThan { get; set; }
		DateMath IDateRangeQuery.LessThan { get; set; }
		string IDateRangeQuery.TimeZone { get; set; }
		string IDateRangeQuery.Format { get; set; }

		public DateRangeQueryDescriptor<T> GreaterThan(DateMath from) => Assign(a => a.GreaterThan = from);

		public DateRangeQueryDescriptor<T> GreaterThanOrEquals(DateMath from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public DateRangeQueryDescriptor<T> LessThan(DateMath to) => Assign(a => a.LessThan = to);

		public DateRangeQueryDescriptor<T> LessThanOrEquals(DateMath to) => Assign(a => a.LessThanOrEqualTo = to);

		public DateRangeQueryDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone); 

		public DateRangeQueryDescriptor<T> Format(string format) => Assign(a => a.Format = format);
	}
}
