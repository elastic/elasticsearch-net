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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class NodeInfoSettingsHttpConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttp>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompression = System.Text.Json.JsonEncodedText.Encode("compression");
	private static readonly System.Text.Json.JsonEncodedText PropPort = System.Text.Json.JsonEncodedText.Encode("port");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropTypeDefault = System.Text.Json.JsonEncodedText.Encode("type.default");

	public override Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttp Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Union<bool, string>?> propCompression = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Union<int, string>?> propPort = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttpType> propType = default;
		LocalJsonValue<string?> propTypeDefault = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompression.TryReadProperty(ref reader, options, PropCompression, static Elastic.Clients.Elasticsearch.Union<bool, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadUnionValue<bool, string>(o, static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.True | Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.False, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String), null, null)))
			{
				continue;
			}

			if (propPort.TryReadProperty(ref reader, options, PropPort, static Elastic.Clients.Elasticsearch.Union<int, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadUnionValue<int, string>(o, static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.Number, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String), null, null)))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
			{
				continue;
			}

			if (propTypeDefault.TryReadProperty(ref reader, options, PropTypeDefault, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Compression = propCompression.Value,
			Port = propPort.Value,
			Type = propType.Value,
			TypeDefault = propTypeDefault.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttp value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompression, value.Compression, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Union<bool, string>? v) => w.WriteUnionValue<bool, string>(o, v, null, null));
		writer.WriteProperty(options, PropPort, value.Port, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Union<int, string>? v) => w.WriteUnionValue<int, string>(o, v, null, null));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropTypeDefault, value.TypeDefault, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttpConverter))]
public sealed partial class NodeInfoSettingsHttp
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodeInfoSettingsHttp(Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttpType type)
	{
		Type = type;
	}
#if NET7_0_OR_GREATER
	public NodeInfoSettingsHttp()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NodeInfoSettingsHttp()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodeInfoSettingsHttp(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Union<bool, string>? Compression { get; set; }
	public Elastic.Clients.Elasticsearch.Union<int, string>? Port { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettingsHttpType Type { get; set; }
	public string? TypeDefault { get; set; }
}