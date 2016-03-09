using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NotQuery>))]
	[Obsolete("Use the bool query with must_not clause instead")]
	public interface INotQuery : IQuery
	{
		[JsonProperty("filters")]
		IEnumerable<QueryContainer> Filters { get; set; }
	}

	[Obsolete("Use the bool query with must_not clause instead")]
	public class NotQuery : QueryBase, INotQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IEnumerable<QueryContainer> Filters { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Not = this;
		internal static bool IsConditionless(INotQuery q)
		{
			return !q.Filters.HasAny() || q.Filters.All(f => f.IsConditionless);
		}
	}

	[Obsolete("Use the bool query with must_not clause instead")]
	public class NotQueryDescriptor<T>
		: QueryDescriptorBase<NotQueryDescriptor<T>, INotQuery>
		, INotQuery where T : class
	{
		protected override bool Conditionless => NotQuery.IsConditionless(this);
		IEnumerable<QueryContainer> INotQuery.Filters { get; set; }

		public NotQueryDescriptor<T> Filters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).Where(q => q != null).ToListOrNullIfEmpty());

		public NotQueryDescriptor<T> Filters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).Where(q => q != null).ToListOrNullIfEmpty());

		public NotQueryDescriptor<T> Filters(params QueryContainer[] queries) => Assign(a => a.Filters = queries.Where(q => q != null).ToListOrNullIfEmpty());

	}
}
