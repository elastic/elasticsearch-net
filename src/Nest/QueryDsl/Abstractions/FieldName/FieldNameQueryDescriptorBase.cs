using System;
using System.Linq.Expressions;

namespace Nest_5_2_0
{
	public abstract class FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>
		: QueryDescriptorBase<TDescriptor, TInterface>, IFieldNameQuery
		where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IFieldNameQuery
		where T : class
	{
		Field IFieldNameQuery.Field { get; set; }

		bool IQuery.IsVerbatim { get; set; }

		bool IQuery.IsStrict { get; set; }

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
