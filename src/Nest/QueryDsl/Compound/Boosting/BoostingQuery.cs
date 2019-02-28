using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(BoostingQueryDescriptor<object>))]
	public interface IBoostingQuery : IQuery
	{
		[DataMember(Name ="negative_boost")]
		double? NegativeBoost { get; set; }

		[DataMember(Name ="negative")]
		QueryContainer NegativeQuery { get; set; }

		[DataMember(Name ="positive")]
		QueryContainer PositiveQuery { get; set; }
	}

	public class BoostingQuery : QueryBase, IBoostingQuery
	{
		public double? NegativeBoost { get; set; }
		public QueryContainer NegativeQuery { get; set; }
		public QueryContainer PositiveQuery { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Boosting = this;

		internal static bool IsConditionless(IBoostingQuery q) => q.NegativeQuery.NotWritable() && q.PositiveQuery.NotWritable();
	}

	public class BoostingQueryDescriptor<T>
		: QueryDescriptorBase<BoostingQueryDescriptor<T>, IBoostingQuery>
			, IBoostingQuery where T : class
	{
		protected override bool Conditionless => BoostingQuery.IsConditionless(this);
		double? IBoostingQuery.NegativeBoost { get; set; }
		QueryContainer IBoostingQuery.NegativeQuery { get; set; }
		QueryContainer IBoostingQuery.PositiveQuery { get; set; }

		public BoostingQueryDescriptor<T> NegativeBoost(double? boost) => Assign(a => a.NegativeBoost = boost);

		public BoostingQueryDescriptor<T> Positive(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.PositiveQuery = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public BoostingQueryDescriptor<T> Negative(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.NegativeQuery = selector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
