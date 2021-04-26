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

namespace Nest
{
	public abstract class FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>
		: QueryDescriptorBase<TDescriptor, TInterface>, IFieldNameQuery
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IFieldNameQuery
		where T : class
	{
		Field IFieldNameQuery.Field { get; set; }

		bool IQuery.IsStrict { get; set; }

		bool IQuery.IsVerbatim { get; set; }

		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
