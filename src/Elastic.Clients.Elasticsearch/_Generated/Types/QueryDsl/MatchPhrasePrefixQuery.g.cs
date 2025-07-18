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

internal sealed partial class MatchPhrasePrefixQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropMaxExpansions = System.Text.Json.JsonEncodedText.Encode("max_expansions");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropSlop = System.Text.Json.JsonEncodedText.Encode("slop");
	private static readonly System.Text.Json.JsonEncodedText PropZeroTermsQuery = System.Text.Json.JsonEncodedText.Encode("zero_terms_query");

	public override Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, static Elastic.Clients.Elasticsearch.Field (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadPropertyName<Elastic.Clients.Elasticsearch.Field>(o));
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<string>(options, null);
			reader.Read();
			return new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
			{
				Field = propField.Value,
				Query = value
			};
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAnalyzer = default;
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<int?> propMaxExpansions = default;
		LocalJsonValue<string> propQuery = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<int?> propSlop = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery?> propZeroTermsQuery = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propBoost.TryReadProperty(ref reader, options, PropBoost, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propMaxExpansions.TryReadProperty(ref reader, options, PropMaxExpansions, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propSlop.TryReadProperty(ref reader, options, PropSlop, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propZeroTermsQuery.TryReadProperty(ref reader, options, PropZeroTermsQuery, static Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery>(o)))
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
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Boost = propBoost.Value,
			Field = propField.Value,
			MaxExpansions = propMaxExpansions.Value,
			Query = propQuery.Value,
			QueryName = propQueryName.Value,
			Slop = propSlop.Value,
			ZeroTermsQuery = propZeroTermsQuery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Field v) => w.WritePropertyName<Elastic.Clients.Elasticsearch.Field>(o, v));
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropBoost, value.Boost, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropMaxExpansions, value.MaxExpansions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropSlop, value.Slop, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropZeroTermsQuery, value.ZeroTermsQuery, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? v) => w.WriteNullableValue<Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery>(o, v));
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryConverter))]
public sealed partial class MatchPhrasePrefixQuery
{
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Field field, string query)
	{
		Field = field;
		Query = query;
	}
#if NET7_0_OR_GREATER
	public MatchPhrasePrefixQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Analyzer used to convert text in the query value into tokens.
	/// </para>
	/// </summary>
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of terms to which the last provided term of the query value will expand.
	/// </para>
	/// </summary>
	public int? MaxExpansions { get; set; }

	/// <summary>
	/// <para>
	/// Text you wish to find in the provided field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Query { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of positions allowed between matching tokens.
	/// </para>
	/// </summary>
	public int? Slop { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether no documents are returned if the analyzer removes all tokens, such as when using a <c>stop</c> filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQuery { get; set; }
}

public readonly partial struct MatchPhrasePrefixQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchPhrasePrefixQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchPhrasePrefixQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to convert text in the query value into tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of terms to which the last provided term of the query value will expand.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> MaxExpansions(int? value)
	{
		Instance.MaxExpansions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Text you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of positions allowed between matching tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> Slop(int? value)
	{
		Instance.Slop = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether no documents are returned if the analyzer removes all tokens, such as when using a <c>stop</c> filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument> ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? value)
	{
		Instance.ZeroTermsQuery = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct MatchPhrasePrefixQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchPhrasePrefixQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchPhrasePrefixQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to convert text in the query value into tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of terms to which the last provided term of the query value will expand.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor MaxExpansions(int? value)
	{
		Instance.MaxExpansions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Text you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of positions allowed between matching tokens.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor Slop(int? value)
	{
		Instance.Slop = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether no documents are returned if the analyzer removes all tokens, such as when using a <c>stop</c> filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? value)
	{
		Instance.ZeroTermsQuery = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.MatchPhrasePrefixQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}