using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(FieldNameQueryJsonConverter<TermRangeQuery>))]
	public interface ITermRangeQuery : IRangeQuery
	{
		[DataMember(Name ="gt")]
		string GreaterThan { get; set; }

		[DataMember(Name ="gte")]
		string GreaterThanOrEqualTo { get; set; }

		[DataMember(Name ="lt")]
		string LessThan { get; set; }

		[DataMember(Name ="lte")]
		string LessThanOrEqualTo { get; set; }
	}

	public class TermRangeQuery : FieldNameQueryBase, ITermRangeQuery
	{
		public string GreaterThan { get; set; }
		public string GreaterThanOrEqualTo { get; set; }
		public string LessThan { get; set; }
		public string LessThanOrEqualTo { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ITermRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo.IsNullOrEmpty()
			&& q.LessThanOrEqualTo.IsNullOrEmpty()
			&& q.GreaterThan.IsNullOrEmpty()
			&& q.LessThan.IsNullOrEmpty();
	}

	[DataContract]
	public class TermRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<TermRangeQueryDescriptor<T>, ITermRangeQuery, T>
			, ITermRangeQuery where T : class
	{
		protected override bool Conditionless => TermRangeQuery.IsConditionless(this);
		string ITermRangeQuery.GreaterThan { get; set; }
		string ITermRangeQuery.GreaterThanOrEqualTo { get; set; }
		string ITermRangeQuery.LessThan { get; set; }
		string ITermRangeQuery.LessThanOrEqualTo { get; set; }

		public TermRangeQueryDescriptor<T> GreaterThan(string from) => Assign(a => a.GreaterThan = from);

		public TermRangeQueryDescriptor<T> GreaterThanOrEquals(string from) => Assign(a => a.GreaterThanOrEqualTo = from);

		public TermRangeQueryDescriptor<T> LessThan(string to) => Assign(a => a.LessThan = to);

		public TermRangeQueryDescriptor<T> LessThanOrEquals(string to) => Assign(a => a.LessThanOrEqualTo = to);
	}
}
