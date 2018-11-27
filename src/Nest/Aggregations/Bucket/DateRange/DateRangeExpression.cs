using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(DateRangeExpression))]
	public interface IDateRangeExpression
	{
		[DataMember(Name ="from")]
		DateMath From { get; set; }

		[DataMember(Name ="key")]
		string Key { get; set; }

		[DataMember(Name ="to")]
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
