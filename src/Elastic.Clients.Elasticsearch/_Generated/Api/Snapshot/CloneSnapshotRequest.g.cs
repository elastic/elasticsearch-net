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

public sealed partial class CloneSnapshotRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class CloneSnapshotRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropIndices = System.Text.Json.JsonEncodedText.Encode("indices");

	public override Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propIndices = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIndices.TryReadProperty(ref reader, options, PropIndices, null))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Indices = propIndices.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIndices, value.Indices, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Clone a snapshot.
/// Clone part of all of a snapshot into another snapshot in the same repository.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestConverter))]
public sealed partial class CloneSnapshotRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name targetSnapshot) : base(r => r.Required("repository", repository).Required("snapshot", snapshot).Required("target_snapshot", targetSnapshot))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name targetSnapshot, string indices) : base(r => r.Required("repository", repository).Required("snapshot", snapshot).Required("target_snapshot", targetSnapshot))
	{
		Indices = indices;
	}
#if NET7_0_OR_GREATER
	public CloneSnapshotRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SnapshotClone;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "snapshot.clone";

	/// <summary>
	/// <para>
	/// The name of the snapshot repository that both source and target snapshot belong to.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Repository { get => P<Elastic.Clients.Elasticsearch.Name>("repository"); set => PR("repository", value); }

	/// <summary>
	/// <para>
	/// The source snapshot name.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Snapshot { get => P<Elastic.Clients.Elasticsearch.Name>("snapshot"); set => PR("snapshot", value); }

	/// <summary>
	/// <para>
	/// The target snapshot name.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name TargetSnapshot { get => P<Elastic.Clients.Elasticsearch.Name>("target_snapshot"); set => PR("target_snapshot", value); }

	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of indices to include in the snapshot.
	/// Multi-target syntax is supported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Indices { get; set; }
}

/// <summary>
/// <para>
/// Clone a snapshot.
/// Clone part of all of a snapshot into another snapshot in the same repository.
/// </para>
/// </summary>
public readonly partial struct CloneSnapshotRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CloneSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest instance)
	{
		Instance = instance;
	}

	public CloneSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Name repository, Elastic.Clients.Elasticsearch.Name snapshot, Elastic.Clients.Elasticsearch.Name targetSnapshot)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest(repository, snapshot, targetSnapshot);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public CloneSnapshotRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest instance) => new Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the snapshot repository that both source and target snapshot belong to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor Repository(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Repository = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The source snapshot name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor Snapshot(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Snapshot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The target snapshot name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor TargetSnapshot(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.TargetSnapshot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// To indicate that the request should never timeout, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of indices to include in the snapshot.
	/// Multi-target syntax is supported.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor Indices(string value)
	{
		Instance.Indices = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Snapshot.CloneSnapshotRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}