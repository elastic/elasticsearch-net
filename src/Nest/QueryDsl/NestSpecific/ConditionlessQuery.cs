/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		public ConditionlessQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public ConditionlessQueryDescriptor<T> Fallback(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(querySelector, (a, v) => a.Fallback = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
