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

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class FlattenedConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.Flattened>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvailable = System.Text.Json.JsonEncodedText.Encode("available");
	private static readonly System.Text.Json.JsonEncodedText PropEnabled = System.Text.Json.JsonEncodedText.Encode("enabled");
	private static readonly System.Text.Json.JsonEncodedText PropFieldCount = System.Text.Json.JsonEncodedText.Encode("field_count");

	public override Elastic.Clients.Elasticsearch.Xpack.Flattened Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAvailable = default;
		LocalJsonValue<bool> propEnabled = default;
		LocalJsonValue<int> propFieldCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvailable.TryReadProperty(ref reader, options, PropAvailable, null))
			{
				continue;
			}

			if (propEnabled.TryReadProperty(ref reader, options, PropEnabled, null))
			{
				continue;
			}

			if (propFieldCount.TryReadProperty(ref reader, options, PropFieldCount, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.Flattened(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Available = propAvailable.Value,
			Enabled = propEnabled.Value,
			FieldCount = propFieldCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.Flattened value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvailable, value.Available, null, null);
		writer.WriteProperty(options, PropEnabled, value.Enabled, null, null);
		writer.WriteProperty(options, PropFieldCount, value.FieldCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.FlattenedConverter))]
public sealed partial class Flattened
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Flattened(bool available, bool enabled, int fieldCount)
	{
		Available = available;
		Enabled = enabled;
		FieldCount = fieldCount;
	}
#if NET7_0_OR_GREATER
	public Flattened()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Flattened()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Flattened(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Available { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Enabled { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int FieldCount { get; set; }
}