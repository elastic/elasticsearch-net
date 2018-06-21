using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl
{
	public abstract class ConditionlessWhen : List<Action<QueryContainer>> { }
	public class ConditionlessWhen<TQuery> : ConditionlessWhen where TQuery : IQuery
	{
		private readonly Func<IQueryContainer, TQuery> _dispatch;

		public ConditionlessWhen(Func<IQueryContainer, TQuery> dispatch)
		{
			_dispatch = dispatch;
		}

		public void Add(Action<TQuery> when) => this.Add(q => Assert(q, when));

		private void Assert(IQueryContainer c, Action<TQuery> when)
		{
			var q = this._dispatch(c);
			q.Conditionless.Should().BeFalse();
			c.IsConditionless.Should().BeFalse();
			when(q);
			q.Conditionless.Should().BeTrue();
			c.IsConditionless.Should().BeTrue();
		}
	}
}
