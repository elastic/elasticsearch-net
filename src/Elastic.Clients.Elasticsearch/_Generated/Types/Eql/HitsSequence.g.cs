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

namespace Elastic.Clients.Elasticsearch.Eql;

internal sealed partial class HitsSequenceConverter<TEvent> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Eql.HitsSequence<TEvent>>
{
	private static readonly System.Text.Json.JsonEncodedText PropEvents = System.Text.Json.JsonEncodedText.Encode("events");
	private static readonly System.Text.Json.JsonEncodedText PropJoinKeys = System.Text.Json.JsonEncodedText.Encode("join_keys");

	public override Elastic.Clients.Elasticsearch.Eql.HitsSequence<TEvent> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>>> propEvents = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<object>?> propJoinKeys = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEvents.TryReadProperty(ref reader, options, PropEvents, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>>(o, null)!))
			{
				continue;
			}

			if (propJoinKeys.TryReadProperty(ref reader, options, PropJoinKeys, static System.Collections.Generic.IReadOnlyCollection<object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<object>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Eql.HitsSequence<TEvent>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Events = propEvents.Value,
			JoinKeys = propJoinKeys.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.HitsSequence<TEvent> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEvents, value.Events, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>>(o, v, null));
		writer.WriteProperty(options, PropJoinKeys, value.JoinKeys, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<object>? v) => w.WriteCollectionValue<object>(o, v, null));
		writer.WriteEndObject();
	}
}

internal sealed partial class HitsSequenceConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(HitsSequence<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(HitsSequenceConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Eql.HitsSequenceConverterFactory))]
public sealed partial class HitsSequence<TEvent>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HitsSequence(System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>> events)
	{
		Events = events;
	}
#if NET7_0_OR_GREATER
	public HitsSequence()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public HitsSequence()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HitsSequence(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Contains events matching the query. Each object represents a matching event.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Eql.HitsEvent<TEvent>> Events { get; set; }

	/// <summary>
	/// <para>
	/// Shared field values used to constrain matches in the sequence. These are defined using the by keyword in the EQL query syntax.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<object>? JoinKeys { get; set; }
}