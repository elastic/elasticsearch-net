// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
		IDictionary<string, string> IProperty.Meta { get; set; }

		string IProperty.Type
		{
			get => _type;
			set => _type = value;
		}

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name(PropertyName name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IProperty.Name" />
		public TDescriptor Name<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Name = v);

		/// <inheritdoc cref="IProperty.LocalMetadata" />
		public TDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.LocalMetadata = v?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IProperty.Meta" />
		public TDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, string>()));
	}
}
