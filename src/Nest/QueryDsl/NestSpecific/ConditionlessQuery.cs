// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

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

		public ConditionlessQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public ConditionlessQueryDescriptor<T> Fallback(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Fallback = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
