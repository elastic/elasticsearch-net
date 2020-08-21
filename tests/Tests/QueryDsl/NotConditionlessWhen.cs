// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl
{
	public abstract class NotConditionlessWhen : List<Action<QueryContainer>> { }

	public class NotConditionlessWhen<TQuery> : NotConditionlessWhen where TQuery : IQuery
	{
		private readonly Func<IQueryContainer, TQuery> _dispatch;

		public NotConditionlessWhen(Func<IQueryContainer, TQuery> dispatch) => _dispatch = dispatch;

		public void Add(Action<TQuery> when) => Add(q => Assert(q, when));

		private void Assert(IQueryContainer c, Action<TQuery> when)
		{
			var q = _dispatch(c);
			when(q);
			q.Conditionless.Should().BeFalse();
			c.IsConditionless.Should().BeFalse();
		}
	}
}
