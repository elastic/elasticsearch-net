using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DateRangeExpression>))]
	public interface IDateRangeExpression
	{
		[JsonProperty("from")]
		DateMath From { get; set; }

		[JsonProperty("key")]
		string Key { get; set; }

		[JsonProperty("to")]
		DateMath To { get; set; }
	}

	public class DateRangeExpression : IDateRangeExpression
	{
		public DateMath From { get; set; }

		public string Key { get; set; }

		public DateMath To { get; set; }
	}

	public class DateRangeExpressionDescriptor
		: DescriptorBase<DateRangeExpressionDescriptor, IDateRangeExpression>, IDateRangeExpression
	{
		DateMath IDateRangeExpression.From { get; set; }

		string IDateRangeExpression.Key { get; set; }

		DateMath IDateRangeExpression.To { get; set; }

		public DateRangeExpressionDescriptor From(DateMath from) => Assign(from, (a, v) => a.From = v);

		public DateRangeExpressionDescriptor To(DateMath to) => Assign(to, (a, v) => a.To = v);

		public DateRangeExpressionDescriptor Key(string key) => Assign(key, (a, v) => a.Key = v);
	}
}
