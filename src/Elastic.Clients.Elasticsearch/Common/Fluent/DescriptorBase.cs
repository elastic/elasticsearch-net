// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Elastic.Clients.Elasticsearch;

public interface IDescriptor { }

public abstract class OldDescriptorBase<TDescriptor, TInterface> : IDescriptor
	where TDescriptor : OldDescriptorBase<TDescriptor, TInterface>, TInterface
	where TInterface : class
{
	private readonly TDescriptor _self;

	protected OldDescriptorBase() => _self = (TDescriptor)this;

	[IgnoreDataMember]
	protected TInterface Self => _self;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TDescriptor Assign<TValue>(TValue value, Action<TInterface, TValue> assigner) => Fluent.Assign(_self, value, assigner);

	/// <summary>
	/// Hides the <see cref="Equals" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectEqualsIsObjectEquals
	//only used to hide by default
	public override bool Equals(object obj) => base.Equals(obj);

	/// <summary>
	/// Hides the <see cref="GetHashCode" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
	//only used to hide by default
	public override int GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Hides the <see cref="ToString" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => base.ToString();
}

public abstract class DescriptorBase<TDescriptor> : IDescriptor
	where TDescriptor : DescriptorBase<TDescriptor>
{
	private readonly TDescriptor _self;

	protected DescriptorBase() => _self = (TDescriptor)this;

	[IgnoreDataMember]
	protected TDescriptor Self => _self;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_self, value, assign);

	/// <summary>
	/// Hides the <see cref="Equals" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectEqualsIsObjectEquals
	//only used to hide by default
	public override bool Equals(object obj) => base.Equals(obj);

	/// <summary>
	/// Hides the <see cref="GetHashCode" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
	//only used to hide by default
	public override int GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Hides the <see cref="ToString" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => base.ToString();
}
