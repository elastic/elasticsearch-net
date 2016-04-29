using System;
using System.Linq.Expressions;

namespace Nest
{
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IProperty
		where T : class
	{
		PropertyName IProperty.Name { get; set; }
		TypeName IProperty.Type { get; set; }
		string IProperty.IndexName { get; set; }

		protected PropertyDescriptorBase(string type) { Self.Type = type; }

		public TDescriptor Name(PropertyName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		public TDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);
	}
}