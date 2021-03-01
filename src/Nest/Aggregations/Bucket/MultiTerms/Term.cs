// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(Term))]
	public interface ITerm
	{
		Field Field { get; set; }
		object Missing { get; set; }
	}

	public class Term : ITerm
	{
		[DataMember(Name = "field")]
		public Field Field { get; set; }
		[DataMember(Name = "missing")]
		public object Missing { get; set; }
	}

	public class TermDescriptor<T> : DescriptorBase<TermDescriptor<T>, ITerm>, ITerm where T : class
	{
		Field ITerm.Field { get; set; }
		object ITerm.Missing { get; set; }

		public TermDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);
		public TermDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);
		public TermDescriptor<T> Missing(object missing) => Assign(missing, (a, v) => a.Missing = v);
	}
}
