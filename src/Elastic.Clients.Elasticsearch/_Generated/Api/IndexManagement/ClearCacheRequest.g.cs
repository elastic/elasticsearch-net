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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class ClearCacheRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the fields cache.
	/// Use the <c>fields</c> parameter to clear the cache of specific fields only.
	/// </para>
	/// </summary>
	public bool? Fielddata { get => Q<bool?>("fielddata"); set => Q("fielddata", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the query cache.
	/// </para>
	/// </summary>
	public bool? Query { get => Q<bool?>("query"); set => Q("query", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the request cache.
	/// </para>
	/// </summary>
	public bool? Request { get => Q<bool?>("request"); set => Q("request", value); }
}

internal sealed partial class ClearCacheRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Clear the cache.
/// Clear the cache of one or more indices.
/// For data streams, the API clears the caches of the stream's backing indices.
/// </para>
/// <para>
/// By default, the clear cache API clears all caches.
/// To clear only specific caches, use the <c>fielddata</c>, <c>query</c>, or <c>request</c> parameters.
/// To clear the cache only of specific fields, use the <c>fields</c> parameter.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestConverter))]
public sealed partial class ClearCacheRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestParameters>
{
	public ClearCacheRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public ClearCacheRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ClearCacheRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementClearCache;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.clear_cache";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the fields cache.
	/// Use the <c>fields</c> parameter to clear the cache of specific fields only.
	/// </para>
	/// </summary>
	public bool? Fielddata { get => Q<bool?>("fielddata"); set => Q("fielddata", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the query cache.
	/// </para>
	/// </summary>
	public bool? Query { get => Q<bool?>("query"); set => Q("query", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the request cache.
	/// </para>
	/// </summary>
	public bool? Request { get => Q<bool?>("request"); set => Q("request", value); }
}

/// <summary>
/// <para>
/// Clear the cache.
/// Clear the cache of one or more indices.
/// For data streams, the API clears the caches of the stream's backing indices.
/// </para>
/// <para>
/// By default, the clear cache API clears all caches.
/// To clear only specific caches, use the <c>fielddata</c>, <c>query</c>, or <c>request</c> parameters.
/// To clear the cache only of specific fields, use the <c>fields</c> parameter.
/// </para>
/// </summary>
public readonly partial struct ClearCacheRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearCacheRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest instance)
	{
		Instance = instance;
	}

	public ClearCacheRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(indices);
	}

	public ClearCacheRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the fields cache.
	/// Use the <c>fields</c> parameter to clear the cache of specific fields only.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Fielddata(bool? value = true)
	{
		Instance.Fielddata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Fields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the query cache.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Query(bool? value = true)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the request cache.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Request(bool? value = true)
	{
		Instance.Request = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Clear the cache.
/// Clear the cache of one or more indices.
/// For data streams, the API clears the caches of the stream's backing indices.
/// </para>
/// <para>
/// By default, the clear cache API clears all caches.
/// To clear only specific caches, use the <c>fielddata</c>, <c>query</c>, or <c>request</c> parameters.
/// To clear the cache only of specific fields, use the <c>fields</c> parameter.
/// </para>
/// </summary>
public readonly partial struct ClearCacheRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClearCacheRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest instance)
	{
		Instance = instance;
	}

	public ClearCacheRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(indices);
	}

	public ClearCacheRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the fields cache.
	/// Use the <c>fields</c> parameter to clear the cache of specific fields only.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Fielddata(bool? value = true)
	{
		Instance.Fielddata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of field names used to limit the <c>fielddata</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Fields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the query cache.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Query(bool? value = true)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, clears the request cache.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Request(bool? value = true)
	{
		Instance.Request = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ClearCacheRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}