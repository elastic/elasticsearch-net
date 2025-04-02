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

internal sealed partial class InvalidateApiKeyResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.InvalidateApiKeyResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropErrorCount = System.Text.Json.JsonEncodedText.Encode("error_count");
	private static readonly System.Text.Json.JsonEncodedText PropErrorDetails = System.Text.Json.JsonEncodedText.Encode("error_details");
	private static readonly System.Text.Json.JsonEncodedText PropInvalidatedApiKeys = System.Text.Json.JsonEncodedText.Encode("invalidated_api_keys");
	private static readonly System.Text.Json.JsonEncodedText PropPreviouslyInvalidatedApiKeys = System.Text.Json.JsonEncodedText.Encode("previously_invalidated_api_keys");

	public override Elastic.Clients.Elasticsearch.Security.InvalidateApiKeyResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propErrorCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propErrorDetails = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propInvalidatedApiKeys = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propPreviouslyInvalidatedApiKeys = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propErrorCount.TryReadProperty(ref reader, options, PropErrorCount, null))
			{
				continue;
			}

			if (propErrorDetails.TryReadProperty(ref reader, options, PropErrorDetails, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.ErrorCause>(o, null)))
			{
				continue;
			}

			if (propInvalidatedApiKeys.TryReadProperty(ref reader, options, PropInvalidatedApiKeys, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propPreviouslyInvalidatedApiKeys.TryReadProperty(ref reader, options, PropPreviouslyInvalidatedApiKeys, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Security.InvalidateApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ErrorCount = propErrorCount.Value,
			ErrorDetails = propErrorDetails.Value,
			InvalidatedApiKeys = propInvalidatedApiKeys.Value,
			PreviouslyInvalidatedApiKeys = propPreviouslyInvalidatedApiKeys.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.InvalidateApiKeyResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropErrorCount, value.ErrorCount, null, null);
		writer.WriteProperty(options, PropErrorDetails, value.ErrorDetails, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ErrorCause>(o, v, null));
		writer.WriteProperty(options, PropInvalidatedApiKeys, value.InvalidatedApiKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropPreviouslyInvalidatedApiKeys, value.PreviouslyInvalidatedApiKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.InvalidateApiKeyResponseConverter))]
public sealed partial class InvalidateApiKeyResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InvalidateApiKeyResponse(int errorCount, System.Collections.Generic.IReadOnlyCollection<string> invalidatedApiKeys, System.Collections.Generic.IReadOnlyCollection<string> previouslyInvalidatedApiKeys)
	{
		ErrorCount = errorCount;
		InvalidatedApiKeys = invalidatedApiKeys;
		PreviouslyInvalidatedApiKeys = previouslyInvalidatedApiKeys;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InvalidateApiKeyResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal InvalidateApiKeyResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of errors that were encountered when invalidating the API keys.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ErrorCount { get; set; }

	/// <summary>
	/// <para>
	/// Details about the errors.
	/// This field is not present in the response when <c>error_count</c> is <c>0</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? ErrorDetails { get; set; }

	/// <summary>
	/// <para>
	/// The IDs of the API keys that were invalidated as part of this request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> InvalidatedApiKeys { get; set; }

	/// <summary>
	/// <para>
	/// The IDs of the API keys that were already invalidated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> PreviouslyInvalidatedApiKeys { get; set; }
}