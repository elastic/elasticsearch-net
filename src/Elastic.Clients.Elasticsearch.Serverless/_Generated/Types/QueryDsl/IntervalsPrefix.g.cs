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

public sealed partial class IntervalsPrefix
{
	/// <summary>
	/// <para>Analyzer used to analyze the `prefix`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>Beginning characters of terms you wish to find in the top-level field.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix")]
	public string Prefix { get; set; }

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("use_field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field? UseField { get; set; }
}

public sealed partial class IntervalsPrefixDescriptor<TDocument> : SerializableDescriptor<IntervalsPrefixDescriptor<TDocument>>
{
	internal IntervalsPrefixDescriptor(Action<IntervalsPrefixDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IntervalsPrefixDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private string PrefixValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? UseFieldValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to analyze the `prefix`.</para>
	/// </summary>
	public IntervalsPrefixDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>Beginning characters of terms you wish to find in the top-level field.</para>
	/// </summary>
	public IntervalsPrefixDescriptor<TDocument> Prefix(string prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsPrefixDescriptor<TDocument> UseField(Elastic.Clients.Elasticsearch.Serverless.Field? useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsPrefixDescriptor<TDocument> UseField<TValue>(Expression<Func<TDocument, TValue>> useField)
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

		writer.WritePropertyName("prefix");
		writer.WriteStringValue(PrefixValue);
		if (UseFieldValue is not null)
		{
			writer.WritePropertyName("use_field");
			JsonSerializer.Serialize(writer, UseFieldValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class IntervalsPrefixDescriptor : SerializableDescriptor<IntervalsPrefixDescriptor>
{
	internal IntervalsPrefixDescriptor(Action<IntervalsPrefixDescriptor> configure) => configure.Invoke(this);

	public IntervalsPrefixDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private string PrefixValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field? UseFieldValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to analyze the `prefix`.</para>
	/// </summary>
	public IntervalsPrefixDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>Beginning characters of terms you wish to find in the top-level field.</para>
	/// </summary>
	public IntervalsPrefixDescriptor Prefix(string prefix)
	{
		PrefixValue = prefix;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsPrefixDescriptor UseField(Elastic.Clients.Elasticsearch.Serverless.Field? useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsPrefixDescriptor UseField<TDocument, TValue>(Expression<Func<TDocument, TValue>> useField)
	{
		UseFieldValue = useField;
		return Self;
	}

	/// <summary>
	/// <para>If specified, match intervals from this field rather than the top-level field.<br/>The `prefix` is normalized using the search analyzer from this field, unless `analyzer` is specified separately.</para>
	/// </summary>
	public IntervalsPrefixDescriptor UseField<TDocument>(Expression<Func<TDocument, object>> useField)
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

		writer.WritePropertyName("prefix");
		writer.WriteStringValue(PrefixValue);
		if (UseFieldValue is not null)
		{
			writer.WritePropertyName("use_field");
			JsonSerializer.Serialize(writer, UseFieldValue, options);
		}

		writer.WriteEndObject();
	}
}