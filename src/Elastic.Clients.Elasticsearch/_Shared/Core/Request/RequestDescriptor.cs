// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Text.Json;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Requests;

/// <summary>
/// Base class for all request descriptor types.
/// </summary>
public abstract partial class RequestDescriptor<TDescriptor, TParameters> :
	Request<TParameters>, ISelfSerializable
	where TDescriptor : RequestDescriptor<TDescriptor, TParameters>
	where TParameters : RequestParameters, new()
{
	private readonly TDescriptor _descriptor;

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => Serialize(writer, options, settings);

	// This internal ctor ensures that only types defined within the Elastic.Clients.Elasticsearch assembly can derive from this base class.
	// We don't expect consumers to derive from this public base class.
	internal RequestDescriptor() => _descriptor = (TDescriptor)this;

	protected abstract void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

	internal RequestDescriptor(Func<RouteValues, RouteValues> pathSelector) : base(pathSelector) =>
		_descriptor = (TDescriptor)this;

	protected TDescriptor Self => _descriptor;

	protected TDescriptor Qs(string name, object? value)
	{
		Q(name, value);
		return _descriptor;
	}

	protected TDescriptor Qs(string name, IStringable value)
	{
		Q(name, value.GetString());
		return _descriptor;
	}

	/// <summary>
	///     Specify settings for this request alone, handy if you need a custom timeout or want to bypass sniffing, retries
	/// </summary>
	public TDescriptor RequestConfiguration(Func<RequestConfigurationDescriptor, IRequestConfiguration> configurationSelector)
	{
		RequestConfig = configurationSelector?.Invoke(RequestConfig is null
			? new RequestConfigurationDescriptor()
			: new RequestConfigurationDescriptor(RequestConfig)) ?? RequestConfig;
		return _descriptor;
	}

	/// <summary>
	/// A list of filters used to reduce the response.
	/// <para>
	///     Use of response filtering can result in a response from Elasticsearch
	///     that cannot be correctly deserialized to the respective response type for the request.
	///     In such situations, use the low level client to issue the request and handle response deserialization.
	/// </para>
	/// </summary>
	public TDescriptor FilterPath(params string[] value) => Qs("filter_path", value);

	///<summary>Return human-readable values for statistics.</summary>
	public TDescriptor Human(bool? value) => Qs("human", value);

	///<summary>Pretty format the returned JSON response.</summary>
	public TDescriptor Pretty(bool? value) => Qs("pretty", value);

	/// <summary>
	///     Hides the <see cref="ToString" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => base.ToString();

	/// <summary>
	///     Hides the <see cref="Equals" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable BaseObjectEqualsIsObjectEquals
	public override bool Equals(object obj) => base.Equals(obj);

	/// <summary>
	///     Hides the <see cref="GetHashCode" /> method.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
	public override int GetHashCode() => base.GetHashCode();
	// ReSharper restore BaseObjectEqualsIsObjectEquals
}
