using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AndQuery>))]
	[Obsolete("Use the bool query with a must clause query instead. The bool query should not have other clauses to be semantically correct")]
	public interface IAndQuery : IQuery
	{
		[JsonProperty("filters")]
		IEnumerable<QueryContainer> Filters { get; set; }
	}

	[Obsolete("Use the bool query with a must clause query instead. The bool query should not have other clauses to be semantically correct")]
	public class AndQuery : QueryBase, IAndQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IEnumerable<QueryContainer> Filters { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.And = this;
		internal static bool IsConditionless(IAndQuery q)
		{
			return !q.Filters.HasAny() || q.Filters.All(f => f.IsConditionless);
		}
	}

	[Obsolete("Use the bool query with a must clause instead. The bool query should not have other clauses to be semantically correct")]
	public class AndQueryDescriptor<T>
		: QueryDescriptorBase<AndQueryDescriptor<T>, IAndQuery>
		, IAndQuery where T : class
	{
		protected override bool Conditionless => AndQuery.IsConditionless(this);
		IEnumerable<QueryContainer> IAndQuery.Filters { get; set; }

		public AndQueryDescriptor<T> Filters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		public AndQueryDescriptor<T> Filters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		public AndQueryDescriptor<T> Filters(params QueryContainer[] queries) => Assign(a => a.Filters = queries.ToListOrNullIfEmpty());

	}
}
