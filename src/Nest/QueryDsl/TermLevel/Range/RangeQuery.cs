using System;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (FieldNameQueryJsonConverter<RangeQuery>))]
	public interface IRangeQuery : IFieldNameQuery
	{
		[JsonProperty("gte")]
		string GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		string LowerThanOrEqualTo { get; set; }
		
		[JsonProperty("gt")]
		string GreaterThan { get; set; }

		[JsonProperty("lt")]
		string LowerThan { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }
	}

	public class RangeQuery : FieldNameQueryBase, IRangeQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string GreaterThanOrEqualTo { get; set; }
		public string LowerThanOrEqualTo { get; set; }
		public string GreaterThan { get; set; }
		public string LowerThan { get; set; }
		public bool? Cache { get; set; }
		public string TimeZone { get; set; }
		public string Format { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(IRangeQuery q)
		{
			return q.Field.IsConditionless() 
				|| (q.GreaterThanOrEqualTo == null
				&& q.LowerThanOrEqualTo == null
				&& q.GreaterThan == null
				&& q.LowerThan == null);
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RangeQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<RangeQueryDescriptor<T>, IRangeQuery, T>
		, IRangeQuery where T : class
	{
		bool IQuery.Conditionless => RangeQuery.IsConditionless(this);
		string IRangeQuery.GreaterThanOrEqualTo { get; set; }
		string IRangeQuery.LowerThanOrEqualTo { get; set; }
		string IRangeQuery.GreaterThan { get; set; }
		string IRangeQuery.LowerThan { get; set; }
		string IRangeQuery.TimeZone { get; set; }
		string IRangeQuery.Format { get; set; }

		public RangeQueryDescriptor<T> Greater(double? from) => 
			Assign(a => a.GreaterThan = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null);

		public RangeQueryDescriptor<T> GreaterOrEquals(double? from) =>
			Assign(a => a.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(CultureInfo.InvariantCulture) : null);

		public RangeQueryDescriptor<T> Lower(double? to) =>
			Assign(a => a.LowerThan = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null);

		public RangeQueryDescriptor<T> LowerOrEquals(double? to) =>
			Assign(a => a.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(CultureInfo.InvariantCulture) : null);

		public RangeQueryDescriptor<T> Greater(string from) => Assign(a => a.GreaterThan = from);

		public RangeQueryDescriptor<T> GreaterOrEquals(string from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public RangeQueryDescriptor<T> Lower(string to) => Assign(a => a.LowerThan = to);

		public RangeQueryDescriptor<T> LowerOrEquals(string to) => Assign(a => a.LowerThanOrEqualTo = to);

		public RangeQueryDescriptor<T> Greater(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff") =>
			Assign(a => a.GreaterThan = from.HasValue ? from.Value.ToString(format) : null);

		public RangeQueryDescriptor<T> GreaterOrEquals(DateTime? from, string format = "yyyy-MM-dd'T'HH:mm:ss.fff") =>
			Assign(a => a.GreaterThanOrEqualTo = from.HasValue ? from.Value.ToString(format) : null);

		public RangeQueryDescriptor<T> Lower(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff") =>
			Assign(a => a.LowerThan = to.HasValue ? to.Value.ToString(format) : null);

		public RangeQueryDescriptor<T> LowerOrEquals(DateTime? to, string format = "yyyy-MM-dd'T'HH:mm:ss.fff") =>
			Assign(a => a.LowerThanOrEqualTo = to.HasValue ? to.Value.ToString(format) : null);

		public RangeQueryDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public RangeQueryDescriptor<T> Format(string format) => Assign(a => a.Format = format);
	}
}
