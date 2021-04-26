/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.ComponentModel;

namespace Nest
{
	public interface IPromise<out TValue> where TValue : class
	{
		TValue Value { get; }
	}

	public abstract class DescriptorPromiseBase<TDescriptor, TValue> : IDescriptor, IPromise<TValue>
		where TDescriptor : DescriptorPromiseBase<TDescriptor, TValue>
		where TValue : class
	{
		internal readonly TValue PromisedValue;

		protected DescriptorPromiseBase(TValue instance)
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

		/// <summary>
		/// Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable BaseObjectEqualsIsObjectEquals
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
		public override int GetHashCode() => base.GetHashCode();
		// ReSharper restore BaseObjectEqualsIsObjectEquals

		/// <summary>
		/// Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}
