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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ValidateQueryResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ValidateQueryResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropError = System.Text.Json.JsonEncodedText.Encode("error");
	private static readonly System.Text.Json.JsonEncodedText PropExplanations = System.Text.Json.JsonEncodedText.Encode("explanations");
	private static readonly System.Text.Json.JsonEncodedText PropShards = System.Text.Json.JsonEncodedText.Encode("_shards");
	private static readonly System.Text.Json.JsonEncodedText PropValid = System.Text.Json.JsonEncodedText.Encode("valid");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ValidateQueryResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propError = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>?> propExplanations = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ShardStatistics?> propShards = default;
		LocalJsonValue<bool> propValid = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propError.TryReadProperty(ref reader, options, PropError, null))
			{
				continue;
			}

			if (propExplanations.TryReadProperty(ref reader, options, PropExplanations, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>(o, null)))
			{
				continue;
			}

			if (propShards.TryReadProperty(ref reader, options, PropShards, null))
			{
				continue;
			}

			if (propValid.TryReadProperty(ref reader, options, PropValid, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ValidateQueryResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Error = propError.Value,
			Explanations = propExplanations.Value,
			Shards = propShards.Value,
			Valid = propValid.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ValidateQueryResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropError, value.Error, null, null);
		writer.WriteProperty(options, PropExplanations, value.Explanations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>(o, v, null));
		writer.WriteProperty(options, PropShards, value.Shards, null, null);
		writer.WriteProperty(options, PropValid, value.Valid, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ValidateQueryResponseConverter))]
public sealed partial class ValidateQueryResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ValidateQueryResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ValidateQueryResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? Error { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.IndicesValidationExplanation>? Explanations { get; set; }
	public Elastic.Clients.Elasticsearch.ShardStatistics? Shards { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		bool Valid { get; set; }
}