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

internal sealed partial class NodeInfoXpackSecuritySslConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodeInfoXpackSecuritySsl>
{
	private static readonly System.Text.Json.JsonEncodedText PropSsl = System.Text.Json.JsonEncodedText.Encode("ssl");

	public override Elastic.Clients.Elasticsearch.Nodes.NodeInfoXpackSecuritySsl Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propSsl = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propSsl.TryReadProperty(ref reader, options, PropSsl, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodeInfoXpackSecuritySsl(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Ssl = propSsl.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodeInfoXpackSecuritySsl value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropSsl, value.Ssl, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodeInfoXpackSecuritySslConverter))]
public sealed partial class NodeInfoXpackSecuritySsl
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodeInfoXpackSecuritySsl(System.Collections.Generic.IReadOnlyDictionary<string, string> ssl)
	{
		Ssl = ssl;
	}
#if NET7_0_OR_GREATER
	public NodeInfoXpackSecuritySsl()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NodeInfoXpackSecuritySsl()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodeInfoXpackSecuritySsl(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> Ssl { get; set; }
}