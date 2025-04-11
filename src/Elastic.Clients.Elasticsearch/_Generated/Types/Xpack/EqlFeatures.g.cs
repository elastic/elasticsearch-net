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

internal sealed partial class EqlFeaturesConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.EqlFeatures>
{
	private static readonly System.Text.Json.JsonEncodedText PropEvent = System.Text.Json.JsonEncodedText.Encode("event");
	private static readonly System.Text.Json.JsonEncodedText PropJoin = System.Text.Json.JsonEncodedText.Encode("join");
	private static readonly System.Text.Json.JsonEncodedText PropJoins = System.Text.Json.JsonEncodedText.Encode("joins");
	private static readonly System.Text.Json.JsonEncodedText PropKeys = System.Text.Json.JsonEncodedText.Encode("keys");
	private static readonly System.Text.Json.JsonEncodedText PropPipes = System.Text.Json.JsonEncodedText.Encode("pipes");
	private static readonly System.Text.Json.JsonEncodedText PropSequence = System.Text.Json.JsonEncodedText.Encode("sequence");
	private static readonly System.Text.Json.JsonEncodedText PropSequences = System.Text.Json.JsonEncodedText.Encode("sequences");

	public override Elastic.Clients.Elasticsearch.Xpack.EqlFeatures Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propEvent = default;
		LocalJsonValue<int> propJoin = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesJoin> propJoins = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys> propKeys = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesPipes> propPipes = default;
		LocalJsonValue<int> propSequence = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesSequences> propSequences = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEvent.TryReadProperty(ref reader, options, PropEvent, null))
			{
				continue;
			}

			if (propJoin.TryReadProperty(ref reader, options, PropJoin, null))
			{
				continue;
			}

			if (propJoins.TryReadProperty(ref reader, options, PropJoins, null))
			{
				continue;
			}

			if (propKeys.TryReadProperty(ref reader, options, PropKeys, null))
			{
				continue;
			}

			if (propPipes.TryReadProperty(ref reader, options, PropPipes, null))
			{
				continue;
			}

			if (propSequence.TryReadProperty(ref reader, options, PropSequence, null))
			{
				continue;
			}

			if (propSequences.TryReadProperty(ref reader, options, PropSequences, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.EqlFeatures(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Event = propEvent.Value,
			Join = propJoin.Value,
			Joins = propJoins.Value,
			Keys = propKeys.Value,
			Pipes = propPipes.Value,
			Sequence = propSequence.Value,
			Sequences = propSequences.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.EqlFeatures value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEvent, value.Event, null, null);
		writer.WriteProperty(options, PropJoin, value.Join, null, null);
		writer.WriteProperty(options, PropJoins, value.Joins, null, null);
		writer.WriteProperty(options, PropKeys, value.Keys, null, null);
		writer.WriteProperty(options, PropPipes, value.Pipes, null, null);
		writer.WriteProperty(options, PropSequence, value.Sequence, null, null);
		writer.WriteProperty(options, PropSequences, value.Sequences, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesConverter))]
public sealed partial class EqlFeatures
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlFeatures(int @event, int join, Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesJoin joins, Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys keys, Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesPipes pipes, int sequence, Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesSequences sequences)
	{
		Event = @event;
		Join = join;
		Joins = joins;
		Keys = keys;
		Pipes = pipes;
		Sequence = sequence;
		Sequences = sequences;
	}
#if NET7_0_OR_GREATER
	public EqlFeatures()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public EqlFeatures()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EqlFeatures(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Event { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Join { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesJoin Joins { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys Keys { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesPipes Pipes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Sequence { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesSequences Sequences { get; set; }
}