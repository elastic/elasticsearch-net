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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SpanFieldMaskingQuery))]
	public interface ISpanFieldMaskingQuery : ISpanSubQuery
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "query")]
		ISpanQuery Query { get; set; }
	}

	public class SpanFieldMaskingQuery : QueryBase, ISpanFieldMaskingQuery
	{
		public Field Field { get; set; }
		public ISpanQuery Query { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SpanFieldMasking = this;

		internal static bool IsConditionless(ISpanFieldMaskingQuery q) =>
			q.Field.IsConditionless() || q.Query == null || q.Query.Conditionless;
	}

	public class SpanFieldMaskingQueryDescriptor<T>
		: QueryDescriptorBase<SpanFieldMaskingQueryDescriptor<T>, ISpanFieldMaskingQuery>
			, ISpanFieldMaskingQuery where T : class
	{
		protected override bool Conditionless => SpanFieldMaskingQuery.IsConditionless(this);
		Field ISpanFieldMaskingQuery.Field { get; set; }
		ISpanQuery ISpanFieldMaskingQuery.Query { get; set; }

		public SpanFieldMaskingQueryDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SpanFieldMaskingQueryDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public SpanFieldMaskingQueryDescriptor<T> Query(Func<SpanQueryDescriptor<T>, ISpanQuery> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new SpanQueryDescriptor<T>()));
	}
}
