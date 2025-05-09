// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class ClearApiKeyCacheRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class ClearApiKeyCacheRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Clear the API key cache.
/// </para>
/// <para>
/// Evict a subset of all entries from the API key cache.
/// The cache is also automatically cleared on state changes of the security index.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestConverter))]
public sealed partial class ClearApiKeyCacheRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearApiKeyCacheRequest(Elastic.Clients.Elasticsearch.Ids ids) : base(r => r.Required("ids", ids))
	{
	}
#if NET7_0_OR_GREATER
	public ClearApiKeyCacheRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClearApiKeyCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityClearApiKeyCache;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.clear_api_key_cache";

	/// <summary>
	/// <para>
	/// Comma-separated list of API key IDs to evict from the API key cache.
	/// To evict all API keys, use <c>*</c>.
	/// Does not support other wildcard patterns.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Ids Ids { get => P<Elastic.Clients.Elasticsearch.Ids>("ids"); set => PR("ids", value); }
}

/// <summary>
/// <para>
/// Clear the API key cache.
/// </para>
/// <para>
/// Evict a subset of all entries from the API key cache.
/// The cache is also automatically cleared on state changes of the security index.
/// </para>
/// </summary>
public readonly partial struct ClearApiKeyCacheRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearApiKeyCacheRequestDescriptor(Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest instance)
	{
		Instance = instance;
	}

	public ClearApiKeyCacheRequestDescriptor(Elastic.Clients.Elasticsearch.Ids ids)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest(ids);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public ClearApiKeyCacheRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor(Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest instance) => new Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest(Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of API key IDs to evict from the API key cache.
	/// To evict all API keys, use <c>*</c>.
	/// Does not support other wildcard patterns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor Ids(Elastic.Clients.Elasticsearch.Ids value)
	{
		Instance.Ids = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.ClearApiKeyCacheRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}