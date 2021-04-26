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
	[ReadAs(typeof(ExistsQuery))]
	public interface IExistsQuery : IQuery
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }
	}

	public class ExistsQuery : QueryBase, IExistsQuery
	{
		public Field Field { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Exists = this;

		internal static bool IsConditionless(IExistsQuery q) => q.Field.IsConditionless();
	}

	public class ExistsQueryDescriptor<T>
		: QueryDescriptorBase<ExistsQueryDescriptor<T>, IExistsQuery>
			, IExistsQuery where T : class
	{
		protected override bool Conditionless => ExistsQuery.IsConditionless(this);

		Field IExistsQuery.Field { get; set; }

		public ExistsQueryDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public ExistsQueryDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
