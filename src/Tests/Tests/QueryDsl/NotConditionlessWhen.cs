using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.QueryDsl
{
	public abstract class NotConditionlessWhen : List<Action<QueryContainer>>
	{
	}
	public class NotConditionlessWhen<TQuery> : NotConditionlessWhen where TQuery : IQuery
	{
		private readonly Func<IQueryContainer, TQuery> _dispatch;

		public NotConditionlessWhen(Func<IQueryContainer, TQuery> dispatch)
		{
			_dispatch = dispatch;
		}

		public void Add(Action<TQuery> when)
		{
			this.Add(q => Assert(q, when));
		}

		private void Assert(IQueryContainer c, Action<TQuery> when)
		{
			var q = this._dispatch(c);
			when(q);
			q.Conditionless.Should().BeFalse();
			c.IsConditionless.Should().BeFalse();
		}
	}
}
