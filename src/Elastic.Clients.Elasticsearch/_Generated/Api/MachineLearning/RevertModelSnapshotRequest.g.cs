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

public sealed partial class RevertModelSnapshotRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class RevertModelSnapshotRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeleteInterveningResults = System.Text.Json.JsonEncodedText.Encode("delete_intervening_results");

	public override Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propDeleteInterveningResults = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeleteInterveningResults.TryReadProperty(ref reader, options, PropDeleteInterveningResults, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DeleteInterveningResults = propDeleteInterveningResults.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeleteInterveningResults, value.DeleteInterveningResults, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Revert to a snapshot.
/// The machine learning features react quickly to anomalous input, learning new
/// behaviors in data. Highly anomalous input increases the variance in the
/// models whilst the system learns whether this is a new step-change in behavior
/// or a one-off event. In the case where this anomalous input is known to be a
/// one-off, then it might be appropriate to reset the model state to a time
/// before this event. For example, you might consider reverting to a saved
/// snapshot after Black Friday or a critical system failure.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestConverter))]
public sealed partial class RevertModelSnapshotRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RevertModelSnapshotRequest(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId) : base(r => r.Required("job_id", jobId).Required("snapshot_id", snapshotId))
	{
	}
#if NET7_0_OR_GREATER
	public RevertModelSnapshotRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RevertModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningRevertModelSnapshot;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.revert_model_snapshot";

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
	/// You can specify <c>empty</c> as the &lt;snapshot_id>. Reverting to the empty
	/// snapshot means the anomaly detection job starts learning a new model from
	/// scratch when it is started.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id SnapshotId { get => P<Elastic.Clients.Elasticsearch.Id>("snapshot_id"); set => PR("snapshot_id", value); }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>delete_intervening_results</c> query parameter.
	/// </para>
	/// </summary>
	public bool? DeleteInterveningResults { get; set; }
}

/// <summary>
/// <para>
/// Revert to a snapshot.
/// The machine learning features react quickly to anomalous input, learning new
/// behaviors in data. Highly anomalous input increases the variance in the
/// models whilst the system learns whether this is a new step-change in behavior
/// or a one-off event. In the case where this anomalous input is known to be a
/// one-off, then it might be appropriate to reset the model state to a time
/// before this event. For example, you might consider reverting to a saved
/// snapshot after Black Friday or a critical system failure.
/// </para>
/// </summary>
public readonly partial struct RevertModelSnapshotRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RevertModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest instance)
	{
		Instance = instance;
	}

	public RevertModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId, Elastic.Clients.Elasticsearch.Id snapshotId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest(jobId, snapshotId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public RevertModelSnapshotRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest(Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// You can specify <c>empty</c> as the &lt;snapshot_id>. Reverting to the empty
	/// snapshot means the anomaly detection job starts learning a new model from
	/// scratch when it is started.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor SnapshotId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.SnapshotId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>delete_intervening_results</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor DeleteInterveningResults(bool? value = true)
	{
		Instance.DeleteInterveningResults = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.RevertModelSnapshotRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}