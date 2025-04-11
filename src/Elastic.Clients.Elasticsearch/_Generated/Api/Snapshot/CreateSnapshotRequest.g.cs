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

namespace Elastic.Clients.Elasticsearch.Snapshot;

public sealed partial class CreateSnapshotRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request returns a response when the snapshot is complete.
	/// If <c>false</c>, the request returns a response when the snapshot initializes.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class CreateSnapshotRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropExpandWildcards = System.Text.Json.JsonEncodedText.Encode("expand_wildcards");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureStates = System.Text.Json.JsonEncodedText.Encode("feature_states");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnavailable = System.Text.Json.JsonEncodedText.Encode("ignore_unavailable");
	private static readonly System.Text.Json.JsonEncodedText PropIncludeGlobalState = System.Text.Json.JsonEncodedText.Encode("include_global_state");
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropPartial = System.Text.Json.JsonEncodedText.Encode("partial");

	public override Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?> propExpandWildcards = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propFeatureStates = default;
		LocalJsonValue<bool?> propIgnoreUnavailable = default;
		LocalJsonValue<bool?> propIncludeGlobalState = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Indices?> propIndices = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMetadata = default;
		LocalJsonValue<bool?> propPartial = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExpandWildcards.TryReadProperty(ref reader, options, PropExpandWildcards, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, null)))
			{
				continue;
			}

			if (propFeatureStates.TryReadProperty(ref reader, options, PropFeatureStates, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propIgnoreUnavailable.TryReadProperty(ref reader, options, PropIgnoreUnavailable, null))
			{
				continue;
			}

			if (propIncludeGlobalState.TryReadProperty(ref reader, options, PropIncludeGlobalState, null))
			{
				continue;
			}

			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
			{
				continue;
			}

			if (propMetadata.TryReadProperty(ref reader, options, PropMetadata, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propPartial.TryReadProperty(ref reader, options, PropPartial, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ExpandWildcards = propExpandWildcards.Value,
			FeatureStates = propFeatureStates.Value,
			IgnoreUnavailable = propIgnoreUnavailable.Value,
			IncludeGlobalState = propIncludeGlobalState.Value,
			Indices = propIndices.Value,
			Metadata = propMetadata.Value,
			Partial = propPartial.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExpandWildcards, value.ExpandWildcards, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, v, null));
		writer.WriteProperty(options, PropFeatureStates, value.FeatureStates, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIgnoreUnavailable, value.IgnoreUnavailable, null, null);
		writer.WriteProperty(options, PropIncludeGlobalState, value.IncludeGlobalState, null, null);
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteProperty(options, PropMetadata, value.Metadata, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropPartial, value.Partial, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a snapshot.
/// Take a snapshot of a cluster or of data streams and indices.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestConverter))]
public sealed partial class CreateSnapshotRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateSnapshotRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot))
	{
	}
#if NET7_0_OR_GREATER
	public CreateSnapshotRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SnapshotCreate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "snapshot.create";

	/// <summary>
	/// <para>
	/// The name of the repository for the snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Repository { get => P<Elastic.Clients.Elasticsearch.Name>("repository"); set => PR("repository", value); }

	/// <summary>
	/// <para>
	/// The name of the snapshot.
	/// It supportes date math.
	/// It must be unique in the repository.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Snapshot { get => P<Elastic.Clients.Elasticsearch.Name>("snapshot"); set => PR("snapshot", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request returns a response when the snapshot is complete.
	/// If <c>false</c>, the request returns a response when the snapshot initializes.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }

	/// <summary>
	/// <para>
	/// Determines how wildcard patterns in the <c>indices</c> parameter match data streams and indices.
	/// It supports comma-separated values such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get; set; }

	/// <summary>
	/// <para>
	/// The feature states to include in the snapshot.
	/// Each feature state includes one or more system indices containing related data.
	/// You can view a list of eligible features using the get features API.
	/// </para>
	/// <para>
	/// If <c>include_global_state</c> is <c>true</c>, all current feature states are included by default.
	/// If <c>include_global_state</c> is <c>false</c>, no feature states are included by default.
	/// </para>
	/// <para>
	/// Note that specifying an empty array will result in the default behavior.
	/// To exclude all feature states, regardless of the <c>include_global_state</c> value, specify an array with only the value <c>none</c> (<c>["none"]</c>).
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? FeatureStates { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request ignores data streams and indices in <c>indices</c> that are missing or closed.
	/// If <c>false</c>, the request returns an error for any data stream or index that is missing or closed.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the current cluster state is included in the snapshot.
	/// The cluster state includes persistent cluster settings, composable index templates, legacy index templates, ingest pipelines, and ILM policies.
	/// It also includes data stored in system indices, such as Watches and task records (configurable via <c>feature_states</c>).
	/// </para>
	/// </summary>
	public bool? IncludeGlobalState { get; set; }

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams and indices to include in the snapshot.
	/// It supports a multi-target syntax.
	/// The default is an empty array (<c>[]</c>), which includes all regular data streams and regular indices.
	/// To exclude all data streams and indices, use <c>-*</c>.
	/// </para>
	/// <para>
	/// You can't use this parameter to include or exclude system indices or system data streams from a snapshot.
	/// Use <c>feature_states</c> instead.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata to the snapshot, such as a record of who took the snapshot, why it was taken, or any other useful data.
	/// It can have any contents but it must be less than 1024 bytes.
	/// This information is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, it enables you to restore a partial snapshot of indices with unavailable shards.
	/// Only shards that were successfully included in the snapshot will be restored.
	/// All missing shards will be recreated as empty.
	/// </para>
	/// <para>
	/// If <c>false</c>, the entire restore operation will fail if one or more indices included in the snapshot do not have all primary shards available.
	/// </para>
	/// </summary>
	public bool? Partial { get; set; }
}

/// <summary>
/// <para>
/// Create a snapshot.
/// Take a snapshot of a cluster or of data streams and indices.
/// </para>
/// </summary>
public readonly partial struct CreateSnapshotRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest instance)
	{
		Instance = instance;
	}

	public CreateSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot)
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest(repository, snapshot);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public CreateSnapshotRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest instance) => new Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest(Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the repository for the snapshot.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Repository = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the snapshot.
	/// It supportes date math.
	/// It must be unique in the repository.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Snapshot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request returns a response when the snapshot is complete.
	/// If <c>false</c>, the request returns a response when the snapshot initializes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how wildcard patterns in the <c>indices</c> parameter match data streams and indices.
	/// It supports comma-separated values such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how wildcard patterns in the <c>indices</c> parameter match data streams and indices.
	/// It supports comma-separated values such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The feature states to include in the snapshot.
	/// Each feature state includes one or more system indices containing related data.
	/// You can view a list of eligible features using the get features API.
	/// </para>
	/// <para>
	/// If <c>include_global_state</c> is <c>true</c>, all current feature states are included by default.
	/// If <c>include_global_state</c> is <c>false</c>, no feature states are included by default.
	/// </para>
	/// <para>
	/// Note that specifying an empty array will result in the default behavior.
	/// To exclude all feature states, regardless of the <c>include_global_state</c> value, specify an array with only the value <c>none</c> (<c>["none"]</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor FeatureStates(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.FeatureStates = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The feature states to include in the snapshot.
	/// Each feature state includes one or more system indices containing related data.
	/// You can view a list of eligible features using the get features API.
	/// </para>
	/// <para>
	/// If <c>include_global_state</c> is <c>true</c>, all current feature states are included by default.
	/// If <c>include_global_state</c> is <c>false</c>, no feature states are included by default.
	/// </para>
	/// <para>
	/// Note that specifying an empty array will result in the default behavior.
	/// To exclude all feature states, regardless of the <c>include_global_state</c> value, specify an array with only the value <c>none</c> (<c>["none"]</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor FeatureStates(params string[] values)
	{
		Instance.FeatureStates = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request ignores data streams and indices in <c>indices</c> that are missing or closed.
	/// If <c>false</c>, the request returns an error for any data stream or index that is missing or closed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the current cluster state is included in the snapshot.
	/// The cluster state includes persistent cluster settings, composable index templates, legacy index templates, ingest pipelines, and ILM policies.
	/// It also includes data stored in system indices, such as Watches and task records (configurable via <c>feature_states</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor IncludeGlobalState(bool? value = true)
	{
		Instance.IncludeGlobalState = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams and indices to include in the snapshot.
	/// It supports a multi-target syntax.
	/// The default is an empty array (<c>[]</c>), which includes all regular data streams and regular indices.
	/// To exclude all data streams and indices, use <c>-*</c>.
	/// </para>
	/// <para>
	/// You can't use this parameter to include or exclude system indices or system data streams from a snapshot.
	/// Use <c>feature_states</c> instead.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata to the snapshot, such as a record of who took the snapshot, why it was taken, or any other useful data.
	/// It can have any contents but it must be less than 1024 bytes.
	/// This information is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Metadata(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata to the snapshot, such as a record of who took the snapshot, why it was taken, or any other useful data.
	/// It can have any contents but it must be less than 1024 bytes.
	/// This information is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Metadata()
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata to the snapshot, such as a record of who took the snapshot, why it was taken, or any other useful data.
	/// It can have any contents but it must be less than 1024 bytes.
	/// This information is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Metadata(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor AddMetadatum(string key, object value)
	{
		Instance.Metadata ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Metadata.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, it enables you to restore a partial snapshot of indices with unavailable shards.
	/// Only shards that were successfully included in the snapshot will be restored.
	/// All missing shards will be recreated as empty.
	/// </para>
	/// <para>
	/// If <c>false</c>, the entire restore operation will fail if one or more indices included in the snapshot do not have all primary shards available.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Partial(bool? value = true)
	{
		Instance.Partial = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CreateSnapshotRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}