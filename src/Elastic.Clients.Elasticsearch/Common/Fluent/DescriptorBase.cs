// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public abstract class Descriptor
{
	// This internal ctor ensures that only types defined within the client assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal Descriptor() { }

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
	// only used to hide by default
	public override int GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Hides the <see cref="ToString" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => base.ToString();
}

public abstract class DescriptorBase<TDescriptor> : Descriptor
	where TDescriptor : DescriptorBase<TDescriptor>
{
	private readonly TDescriptor _self;

	internal DescriptorBase() : base() => _self = (TDescriptor)this;

	[JsonIgnore]
	[IgnoreDataMember]
	protected TDescriptor Self => _self;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected TDescriptor Assign<TValue>(TValue value, Action<TDescriptor, TValue> assign) => Fluent.Assign(_self, value, assign);
}

public abstract class SerializableDescriptorBase<TDescriptor> : DescriptorBase<TDescriptor>, ISelfSerializable
	where TDescriptor : SerializableDescriptorBase<TDescriptor>
{
	internal SerializableDescriptorBase(): base() { }

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => Serialize(writer, options, settings);

	protected abstract void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);
}
