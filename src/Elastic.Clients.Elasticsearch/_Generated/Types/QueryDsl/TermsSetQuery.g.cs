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

internal sealed partial class TermsSetQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatch = System.Text.Json.JsonEncodedText.Encode("minimum_should_match");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatchField = System.Text.Json.JsonEncodedText.Encode("minimum_should_match_field");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatchScript = System.Text.Json.JsonEncodedText.Encode("minimum_should_match_script");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropTerms = System.Text.Json.JsonEncodedText.Encode("terms");

	public override Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, static Elastic.Clients.Elasticsearch.Field (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadPropertyName<Elastic.Clients.Elasticsearch.Field>(o));
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MinimumShouldMatch?> propMinimumShouldMatch = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propMinimumShouldMatchField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propMinimumShouldMatchScript = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>> propTerms = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, static float? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<float>(o)))
			{
				continue;
			}

			if (propMinimumShouldMatch.TryReadProperty(ref reader, options, PropMinimumShouldMatch, null))
			{
				continue;
			}

			if (propMinimumShouldMatchField.TryReadProperty(ref reader, options, PropMinimumShouldMatchField, null))
			{
				continue;
			}

			if (propMinimumShouldMatchScript.TryReadProperty(ref reader, options, PropMinimumShouldMatchScript, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propTerms.TryReadProperty(ref reader, options, PropTerms, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Field = propField.Value,
			MinimumShouldMatch = propMinimumShouldMatch.Value,
			MinimumShouldMatchField = propMinimumShouldMatchField.Value,
			MinimumShouldMatchScript = propMinimumShouldMatchScript.Value,
			QueryName = propQueryName.Value,
			Terms = propTerms.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Field v) => w.WritePropertyName<Elastic.Clients.Elasticsearch.Field>(o, v));
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, float? v) => w.WriteNullableValue<float>(o, v));
		writer.WriteProperty(options, PropMinimumShouldMatch, value.MinimumShouldMatch, null, null);
		writer.WriteProperty(options, PropMinimumShouldMatchField, value.MinimumShouldMatchField, null, null);
		writer.WriteProperty(options, PropMinimumShouldMatchScript, value.MinimumShouldMatchScript, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropTerms, value.Terms, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null));
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryConverter))]
public sealed partial class TermsSetQuery
{
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TermsSetQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsSetQuery(Elastic.Clients.Elasticsearch.Field field, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> terms)
	{
		Field = field;
		Terms = terms;
	}
#if NET7_0_OR_GREATER
	public TermsSetQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatch { get; set; }

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchField { get; set; }

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScript { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> Terms { get; set; }
}

public readonly partial struct TermsSetQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsSetQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsSetQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? value)
	{
		Instance.MinimumShouldMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatchField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.MinimumShouldMatchField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatchField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.MinimumShouldMatchField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.MinimumShouldMatchScript = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript()
	{
		Instance.MinimumShouldMatchScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.MinimumShouldMatchScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> Terms(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Terms = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument> Terms(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Terms = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct TermsSetQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsSetQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsSetQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? value)
	{
		Instance.MinimumShouldMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatchField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.MinimumShouldMatchField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatchField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.MinimumShouldMatchField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.Script? value)
	{
		Instance.MinimumShouldMatchScript = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatchScript()
	{
		Instance.MinimumShouldMatchScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor MinimumShouldMatchScript(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.MinimumShouldMatchScript = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor Terms(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue> value)
	{
		Instance.Terms = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor Terms(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Terms = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.TermsSetQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}