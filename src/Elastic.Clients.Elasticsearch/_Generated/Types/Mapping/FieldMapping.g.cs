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

namespace Elastic.Clients.Elasticsearch.Mapping;

internal sealed partial class FieldMappingConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Mapping.FieldMapping>
{
	private static readonly System.Text.Json.JsonEncodedText PropFullName = System.Text.Json.JsonEncodedText.Encode("full_name");
	private static readonly System.Text.Json.JsonEncodedText PropMapping = System.Text.Json.JsonEncodedText.Encode("mapping");

	public override Elastic.Clients.Elasticsearch.Mapping.FieldMapping Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propFullName = default;
		LocalJsonValue<System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.Mapping.IProperty>> propMapping = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFullName.TryReadProperty(ref reader, options, PropFullName, null))
			{
				continue;
			}

			if (propMapping.TryReadProperty(ref reader, options, PropMapping, static System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.Mapping.IProperty> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadKeyValuePairValue<string, Elastic.Clients.Elasticsearch.Mapping.IProperty>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.Mapping.FieldMapping(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FullName = propFullName.Value,
			Mapping = propMapping.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Mapping.FieldMapping value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFullName, value.FullName, null, null);
		writer.WriteProperty(options, PropMapping, value.Mapping, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.Mapping.IProperty> v) => w.WriteKeyValuePairValue<string, Elastic.Clients.Elasticsearch.Mapping.IProperty>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Mapping.FieldMappingConverter))]
public sealed partial class FieldMapping
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldMapping(string fullName, System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.Mapping.IProperty> mapping)
	{
		FullName = fullName;
		Mapping = mapping;
	}
#if NET7_0_OR_GREATER
	public FieldMapping()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FieldMapping()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldMapping(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string FullName { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.KeyValuePair<string, Elastic.Clients.Elasticsearch.Mapping.IProperty> Mapping { get; set; }
}