using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Core;

namespace Nest
{
	public class FieldsDescriptor<T> : DescriptorPromiseBase<FieldsDescriptor<T>, Fields>
		where T : class
	{
		public FieldsDescriptor() : base(new Fields()) { }

		public FieldsDescriptor<T> Fields(params Expression<Func<T, object>>[] fields) =>
			Assign(fields, (f, v) => f.And(v));

		public FieldsDescriptor<T> Fields(params string[] fields) => Assign(fields, (f, v) => f.And(v));

		public FieldsDescriptor<T> Fields(IEnumerable<Field> fields) =>
			Assign(fields, (f, v) => f.ListOfFields.AddRange(v));

		public FieldsDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, double? boost = null,
			string format = null) =>
			Assign(new Field(field, boost, format), (f, v) => f.And(v));

		public FieldsDescriptor<T> Field(string field, double? boost = null, string format = null) =>
			Assign(new Field(field, boost, format), (f, v) => f.And(v));

		public FieldsDescriptor<T> Field(Field field) => Assign(field, (f, v) => f.And(v));
	}
}
