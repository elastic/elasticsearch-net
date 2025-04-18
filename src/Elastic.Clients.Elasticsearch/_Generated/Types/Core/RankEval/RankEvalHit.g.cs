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

namespace Elastic.Clients.Elasticsearch.Core.RankEval;

internal sealed partial class RankEvalHitConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalHit>
{
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropScore = System.Text.Json.JsonEncodedText.Encode("_score");

	public override Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalHit Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propId = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<double> propScore = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propScore.TryReadProperty(ref reader, options, PropScore, null))
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
		return new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalHit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Id = propId.Value,
			Index = propIndex.Value,
			Score = propScore.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalHit value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropScore, value.Score, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalHitConverter))]
public sealed partial class RankEvalHit
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankEvalHit(string id, string index, double score)
	{
		Id = id;
		Index = index;
		Score = score;
	}
#if NET7_0_OR_GREATER
	public RankEvalHit()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RankEvalHit()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RankEvalHit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Index { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Score { get; set; }
}