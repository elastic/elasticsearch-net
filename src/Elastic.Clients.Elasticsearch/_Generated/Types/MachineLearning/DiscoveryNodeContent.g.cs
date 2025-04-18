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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DiscoveryNodeContentConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent>
{
	private static readonly System.Text.Json.JsonEncodedText PropAttributes = System.Text.Json.JsonEncodedText.Encode("attributes");
	private static readonly System.Text.Json.JsonEncodedText PropEphemeralId = System.Text.Json.JsonEncodedText.Encode("ephemeral_id");
	private static readonly System.Text.Json.JsonEncodedText PropExternalId = System.Text.Json.JsonEncodedText.Encode("external_id");
	private static readonly System.Text.Json.JsonEncodedText PropMaxIndexVersion = System.Text.Json.JsonEncodedText.Encode("max_index_version");
	private static readonly System.Text.Json.JsonEncodedText PropMinIndexVersion = System.Text.Json.JsonEncodedText.Encode("min_index_version");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropTransportAddress = System.Text.Json.JsonEncodedText.Encode("transport_address");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propAttributes = default;
		LocalJsonValue<string> propEphemeralId = default;
		LocalJsonValue<string> propExternalId = default;
		LocalJsonValue<int> propMaxIndexVersion = default;
		LocalJsonValue<int> propMinIndexVersion = default;
		LocalJsonValue<string?> propName = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propRoles = default;
		LocalJsonValue<string> propTransportAddress = default;
		LocalJsonValue<string> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAttributes.TryReadProperty(ref reader, options, PropAttributes, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
			{
				continue;
			}

			if (propEphemeralId.TryReadProperty(ref reader, options, PropEphemeralId, null))
			{
				continue;
			}

			if (propExternalId.TryReadProperty(ref reader, options, PropExternalId, null))
			{
				continue;
			}

			if (propMaxIndexVersion.TryReadProperty(ref reader, options, PropMaxIndexVersion, null))
			{
				continue;
			}

			if (propMinIndexVersion.TryReadProperty(ref reader, options, PropMinIndexVersion, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propRoles.TryReadProperty(ref reader, options, PropRoles, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propTransportAddress.TryReadProperty(ref reader, options, PropTransportAddress, null))
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
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Attributes = propAttributes.Value,
			EphemeralId = propEphemeralId.Value,
			ExternalId = propExternalId.Value,
			MaxIndexVersion = propMaxIndexVersion.Value,
			MinIndexVersion = propMinIndexVersion.Value,
			Name = propName.Value,
			Roles = propRoles.Value,
			TransportAddress = propTransportAddress.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContent value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAttributes, value.Attributes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropEphemeralId, value.EphemeralId, null, null);
		writer.WriteProperty(options, PropExternalId, value.ExternalId, null, null);
		writer.WriteProperty(options, PropMaxIndexVersion, value.MaxIndexVersion, null, null);
		writer.WriteProperty(options, PropMinIndexVersion, value.MinIndexVersion, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropRoles, value.Roles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTransportAddress, value.TransportAddress, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeContentConverter))]
public sealed partial class DiscoveryNodeContent
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DiscoveryNodeContent(System.Collections.Generic.IReadOnlyDictionary<string, string> attributes, string ephemeralId, string externalId, int maxIndexVersion, int minIndexVersion, System.Collections.Generic.IReadOnlyCollection<string> roles, string transportAddress, string version)
	{
		Attributes = attributes;
		EphemeralId = ephemeralId;
		ExternalId = externalId;
		MaxIndexVersion = maxIndexVersion;
		MinIndexVersion = minIndexVersion;
		Roles = roles;
		TransportAddress = transportAddress;
		Version = version;
	}
#if NET7_0_OR_GREATER
	public DiscoveryNodeContent()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DiscoveryNodeContent()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DiscoveryNodeContent(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> Attributes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string EphemeralId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ExternalId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int MaxIndexVersion { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int MinIndexVersion { get; set; }
	public string? Name { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> Roles { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string TransportAddress { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Version { get; set; }
}