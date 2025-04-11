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

internal sealed partial class CreateApiKeyResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.CreateApiKeyResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropApiKey = System.Text.Json.JsonEncodedText.Encode("api_key");
	private static readonly System.Text.Json.JsonEncodedText PropEncoded = System.Text.Json.JsonEncodedText.Encode("encoded");
	private static readonly System.Text.Json.JsonEncodedText PropExpiration = System.Text.Json.JsonEncodedText.Encode("expiration");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");

	public override Elastic.Clients.Elasticsearch.Security.CreateApiKeyResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propApiKey = default;
		LocalJsonValue<string> propEncoded = default;
		LocalJsonValue<long?> propExpiration = default;
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

			if (propExpiration.TryReadProperty(ref reader, options, PropExpiration, null))
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
		return new Elastic.Clients.Elasticsearch.Security.CreateApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ApiKey = propApiKey.Value,
			Encoded = propEncoded.Value,
			Expiration = propExpiration.Value,
			Id = propId.Value,
			Name = propName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.CreateApiKeyResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropApiKey, value.ApiKey, null, null);
		writer.WriteProperty(options, PropEncoded, value.Encoded, null, null);
		writer.WriteProperty(options, PropExpiration, value.Expiration, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.CreateApiKeyResponseConverter))]
public sealed partial class CreateApiKeyResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateApiKeyResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Generated API key.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ApiKey { get; set; }

	/// <summary>
	/// <para>
	/// API key credentials which is the base64-encoding of
	/// the UTF-8 representation of <c>id</c> and <c>api_key</c> joined
	/// by a colon (<c>:</c>).
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Encoded { get; set; }

	/// <summary>
	/// <para>
	/// Expiration in milliseconds for the API key.
	/// </para>
	/// </summary>
	public long? Expiration { get; set; }

	/// <summary>
	/// <para>
	/// Unique ID for this API key.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
}