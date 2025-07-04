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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class SpanFirstQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropEnd = System.Text.Json.JsonEncodedText.Encode("end");
	private static readonly System.Text.Json.JsonEncodedText PropMatch = System.Text.Json.JsonEncodedText.Encode("match");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");

	public override Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<int> propEnd = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery> propMatch = default;
		LocalJsonValue<string?> propQueryName = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propEnd.TryReadProperty(ref reader, options, PropEnd, null))
			{
				continue;
			}

			if (propMatch.TryReadProperty(ref reader, options, PropMatch, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			End = propEnd.Value,
			Match = propMatch.Value,
			QueryName = propQueryName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropEnd, value.End, null, null);
		writer.WriteProperty(options, PropMatch, value.Match, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryConverter))]
public sealed partial class SpanFirstQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanFirstQuery(int end, Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery match)
	{
		End = end;
		Match = match;
	}
#if NET7_0_OR_GREATER
	public SpanFirstQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SpanFirstQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// Controls the maximum end position permitted in a match.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int End { get; set; }

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery Match { get; set; }
	public string? QueryName { get; set; }
}

public readonly partial struct SpanFirstQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanFirstQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanFirstQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls the maximum end position permitted in a match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> End(int value)
	{
		Instance.End = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> Match(Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery value)
	{
		Instance.Match = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> Match(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct SpanFirstQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanFirstQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SpanFirstQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls the maximum end position permitted in a match.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor End(int value)
	{
		Instance.End = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor Match(Elastic.Clients.Elasticsearch.QueryDsl.SpanQuery value)
	{
		Instance.Match = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor Match(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Can be any other span type query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor Match<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>> action)
	{
		Instance.Match = Elastic.Clients.Elasticsearch.QueryDsl.SpanQueryDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.SpanFirstQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}