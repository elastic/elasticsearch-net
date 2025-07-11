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

internal sealed partial class WildcardQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRewrite = System.Text.Json.JsonEncodedText.Encode("rewrite");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");
	private static readonly System.Text.Json.JsonEncodedText PropWildcard = System.Text.Json.JsonEncodedText.Encode("wildcard");

	public override Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, static Elastic.Clients.Elasticsearch.Field (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadPropertyName<Elastic.Clients.Elasticsearch.Field>(o));
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<string?>(options, null);
			reader.Read();
			return new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
			{
				Field = propField.Value,
				Value = value
			};
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<string?> propRewrite = default;
		LocalJsonValue<string?> propValue = default;
		LocalJsonValue<string?> propWildcard = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propCaseInsensitive.TryReadProperty(ref reader, options, PropCaseInsensitive, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propRewrite.TryReadProperty(ref reader, options, PropRewrite, null))
			{
				continue;
			}

			if (propValue.TryReadProperty(ref reader, options, PropValue, null))
			{
				continue;
			}

			if (propWildcard.TryReadProperty(ref reader, options, PropWildcard, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			CaseInsensitive = propCaseInsensitive.Value,
			Field = propField.Value,
			QueryName = propQueryName.Value,
			Rewrite = propRewrite.Value,
			Value = propValue.Value,
			Wildcard = propWildcard.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Field v) => w.WritePropertyName<Elastic.Clients.Elasticsearch.Field>(o, v));
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropRewrite, value.Rewrite, null, null);
		writer.WriteProperty(options, PropValue, value.Value, null, null);
		writer.WriteProperty(options, PropWildcard, value.Wildcard, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryConverter))]
public sealed partial class WildcardQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WildcardQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public WildcardQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public bool? CaseInsensitive { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public string? Rewrite { get; set; }

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public string? Wildcard { get; set; }
}

public readonly partial struct WildcardQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WildcardQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WildcardQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> CaseInsensitive(bool? value = true)
	{
		Instance.CaseInsensitive = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Rewrite(string? value)
	{
		Instance.Rewrite = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Value(string? value)
	{
		Instance.Value = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument> Wildcard(string? value)
	{
		Instance.Wildcard = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct WildcardQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WildcardQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public WildcardQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor CaseInsensitive(bool? value = true)
	{
		Instance.CaseInsensitive = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Rewrite(string? value)
	{
		Instance.Rewrite = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Value(string? value)
	{
		Instance.Value = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor Wildcard(string? value)
	{
		Instance.Wildcard = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.WildcardQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}