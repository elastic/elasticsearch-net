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

internal sealed partial class ShardCommitConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardCommit>
{
	private static readonly System.Text.Json.JsonEncodedText PropGeneration = System.Text.Json.JsonEncodedText.Encode("generation");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropNumDocs = System.Text.Json.JsonEncodedText.Encode("num_docs");
	private static readonly System.Text.Json.JsonEncodedText PropUserData = System.Text.Json.JsonEncodedText.Encode("user_data");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardCommit Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propGeneration = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<long> propNumDocs = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propUserData = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propGeneration.TryReadProperty(ref reader, options, PropGeneration, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propNumDocs.TryReadProperty(ref reader, options, PropNumDocs, null))
			{
				continue;
			}

			if (propUserData.TryReadProperty(ref reader, options, PropUserData, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardCommit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Generation = propGeneration.Value,
			Id = propId.Value,
			NumDocs = propNumDocs.Value,
			UserData = propUserData.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardCommit value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropGeneration, value.Generation, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropNumDocs, value.NumDocs, null, null);
		writer.WriteProperty(options, PropUserData, value.UserData, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardCommitConverter))]
public sealed partial class ShardCommit
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardCommit(int generation, string id, long numDocs, System.Collections.Generic.IReadOnlyDictionary<string, string> userData)
	{
		Generation = generation;
		Id = id;
		NumDocs = numDocs;
		UserData = userData;
	}
#if NET7_0_OR_GREATER
	public ShardCommit()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ShardCommit()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardCommit(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Generation { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long NumDocs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> UserData { get; set; }
}