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

public sealed partial class RecoveryRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }
}

internal sealed partial class RecoveryRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestConverter))]
public sealed partial class RecoveryRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestParameters>
{
	public RecoveryRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public RecoveryRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RecoveryRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementRecovery;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.recovery";

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
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
public readonly partial struct RecoveryRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest instance)
	{
		Instance = instance;
	}

	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(indices);
	}

	public RecoveryRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor ActiveOnly(bool? value = true)
	{
		Instance.ActiveOnly = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor Detailed(bool? value = true)
	{
		Instance.Detailed = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get index recovery information.
/// Get information about ongoing and completed shard recoveries for one or more indices.
/// For data streams, the API returns information for the stream's backing indices.
/// </para>
/// <para>
/// All recoveries, whether ongoing or complete, are kept in the cluster state and may be reported on at any time.
/// </para>
/// <para>
/// Shard recovery is the process of initializing a shard copy, such as restoring a primary shard from a snapshot or creating a replica shard from a primary shard.
/// When a shard recovery completes, the recovered shard is available for search and indexing.
/// </para>
/// <para>
/// Recovery automatically occurs during the following processes:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// When creating an index for the first time.
/// </para>
/// </item>
/// <item>
/// <para>
/// When a node rejoins the cluster and starts up any missing primary shard copies using the data that it holds in its data path.
/// </para>
/// </item>
/// <item>
/// <para>
/// Creation of new replica shard copies from the primary.
/// </para>
/// </item>
/// <item>
/// <para>
/// Relocation of a shard copy to a different node in the same cluster.
/// </para>
/// </item>
/// <item>
/// <para>
/// A snapshot restore operation.
/// </para>
/// </item>
/// <item>
/// <para>
/// A clone, shrink, or split operation.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can determine the cause of a shard recovery using the recovery or cat recovery APIs.
/// </para>
/// <para>
/// The index recovery API reports information about completed recoveries only for shard copies that currently exist in the cluster.
/// It only reports the last recovery for each shard copy and does not report historical information about earlier recoveries, nor does it report information about the recoveries of shard copies that no longer exist.
/// This means that if a shard copy completes a recovery and then Elasticsearch relocates it onto a different node then the information about the original recovery will not be shown in the recovery API.
/// </para>
/// </summary>
public readonly partial struct RecoveryRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest instance)
	{
		Instance = instance;
	}

	public RecoveryRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(indices);
	}

	public RecoveryRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response only includes ongoing shard recoveries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> ActiveOnly(bool? value = true)
	{
		Instance.ActiveOnly = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about shard recoveries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> Detailed(bool? value = true)
	{
		Instance.Detailed = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.RecoveryRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}