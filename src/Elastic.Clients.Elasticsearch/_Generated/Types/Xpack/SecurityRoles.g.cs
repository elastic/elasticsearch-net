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

internal sealed partial class SecurityRolesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.SecurityRoles>
{
	private static readonly System.Text.Json.JsonEncodedText PropDls = System.Text.Json.JsonEncodedText.Encode("dls");
	private static readonly System.Text.Json.JsonEncodedText PropFile = System.Text.Json.JsonEncodedText.Encode("file");
	private static readonly System.Text.Json.JsonEncodedText PropNative = System.Text.Json.JsonEncodedText.Encode("native");

	public override Elastic.Clients.Elasticsearch.Xpack.SecurityRoles Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.SecurityRolesDls> propDls = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.SecurityRolesFile> propFile = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative> propNative = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDls.TryReadProperty(ref reader, options, PropDls, null))
			{
				continue;
			}

			if (propFile.TryReadProperty(ref reader, options, PropFile, null))
			{
				continue;
			}

			if (propNative.TryReadProperty(ref reader, options, PropNative, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.SecurityRoles(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Dls = propDls.Value,
			File = propFile.Value,
			Native = propNative.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.SecurityRoles value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDls, value.Dls, null, null);
		writer.WriteProperty(options, PropFile, value.File, null, null);
		writer.WriteProperty(options, PropNative, value.Native, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.SecurityRolesConverter))]
public sealed partial class SecurityRoles
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SecurityRoles(Elastic.Clients.Elasticsearch.Xpack.SecurityRolesDls dls, Elastic.Clients.Elasticsearch.Xpack.SecurityRolesFile file, Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative native)
	{
		Dls = dls;
		File = file;
		Native = native;
	}
#if NET7_0_OR_GREATER
	public SecurityRoles()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SecurityRoles()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SecurityRoles(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.SecurityRolesDls Dls { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.SecurityRolesFile File { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.SecurityRolesNative Native { get; set; }
}