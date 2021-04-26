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
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	public class SortDescriptor<T> : DescriptorPromiseBase<SortDescriptor<T>, IList<ISort>>
		where T : class
	{
		public SortDescriptor() : base(new List<ISort>()) { }

		public SortDescriptor<T> Ascending<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Descending }));

		public SortDescriptor<T> Ascending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending(Field field) => Assign(field, (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Descending }));

		public SortDescriptor<T> Ascending(SortSpecialField field) =>
			Assign(field.GetStringValue(), (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Ascending }));

		public SortDescriptor<T> Descending(SortSpecialField field) =>
			Assign(field.GetStringValue(), (a, v) => a.Add(new FieldSort { Field = v, Order = SortOrder.Descending }));

		public SortDescriptor<T> Field(Func<FieldSortDescriptor<T>, IFieldSort> sortSelector) =>
			AddSort(sortSelector?.Invoke(new FieldSortDescriptor<T>()));

		public SortDescriptor<T> Field(Field field, SortOrder order) => AddSort(new FieldSort { Field = field, Order = order });

		public SortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, SortOrder order) =>
			AddSort(new FieldSort { Field = field, Order = order });

		public SortDescriptor<T> GeoDistance(Func<GeoDistanceSortDescriptor<T>, IGeoDistanceSort> sortSelector) =>
			AddSort(sortSelector?.Invoke(new GeoDistanceSortDescriptor<T>()));

		public SortDescriptor<T> Script(Func<ScriptSortDescriptor<T>, IScriptSort> sortSelector) =>
			AddSort(sortSelector?.Invoke(new ScriptSortDescriptor<T>()));

		private SortDescriptor<T> AddSort(ISort sort) => sort == null ? this : Assign(sort, (a, v) => a.Add(v));
	}
}
