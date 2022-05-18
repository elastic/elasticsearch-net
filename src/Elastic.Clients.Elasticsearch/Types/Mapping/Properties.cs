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

public partial class Properties<T> : Properties
{
	public void Add<TValue>(Expression<Func<T, TValue>> name, IProperty property) => BackingDictionary.Add(name, property);
}


// TODO - BUG
// BuildableDescriptor is not marked correctly higher up the chain (or we fail to generate expected descriptor), e.g. BooleanPropertyDescriptor and NumericFielddata

// TODO
// Generate after Buildable implementation
public partial class PropertiesDescriptor<TDocument>
		: IsADictionaryDescriptor<PropertiesDescriptor<TDocument>, Properties, PropertyName, IProperty>
{
	public PropertiesDescriptor() : base(new Properties<TDocument>()) { }

	public PropertiesDescriptor(Properties properties) : base(properties ?? new Properties<TDocument>()) { }

	// Do we continue to special case properties to support the fluent Name on the property descriptor? If so, we generate this
	//public PropertiesDescriptor<T> Boolean(Action<BooleanPropertyDescriptor<T>> selector) => SetVariant<BooleanPropertyDescriptor<T>, BooleanProperty>(selector);

	public PropertiesDescriptor<TDocument> Boolean(PropertyName fieldName) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, null);

	public PropertiesDescriptor<TDocument> Boolean(PropertyName fieldName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, configure);

	public PropertiesDescriptor<TDocument> Boolean(Expression<Func<TDocument, object>> fieldName) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, null);

	public PropertiesDescriptor<TDocument> Boolean(Expression<Func<TDocument, object>> fieldName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, configure);

	public PropertiesDescriptor<TDocument> Boolean<TValue>(Expression<Func<TDocument, TValue>> fieldName) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, null);

	public PropertiesDescriptor<TDocument> Boolean<TValue>(Expression<Func<TDocument, TValue>> fieldName, Action<BooleanPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<BooleanPropertyDescriptor<TDocument>, BooleanProperty>(fieldName, configure);

	// These might be manually added to a partial class which seems reasonable.

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> fieldName) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(fieldName, null);

	public PropertiesDescriptor<TDocument> Scalar(Expression<Func<TDocument, int>> fieldName, Action<IntegerNumberPropertyDescriptor<TDocument>> configure) =>
		AssignVariant<IntegerNumberPropertyDescriptor<TDocument>, IntegerNumberProperty>(fieldName, configure);

	//private PropertiesDescriptor<TDocument> SetVariant<TDescriptor, TProperty>(Action<TDescriptor> selector)
	//		where TDescriptor : Descriptor, IBuildableDescriptor<TProperty>, IPropertyDescriptor, new()
	//		where TProperty : IProperty
	//{
	//	var descriptor = new TDescriptor();
	//	selector?.Invoke(descriptor);
	//	return SetVariant(descriptor.Name, descriptor.Build());
	//}

	//private PropertiesDescriptor<TDocument> SetVariant<TDescriptor, TProperty>(PropertyName name, Action<TDescriptor> selector)
	//		where TDescriptor : Descriptor, IBuildableDescriptor<TProperty>, new()
	//		where TProperty : IProperty
	//{
	//	var descriptor = new TDescriptor();
	//	selector?.Invoke(descriptor);
	//	return SetVariant(name, descriptor.Build());
	//}

	//private PropertiesDescriptor<TDocument> SetVariant(PropertyName name, IProperty type)
	//{
	//	type.ThrowIfNull(nameof(type));

	//	if (name.IsConditionless())
	//		throw new ArgumentException($"Could not get property name for {type.GetType().Name} mapping.");

	//	return Assign(name, type);
	//}

	protected override PropertiesDescriptor<TDocument> AssignVariant(PropertyName name, IProperty type)
	{
		type.ThrowIfNull(nameof(type));

		if (name.IsConditionless())
			throw new ArgumentException($"Could not get property name for {type.GetType().Name} mapping.");

		return Assign(name, type);
	}
}

// TODO
// After we are generating the container descriptor e.g. PropertiesDescriptor
// Code generator should generate these for any InternallyTaggedUnions that are IsADictionary types
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

internal static class PropertyNameExtensions
{
	internal static bool IsConditionless(this PropertyName property) =>
		property is null || property.Name.IsNullOrEmpty() && property.Expression is null && property.Property is null;
}
