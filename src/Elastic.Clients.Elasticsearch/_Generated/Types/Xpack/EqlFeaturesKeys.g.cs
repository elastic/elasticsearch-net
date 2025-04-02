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

internal sealed partial class EqlFeaturesKeysConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys>
{
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeysFiveOrMore = System.Text.Json.JsonEncodedText.Encode("join_keys_five_or_more");
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeysFour = System.Text.Json.JsonEncodedText.Encode("join_keys_four");
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeysOne = System.Text.Json.JsonEncodedText.Encode("join_keys_one");
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeysThree = System.Text.Json.JsonEncodedText.Encode("join_keys_three");
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeysTwo = System.Text.Json.JsonEncodedText.Encode("join_keys_two");

	public override Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propJoinKeysFiveOrMore = default;
		LocalJsonValue<int> propJoinKeysFour = default;
		LocalJsonValue<int> propJoinKeysOne = default;
		LocalJsonValue<int> propJoinKeysThree = default;
		LocalJsonValue<int> propJoinKeysTwo = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propJoinKeysFiveOrMore.TryReadProperty(ref reader, options, PropJoinKeysFiveOrMore, null))
			{
				continue;
			}

			if (propJoinKeysFour.TryReadProperty(ref reader, options, PropJoinKeysFour, null))
			{
				continue;
			}

			if (propJoinKeysOne.TryReadProperty(ref reader, options, PropJoinKeysOne, null))
			{
				continue;
			}

			if (propJoinKeysThree.TryReadProperty(ref reader, options, PropJoinKeysThree, null))
			{
				continue;
			}

			if (propJoinKeysTwo.TryReadProperty(ref reader, options, PropJoinKeysTwo, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			JoinKeysFiveOrMore = propJoinKeysFiveOrMore.Value,
			JoinKeysFour = propJoinKeysFour.Value,
			JoinKeysOne = propJoinKeysOne.Value,
			JoinKeysThree = propJoinKeysThree.Value,
			JoinKeysTwo = propJoinKeysTwo.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeys value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropJoinKeysFiveOrMore, value.JoinKeysFiveOrMore, null, null);
		writer.WriteProperty(options, PropJoinKeysFour, value.JoinKeysFour, null, null);
		writer.WriteProperty(options, PropJoinKeysOne, value.JoinKeysOne, null, null);
		writer.WriteProperty(options, PropJoinKeysThree, value.JoinKeysThree, null, null);
		writer.WriteProperty(options, PropJoinKeysTwo, value.JoinKeysTwo, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.EqlFeaturesKeysConverter))]
public sealed partial class EqlFeaturesKeys
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlFeaturesKeys(int joinKeysFiveOrMore, int joinKeysFour, int joinKeysOne, int joinKeysThree, int joinKeysTwo)
	{
		JoinKeysFiveOrMore = joinKeysFiveOrMore;
		JoinKeysFour = joinKeysFour;
		JoinKeysOne = joinKeysOne;
		JoinKeysThree = joinKeysThree;
		JoinKeysTwo = joinKeysTwo;
	}
#if NET7_0_OR_GREATER
	public EqlFeaturesKeys()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public EqlFeaturesKeys()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EqlFeaturesKeys(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int JoinKeysFiveOrMore { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JoinKeysFour { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JoinKeysOne { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JoinKeysThree { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JoinKeysTwo { get; set; }
}