using System;
using System.Collections.Generic;
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
		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

		protected PropertyDescriptorBase(string type) { Self.Type = type; }

		public TDescriptor Name(PropertyName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		/// <summary>
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
		/// </summary>
		public TDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.LocalMetadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
