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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ShardPathConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardPath>
{
	private static readonly System.Text.Json.JsonEncodedText PropDataPath = System.Text.Json.JsonEncodedText.Encode("data_path");
	private static readonly System.Text.Json.JsonEncodedText PropIsCustomDataPath = System.Text.Json.JsonEncodedText.Encode("is_custom_data_path");
	private static readonly System.Text.Json.JsonEncodedText PropStatePath = System.Text.Json.JsonEncodedText.Encode("state_path");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardPath Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propDataPath = default;
		LocalJsonValue<bool> propIsCustomDataPath = default;
		LocalJsonValue<string> propStatePath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDataPath.TryReadProperty(ref reader, options, PropDataPath, null))
			{
				continue;
			}

			if (propIsCustomDataPath.TryReadProperty(ref reader, options, PropIsCustomDataPath, null))
			{
				continue;
			}

			if (propStatePath.TryReadProperty(ref reader, options, PropStatePath, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardPath(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DataPath = propDataPath.Value,
			IsCustomDataPath = propIsCustomDataPath.Value,
			StatePath = propStatePath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardPath value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDataPath, value.DataPath, null, null);
		writer.WriteProperty(options, PropIsCustomDataPath, value.IsCustomDataPath, null, null);
		writer.WriteProperty(options, PropStatePath, value.StatePath, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardPathConverter))]
public sealed partial class ShardPath
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardPath(string dataPath, bool isCustomDataPath, string statePath)
	{
		DataPath = dataPath;
		IsCustomDataPath = isCustomDataPath;
		StatePath = statePath;
	}
#if NET7_0_OR_GREATER
	public ShardPath()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardPath()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardPath(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string DataPath { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IsCustomDataPath { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string StatePath { get; set; }
}