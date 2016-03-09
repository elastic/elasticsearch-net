using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<OrQuery>))]
	[Obsolete("Use the bool query instead with a should clause for the query")]
	public interface IOrQuery : IQuery
	{
		[JsonProperty("filters")]
		IEnumerable<QueryContainer> Filters { get; set; }
	}

	[Obsolete("Use the bool query instead with a should clause for the query")]
	public class OrQuery : QueryBase, IOrQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IEnumerable<QueryContainer> Filters { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Or = this;
		internal static bool IsConditionless(IOrQuery q)
		{
			return !q.Filters.HasAny() || q.Filters.All(f => f.IsConditionless);
		}
	}

	[Obsolete("Use the bool query instead with a should clause for the query")]
	public class OrQueryDescriptor<T>
		: QueryDescriptorBase<OrQueryDescriptor<T>, IOrQuery>
		, IOrQuery where T : class
	{
		protected override bool Conditionless => OrQuery.IsConditionless(this);
		IEnumerable<QueryContainer> IOrQuery.Filters { get; set; }

		public OrQueryDescriptor<T> Filters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).Where(q => q != null).ToListOrNullIfEmpty());

		public OrQueryDescriptor<T> Filters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> querySelectors) =>
			Assign(a => a.Filters = querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).Where(q => q != null).ToListOrNullIfEmpty());

		public OrQueryDescriptor<T> Filters(params QueryContainer[] queries) => Assign(a => a.Filters = queries.Where(q => q != null).ToListOrNullIfEmpty());

	}
}
