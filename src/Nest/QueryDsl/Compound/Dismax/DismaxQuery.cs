using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DisMaxQuery>))]
	public interface IDisMaxQuery : IQuery
	{
		[JsonProperty("queries")]
		IEnumerable<QueryContainer> Queries { get; set; }

		[JsonProperty("tie_breaker")]
		double? TieBreaker { get; set; }
	}

	public class DisMaxQuery : QueryBase, IDisMaxQuery
	{
		public IEnumerable<QueryContainer> Queries { get; set; }
		public double? TieBreaker { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.DisMax = this;

		internal static bool IsConditionless(IDisMaxQuery q) => q.Queries.NotWritable();
	}

	public class DisMaxQueryDescriptor<T>
		: QueryDescriptorBase<DisMaxQueryDescriptor<T>, IDisMaxQuery>
			, IDisMaxQuery where T : class
	{
		protected override bool Conditionless => DisMaxQuery.IsConditionless(this);
		IEnumerable<QueryContainer> IDisMaxQuery.Queries { get; set; }
		double? IDisMaxQuery.TieBreaker { get; set; }

		public DisMaxQueryDescriptor<T> Queries(params Func<QueryContainerDescriptor<T>, QueryContainer>[] querySelectors) =>
			Assign(querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>()))
				.Where(q => q != null)
				.ToListOrNullIfEmpty(), (a, v) => a.Queries = v);

		public DisMaxQueryDescriptor<T> Queries(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> querySelectors) =>
			Assign(querySelectors.Select(q => q?.Invoke(new QueryContainerDescriptor<T>()))
				.Where(q => q != null)
				.ToListOrNullIfEmpty(), (a, v) => a.Queries = v);

		public DisMaxQueryDescriptor<T> Queries(params QueryContainer[] queries) =>
			Assign(queries.Where(q => q != null).ToListOrNullIfEmpty(), (a, v) => a.Queries = v);

		public DisMaxQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(tieBreaker, (a, v) => a.TieBreaker = v);
	}
}
