// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
