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

public sealed partial class GetDataLifecycleRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Type of data stream that wildcard patterns can match.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class GetDataLifecycleRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get data stream lifecycles.
/// </para>
/// <para>
/// Get the data stream lifecycle configuration of one or more data streams.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestConverter))]
public sealed partial class GetDataLifecycleRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.DataStreamNames name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public GetDataLifecycleRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementGetDataLifecycle;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_data_lifecycle";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.DataStreamNames Name { get => P<Elastic.Clients.Elasticsearch.DataStreamNames>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// Type of data stream that wildcard patterns can match.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get data stream lifecycles.
/// </para>
/// <para>
/// Get the data stream lifecycle configuration of one or more data streams.
/// </para>
/// </summary>
public readonly partial struct GetDataLifecycleRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetDataLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest instance)
	{
		Instance = instance;
	}

	public GetDataLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.DataStreamNames name)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest(name);
	}

	[System.Obsolete("TODO")]
	public GetDataLifecycleRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor Name(Elastic.Clients.Elasticsearch.DataStreamNames value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of data stream that wildcard patterns can match.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of data stream that wildcard patterns can match.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor IncludeDefaults(bool? value = true)
	{
		Instance.IncludeDefaults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}