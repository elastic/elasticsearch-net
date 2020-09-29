// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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

		public BoostingQueryDescriptor<T> NegativeBoost(double? boost) => Assign(boost, (a, v) => a.NegativeBoost = v);

		public BoostingQueryDescriptor<T> Positive(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.PositiveQuery = v?.Invoke(new QueryContainerDescriptor<T>()));

		public BoostingQueryDescriptor<T> Negative(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.NegativeQuery = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
