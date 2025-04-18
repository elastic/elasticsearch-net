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

internal sealed partial class IntervalsWildcardConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropPattern = System.Text.Json.JsonEncodedText.Encode("pattern");
	private static readonly System.Text.Json.JsonEncodedText PropUseField = System.Text.Json.JsonEncodedText.Encode("use_field");

	public override Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAnalyzer = default;
		LocalJsonValue<string> propPattern = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propUseField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propPattern.TryReadProperty(ref reader, options, PropPattern, null))
			{
				continue;
			}

			if (propUseField.TryReadProperty(ref reader, options, PropUseField, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Pattern = propPattern.Value,
			UseField = propUseField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropPattern, value.Pattern, null, null);
		writer.WriteProperty(options, PropUseField, value.UseField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardConverter))]
public sealed partial class IntervalsWildcard
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsWildcard(string pattern)
	{
		Pattern = pattern;
	}
#if NET7_0_OR_GREATER
	public IntervalsWildcard()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public IntervalsWildcard()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Analyzer used to analyze the <c>pattern</c>.
	/// Defaults to the top-level field's analyzer.
	/// </para>
	/// </summary>
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>
	/// Wildcard pattern used to find matching terms.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Pattern { get; set; }

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>pattern</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? UseField { get; set; }
}

public readonly partial struct IntervalsWildcardDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsWildcardDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsWildcardDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to analyze the <c>pattern</c>.
	/// Defaults to the top-level field's analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern used to find matching terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument> Pattern(string value)
	{
		Instance.Pattern = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>pattern</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument> UseField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.UseField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>pattern</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument> UseField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.UseField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct IntervalsWildcardDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsWildcardDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsWildcardDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to analyze the <c>pattern</c>.
	/// Defaults to the top-level field's analyzer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern used to find matching terms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor Pattern(string value)
	{
		Instance.Pattern = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>pattern</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor UseField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.UseField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>pattern</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor UseField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.UseField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcardDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsWildcard(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}