using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DateRangeExpression>))]
	public interface IDateRangeExpression
	{
		[JsonProperty(PropertyName = "from")]
		DateMath From { get; set; }

		[JsonProperty(PropertyName = "key")]
		string Key { get; set; }

		[JsonProperty(PropertyName = "to")]
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

		public DateRangeExpressionDescriptor From(DateMath from) => Assign(a => a.From = from);

		public DateRangeExpressionDescriptor To(DateMath to) => Assign(a => a.To = to);

		public DateRangeExpressionDescriptor Key(string key) => Assign(a => a.Key = key);
	}
}
