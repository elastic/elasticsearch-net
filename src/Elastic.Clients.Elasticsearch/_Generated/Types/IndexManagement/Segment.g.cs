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

internal sealed partial class SegmentConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.Segment>
{
	private static readonly System.Text.Json.JsonEncodedText PropAttributes = System.Text.Json.JsonEncodedText.Encode("attributes");
	private static readonly System.Text.Json.JsonEncodedText PropCommitted = System.Text.Json.JsonEncodedText.Encode("committed");
	private static readonly System.Text.Json.JsonEncodedText PropCompound = System.Text.Json.JsonEncodedText.Encode("compound");
	private static readonly System.Text.Json.JsonEncodedText PropDeletedDocs = System.Text.Json.JsonEncodedText.Encode("deleted_docs");
	private static readonly System.Text.Json.JsonEncodedText PropGeneration = System.Text.Json.JsonEncodedText.Encode("generation");
	private static readonly System.Text.Json.JsonEncodedText PropNumDocs = System.Text.Json.JsonEncodedText.Encode("num_docs");
	private static readonly System.Text.Json.JsonEncodedText PropSearch = System.Text.Json.JsonEncodedText.Encode("search");
	private static readonly System.Text.Json.JsonEncodedText PropSizeInBytes = System.Text.Json.JsonEncodedText.Encode("size_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.IndexManagement.Segment Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propAttributes = default;
		LocalJsonValue<bool> propCommitted = default;
		LocalJsonValue<bool> propCompound = default;
		LocalJsonValue<long> propDeletedDocs = default;
		LocalJsonValue<int> propGeneration = default;
		LocalJsonValue<long> propNumDocs = default;
		LocalJsonValue<bool> propSearch = default;
		LocalJsonValue<double> propSizeInBytes = default;
		LocalJsonValue<string> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAttributes.TryReadProperty(ref reader, options, PropAttributes, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
			{
				continue;
			}

			if (propCommitted.TryReadProperty(ref reader, options, PropCommitted, null))
			{
				continue;
			}

			if (propCompound.TryReadProperty(ref reader, options, PropCompound, null))
			{
				continue;
			}

			if (propDeletedDocs.TryReadProperty(ref reader, options, PropDeletedDocs, null))
			{
				continue;
			}

			if (propGeneration.TryReadProperty(ref reader, options, PropGeneration, null))
			{
				continue;
			}

			if (propNumDocs.TryReadProperty(ref reader, options, PropNumDocs, null))
			{
				continue;
			}

			if (propSearch.TryReadProperty(ref reader, options, PropSearch, null))
			{
				continue;
			}

			if (propSizeInBytes.TryReadProperty(ref reader, options, PropSizeInBytes, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.Segment(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Attributes = propAttributes.Value,
			Committed = propCommitted.Value,
			Compound = propCompound.Value,
			DeletedDocs = propDeletedDocs.Value,
			Generation = propGeneration.Value,
			NumDocs = propNumDocs.Value,
			Search = propSearch.Value,
			SizeInBytes = propSizeInBytes.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.Segment value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAttributes, value.Attributes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropCommitted, value.Committed, null, null);
		writer.WriteProperty(options, PropCompound, value.Compound, null, null);
		writer.WriteProperty(options, PropDeletedDocs, value.DeletedDocs, null, null);
		writer.WriteProperty(options, PropGeneration, value.Generation, null, null);
		writer.WriteProperty(options, PropNumDocs, value.NumDocs, null, null);
		writer.WriteProperty(options, PropSearch, value.Search, null, null);
		writer.WriteProperty(options, PropSizeInBytes, value.SizeInBytes, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SegmentConverter))]
public sealed partial class Segment
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Segment(System.Collections.Generic.IReadOnlyDictionary<string, string> attributes, bool committed, bool compound, long deletedDocs, int generation, long numDocs, bool search, double sizeInBytes, string version)
	{
		Attributes = attributes;
		Committed = committed;
		Compound = compound;
		DeletedDocs = deletedDocs;
		Generation = generation;
		NumDocs = numDocs;
		Search = search;
		SizeInBytes = sizeInBytes;
		Version = version;
	}
#if NET7_0_OR_GREATER
	public Segment()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Segment()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Segment(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> Attributes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Committed { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Compound { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long DeletedDocs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Generation { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long NumDocs { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Search { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double SizeInBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Version { get; set; }
}