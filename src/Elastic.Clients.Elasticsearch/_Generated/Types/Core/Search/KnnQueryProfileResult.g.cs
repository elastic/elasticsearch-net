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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class KnnQueryProfileResultConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>
{
	private static readonly System.Text.Json.JsonEncodedText PropBreakdown = System.Text.Json.JsonEncodedText.Encode("breakdown");
	private static readonly System.Text.Json.JsonEncodedText PropChildren = System.Text.Json.JsonEncodedText.Encode("children");
	private static readonly System.Text.Json.JsonEncodedText PropDebug = System.Text.Json.JsonEncodedText.Encode("debug");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropTime = System.Text.Json.JsonEncodedText.Encode("time");
	private static readonly System.Text.Json.JsonEncodedText PropTimeInNanos = System.Text.Json.JsonEncodedText.Encode("time_in_nanos");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown> propBreakdown = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>?> propChildren = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propDebug = default;
		LocalJsonValue<string> propDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTime = default;
		LocalJsonValue<System.TimeSpan> propTimeInNanos = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBreakdown.TryReadProperty(ref reader, options, PropBreakdown, null))
			{
				continue;
			}

			if (propChildren.TryReadProperty(ref reader, options, PropChildren, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>(o, null)))
			{
				continue;
			}

			if (propDebug.TryReadProperty(ref reader, options, PropDebug, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propTime.TryReadProperty(ref reader, options, PropTime, null))
			{
				continue;
			}

			if (propTimeInNanos.TryReadProperty(ref reader, options, PropTimeInNanos, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanNanosMarker))))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Breakdown = propBreakdown.Value,
			Children = propChildren.Value,
			Debug = propDebug.Value,
			Description = propDescription.Value,
			Time = propTime.Value,
			TimeInNanos = propTimeInNanos.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBreakdown, value.Breakdown, null, null);
		writer.WriteProperty(options, PropChildren, value.Children, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>(o, v, null));
		writer.WriteProperty(options, PropDebug, value.Debug, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropTime, value.Time, null, null);
		writer.WriteProperty(options, PropTimeInNanos, value.TimeInNanos, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanNanosMarker)));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResultConverter))]
public sealed partial class KnnQueryProfileResult
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryProfileResult(Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown breakdown, string description, System.TimeSpan timeInNanos, string type)
	{
		Breakdown = breakdown;
		Description = description;
		TimeInNanos = timeInNanos;
		Type = type;
	}
#if NET7_0_OR_GREATER
	public KnnQueryProfileResult()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KnnQueryProfileResult()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KnnQueryProfileResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown Breakdown { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileResult>? Children { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Debug { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Description { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? Time { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TimeInNanos { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Type { get; set; }
}