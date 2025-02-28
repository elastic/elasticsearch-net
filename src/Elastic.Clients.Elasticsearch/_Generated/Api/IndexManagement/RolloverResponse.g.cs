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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class RolloverResponseConverter : System.Text.Json.Serialization.JsonConverter<RolloverResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAcknowledged = System.Text.Json.JsonEncodedText.Encode("acknowledged");
	private static readonly System.Text.Json.JsonEncodedText PropConditions = System.Text.Json.JsonEncodedText.Encode("conditions");
	private static readonly System.Text.Json.JsonEncodedText PropDryRun = System.Text.Json.JsonEncodedText.Encode("dry_run");
	private static readonly System.Text.Json.JsonEncodedText PropNewIndex = System.Text.Json.JsonEncodedText.Encode("new_index");
	private static readonly System.Text.Json.JsonEncodedText PropOldIndex = System.Text.Json.JsonEncodedText.Encode("old_index");
	private static readonly System.Text.Json.JsonEncodedText PropRolledOver = System.Text.Json.JsonEncodedText.Encode("rolled_over");
	private static readonly System.Text.Json.JsonEncodedText PropShardsAcknowledged = System.Text.Json.JsonEncodedText.Encode("shards_acknowledged");

	public override RolloverResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAcknowledged = default;
		LocalJsonValue<IReadOnlyDictionary<string, bool>> propConditions = default;
		LocalJsonValue<bool> propDryRun = default;
		LocalJsonValue<string> propNewIndex = default;
		LocalJsonValue<string> propOldIndex = default;
		LocalJsonValue<bool> propRolledOver = default;
		LocalJsonValue<bool> propShardsAcknowledged = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAcknowledged.TryReadProperty(ref reader, options, PropAcknowledged, null))
			{
				continue;
			}

			if (propConditions.TryReadProperty(ref reader, options, PropConditions, static IReadOnlyDictionary<string, bool> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, bool>(o, null, null)!))
			{
				continue;
			}

			if (propDryRun.TryReadProperty(ref reader, options, PropDryRun, null))
			{
				continue;
			}

			if (propNewIndex.TryReadProperty(ref reader, options, PropNewIndex, null))
			{
				continue;
			}

			if (propOldIndex.TryReadProperty(ref reader, options, PropOldIndex, null))
			{
				continue;
			}

			if (propRolledOver.TryReadProperty(ref reader, options, PropRolledOver, null))
			{
				continue;
			}

			if (propShardsAcknowledged.TryReadProperty(ref reader, options, PropShardsAcknowledged, null))
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
		return new RolloverResponse
		{
			Acknowledged = propAcknowledged.Value
,
			Conditions = propConditions.Value
,
			DryRun = propDryRun.Value
,
			NewIndex = propNewIndex.Value
,
			OldIndex = propOldIndex.Value
,
			RolledOver = propRolledOver.Value
,
			ShardsAcknowledged = propShardsAcknowledged.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RolloverResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAcknowledged, value.Acknowledged, null, null);
		writer.WriteProperty(options, PropConditions, value.Conditions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, IReadOnlyDictionary<string, bool> v) => w.WriteDictionaryValue<string, bool>(o, v, null, null));
		writer.WriteProperty(options, PropDryRun, value.DryRun, null, null);
		writer.WriteProperty(options, PropNewIndex, value.NewIndex, null, null);
		writer.WriteProperty(options, PropOldIndex, value.OldIndex, null, null);
		writer.WriteProperty(options, PropRolledOver, value.RolledOver, null, null);
		writer.WriteProperty(options, PropShardsAcknowledged, value.ShardsAcknowledged, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RolloverResponseConverter))]
public sealed partial class RolloverResponse : ElasticsearchResponse
{
	public bool Acknowledged { get; init; }
	public IReadOnlyDictionary<string, bool> Conditions { get; init; }
	public bool DryRun { get; init; }
	public string NewIndex { get; init; }
	public string OldIndex { get; init; }
	public bool RolledOver { get; init; }
	public bool ShardsAcknowledged { get; init; }
}