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

internal sealed partial class ShardFileSizeInfoConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfo>
{
	private static readonly System.Text.Json.JsonEncodedText PropAverageSizeInBytes = System.Text.Json.JsonEncodedText.Encode("average_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSizeInBytes = System.Text.Json.JsonEncodedText.Encode("max_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropMinSizeInBytes = System.Text.Json.JsonEncodedText.Encode("min_size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropSizeInBytes = System.Text.Json.JsonEncodedText.Encode("size_in_bytes");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propAverageSizeInBytes = default;
		LocalJsonValue<long?> propCount = default;
		LocalJsonValue<string> propDescription = default;
		LocalJsonValue<long?> propMaxSizeInBytes = default;
		LocalJsonValue<long?> propMinSizeInBytes = default;
		LocalJsonValue<long> propSizeInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAverageSizeInBytes.TryReadProperty(ref reader, options, PropAverageSizeInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCount.TryReadProperty(ref reader, options, PropCount, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propMaxSizeInBytes.TryReadProperty(ref reader, options, PropMaxSizeInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propMinSizeInBytes.TryReadProperty(ref reader, options, PropMinSizeInBytes, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propSizeInBytes.TryReadProperty(ref reader, options, PropSizeInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AverageSizeInBytes = propAverageSizeInBytes.Value,
			Count = propCount.Value,
			Description = propDescription.Value,
			MaxSizeInBytes = propMaxSizeInBytes.Value,
			MinSizeInBytes = propMinSizeInBytes.Value,
			SizeInBytes = propSizeInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfo value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAverageSizeInBytes, value.AverageSizeInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCount, value.Count, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropMaxSizeInBytes, value.MaxSizeInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropMinSizeInBytes, value.MinSizeInBytes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropSizeInBytes, value.SizeInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfoConverter))]
public sealed partial class ShardFileSizeInfo
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardFileSizeInfo(string description, long sizeInBytes)
	{
		Description = description;
		SizeInBytes = sizeInBytes;
	}
#if NET7_0_OR_GREATER
	public ShardFileSizeInfo()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardFileSizeInfo()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardFileSizeInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public long? AverageSizeInBytes { get; set; }
	public long? Count { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Description { get; set; }
	public long? MaxSizeInBytes { get; set; }
	public long? MinSizeInBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SizeInBytes { get; set; }
}