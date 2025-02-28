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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class IndexTemplateConverter : System.Text.Json.Serialization.JsonConverter<IndexTemplate>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowAutoCreate = System.Text.Json.JsonEncodedText.Encode("allow_auto_create");
	private static readonly System.Text.Json.JsonEncodedText PropComposedOf = System.Text.Json.JsonEncodedText.Encode("composed_of");
	private static readonly System.Text.Json.JsonEncodedText PropDataStream = System.Text.Json.JsonEncodedText.Encode("data_stream");
	private static readonly System.Text.Json.JsonEncodedText PropDeprecated = System.Text.Json.JsonEncodedText.Encode("deprecated");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreMissingComponentTemplates = System.Text.Json.JsonEncodedText.Encode("ignore_missing_component_templates");
	private static readonly System.Text.Json.JsonEncodedText PropIndexPatterns = System.Text.Json.JsonEncodedText.Encode("index_patterns");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropPriority = System.Text.Json.JsonEncodedText.Encode("priority");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override IndexTemplate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowAutoCreate = default;
		LocalJsonValue<IReadOnlyCollection<string>> propComposedOf = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexTemplateDataStreamConfiguration?> propDataStream = default;
		LocalJsonValue<bool?> propDeprecated = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propIgnoreMissingComponentTemplates = default;
		LocalJsonValue<IReadOnlyCollection<string>> propIndexPatterns = default;
		LocalJsonValue<IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<long?> propPriority = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexTemplateSummary?> propTemplate = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowAutoCreate.TryReadProperty(ref reader, options, PropAllowAutoCreate, null))
			{
				continue;
			}

			if (propComposedOf.TryReadProperty(ref reader, options, PropComposedOf, static IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propDataStream.TryReadProperty(ref reader, options, PropDataStream, null))
			{
				continue;
			}

			if (propDeprecated.TryReadProperty(ref reader, options, PropDeprecated, null))
			{
				continue;
			}

			if (propIgnoreMissingComponentTemplates.TryReadProperty(ref reader, options, PropIgnoreMissingComponentTemplates, static IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propIndexPatterns.TryReadProperty(ref reader, options, PropIndexPatterns, static IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propPriority.TryReadProperty(ref reader, options, PropPriority, null))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
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
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new IndexTemplate
		{
			AllowAutoCreate = propAllowAutoCreate.Value
,
			ComposedOf = propComposedOf.Value
,
			DataStream = propDataStream.Value
,
			Deprecated = propDeprecated.Value
,
			IgnoreMissingComponentTemplates = propIgnoreMissingComponentTemplates.Value
,
			IndexPatterns = propIndexPatterns.Value
,
			Meta = propMeta.Value
,
			Priority = propPriority.Value
,
			Template = propTemplate.Value
,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, IndexTemplate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowAutoCreate, value.AllowAutoCreate, null, null);
		writer.WriteProperty(options, PropComposedOf, value.ComposedOf, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropDataStream, value.DataStream, null, null);
		writer.WriteProperty(options, PropDeprecated, value.Deprecated, null, null);
		writer.WriteProperty(options, PropIgnoreMissingComponentTemplates, value.IgnoreMissingComponentTemplates, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIndexPatterns, value.IndexPatterns, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyCollection<string> v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropPriority, value.Priority, null, null);
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(IndexTemplateConverter))]
public sealed partial class IndexTemplate
{
	public bool? AllowAutoCreate { get; init; }

	/// <summary>
	/// <para>
	/// An ordered list of component template names.
	/// Component templates are merged in the order specified, meaning that the last component template specified has the highest precedence.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string> ComposedOf { get; init; }

	/// <summary>
	/// <para>
	/// If this object is included, the template is used to create data streams and their backing indices.
	/// Supports an empty object.
	/// Data streams require a matching index template with a <c>data_stream</c> object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexTemplateDataStreamConfiguration? DataStream { get; init; }

	/// <summary>
	/// <para>
	/// Marks this index template as deprecated.
	/// When creating or updating a non-deprecated index template that uses deprecated components,
	/// Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public bool? Deprecated { get; init; }

	/// <summary>
	/// <para>
	/// A list of component template names that are allowed to be absent.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string>? IgnoreMissingComponentTemplates { get; init; }

	/// <summary>
	/// <para>
	/// Name of the index template.
	/// </para>
	/// </summary>
	public IReadOnlyCollection<string> IndexPatterns { get; init; }

	/// <summary>
	/// <para>
	/// Optional user metadata about the index template. May have any contents.
	/// This map is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, object>? Meta { get; init; }

	/// <summary>
	/// <para>
	/// Priority to determine index template precedence when a new data stream or index is created.
	/// The index template with the highest priority is chosen.
	/// If no priority is specified the template is treated as though it is of priority 0 (lowest priority).
	/// This number is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public long? Priority { get; init; }

	/// <summary>
	/// <para>
	/// Template to be applied.
	/// It may optionally include an <c>aliases</c>, <c>mappings</c>, or <c>settings</c> configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexTemplateSummary? Template { get; init; }

	/// <summary>
	/// <para>
	/// Version number used to manage index templates externally.
	/// This number is not automatically generated by Elasticsearch.
	/// </para>
	/// </summary>
	public long? Version { get; init; }
}