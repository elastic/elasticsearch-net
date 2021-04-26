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

namespace Nest
{
	public class FieldsDescriptor<T> : DescriptorPromiseBase<FieldsDescriptor<T>, Fields>
		where T : class
	{
		public FieldsDescriptor() : base(new Fields()) { }

		public FieldsDescriptor<T> Fields(params Expression<Func<T, object>>[] fields) => Assign(fields, (f, v) => f.And(v));

		public FieldsDescriptor<T> Fields(params string[] fields) => Assign(fields,(f, v) => f.And(v));

		public FieldsDescriptor<T> Fields(IEnumerable<Field> fields) => Assign(fields, (f, v) => f.ListOfFields.AddRange(v));

		public FieldsDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, double? boost = null, string format = null) =>
			Assign(new Field(field, boost, format), (f, v) => f.And(v));

		public FieldsDescriptor<T> Field(string field, double? boost = null, string format = null) =>
			Assign(new Field(field, boost, format), (f, v) => f.And(v));

		public FieldsDescriptor<T> Field(Field field) => Assign(field, (f, v) => f.And(v));
	}
}
