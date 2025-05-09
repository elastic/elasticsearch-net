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

public sealed partial class GetDataLifecycleStatsRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetDataLifecycleStatsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get data stream lifecycle stats.
/// Get statistics about the data streams that are managed by a data stream lifecycle.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestConverter))]
public sealed partial class GetDataLifecycleStatsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestParameters>
{
#if NET7_0_OR_GREATER
	public GetDataLifecycleStatsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetDataLifecycleStatsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementGetDataLifecycleStats;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_data_lifecycle_stats";
}

/// <summary>
/// <para>
/// Get data stream lifecycle stats.
/// Get statistics about the data streams that are managed by a data stream lifecycle.
/// </para>
/// </summary>
public readonly partial struct GetDataLifecycleStatsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetDataLifecycleStatsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest instance)
	{
		Instance = instance;
	}

	public GetDataLifecycleStatsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.GetDataLifecycleStatsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}