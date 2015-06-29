
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IConditionlessQuery : IQuery
	{
		QueryContainer Query { get; set; }

		QueryContainer Fallback { get; set; }

	}

	public class ConditionlessQueryDescriptor<T> : IConditionlessQuery where T : class
	{
		private IConditionlessQuery Self => this;

		QueryContainer IConditionlessQuery.Query { get; set; }

		QueryContainer IConditionlessQuery.Fallback { get; set; }

		bool IQuery.Conditionless => (Self.Query == null || Self.Query.IsConditionless)
										&& (Self.Fallback == null || Self.Fallback.IsConditionless);

		string IQuery.Name { get; set; }

		public ConditionlessQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Query = querySelector(new QueryDescriptor<T>());
			return this;
		}

		public ConditionlessQueryDescriptor<T> Fallback(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			Self.Fallback = querySelector(new QueryDescriptor<T>());
			return this;
		}
	}
}
