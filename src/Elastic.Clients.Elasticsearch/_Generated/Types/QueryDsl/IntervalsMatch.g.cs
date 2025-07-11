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

internal sealed partial class IntervalsMatchConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch>
{
	private static readonly System.Text.Json.JsonEncodedText PropAnalyzer = System.Text.Json.JsonEncodedText.Encode("analyzer");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropMaxGaps = System.Text.Json.JsonEncodedText.Encode("max_gaps");
	private static readonly System.Text.Json.JsonEncodedText PropOrdered = System.Text.Json.JsonEncodedText.Encode("ordered");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropUseField = System.Text.Json.JsonEncodedText.Encode("use_field");

	public override Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAnalyzer = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter?> propFilter = default;
		LocalJsonValue<int?> propMaxGaps = default;
		LocalJsonValue<bool?> propOrdered = default;
		LocalJsonValue<string> propQuery = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propUseField = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAnalyzer.TryReadProperty(ref reader, options, PropAnalyzer, null))
			{
				continue;
			}

			if (propFilter.TryReadProperty(ref reader, options, PropFilter, null))
			{
				continue;
			}

			if (propMaxGaps.TryReadProperty(ref reader, options, PropMaxGaps, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propOrdered.TryReadProperty(ref reader, options, PropOrdered, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
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
		return new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Analyzer = propAnalyzer.Value,
			Filter = propFilter.Value,
			MaxGaps = propMaxGaps.Value,
			Ordered = propOrdered.Value,
			Query = propQuery.Value,
			UseField = propUseField.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAnalyzer, value.Analyzer, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, null);
		writer.WriteProperty(options, PropMaxGaps, value.MaxGaps, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropOrdered, value.Ordered, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropUseField, value.UseField, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchConverter))]
public sealed partial class IntervalsMatch
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsMatch(string query)
	{
		Query = query;
	}
#if NET7_0_OR_GREATER
	public IntervalsMatch()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public IntervalsMatch()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Analyzer used to analyze terms in the query.
	/// </para>
	/// </summary>
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? Filter { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of positions between the matching terms.
	/// Terms further apart than this are not considered matches.
	/// </para>
	/// </summary>
	public int? MaxGaps { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, matching terms must appear in their specified order.
	/// </para>
	/// </summary>
	public bool? Ordered { get; set; }

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

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>term</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? UseField { get; set; }
}

public readonly partial struct IntervalsMatchDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsMatchDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsMatchDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to analyze terms in the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of positions between the matching terms.
	/// Terms further apart than this are not considered matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> MaxGaps(int? value)
	{
		Instance.MaxGaps = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, matching terms must appear in their specified order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> Ordered(bool? value = true)
	{
		Instance.Ordered = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Text you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>term</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> UseField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.UseField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>term</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument> UseField(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.UseField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct IntervalsMatchDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsMatchDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IntervalsMatchDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch instance) => new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Analyzer used to analyze terms in the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilter? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// An optional interval filter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<T>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.IntervalsFilterDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum number of positions between the matching terms.
	/// Terms further apart than this are not considered matches.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor MaxGaps(int? value)
	{
		Instance.MaxGaps = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, matching terms must appear in their specified order.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Ordered(bool? value = true)
	{
		Instance.Ordered = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Text you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>term</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor UseField(Elastic.Clients.Elasticsearch.Field? value)
	{
		Instance.UseField = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If specified, match intervals from this field rather than the top-level field.
	/// The <c>term</c> is normalized using the search analyzer from this field, unless <c>analyzer</c> is specified separately.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor UseField<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.UseField = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatchDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.IntervalsMatch(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}