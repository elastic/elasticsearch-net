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

public sealed partial class ValidateRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class ValidateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisConfig = System.Text.Json.JsonEncodedText.Encode("analysis_config");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisLimits = System.Text.Json.JsonEncodedText.Encode("analysis_limits");
	private static readonly System.Text.Json.JsonEncodedText PropDataDescription = System.Text.Json.JsonEncodedText.Encode("data_description");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelPlot = System.Text.Json.JsonEncodedText.Encode("model_plot");
	private static readonly System.Text.Json.JsonEncodedText PropModelSnapshotId = System.Text.Json.JsonEncodedText.Encode("model_snapshot_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelSnapshotRetentionDays = System.Text.Json.JsonEncodedText.Encode("model_snapshot_retention_days");
	private static readonly System.Text.Json.JsonEncodedText PropResultsIndexName = System.Text.Json.JsonEncodedText.Encode("results_index_name");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig?> propAnalysisConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits?> propAnalysisLimits = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataDescription?> propDataDescription = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propJobId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig?> propModelPlot = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propModelSnapshotId = default;
		LocalJsonValue<long?> propModelSnapshotRetentionDays = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexName?> propResultsIndexName = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalysisConfig.TryReadProperty(ref reader, options, PropAnalysisConfig, null))
			{
				continue;
			}

			if (propAnalysisLimits.TryReadProperty(ref reader, options, PropAnalysisLimits, null))
			{
				continue;
			}

			if (propDataDescription.TryReadProperty(ref reader, options, PropDataDescription, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propModelPlot.TryReadProperty(ref reader, options, PropModelPlot, null))
			{
				continue;
			}

			if (propModelSnapshotId.TryReadProperty(ref reader, options, PropModelSnapshotId, null))
			{
				continue;
			}

			if (propModelSnapshotRetentionDays.TryReadProperty(ref reader, options, PropModelSnapshotRetentionDays, null))
			{
				continue;
			}

			if (propResultsIndexName.TryReadProperty(ref reader, options, PropResultsIndexName, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AnalysisConfig = propAnalysisConfig.Value,
			AnalysisLimits = propAnalysisLimits.Value,
			DataDescription = propDataDescription.Value,
			Description = propDescription.Value,
			JobId = propJobId.Value,
			ModelPlot = propModelPlot.Value,
			ModelSnapshotId = propModelSnapshotId.Value,
			ModelSnapshotRetentionDays = propModelSnapshotRetentionDays.Value,
			ResultsIndexName = propResultsIndexName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalysisConfig, value.AnalysisConfig, null, null);
		writer.WriteProperty(options, PropAnalysisLimits, value.AnalysisLimits, null, null);
		writer.WriteProperty(options, PropDataDescription, value.DataDescription, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropModelPlot, value.ModelPlot, null, null);
		writer.WriteProperty(options, PropModelSnapshotId, value.ModelSnapshotId, null, null);
		writer.WriteProperty(options, PropModelSnapshotRetentionDays, value.ModelSnapshotRetentionDays, null, null);
		writer.WriteProperty(options, PropResultsIndexName, value.ResultsIndexName, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Validate an anomaly detection job.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestConverter))]
public sealed partial class ValidateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestParameters>
{
#if NET7_0_OR_GREATER
	public ValidateRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ValidateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningValidate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.validate";

	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig? AnalysisConfig { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits? AnalysisLimits { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.DataDescription? DataDescription { get; set; }
	public string? Description { get; set; }
	public Elastic.Clients.Elasticsearch.Id? JobId { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? ModelPlot { get; set; }
	public Elastic.Clients.Elasticsearch.Id? ModelSnapshotId { get; set; }
	public long? ModelSnapshotRetentionDays { get; set; }
	public Elastic.Clients.Elasticsearch.IndexName? ResultsIndexName { get; set; }
}

/// <summary>
/// <para>
/// Validate an anomaly detection job.
/// </para>
/// </summary>
public readonly partial struct ValidateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ValidateRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest instance)
	{
		Instance = instance;
	}

	public ValidateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisConfig(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig? value)
	{
		Instance.AnalysisConfig = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisConfig(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor> action)
	{
		Instance.AnalysisConfig = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisConfig<T>(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor<T>> action)
	{
		Instance.AnalysisConfig = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisLimits(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits? value)
	{
		Instance.AnalysisLimits = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisLimits()
	{
		Instance.AnalysisLimits = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor AnalysisLimits(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor>? action)
	{
		Instance.AnalysisLimits = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor DataDescription(Elastic.Clients.Elasticsearch.MachineLearning.DataDescription? value)
	{
		Instance.DataDescription = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor DataDescription()
	{
		Instance.DataDescription = Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor DataDescription(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor>? action)
	{
		Instance.DataDescription = Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor DataDescription<T>(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor<T>>? action)
	{
		Instance.DataDescription = Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.JobId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelPlot(Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? value)
	{
		Instance.ModelPlot = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelPlot()
	{
		Instance.ModelPlot = Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelPlot(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor>? action)
	{
		Instance.ModelPlot = Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelPlot<T>(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor<T>>? action)
	{
		Instance.ModelPlot = Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelSnapshotId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.ModelSnapshotId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ModelSnapshotRetentionDays(long? value)
	{
		Instance.ModelSnapshotRetentionDays = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ResultsIndexName(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.ResultsIndexName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Validate an anomaly detection job.
/// </para>
/// </summary>
public readonly partial struct ValidateRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ValidateRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest instance)
	{
		Instance = instance;
	}

	public ValidateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> AnalysisConfig(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig? value)
	{
		Instance.AnalysisConfig = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> AnalysisConfig(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor<TDocument>> action)
	{
		Instance.AnalysisConfig = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> AnalysisLimits(Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits? value)
	{
		Instance.AnalysisLimits = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> AnalysisLimits()
	{
		Instance.AnalysisLimits = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> AnalysisLimits(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor>? action)
	{
		Instance.AnalysisLimits = Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimitsDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> DataDescription(Elastic.Clients.Elasticsearch.MachineLearning.DataDescription? value)
	{
		Instance.DataDescription = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> DataDescription()
	{
		Instance.DataDescription = Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> DataDescription(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor<TDocument>>? action)
	{
		Instance.DataDescription = Elastic.Clients.Elasticsearch.MachineLearning.DataDescriptionDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.JobId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ModelPlot(Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? value)
	{
		Instance.ModelPlot = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ModelPlot()
	{
		Instance.ModelPlot = Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor<TDocument>.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ModelPlot(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor<TDocument>>? action)
	{
		Instance.ModelPlot = Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfigDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ModelSnapshotId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.ModelSnapshotId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ModelSnapshotRetentionDays(long? value)
	{
		Instance.ModelSnapshotRetentionDays = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ResultsIndexName(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.ResultsIndexName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.ValidateRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}