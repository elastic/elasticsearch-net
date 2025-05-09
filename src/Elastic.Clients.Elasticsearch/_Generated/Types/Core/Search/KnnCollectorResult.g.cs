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

internal sealed partial class KnnCollectorResultConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>
{
	private static readonly System.Text.Json.JsonEncodedText PropChildren = System.Text.Json.JsonEncodedText.Encode("children");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropReason = System.Text.Json.JsonEncodedText.Encode("reason");
	private static readonly System.Text.Json.JsonEncodedText PropTime = System.Text.Json.JsonEncodedText.Encode("time");
	private static readonly System.Text.Json.JsonEncodedText PropTimeInNanos = System.Text.Json.JsonEncodedText.Encode("time_in_nanos");

	public override Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>?> propChildren = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<string> propReason = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTime = default;
		LocalJsonValue<System.TimeSpan> propTimeInNanos = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propChildren.TryReadProperty(ref reader, options, PropChildren, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>(o, null)))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propReason.TryReadProperty(ref reader, options, PropReason, null))
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Children = propChildren.Value,
			Name = propName.Value,
			Reason = propReason.Value,
			Time = propTime.Value,
			TimeInNanos = propTimeInNanos.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropChildren, value.Children, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>(o, v, null));
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropReason, value.Reason, null, null);
		writer.WriteProperty(options, PropTime, value.Time, null, null);
		writer.WriteProperty(options, PropTimeInNanos, value.TimeInNanos, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanNanosMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResultConverter))]
public sealed partial class KnnCollectorResult
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnCollectorResult(string name, string reason, System.TimeSpan timeInNanos)
	{
		Name = name;
		Reason = reason;
		TimeInNanos = timeInNanos;
	}
#if NET7_0_OR_GREATER
	public KnnCollectorResult()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KnnCollectorResult()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KnnCollectorResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Core.Search.KnnCollectorResult>? Children { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Reason { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? Time { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TimeInNanos { get; set; }
}