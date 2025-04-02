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

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class GrantApiKeyResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GrantApiKeyResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropApiKey = System.Text.Json.JsonEncodedText.Encode("api_key");
	private static readonly System.Text.Json.JsonEncodedText PropEncoded = System.Text.Json.JsonEncodedText.Encode("encoded");
	private static readonly System.Text.Json.JsonEncodedText PropExpiration = System.Text.Json.JsonEncodedText.Encode("expiration");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");

	public override Elastic.Clients.Elasticsearch.Security.GrantApiKeyResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propApiKey = default;
		LocalJsonValue<string> propEncoded = default;
		LocalJsonValue<System.DateTime?> propExpiration = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<string> propName = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propApiKey.TryReadProperty(ref reader, options, PropApiKey, null))
			{
				continue;
			}

			if (propEncoded.TryReadProperty(ref reader, options, PropEncoded, null))
			{
				continue;
			}

			if (propExpiration.TryReadProperty(ref reader, options, PropExpiration, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
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
		return new Elastic.Clients.Elasticsearch.Security.GrantApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ApiKey = propApiKey.Value,
			Encoded = propEncoded.Value,
			Expiration = propExpiration.Value,
			Id = propId.Value,
			Name = propName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GrantApiKeyResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropApiKey, value.ApiKey, null, null);
		writer.WriteProperty(options, PropEncoded, value.Encoded, null, null);
		writer.WriteProperty(options, PropExpiration, value.Expiration, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GrantApiKeyResponseConverter))]
public sealed partial class GrantApiKeyResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrantApiKeyResponse(string apiKey, string encoded, string id, string name)
	{
		ApiKey = apiKey;
		Encoded = encoded;
		Id = id;
		Name = name;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GrantApiKeyResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GrantApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		string ApiKey { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Encoded { get; set; }
	public System.DateTime? Expiration { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Id { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string Name { get; set; }
}