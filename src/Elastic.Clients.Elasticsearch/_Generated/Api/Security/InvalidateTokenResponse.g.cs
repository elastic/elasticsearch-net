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

internal sealed partial class InvalidateTokenResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.InvalidateTokenResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropErrorCount = System.Text.Json.JsonEncodedText.Encode("error_count");
	private static readonly System.Text.Json.JsonEncodedText PropErrorDetails = System.Text.Json.JsonEncodedText.Encode("error_details");
	private static readonly System.Text.Json.JsonEncodedText PropInvalidatedTokens = System.Text.Json.JsonEncodedText.Encode("invalidated_tokens");
	private static readonly System.Text.Json.JsonEncodedText PropPreviouslyInvalidatedTokens = System.Text.Json.JsonEncodedText.Encode("previously_invalidated_tokens");

	public override Elastic.Clients.Elasticsearch.Security.InvalidateTokenResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propErrorCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propErrorDetails = default;
		LocalJsonValue<long> propInvalidatedTokens = default;
		LocalJsonValue<long> propPreviouslyInvalidatedTokens = default;
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

			if (propInvalidatedTokens.TryReadProperty(ref reader, options, PropInvalidatedTokens, null))
			{
				continue;
			}

			if (propPreviouslyInvalidatedTokens.TryReadProperty(ref reader, options, PropPreviouslyInvalidatedTokens, null))
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
		return new Elastic.Clients.Elasticsearch.Security.InvalidateTokenResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ErrorCount = propErrorCount.Value,
			ErrorDetails = propErrorDetails.Value,
			InvalidatedTokens = propInvalidatedTokens.Value,
			PreviouslyInvalidatedTokens = propPreviouslyInvalidatedTokens.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.InvalidateTokenResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropErrorCount, value.ErrorCount, null, null);
		writer.WriteProperty(options, PropErrorDetails, value.ErrorDetails, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ErrorCause>(o, v, null));
		writer.WriteProperty(options, PropInvalidatedTokens, value.InvalidatedTokens, null, null);
		writer.WriteProperty(options, PropPreviouslyInvalidatedTokens, value.PreviouslyInvalidatedTokens, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.InvalidateTokenResponseConverter))]
public sealed partial class InvalidateTokenResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InvalidateTokenResponse(long errorCount, long invalidatedTokens, long previouslyInvalidatedTokens)
	{
		ErrorCount = errorCount;
		InvalidatedTokens = invalidatedTokens;
		PreviouslyInvalidatedTokens = previouslyInvalidatedTokens;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InvalidateTokenResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal InvalidateTokenResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of errors that were encountered when invalidating the tokens.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ErrorCount { get; set; }

	/// <summary>
	/// <para>
	/// Details about the errors.
	/// This field is not present in the response when <c>error_count</c> is <c>0</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? ErrorDetails { get; set; }

	/// <summary>
	/// <para>
	/// The number of the tokens that were invalidated as part of this request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InvalidatedTokens { get; set; }

	/// <summary>
	/// <para>
	/// The number of tokens that were already invalidated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long PreviouslyInvalidatedTokens { get; set; }
}