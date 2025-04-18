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

internal sealed partial class FieldSummaryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.FieldSummary>
{
	private static readonly System.Text.Json.JsonEncodedText PropAny = System.Text.Json.JsonEncodedText.Encode("any");
	private static readonly System.Text.Json.JsonEncodedText PropDocValues = System.Text.Json.JsonEncodedText.Encode("doc_values");
	private static readonly System.Text.Json.JsonEncodedText PropInvertedIndex = System.Text.Json.JsonEncodedText.Encode("inverted_index");
	private static readonly System.Text.Json.JsonEncodedText PropKnnVectors = System.Text.Json.JsonEncodedText.Encode("knn_vectors");
	private static readonly System.Text.Json.JsonEncodedText PropNorms = System.Text.Json.JsonEncodedText.Encode("norms");
	private static readonly System.Text.Json.JsonEncodedText PropPoints = System.Text.Json.JsonEncodedText.Encode("points");
	private static readonly System.Text.Json.JsonEncodedText PropStoredFields = System.Text.Json.JsonEncodedText.Encode("stored_fields");
	private static readonly System.Text.Json.JsonEncodedText PropTermVectors = System.Text.Json.JsonEncodedText.Encode("term_vectors");

	public override Elastic.Clients.Elasticsearch.IndexManagement.FieldSummary Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propAny = default;
		LocalJsonValue<int> propDocValues = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.InvertedIndex> propInvertedIndex = default;
		LocalJsonValue<int> propKnnVectors = default;
		LocalJsonValue<int> propNorms = default;
		LocalJsonValue<int> propPoints = default;
		LocalJsonValue<int> propStoredFields = default;
		LocalJsonValue<int> propTermVectors = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAny.TryReadProperty(ref reader, options, PropAny, null))
			{
				continue;
			}

			if (propDocValues.TryReadProperty(ref reader, options, PropDocValues, null))
			{
				continue;
			}

			if (propInvertedIndex.TryReadProperty(ref reader, options, PropInvertedIndex, null))
			{
				continue;
			}

			if (propKnnVectors.TryReadProperty(ref reader, options, PropKnnVectors, null))
			{
				continue;
			}

			if (propNorms.TryReadProperty(ref reader, options, PropNorms, null))
			{
				continue;
			}

			if (propPoints.TryReadProperty(ref reader, options, PropPoints, null))
			{
				continue;
			}

			if (propStoredFields.TryReadProperty(ref reader, options, PropStoredFields, null))
			{
				continue;
			}

			if (propTermVectors.TryReadProperty(ref reader, options, PropTermVectors, null))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.FieldSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Any = propAny.Value,
			DocValues = propDocValues.Value,
			InvertedIndex = propInvertedIndex.Value,
			KnnVectors = propKnnVectors.Value,
			Norms = propNorms.Value,
			Points = propPoints.Value,
			StoredFields = propStoredFields.Value,
			TermVectors = propTermVectors.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.FieldSummary value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAny, value.Any, null, null);
		writer.WriteProperty(options, PropDocValues, value.DocValues, null, null);
		writer.WriteProperty(options, PropInvertedIndex, value.InvertedIndex, null, null);
		writer.WriteProperty(options, PropKnnVectors, value.KnnVectors, null, null);
		writer.WriteProperty(options, PropNorms, value.Norms, null, null);
		writer.WriteProperty(options, PropPoints, value.Points, null, null);
		writer.WriteProperty(options, PropStoredFields, value.StoredFields, null, null);
		writer.WriteProperty(options, PropTermVectors, value.TermVectors, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.FieldSummaryConverter))]
public sealed partial class FieldSummary
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldSummary(int any, int docValues, Elastic.Clients.Elasticsearch.IndexManagement.InvertedIndex invertedIndex, int knnVectors, int norms, int points, int storedFields, int termVectors)
	{
		Any = any;
		DocValues = docValues;
		InvertedIndex = invertedIndex;
		KnnVectors = knnVectors;
		Norms = norms;
		Points = points;
		StoredFields = storedFields;
		TermVectors = termVectors;
	}
#if NET7_0_OR_GREATER
	public FieldSummary()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FieldSummary()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldSummary(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Any { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DocValues { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.InvertedIndex InvertedIndex { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int KnnVectors { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Norms { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Points { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int StoredFields { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TermVectors { get; set; }
}