// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.Mapping;

public partial class Properties
{
	// TODO - Generate this
	public void Add<T>(Expression<Func<T, object>> propertyName, IProperty property) => BackingDictionary.Add(propertyName, property);

	// TODO - Generate this
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

// TODO - Generate this?
public partial class Properties<TDocument> : Properties
{
	public void Add<TValue>(Expression<Func<TDocument, TValue>> name, IProperty property) => BackingDictionary.Add(name, property);
}

// TODO
// Generate after Buildable implementation
public sealed partial class PropertiesDescriptor<TDocument>
		: IsADictionaryDescriptor<PropertiesDescriptor<TDocument>, Properties, PropertyName, IProperty>
{
	public PropertiesDescriptor<TDocument> Boolean(PropertyName propertyName, BooleanProperty property) =>
		AssignVariant(propertyName, property);

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

	// This will remain non-code-generated
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
	public TypeMappingDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>>? properties)
	{
		var descriptor = new PropertiesDescriptor<TDocument>();
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
