using System;

namespace Nest
{
	public interface IConditionlessQuery : IQuery
	{
		QueryContainer Fallback { get; set; }
		QueryContainer Query { get; set; }
	}

	public class ConditionlessQueryDescriptor<T>
		: QueryDescriptorBase<ConditionlessQueryDescriptor<T>, IConditionlessQuery>
			, IConditionlessQuery where T : class
	{
		protected override bool Conditionless => (Self.Query == null || Self.Query.IsConditionless)
			&& (Self.Fallback == null || Self.Fallback.IsConditionless);

		QueryContainer IConditionlessQuery.Fallback { get; set; }
		QueryContainer IConditionlessQuery.Query { get; set; }

		public ConditionlessQueryDescriptor<T> Fallback(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Fallback = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		public ConditionlessQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
