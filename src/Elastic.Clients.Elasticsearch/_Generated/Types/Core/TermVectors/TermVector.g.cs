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

namespace Elastic.Clients.Elasticsearch.Core.TermVectors;

internal sealed partial class TermVectorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector>
{
	private static readonly System.Text.Json.JsonEncodedText PropFieldStatistics = System.Text.Json.JsonEncodedText.Encode("field_statistics");
	private static readonly System.Text.Json.JsonEncodedText PropTerms = System.Text.Json.JsonEncodedText.Encode("terms");

	public override Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.TermVectors.FieldStatistics?> propFieldStatistics = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term>> propTerms = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFieldStatistics.TryReadProperty(ref reader, options, PropFieldStatistics, null))
			{
				continue;
			}

			if (propTerms.TryReadProperty(ref reader, options, PropTerms, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term>(o, null, null)!))
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
		return new Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FieldStatistics = propFieldStatistics.Value,
			Terms = propTerms.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.TermVectors.TermVector value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFieldStatistics, value.FieldStatistics, null, null);
		writer.WriteProperty(options, PropTerms, value.Terms, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term> v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term>(o, v, null, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.TermVectors.TermVectorConverter))]
public sealed partial class TermVector
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermVector(System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term> terms)
	{
		Terms = terms;
	}
#if NET7_0_OR_GREATER
	public TermVector()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TermVector()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TermVector(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.Core.TermVectors.FieldStatistics? FieldStatistics { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Core.TermVectors.Term> Terms { get; set; }
}