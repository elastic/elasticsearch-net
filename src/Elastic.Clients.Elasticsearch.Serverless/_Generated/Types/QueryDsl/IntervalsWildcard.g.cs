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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class IntervalsWildcard
{
	/// <summary>
	/// <para>Analyzer used to analyze the `pattern`.<br/>Defaults to the top-level field's analyzer.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>Wildcard pattern used to find matching terms.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pattern")]
	public string Pattern { get; set; }

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("use_field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field? UseField { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals(IntervalsWildcard intervalsWildcard) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Intervals.Wildcard(intervalsWildcard);
	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsQuery(IntervalsWildcard intervalsWildcard) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.IntervalsQuery.Wildcard(intervalsWildcard);
}

public sealed partial class IntervalsWildcardDescriptor<TDocument> : SerializableDescriptor<IntervalsWildcardDescriptor<TDocument>>
{
	internal IntervalsWildcardDescriptor(Action<IntervalsWildcardDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IntervalsWildcardDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private string PatternValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? UseFieldValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to analyze the `pattern`.<br/>Defaults to the top-level field's analyzer.</para>
	/// </summary>
	public IntervalsWildcardDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>Wildcard pattern used to find matching terms.</para>
	/// </summary>
	public IntervalsWildcardDescriptor<TDocument> Pattern(string pattern)
	{
		PatternValue = pattern;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor<TDocument> UseField(Elastic.Clients.Elasticsearch.Serverless.Field? useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor<TDocument> UseField<TValue>(Expression<Func<TDocument, TValue>> useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor<TDocument> UseField(Expression<Func<TDocument, object>> useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		writer.WritePropertyName("pattern");
		writer.WriteStringValue(PatternValue);
		if (UseFieldValue is not null)
		{
			writer.WritePropertyName("use_field");
			JsonSerializer.Serialize(writer, UseFieldValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsWildcardDescriptor : SerializableDescriptor<IntervalsWildcardDescriptor>
{
	internal IntervalsWildcardDescriptor(Action<IntervalsWildcardDescriptor> configure) => configure.Invoke(this);

	public IntervalsWildcardDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private string PatternValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? UseFieldValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to analyze the `pattern`.<br/>Defaults to the top-level field's analyzer.</para>
	/// </summary>
	public IntervalsWildcardDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>Wildcard pattern used to find matching terms.</para>
	/// </summary>
	public IntervalsWildcardDescriptor Pattern(string pattern)
	{
		PatternValue = pattern;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor UseField(Elastic.Clients.Elasticsearch.Serverless.Field? useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor UseField<TDocument, TValue>(Expression<Func<TDocument, TValue>> useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `pattern` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsWildcardDescriptor UseField<TDocument>(Expression<Func<TDocument, object>> useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		writer.WritePropertyName("pattern");
		writer.WriteStringValue(PatternValue);
		if (UseFieldValue is not null)
		{
			writer.WritePropertyName("use_field");
			JsonSerializer.Serialize(writer, UseFieldValue, options);
		}

		writer.WriteEndObject();
	}
}