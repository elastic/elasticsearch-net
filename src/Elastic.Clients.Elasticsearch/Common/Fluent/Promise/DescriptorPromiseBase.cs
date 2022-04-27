// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;

namespace Elastic.Clients.Elasticsearch
{
	public interface IPromise<out TValue> where TValue : class
	{
		TValue Value { get; }
	}

	public abstract class DescriptorPromiseBase<TDescriptor, TValue> : Descriptor, IPromise<TValue>
		where TDescriptor : DescriptorPromiseBase<TDescriptor, TValue>
		where TValue : class
	{
		internal readonly TValue PromisedValue;

		internal DescriptorPromiseBase(TValue instance)
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
}
