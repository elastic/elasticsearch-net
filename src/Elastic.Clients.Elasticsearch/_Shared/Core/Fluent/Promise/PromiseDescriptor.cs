// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Fluent;

public abstract class PromiseDescriptor<TDescriptor, TValue> : Descriptor, IPromise<TValue>
	where TDescriptor : PromiseDescriptor<TDescriptor, TValue>
	where TValue : class
{
	internal readonly TValue PromisedValue;

	// This internal ctor ensures that only types defined within the Elastic.Clients.Elasticsearch assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal PromiseDescriptor(TValue instance) : base()
	{
		PromisedValue = instance;
		Self = (TDescriptor)this;
	}

	TValue IPromise<TValue>.Value => PromisedValue;

	protected TDescriptor Self { get; }

	protected TDescriptor Assign(Action<TValue> assigner)
	{
		assigner(PromisedValue);
		return Self;
	}

	protected TDescriptor Assign<TNewValue>(TNewValue value, Action<TValue, TNewValue> assigner)
	{
		assigner(PromisedValue, value);
		return Self;
	}

	protected TDescriptor Assign<TNewValue>(TNewValue value, Action<TValue, TNewValue> assigner, Action<TDescriptor> descriptorAction)
	{
		assigner(PromisedValue, value);
		descriptorAction(Self);
		return Self;
	}
}
