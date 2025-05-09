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

internal sealed partial class SecurityRolesNativeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative>
{
	private static readonly System.Text.Json.JsonEncodedText PropDls = System.Text.Json.JsonEncodedText.Encode("dls");
	private static readonly System.Text.Json.JsonEncodedText PropFls = System.Text.Json.JsonEncodedText.Encode("fls");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propDls = default;
		LocalJsonValue<bool> propFls = default;
		LocalJsonValue<long> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDls.TryReadProperty(ref reader, options, PropDls, null))
			{
				continue;
			}

			if (propFls.TryReadProperty(ref reader, options, PropFls, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Dls = propDls.Value,
			Fls = propFls.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDls, value.Dls, null, null);
		writer.WriteProperty(options, PropFls, value.Fls, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNativeConverter))]
public sealed partial class SecurityRolesNative
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SecurityRolesNative(bool dls, bool fls, long size)
	{
		Dls = dls;
		Fls = fls;
		Size = size;
	}
#if NET7_0_OR_GREATER
	public SecurityRolesNative()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SecurityRolesNative()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SecurityRolesNative(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Dls { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Fls { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Size { get; set; }
}