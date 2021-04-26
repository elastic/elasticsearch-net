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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanOrQuery))]
	public interface ISpanOrQuery : ISpanSubQuery
	{
		[DataMember(Name ="clauses")]
		IEnumerable<ISpanQuery> Clauses { get; set; }
	}

	public class SpanOrQuery : QueryBase, ISpanOrQuery
	{
		public IEnumerable<ISpanQuery> Clauses { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanOr = this;

		internal static bool IsConditionless(ISpanOrQuery q) => !q.Clauses.HasAny() || q.Clauses.Cast<IQuery>().All(qq => qq.Conditionless);
	}

	public class SpanOrQueryDescriptor<T>
		: QueryDescriptorBase<SpanOrQueryDescriptor<T>, ISpanOrQuery>
			, ISpanOrQuery where T : class
	{
		protected override bool Conditionless => SpanOrQuery.IsConditionless(this);
		IEnumerable<ISpanQuery> ISpanOrQuery.Clauses { get; set; }

		public SpanOrQueryDescriptor<T> Clauses(params Func<SpanQueryDescriptor<T>, SpanQueryDescriptor<T>>[] selectors) => Assign(selectors, (a, v) =>
		{
			var clauses = (
				from selector in v
				select selector(new SpanQueryDescriptor<T>())
				into q
				where !(q as IQuery).Conditionless
				select q
			).ToList();
			a.Clauses = clauses.HasAny() ? clauses : null;
		});
	}
}
