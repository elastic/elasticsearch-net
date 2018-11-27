using System.Runtime.Serialization;

namespace Nest
{
	public interface INumericRangeQuery : IRangeQuery
	{
		[DataMember(Name ="gt")]
		double? GreaterThan { get; set; }

		[DataMember(Name ="gte")]
		double? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name ="lt")]
		double? LessThan { get; set; }

		[DataMember(Name ="lte")]
		double? LessThanOrEqualTo { get; set; }

		[DataMember(Name ="relation")]
		RangeRelation? Relation { get; set; }
	}

	public class NumericRangeQuery : FieldNameQueryBase, INumericRangeQuery
	{
		public double? GreaterThan { get; set; }
		public double? GreaterThanOrEqualTo { get; set; }
		public double? LessThan { get; set; }
		public double? LessThanOrEqualTo { get; set; }

		public RangeRelation? Relation { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(INumericRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo == null
			&& q.LessThanOrEqualTo == null
			&& q.GreaterThan == null
			&& q.LessThan == null;
	}

	[DataContract]
	public class NumericRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<NumericRangeQueryDescriptor<T>, INumericRangeQuery, T>
			, INumericRangeQuery where T : class
	{
		protected override bool Conditionless => NumericRangeQuery.IsConditionless(this);
		double? INumericRangeQuery.GreaterThan { get; set; }
		double? INumericRangeQuery.GreaterThanOrEqualTo { get; set; }
		double? INumericRangeQuery.LessThan { get; set; }
		double? INumericRangeQuery.LessThanOrEqualTo { get; set; }

		RangeRelation? INumericRangeQuery.Relation { get; set; }

		public NumericRangeQueryDescriptor<T> GreaterThan(double? from) => Assign(a => a.GreaterThan = from);

		public NumericRangeQueryDescriptor<T> GreaterThanOrEquals(double? from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public NumericRangeQueryDescriptor<T> LessThan(double? to) => Assign(a => a.LessThan = to);

		public NumericRangeQueryDescriptor<T> LessThanOrEquals(double? to) => Assign(a => a.LessThanOrEqualTo = to);

		public NumericRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(a => a.Relation = relation);
	}
}
