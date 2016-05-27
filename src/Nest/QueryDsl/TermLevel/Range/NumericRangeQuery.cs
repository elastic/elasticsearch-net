using Newtonsoft.Json;

namespace Nest
{
	public interface INumericRangeQuery : IRangeQuery
	{
		[JsonProperty("gte")]
		double? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		double? LessThanOrEqualTo { get; set; }
		
		[JsonProperty("gt")]
		double? GreaterThan { get; set; }

		[JsonProperty("lt")]
		double? LessThan { get; set; }
	}

	public class NumericRangeQuery : FieldNameQueryBase, INumericRangeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public double? GreaterThanOrEqualTo { get; set; }
		public double? LessThanOrEqualTo { get; set; }
		public double? GreaterThan { get; set; }
		public double? LessThan { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(INumericRangeQuery q)
		{
			return q.Field.IsConditionless() 
				|| (q.GreaterThanOrEqualTo == null
				&& q.LessThanOrEqualTo == null
				&& q.GreaterThan == null
				&& q.LessThan == null);
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class NumericRangeQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<NumericRangeQueryDescriptor<T>, INumericRangeQuery, T>
		, INumericRangeQuery where T : class
	{
		protected override bool Conditionless => NumericRangeQuery.IsConditionless(this);
		double? INumericRangeQuery.GreaterThanOrEqualTo { get; set; }
		double? INumericRangeQuery.LessThanOrEqualTo { get; set; }
		double? INumericRangeQuery.GreaterThan { get; set; }
		double? INumericRangeQuery.LessThan { get; set; }

		public NumericRangeQueryDescriptor<T> GreaterThan(double? from) => Assign(a => a.GreaterThan = from);

		public NumericRangeQueryDescriptor<T> GreaterThanOrEquals(double? from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public NumericRangeQueryDescriptor<T> LessThan(double? to) => Assign(a => a.LessThan = to);

		public NumericRangeQueryDescriptor<T> LessThanOrEquals(double? to) => Assign(a => a.LessThanOrEqualTo = to);
	}
}
