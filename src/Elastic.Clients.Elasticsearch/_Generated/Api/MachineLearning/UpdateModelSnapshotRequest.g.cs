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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class UpdateModelSnapshotRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class UpdateModelSnapshotRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropRetain = System.Text.Json.JsonEncodedText.Encode("retain");

	public override Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<bool?> propRetain = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propRetain.TryReadProperty(ref reader, options, PropRetain, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Retain = propRetain.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropRetain, value.Retain, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Update a snapshot.
/// Updates certain properties of a snapshot.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestConverter))]
public sealed partial class UpdateModelSnapshotRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateModelSnapshotRequest(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}
#if NET7_0_OR_GREATER
	public UpdateModelSnapshotRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UpdateModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningUpdateModelSnapshot;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.update_model_snapshot";

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id JobId { get => P<Elastic.Clients.Elasticsearch.Id>("job_id"); set => PR("job_id", value); }

	/// <summary>
	/// <para>
	/// Identifier for the model snapshot.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id SnapshotId { get => P<Elastic.Clients.Elasticsearch.Id>("snapshot_id"); set => PR("snapshot_id", value); }

	/// <summary>
	/// <para>
	/// A description of the model snapshot.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, this snapshot will not be deleted during automatic cleanup of
	/// snapshots older than <c>model_snapshot_retention_days</c>. However, this
	/// snapshot will be deleted when the job is deleted.
	/// </para>
	/// </summary>
	public bool? Retain { get; set; }
}

/// <summary>
/// <para>
/// Update a snapshot.
/// Updates certain properties of a snapshot.
/// </para>
/// </summary>
public readonly partial struct UpdateModelSnapshotRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest instance)
	{
		Instance = instance;
	}

	public UpdateModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest(jobId, snapshotId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public UpdateModelSnapshotRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest(Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Identifier for the model snapshot.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor SnapshotId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.SnapshotId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A description of the model snapshot.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, this snapshot will not be deleted during automatic cleanup of
	/// snapshots older than <c>model_snapshot_retention_days</c>. However, this
	/// snapshot will be deleted when the job is deleted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor Retain(bool? value = true)
	{
		Instance.Retain = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.UpdateModelSnapshotRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}