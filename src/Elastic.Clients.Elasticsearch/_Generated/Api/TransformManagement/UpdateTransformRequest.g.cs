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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class UpdateTransformRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// When true, deferrable validations are not run. This behavior may be
	/// desired if the source index does not exist until after the transform is
	/// created.
	/// </para>
	/// </summary>
	public bool? DeferValidation { get => Q<bool?>("defer_validation"); set => Q("defer_validation", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Update a transform.
/// Updates certain properties of a transform.
/// </para>
/// <para>
/// All updated properties except <c>description</c> do not take effect until after the transform starts the next checkpoint,
/// thus there is data consistency in each checkpoint. To use this API, you must have <c>read</c> and <c>view_index_metadata</c>
/// privileges for the source indices. You must also have <c>index</c> and <c>read</c> privileges for the destination index. When
/// Elasticsearch security features are enabled, the transform remembers which roles the user who updated it had at the
/// time of update and runs with those privileges.
/// </para>
/// </summary>
public sealed partial class UpdateTransformRequest : PlainRequest<UpdateTransformRequestParameters>
{
	public UpdateTransformRequest(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	[JsonConstructor]
	internal UpdateTransformRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementUpdateTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "transform.update_transform";

	/// <summary>
	/// <para>
	/// Identifier for the transform.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Id TransformId { get => P<Elastic.Clients.Elasticsearch.Id>("transform_id"); set => PR("transform_id", value); }

	/// <summary>
	/// <para>
	/// When true, deferrable validations are not run. This behavior may be
	/// desired if the source index does not exist until after the transform is
	/// created.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? DeferValidation { get => Q<bool?>("defer_validation"); set => Q("defer_validation", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the
	/// timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("dest")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Destination? Dest { get; set; }

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("frequency")]
	public Elastic.Clients.Elasticsearch.Duration? Frequency { get; set; }

	/// <summary>
	/// <para>
	/// Defines optional transform metadata.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_meta")]
	public IDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("retention_policy")]
	public Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? RetentionPolicy { get; set; }

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Settings? Settings { get; set; }

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Source? Source { get; set; }

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sync")]
	public Elastic.Clients.Elasticsearch.TransformManagement.Sync? Sync { get; set; }
}

/// <summary>
/// <para>
/// Update a transform.
/// Updates certain properties of a transform.
/// </para>
/// <para>
/// All updated properties except <c>description</c> do not take effect until after the transform starts the next checkpoint,
/// thus there is data consistency in each checkpoint. To use this API, you must have <c>read</c> and <c>view_index_metadata</c>
/// privileges for the source indices. You must also have <c>index</c> and <c>read</c> privileges for the destination index. When
/// Elasticsearch security features are enabled, the transform remembers which roles the user who updated it had at the
/// time of update and runs with those privileges.
/// </para>
/// </summary>
public sealed partial class UpdateTransformRequestDescriptor<TDocument> : RequestDescriptor<UpdateTransformRequestDescriptor<TDocument>, UpdateTransformRequestParameters>
{
	internal UpdateTransformRequestDescriptor(Action<UpdateTransformRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public UpdateTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementUpdateTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "transform.update_transform";

	public UpdateTransformRequestDescriptor<TDocument> DeferValidation(bool? deferValidation = true) => Qs("defer_validation", deferValidation);
	public UpdateTransformRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public UpdateTransformRequestDescriptor<TDocument> TransformId(Elastic.Clients.Elasticsearch.Id transformId)
	{
		RouteValues.Required("transform_id", transformId);
		return Self;
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Destination? DestValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor DestDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor> DestDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? RetentionPolicyValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument> RetentionPolicyDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument>> RetentionPolicyDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Settings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor> SettingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Source? SourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument> SourceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument>> SourceDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Sync? SyncValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument> SyncDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>> SyncDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.TransformManagement.Destination? dest)
	{
		DestDescriptor = null;
		DestDescriptorAction = null;
		DestValue = dest;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Dest(Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor descriptor)
	{
		DestValue = null;
		DestDescriptorAction = null;
		DestDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Dest(Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor> configure)
	{
		DestValue = null;
		DestDescriptor = null;
		DestDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
	{
		FrequencyValue = frequency;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform metadata.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? retentionPolicy)
	{
		RetentionPolicyDescriptor = null;
		RetentionPolicyDescriptorAction = null;
		RetentionPolicyValue = retentionPolicy;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument> descriptor)
	{
		RetentionPolicyValue = null;
		RetentionPolicyDescriptorAction = null;
		RetentionPolicyDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> RetentionPolicy(Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument>> configure)
	{
		RetentionPolicyValue = null;
		RetentionPolicyDescriptor = null;
		RetentionPolicyDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.TransformManagement.Settings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Settings(Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.TransformManagement.Source? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument> descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Source(Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument>> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor<TDocument> Sync(Elastic.Clients.Elasticsearch.TransformManagement.Sync? sync)
	{
		SyncDescriptor = null;
		SyncDescriptorAction = null;
		SyncValue = sync;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Sync(Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument> descriptor)
	{
		SyncValue = null;
		SyncDescriptorAction = null;
		SyncDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor<TDocument> Sync(Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>> configure)
	{
		SyncValue = null;
		SyncDescriptor = null;
		SyncDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (DestDescriptor is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestDescriptor, options);
		}
		else if (DestDescriptorAction is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor(DestDescriptorAction), options);
		}
		else if (DestValue is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestValue, options);
		}

		if (FrequencyValue is not null)
		{
			writer.WritePropertyName("frequency");
			JsonSerializer.Serialize(writer, FrequencyValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("_meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (RetentionPolicyDescriptor is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, RetentionPolicyDescriptor, options);
		}
		else if (RetentionPolicyDescriptorAction is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor<TDocument>(RetentionPolicyDescriptorAction), options);
		}
		else if (RetentionPolicyValue is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, RetentionPolicyValue, options);
		}

		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor<TDocument>(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (SyncDescriptor is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, SyncDescriptor, options);
		}
		else if (SyncDescriptorAction is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor<TDocument>(SyncDescriptorAction), options);
		}
		else if (SyncValue is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, SyncValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Update a transform.
/// Updates certain properties of a transform.
/// </para>
/// <para>
/// All updated properties except <c>description</c> do not take effect until after the transform starts the next checkpoint,
/// thus there is data consistency in each checkpoint. To use this API, you must have <c>read</c> and <c>view_index_metadata</c>
/// privileges for the source indices. You must also have <c>index</c> and <c>read</c> privileges for the destination index. When
/// Elasticsearch security features are enabled, the transform remembers which roles the user who updated it had at the
/// time of update and runs with those privileges.
/// </para>
/// </summary>
public sealed partial class UpdateTransformRequestDescriptor : RequestDescriptor<UpdateTransformRequestDescriptor, UpdateTransformRequestParameters>
{
	internal UpdateTransformRequestDescriptor(Action<UpdateTransformRequestDescriptor> configure) => configure.Invoke(this);

	public UpdateTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Id transformId) : base(r => r.Required("transform_id", transformId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.TransformManagementUpdateTransform;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "transform.update_transform";

	public UpdateTransformRequestDescriptor DeferValidation(bool? deferValidation = true) => Qs("defer_validation", deferValidation);
	public UpdateTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public UpdateTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Id transformId)
	{
		RouteValues.Required("transform_id", transformId);
		return Self;
	}

	private string? DescriptionValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Destination? DestValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor DestDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor> DestDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }
	private IDictionary<string, object>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? RetentionPolicyValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor RetentionPolicyDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor> RetentionPolicyDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Settings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor> SettingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Source? SourceValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor SourceDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor> SourceDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.Sync? SyncValue { get; set; }
	private Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor SyncDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor> SyncDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Free text description of the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Description(string? description)
	{
		DescriptionValue = description;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The destination for the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Dest(Elastic.Clients.Elasticsearch.TransformManagement.Destination? dest)
	{
		DestDescriptor = null;
		DestDescriptorAction = null;
		DestValue = dest;
		return Self;
	}

	public UpdateTransformRequestDescriptor Dest(Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor descriptor)
	{
		DestValue = null;
		DestDescriptorAction = null;
		DestDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor Dest(Action<Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor> configure)
	{
		DestValue = null;
		DestDescriptor = null;
		DestDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The interval between checks for changes in the source indices when the
	/// transform is running continuously. Also determines the retry interval in
	/// the event of transient failures while the transform is searching or
	/// indexing. The minimum value is 1s and the maximum is 1h.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
	{
		FrequencyValue = frequency;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform metadata.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines a retention policy for the transform. Data that meets the defined
	/// criteria is deleted from the destination index.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy? retentionPolicy)
	{
		RetentionPolicyDescriptor = null;
		RetentionPolicyDescriptorAction = null;
		RetentionPolicyValue = retentionPolicy;
		return Self;
	}

	public UpdateTransformRequestDescriptor RetentionPolicy(Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor descriptor)
	{
		RetentionPolicyValue = null;
		RetentionPolicyDescriptorAction = null;
		RetentionPolicyDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor RetentionPolicy(Action<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor> configure)
	{
		RetentionPolicyValue = null;
		RetentionPolicyDescriptor = null;
		RetentionPolicyDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines optional transform settings.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Settings(Elastic.Clients.Elasticsearch.TransformManagement.Settings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public UpdateTransformRequestDescriptor Settings(Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor Settings(Action<Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The source of the data for the transform.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Source(Elastic.Clients.Elasticsearch.TransformManagement.Source? source)
	{
		SourceDescriptor = null;
		SourceDescriptorAction = null;
		SourceValue = source;
		return Self;
	}

	public UpdateTransformRequestDescriptor Source(Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor descriptor)
	{
		SourceValue = null;
		SourceDescriptorAction = null;
		SourceDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor Source(Action<Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor> configure)
	{
		SourceValue = null;
		SourceDescriptor = null;
		SourceDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public UpdateTransformRequestDescriptor Sync(Elastic.Clients.Elasticsearch.TransformManagement.Sync? sync)
	{
		SyncDescriptor = null;
		SyncDescriptorAction = null;
		SyncValue = sync;
		return Self;
	}

	public UpdateTransformRequestDescriptor Sync(Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor descriptor)
	{
		SyncValue = null;
		SyncDescriptorAction = null;
		SyncDescriptor = descriptor;
		return Self;
	}

	public UpdateTransformRequestDescriptor Sync(Action<Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor> configure)
	{
		SyncValue = null;
		SyncDescriptor = null;
		SyncDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DescriptionValue))
		{
			writer.WritePropertyName("description");
			writer.WriteStringValue(DescriptionValue);
		}

		if (DestDescriptor is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestDescriptor, options);
		}
		else if (DestDescriptorAction is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.DestinationDescriptor(DestDescriptorAction), options);
		}
		else if (DestValue is not null)
		{
			writer.WritePropertyName("dest");
			JsonSerializer.Serialize(writer, DestValue, options);
		}

		if (FrequencyValue is not null)
		{
			writer.WritePropertyName("frequency");
			JsonSerializer.Serialize(writer, FrequencyValue, options);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("_meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (RetentionPolicyDescriptor is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, RetentionPolicyDescriptor, options);
		}
		else if (RetentionPolicyDescriptorAction is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicyDescriptor(RetentionPolicyDescriptorAction), options);
		}
		else if (RetentionPolicyValue is not null)
		{
			writer.WritePropertyName("retention_policy");
			JsonSerializer.Serialize(writer, RetentionPolicyValue, options);
		}

		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SettingsDescriptor(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		if (SourceDescriptor is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceDescriptor, options);
		}
		else if (SourceDescriptorAction is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SourceDescriptor(SourceDescriptorAction), options);
		}
		else if (SourceValue is not null)
		{
			writer.WritePropertyName("source");
			JsonSerializer.Serialize(writer, SourceValue, options);
		}

		if (SyncDescriptor is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, SyncDescriptor, options);
		}
		else if (SyncDescriptorAction is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.TransformManagement.SyncDescriptor(SyncDescriptorAction), options);
		}
		else if (SyncValue is not null)
		{
			writer.WritePropertyName("sync");
			JsonSerializer.Serialize(writer, SyncValue, options);
		}

		writer.WriteEndObject();
	}
}