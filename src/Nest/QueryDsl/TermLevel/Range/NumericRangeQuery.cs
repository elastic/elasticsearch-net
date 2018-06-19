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

		[JsonProperty("relation")]
		RangeRelation? Relation { get; set; }
	}

	public interface ILongRangeQuery : IRangeQuery
	{
		[JsonProperty("gte")]
		long? GreaterThanOrEqualTo { get; set; }

		[JsonProperty("lte")]
		long? LessThanOrEqualTo { get; set; }

		[JsonProperty("gt")]
		long? GreaterThan { get; set; }

		[JsonProperty("lt")]
		long? LessThan { get; set; }

		[JsonProperty("relation")]
		RangeRelation? Relation { get; set; }
	}

	public class NumericRangeQuery : FieldNameQueryBase, INumericRangeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public double? GreaterThanOrEqualTo { get; set; }
		public double? LessThanOrEqualTo { get; set; }
		public double? GreaterThan { get; set; }
		public double? LessThan { get; set; }

		public RangeRelation? Relation { get; set; }

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

	public class LongRangeQuery : FieldNameQueryBase, ILongRangeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public long? GreaterThanOrEqualTo { get; set; }
		public long? LessThanOrEqualTo { get; set; }
		public long? GreaterThan { get; set; }
		public long? LessThan { get; set; }

		public RangeRelation? Relation { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ILongRangeQuery q)
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

		RangeRelation? INumericRangeQuery.Relation{ get; set; }

		public NumericRangeQueryDescriptor<T> GreaterThan(double? from) => Assign(a => a.GreaterThan = from);

		public NumericRangeQueryDescriptor<T> GreaterThanOrEquals(double? from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public NumericRangeQueryDescriptor<T> LessThan(double? to) => Assign(a => a.LessThan = to);

		public NumericRangeQueryDescriptor<T> LessThanOrEquals(double? to) => Assign(a => a.LessThanOrEqualTo = to);

		public NumericRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(a => a.Relation = relation);
	}

	public class LongRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<LongRangeQueryDescriptor<T>, ILongRangeQuery, T>
			, ILongRangeQuery where T : class
	{
		protected override bool Conditionless => LongRangeQuery.IsConditionless(this);
		long? ILongRangeQuery.GreaterThanOrEqualTo { get; set; }
		long? ILongRangeQuery.LessThanOrEqualTo { get; set; }
		long? ILongRangeQuery.GreaterThan { get; set; }
		long? ILongRangeQuery.LessThan { get; set; }
		RangeRelation? ILongRangeQuery.Relation { get; set; }

		public LongRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(a => a.Relation = relation);

		public LongRangeQueryDescriptor<T> GreaterThan(long? from) => Assign(a => a.GreaterThan = from);

		public LongRangeQueryDescriptor<T> GreaterThanOrEquals(long? from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public LongRangeQueryDescriptor<T> LessThan(long? to) => Assign(a => a.LessThan = to);

		public LongRangeQueryDescriptor<T> LessThanOrEquals(long? to) => Assign(a => a.LessThanOrEqualTo = to);
	}
}
