using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	/// <inheritdoc cref="IProperty" />
	public abstract class PropertyDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, IProperty
		where TDescriptor : PropertyDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, IProperty
		where T : class
	{
		private string _type;

		protected PropertyDescriptorBase(FieldType type) => Self.Type = type.GetStringValue();

		protected string DebugDisplay => $"Type: {Self.Type ?? "<empty>"}, Name: {Self.Name.DebugDisplay} ";

		protected string TypeOverride
		{
			set => _type = value;
		}

		IDictionary<string, object> IProperty.LocalMetadata { get; set; }
		PropertyName IProperty.Name { get; set; }

		string IProperty.Type
		{
			get => _type;
			set => _type = value;
		}

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name(PropertyName name) => Assign(a => a.Name = name);

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name(Expression<Func<T, object>> objectPath) => Assign(a => a.Name = objectPath);

		/// <inheritdoc cref="IProperty.LocalMetadata" />
		public TDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.LocalMetadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
