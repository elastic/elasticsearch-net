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

internal sealed partial class TransformSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TransformManagement.TransformSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropAuthorization = System.Text.Json.JsonEncodedText.Encode("authorization");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTime = System.Text.Json.JsonEncodedText.Encode("create_time");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTimeString = System.Text.Json.JsonEncodedText.Encode("create_time_string");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropDest = System.Text.Json.JsonEncodedText.Encode("dest");
	private static readonly System.Text.Json.JsonEncodedText PropFrequency = System.Text.Json.JsonEncodedText.Encode("frequency");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropLatest = System.Text.Json.JsonEncodedText.Encode("latest");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropPivot = System.Text.Json.JsonEncodedText.Encode("pivot");
	private static readonly System.Text.Json.JsonEncodedText PropRetentionPolicy = System.Text.Json.JsonEncodedText.Encode("retention_policy");
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");
	private static readonly System.Text.Json.JsonEncodedText PropSync = System.Text.Json.JsonEncodedText.Encode("sync");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.TransformManagement.TransformSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TransformAuthorization?> propAuthorization = default;
		LocalJsonValue<System.DateTime?> propCreateTime = default;
		LocalJsonValue<System.DateTime?> propCreateTimeString = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Reindex.Destination> propDest = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propFrequency = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Latest?> propLatest = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Pivot?> propPivot = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.RetentionPolicy?> propRetentionPolicy = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Settings?> propSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Source> propSource = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TransformManagement.Sync?> propSync = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAuthorization.TryReadProperty(ref reader, options, PropAuthorization, null))
			{
				continue;
			}

			if (propCreateTime.TryReadProperty(ref reader, options, PropCreateTime, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propCreateTimeString.TryReadProperty(ref reader, options, PropCreateTimeString, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

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

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propLatest.TryReadProperty(ref reader, options, PropLatest, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
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

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.TransformManagement.TransformSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Authorization = propAuthorization.Value,
			CreateTime = propCreateTime.Value,
			CreateTimeString = propCreateTimeString.Value,
			Description = propDescription.Value,
			Dest = propDest.Value,
			Frequency = propFrequency.Value,
			Id = propId.Value,
			Latest = propLatest.Value,
			Meta = propMeta.Value,
			Pivot = propPivot.Value,
			RetentionPolicy = propRetentionPolicy.Value,
			Settings = propSettings.Value,
			Source = propSource.Value,
			Sync = propSync.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TransformManagement.TransformSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAuthorization, value.Authorization, null, null);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropCreateTimeString, value.CreateTimeString, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropDest, value.Dest, null, null);
		writer.WriteProperty(options, PropFrequency, value.Frequency, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropLatest, value.Latest, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropPivot, value.Pivot, null, null);
		writer.WriteProperty(options, PropRetentionPolicy, value.RetentionPolicy, null, null);
		writer.WriteProperty(options, PropSettings, value.Settings, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropSync, value.Sync, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TransformManagement.TransformSummaryConverter))]
public sealed partial class TransformSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TransformSummary(Elastic.Clients.Elasticsearch.Core.Reindex.Destination dest, string id, Elastic.Clients.Elasticsearch.TransformManagement.Source source)
	{
		Dest = dest;
		Id = id;
		Source = source;
	}
#if NET7_0_OR_GREATER
	public TransformSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TransformSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TransformSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The security privileges that the transform uses to run its queries. If Elastic Stack security features were disabled at the time of the most recent update to the transform, this property is omitted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TransformAuthorization? Authorization { get; set; }

	/// <summary>
	/// <para>
	/// The time the transform was created.
	/// </para>
	/// </summary>
	public System.DateTime? CreateTime { get; set; }
	public System.DateTime? CreateTimeString { get; set; }

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
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.Reindex.Destination Dest { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? Frequency { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }
	public Elastic.Clients.Elasticsearch.TransformManagement.Latest? Latest { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// The pivot method transforms the data by aggregating and grouping it.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Pivot? Pivot { get; set; }
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
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.TransformManagement.Source Source { get; set; }

	/// <summary>
	/// <para>
	/// Defines the properties transforms require to run continuously.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TransformManagement.Sync? Sync { get; set; }

	/// <summary>
	/// <para>
	/// The version of Elasticsearch that existed on the node when the transform was created.
	/// </para>
	/// </summary>
	public string? Version { get; set; }
}