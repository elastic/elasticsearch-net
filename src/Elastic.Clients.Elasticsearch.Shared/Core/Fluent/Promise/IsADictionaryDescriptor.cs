// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Fluent;

public abstract class IsADictionaryDescriptor<TDescriptor, TPromised, TKey, TValue>
	: PromiseDescriptor<TDescriptor, TPromised>
	where TDescriptor : IsADictionaryDescriptor<TDescriptor, TPromised, TKey, TValue>
	where TPromised : class, IIsADictionary<TKey, TValue>
{
	// This internal ctor ensures that only types defined within the Elastic.Clients.Elasticsearch assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal IsADictionaryDescriptor(TPromised instance) : base(instance) { }

	protected TDescriptor Assign(TKey key, TValue value)
	{
		PromisedValue.Add(key, value);
		return Self;
	}

	private protected TDescriptor AssignVariant<TVariantDescriptor, TProperty>(TKey name, Action<TVariantDescriptor> selector)
			where TVariantDescriptor : Descriptor, IBuildableDescriptor<TProperty>, new()
			where TProperty : TValue
	{
		var descriptor = new TVariantDescriptor();
		selector?.Invoke(descriptor);
		return AssignVariant(name, descriptor.Build());
	}

	/// <summary>
	/// May be overridden by derived types to add their own validation before assigning the variant.
	/// e.g. <see cref="Mapping.PropertiesDescriptor{TDocument}"/>.
	/// </summary>
	protected virtual TDescriptor AssignVariant(TKey name, TValue type)
	{
		type.ThrowIfNull(nameof(type));
		return Assign(name, type);
	}
}
