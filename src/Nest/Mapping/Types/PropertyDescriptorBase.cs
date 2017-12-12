using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IProperty
		where T : class
	{
		PropertyName IProperty.Name { get; set; }
		private string _type;
		protected string TypeOverride { set => _type = value; }
		string IProperty.Type { get => _type; set => _type = value; }
		IDictionary<string, object> IProperty.LocalMetadata { get; set; }

		protected string DebugDisplay => $"Type: {Self.Type ?? "<empty>"}, Name: {Self.Name.DebugDisplay} ";

		protected PropertyDescriptorBase(FieldType type) { Self.Type = type.GetStringValue(); }

		public TDescriptor Name(PropertyName name) => Assign(a => a.Name = name);

		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		/// <summary>
		/// Local property metadata that will NOT be stored in Elasticsearch with the mappings
		/// </summary>
		public TDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.LocalMetadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
