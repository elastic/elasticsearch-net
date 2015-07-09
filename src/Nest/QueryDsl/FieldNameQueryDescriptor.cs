using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public abstract class FieldNameQueryDescriptor<TDescriptor, TInterface, T> 
		: QueryDescriptorBase<TDescriptor, TInterface>, IFieldNameQuery
		where TDescriptor : FieldNameQueryDescriptor<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IFieldNameQuery
		where T : class
	{
		FieldName IFieldNameQuery.Field { get; set; }

		public TDescriptor OnField(string field) => Assign(a => a.Field = field);

		public TDescriptor OnField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
