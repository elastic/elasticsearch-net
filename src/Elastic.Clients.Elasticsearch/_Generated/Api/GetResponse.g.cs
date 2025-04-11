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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class GetResponseConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.GetResponse<TDocument>>
{
	private static readonly System.Text.Json.JsonEncodedText PropFields = System.Text.Json.JsonEncodedText.Encode("fields");
	private static readonly System.Text.Json.JsonEncodedText PropFound = System.Text.Json.JsonEncodedText.Encode("found");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("_id");
	private static readonly System.Text.Json.JsonEncodedText PropIgnored = System.Text.Json.JsonEncodedText.Encode("_ignored");
	private static readonly System.Text.Json.JsonEncodedText PropIndex = System.Text.Json.JsonEncodedText.Encode("_index");
	private static readonly System.Text.Json.JsonEncodedText PropPrimaryTerm = System.Text.Json.JsonEncodedText.Encode("_primary_term");
	private static readonly System.Text.Json.JsonEncodedText PropRouting = System.Text.Json.JsonEncodedText.Encode("_routing");
	private static readonly System.Text.Json.JsonEncodedText PropSeqNo = System.Text.Json.JsonEncodedText.Encode("_seq_no");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("_source");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("_version");

	public override Elastic.Clients.Elasticsearch.GetResponse<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propFields = default;
		LocalJsonValue<bool> propFound = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propIgnored = default;
		LocalJsonValue<string> propIndex = default;
		LocalJsonValue<long?> propPrimaryTerm = default;
		LocalJsonValue<string?> propRouting = default;
		LocalJsonValue<long?> propSeqNo = default;
		LocalJsonValue<TDocument?> propSource = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFields.TryReadProperty(ref reader, options, PropFields, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propFound.TryReadProperty(ref reader, options, PropFound, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIgnored.TryReadProperty(ref reader, options, PropIgnored, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propIndex.TryReadProperty(ref reader, options, PropIndex, null))
			{
				continue;
			}

			if (propPrimaryTerm.TryReadProperty(ref reader, options, PropPrimaryTerm, null))
			{
				continue;
			}

			if (propRouting.TryReadProperty(ref reader, options, PropRouting, null))
			{
				continue;
			}

			if (propSeqNo.TryReadProperty(ref reader, options, PropSeqNo, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, static TDocument? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<TDocument?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.SourceMarker<TDocument?>))))
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
		return new Elastic.Clients.Elasticsearch.GetResponse<TDocument>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Fields = propFields.Value,
			Found = propFound.Value,
			Id = propId.Value,
			Ignored = propIgnored.Value,
			Index = propIndex.Value,
			PrimaryTerm = propPrimaryTerm.Value,
			Routing = propRouting.Value,
			SeqNo = propSeqNo.Value,
			Source = propSource.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.GetResponse<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFields, value.Fields, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropFound, value.Found, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIgnored, value.Ignored, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropIndex, value.Index, null, null);
		writer.WriteProperty(options, PropPrimaryTerm, value.PrimaryTerm, null, null);
		writer.WriteProperty(options, PropRouting, value.Routing, null, null);
		writer.WriteProperty(options, PropSeqNo, value.SeqNo, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, TDocument? v) => w.WriteValueEx<TDocument?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.SourceMarker<TDocument?>)));
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

internal sealed partial class GetResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(GetResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(GetResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.GetResponseConverterFactory))]
public sealed partial class GetResponse<TDocument> : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If the <c>stored_fields</c> parameter is set to <c>true</c> and <c>found</c> is <c>true</c>, it contains the document fields stored in the index.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Fields { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether the document exists.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Found { get; set; }

	/// <summary>
	/// <para>
	/// The unique identifier for the document.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<string>? Ignored { get; set; }

	/// <summary>
	/// <para>
	/// The name of the index the document belongs to.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Index { get; set; }

	/// <summary>
	/// <para>
	/// The primary term assigned to the document for the indexing operation.
	/// </para>
	/// </summary>
	public long? PrimaryTerm { get; set; }

	/// <summary>
	/// <para>
	/// The explicit routing, if set.
	/// </para>
	/// </summary>
	public string? Routing { get; set; }

	/// <summary>
	/// <para>
	/// The sequence number assigned to the document for the indexing operation.
	/// Sequence numbers are used to ensure an older version of a document doesn't overwrite a newer version.
	/// </para>
	/// </summary>
	public long? SeqNo { get; set; }

	/// <summary>
	/// <para>
	/// If <c>found</c> is <c>true</c>, it contains the document data formatted in JSON.
	/// If the <c>_source</c> parameter is set to <c>false</c> or the <c>stored_fields</c> parameter is set to <c>true</c>, it is excluded.
	/// </para>
	/// </summary>
	public TDocument? Source { get; set; }

	/// <summary>
	/// <para>
	/// The document version, which is ncremented each time the document is updated.
	/// </para>
	/// </summary>
	public long? Version { get; set; }
}