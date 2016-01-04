using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class FieldsDescriptor<T> : DescriptorPromiseBase<FieldsDescriptor<T>, Fields> 
		where T : class
	{
		public FieldsDescriptor() : base(new Fields()) { }

		public FieldsDescriptor<T> Fields(params Expression<Func<T, object>>[] fields) => Assign(f => f.And(fields));
		public FieldsDescriptor<T> Fields(params string[] fields) => Assign(f => f.And(fields));
		public FieldsDescriptor<T> Fields(IEnumerable<Field> fields) => Assign(f => f.ListOfFields.AddRange(fields));

		public FieldsDescriptor<T> Field(Expression<Func<T, object>> field, double? boost = null) => Assign(f => f.And(field, boost));
		public FieldsDescriptor<T> Field(string field, double? boost = null) => Assign(f => f.And(field, boost));
	}
}