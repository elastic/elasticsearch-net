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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class PreviewTransformRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class PreviewTransformRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropDest = System.Text.Json.JsonEncodedText.Encode("dest");
	private static readonly System.Text.Json.JsonEncodedText PropFrequency = System.Text.Json.JsonEncodedText.Encode("frequency");
	private static readonly System.Text.Json.JsonEncodedText PropLatest = System.Text.Json.JsonEncodedText.Encode("latest");
	private static readonly System.Text.Json.JsonEncodedText PropPivot = System.Text.Json.JsonEncodedText.Encode("pivot");
	private static readonly System.Text.Json.JsonEncodedText PropRetentionPolicy = System.Text.Json.JsonEncodedText.Encode("retention_policy");
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");
	private static readonly System.Text.Json.JsonEncodedText PropSync = System.Text.Json.JsonEncodedText.Encode("sync");

	public override Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Destination?> propDest = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propFrequency = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Latest?> propLatest = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Pivot?> propPivot = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy?> propRetentionPolicy = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Settings?> propSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Source?> propSource = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Sync?> propSync = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propDest.TryReadProperty(ref reader, options, PropDest, null))
			{
				continue;
			}

			if (propFrequency.TryReadProperty(ref reader, options, PropFrequency, null))
			{
				continue;
			}

			if (propLatest.TryReadProperty(ref reader, options, PropLatest, null))
			{
				continue;
			}

			if (propPivot.TryReadProperty(ref reader, options, PropPivot, null))
			{
				continue;
			}

			if (propRetentionPolicy.TryReadProperty(ref reader, options, PropRetentionPolicy, null))
			{
				continue;
			}

			if (propSettings.TryReadProperty(ref reader, options, PropSettings, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (propSync.TryReadProperty(ref reader, options, PropSync, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Description = propDescription.Value,
			Dest = propDest.Value,
			Frequency = propFrequency.Value,
			Latest = propLatest.Value,
			Pivot = propPivot.Value,
			RetentionPolicy = propRetentionPolicy.Value,
			Settings = propSettings.Value,
			Source = propSource.Value,
			Sync = propSync.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropDest, value.Dest, null, null);
		writer.WriteProperty(options, PropFrequency, value.Frequency, null, null);
		writer.WriteProperty(options, PropLatest, value.Latest, null, null);
		writer.WriteProperty(options, PropPivot, value.Pivot, null, null);
		writer.WriteProperty(options, PropRetentionPolicy, value.RetentionPolicy, null, null);
		writer.WriteProperty(options, PropSettings, value.Settings, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropSync, value.Sync, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Preview a transform.
/// Generates a preview of the results that you will get when you create a transform with the same configuration.
/// </para>
/// <para>
/// It returns a maximum of 100 results. The calculations are based on all the current data in the source index. It also
/// generates a list of mappings and settings for the destination index. These values are determined based on the field
/// types of the source index and the transform aggregations.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestConverter))]
public sealed partial class PreviewTransformRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestParameters>
{
	public PreviewTransformRequest(Elastic.Clients.Elasticsearch.Id? transformId) : base(r => r.Optional("transform_id", transformId))
	{
	}
#if NET7_0_OR_GREATER
	public PreviewTransformRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public PreviewTransformRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.TransformManagementPreviewTransform;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "transform.preview_transform";

	/// <summary>
	/// <para>
	/// Identifier for the transform to preview. If you specify this path parameter, you cannot provide transform
	/// configuration details in the request body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? TransformId { get => P<Elastic.Clients.Elasticsearch.Id?>("transform_id"); set => PO("transform_id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Destination? Dest { get; set; }

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Frequency { get; set; }

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Latest? Latest { get; set; }

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Pivot? Pivot { get; set; }

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? RetentionPolicy { get; set; }

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Settings? Settings { get; set; }

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Source? Source { get; set; }

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Sync? Sync { get; set; }
}

/// <summary>
/// <para>
/// Preview a transform.
/// Generates a preview of the results that you will get when you create a transform with the same configuration.
/// </para>
/// <para>
/// It returns a maximum of 100 results. The calculations are based on all the current data in the source index. It also
/// generates a list of mappings and settings for the destination index. These values are determined based on the field
/// types of the source index and the transform aggregations.
/// </para>
/// </summary>
public readonly partial struct PreviewTransformRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PreviewTransformRequestDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest instance)
	{
		Instance = instance;
	}

	public PreviewTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId)
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(transformId);
	}

	public PreviewTransformRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest instance) => new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the transform to preview. If you specify this path parameter, you cannot provide transform
	/// configuration details in the request body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.TransformId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Dest(Elastic.Clients.Elasticsearch.TransformManagement.Destination? value)
	{
		Instance.Dest = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Dest()
	{
		Instance.Dest = Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Dest(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor>? action)
	{
		Instance.Dest = Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Frequency(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Frequency = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Latest(Elastic.Clients.Elasticsearch.TransformManagement.Latest? value)
	{
		Instance.Latest = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Latest(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor> action)
	{
		Instance.Latest = Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Latest<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor<T>> action)
	{
		Instance.Latest = Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Pivot(Elastic.Clients.Elasticsearch.TransformManagement.Pivot? value)
	{
		Instance.Pivot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Pivot()
	{
		Instance.Pivot = Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Pivot(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor>? action)
	{
		Instance.Pivot = Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Pivot<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor<T>>? action)
	{
		Instance.Pivot = Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? value)
	{
		Instance.RetentionPolicy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor RetentionPolicy(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor> action)
	{
		Instance.RetentionPolicy = Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor RetentionPolicy<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<T>> action)
	{
		Instance.RetentionPolicy = Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Settings(Elastic.Clients.Elasticsearch.TransformManagement.Settings? value)
	{
		Instance.Settings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Settings(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Source(Elastic.Clients.Elasticsearch.TransformManagement.Source? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Source(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Source<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<T>> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Sync(Elastic.Clients.Elasticsearch.TransformManagement.Sync? value)
	{
		Instance.Sync = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Sync(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor> action)
	{
		Instance.Sync = Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Sync<T>(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<T>> action)
	{
		Instance.Sync = Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor(new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Preview a transform.
/// Generates a preview of the results that you will get when you create a transform with the same configuration.
/// </para>
/// <para>
/// It returns a maximum of 100 results. The calculations are based on all the current data in the source index. It also
/// generates a list of mappings and settings for the destination index. These values are determined based on the field
/// types of the source index and the transform aggregations.
/// </para>
/// </summary>
public readonly partial struct PreviewTransformRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PreviewTransformRequestDescriptor(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest instance)
	{
		Instance = instance;
	}

	public PreviewTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId)
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(transformId);
	}

	public PreviewTransformRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest instance) => new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the transform to preview. If you specify this path parameter, you cannot provide transform
	/// configuration details in the request body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> TransformId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.TransformId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Description(string? value)
	{
		Instance.Description = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.TransformManagement.Destination? value)
	{
		Instance.Dest = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Dest()
	{
		Instance.Dest = Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Dest(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor>? action)
	{
		Instance.Dest = Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Frequency(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Frequency = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Latest(Elastic.Clients.Elasticsearch.TransformManagement.Latest? value)
	{
		Instance.Latest = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The latest method transforms the data by finding the latest document for
	/// each unique key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Latest(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor<TDocument>> action)
	{
		Instance.Latest = Elastic.Clients.Elasticsearch.TransformManagement.LatestDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Pivot(Elastic.Clients.Elasticsearch.TransformManagement.Pivot? value)
	{
		Instance.Pivot = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Pivot()
	{
		Instance.Pivot = Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// These objects define the group by fields and the aggregation to reduce
	/// the data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Pivot(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor<TDocument>>? action)
	{
		Instance.Pivot = Elastic.Clients.Elasticsearch.TransformManagement.PivotDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? value)
	{
		Instance.RetentionPolicy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> RetentionPolicy(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument>> action)
	{
		Instance.RetentionPolicy = Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.TransformManagement.Settings? value)
	{
		Instance.Settings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Settings(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.TransformManagement.Source? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Source(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument>> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Sync(Elastic.Clients.Elasticsearch.TransformManagement.Sync? value)
	{
		Instance.Sync = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Sync(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>> action)
	{
		Instance.Sync = Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest Build(System.Action<Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TransformManagement.PreviewTransformRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}