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

public sealed partial class GetIndicesSettingsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index
	/// alias, or <c>_all</c> value targets only missing or closed indices. This
	/// behavior applies even if the request targets other open indices. For
	/// example, a request targeting <c>foo*,bar*</c> returns an error if an index
	/// starts with foo but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns settings in flat format.
	/// </para>
	/// </summary>
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only. If
	/// <c>false</c>, information is retrieved from the master node.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class GetIndicesSettingsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get index settings.
/// Get setting information for one or more indices.
/// For data streams, it returns setting information for the stream's backing indices.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestConverter))]
public sealed partial class GetIndicesSettingsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestParameters>
{
	public GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("index", indices).Optional("name", name))
	{
	}

	public GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public GetIndicesSettingsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetIndicesSettingsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementGetSettings;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_settings";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit
	/// the request. Supports wildcards (<c>*</c>). To target all data streams and
	/// indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expression of settings to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Names? Name { get => P<Elastic.Clients.Elasticsearch.Names?>("name"); set => PO("name", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index
	/// alias, or <c>_all</c> value targets only missing or closed indices. This
	/// behavior applies even if the request targets other open indices. For
	/// example, a request targeting <c>foo*,bar*</c> returns an error if an index
	/// starts with foo but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns settings in flat format.
	/// </para>
	/// </summary>
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only. If
	/// <c>false</c>, information is retrieved from the master node.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get index settings.
/// Get setting information for one or more indices.
/// For data streams, it returns setting information for the stream's backing indices.
/// </para>
/// </summary>
public readonly partial struct GetIndicesSettingsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest instance)
	{
		Instance = instance;
	}

	public GetIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(indices);
	}

	public GetIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Names name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(indices, name);
	}

	public GetIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Names name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(name);
	}

	public GetIndicesSettingsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit
	/// the request. Supports wildcards (<c>*</c>). To target all data streams and
	/// indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expression of settings to retrieve.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index
	/// alias, or <c>_all</c> value targets only missing or closed indices. This
	/// behavior applies even if the request targets other open indices. For
	/// example, a request targeting <c>foo*,bar*</c> returns an error if an index
	/// starts with foo but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor ExpandWildcards()
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor ExpandWildcards(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard>? action)
	{
		Instance.ExpandWildcards = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfExpandWildcard.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, returns settings in flat format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor FlatSettings(bool? value = true)
	{
		Instance.FlatSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor IncludeDefaults(bool? value = true)
	{
		Instance.IncludeDefaults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only. If
	/// <c>false</c>, information is retrieved from the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor Local(bool? value = true)
	{
		Instance.Local = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetIndicesSettingsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}