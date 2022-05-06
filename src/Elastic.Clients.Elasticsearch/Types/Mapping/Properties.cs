// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.Mapping;

public partial class Properties
{
	public void Add<T>(Expression<Func<T, object>> name, IProperty property) => BackingDictionary.Add(name, property);

	public bool TryGetProperty<T>(PropertyName propertyName, out T property) where T : IProperty
	{
		if (BackingDictionary.TryGetValue(propertyName, out var matchedProperty) && matchedProperty is T finalProperty)
		{
			property = finalProperty;
			return true;
		}

		property = default;
		return false;
	}
}

public partial class BooleanPropertyDescriptor<TDocument> : IBuildable<BooleanProperty>
{
	BooleanProperty IBuildable<BooleanProperty>.Build() =>
		new()
		{
			Boost = BoostValue,
			CopyTo = CopyToValue,
			DocValues = DocValuesValue,
			Name = NameValue,
			NullValue = NullValueValue,
			Store = StoreValue,
		};
}

internal interface IBuildable<T>
{
	T Build();
}

public partial class Properties<T> : Properties
{
	public void Add<TValue>(Expression<Func<T, TValue>> name, IProperty property) => BackingDictionary.Add(name, property);
}

public partial class PropertiesDescriptor<T>
		: IsADictionaryDescriptorBase<PropertiesDescriptor<T>, Properties, PropertyName, IProperty>
{
	public PropertiesDescriptor() : base(new Properties<T>()) { }

	public PropertiesDescriptor(Properties properties) : base(properties ?? new Properties<T>()) { }

	public PropertiesDescriptor<T> Boolean(Action<BooleanPropertyDescriptor<T>> selector) => SetProperty<BooleanPropertyDescriptor<T>, BooleanProperty>(selector);

	private PropertiesDescriptor<T> SetProperty<TDescriptor, TProperty>(Action<TDescriptor> selector)
			where TDescriptor : Descriptor, IBuildable<TProperty>, new()
			where TProperty : IProperty
	{
		selector.ThrowIfNull(nameof(selector));

		var descriptor = new TDescriptor();
		selector(descriptor);

		var property = descriptor.Build();

		return SetProperty(property);
	}

	private PropertiesDescriptor<T> SetProperty(IProperty type)
	{
		type.ThrowIfNull(nameof(type));

		var typeName = type.GetType().Name;

		if (type.Name.IsConditionless())
			throw new ArgumentException($"Could not get property name for {typeName} mapping.");

		return Assign(type, (a, v) => a[v.Name] = v);
	}
}

public partial class TypeMappingDescriptor
{
	public TypeMappingDescriptor Properties<T>(Action<PropertiesDescriptor<T>>? properties)
	{
		var descriptor = new PropertiesDescriptor<T>();
		properties?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}
}

public partial interface IProperty
{
	public PropertyName Name { get; }
}

internal static class PropertyNameExtensions
{
	internal static bool IsConditionless(this PropertyName property) =>
		property == null || property.Name.IsNullOrEmpty() && property.Expression == null && property.Property == null;
}
