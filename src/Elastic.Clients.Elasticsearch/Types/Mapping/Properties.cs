// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.Mapping;

public partial class Properties
{
	// TODO - THE ADD METHOD IN THE GENERATED VERSION OF THIS SHOULD TAKE PROPERTYNAME

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
// Code generator should generate these for any InternallyTaggedUnions that are IsADictionary types.
// These work on generic and non-generic descriptors.
//public partial class TypeMappingDescriptor
//{
//	public TypeMappingDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
//	{
//		PropertiesValue = descriptor.PromisedValue;
//		return Self;
//	}

//	public TypeMappingDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
//	{
//		var descriptor = new PropertiesDescriptor<TDocument>();
//		configure?.Invoke(descriptor);
//		PropertiesValue = descriptor.PromisedValue;
//		return Self;
//	}
//}

//public partial class TypeMappingDescriptor<TDocument>
//{
//	public TypeMappingDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
//	{
//		PropertiesValue = descriptor.PromisedValue;
//		return Self;
//	}

//	public TypeMappingDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
//	{
//		var descriptor = new PropertiesDescriptor<TDocument>();
//		configure?.Invoke(descriptor);
//		PropertiesValue = descriptor.PromisedValue;
//		return Self;
//	}
//}
