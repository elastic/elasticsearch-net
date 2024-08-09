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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class ModelPlotConfig
{
	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model change annotations for each entity that is being analyzed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("annotations_enabled")]
	public bool? AnnotationsEnabled { get; set; }

	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model bounds for each entity that is being analyzed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("enabled")]
	public bool? Enabled { get; set; }

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("terms")]
	public Elastic.Clients.Elasticsearch.Field? Terms { get; set; }
}

public sealed partial class ModelPlotConfigDescriptor<TDocument> : SerializableDescriptor<ModelPlotConfigDescriptor<TDocument>>
{
	internal ModelPlotConfigDescriptor(Action<ModelPlotConfigDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ModelPlotConfigDescriptor() : base()
	{
	}

	private bool? AnnotationsEnabledValue { get; set; }
	private bool? EnabledValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TermsValue { get; set; }

	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model change annotations for each entity that is being analyzed.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor<TDocument> AnnotationsEnabled(bool? annotationsEnabled = true)
	{
		AnnotationsEnabledValue = annotationsEnabled;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model bounds for each entity that is being analyzed.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor<TDocument> Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor<TDocument> Terms(Elastic.Clients.Elasticsearch.Field? terms)
	{
		TermsValue = terms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor<TDocument> Terms<TValue>(Expression<Func<TDocument, TValue>> terms)
	{
		TermsValue = terms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor<TDocument> Terms(Expression<Func<TDocument, object>> terms)
	{
		TermsValue = terms;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AnnotationsEnabledValue.HasValue)
		{
			writer.WritePropertyName("annotations_enabled");
			writer.WriteBooleanValue(AnnotationsEnabledValue.Value);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (TermsValue is not null)
		{
			writer.WritePropertyName("terms");
			JsonSerializer.Serialize(writer, TermsValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class ModelPlotConfigDescriptor : SerializableDescriptor<ModelPlotConfigDescriptor>
{
	internal ModelPlotConfigDescriptor(Action<ModelPlotConfigDescriptor> configure) => configure.Invoke(this);

	public ModelPlotConfigDescriptor() : base()
	{
	}

	private bool? AnnotationsEnabledValue { get; set; }
	private bool? EnabledValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? TermsValue { get; set; }

	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model change annotations for each entity that is being analyzed.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor AnnotationsEnabled(bool? annotationsEnabled = true)
	{
		AnnotationsEnabledValue = annotationsEnabled;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If true, enables calculation and storage of the model bounds for each entity that is being analyzed.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor Terms(Elastic.Clients.Elasticsearch.Field? terms)
	{
		TermsValue = terms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor Terms<TDocument, TValue>(Expression<Func<TDocument, TValue>> terms)
	{
		TermsValue = terms;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Limits data collection to this comma separated list of partition or by field values. If terms are not specified or it is an empty string, no filtering is applied. Wildcards are not supported. Only the specified terms can be viewed when using the Single Metric Viewer.
	/// </para>
	/// </summary>
	public ModelPlotConfigDescriptor Terms<TDocument>(Expression<Func<TDocument, object>> terms)
	{
		TermsValue = terms;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AnnotationsEnabledValue.HasValue)
		{
			writer.WritePropertyName("annotations_enabled");
			writer.WriteBooleanValue(AnnotationsEnabledValue.Value);
		}

		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (TermsValue is not null)
		{
			writer.WritePropertyName("terms");
			JsonSerializer.Serialize(writer, TermsValue, options);
		}

		writer.WriteEndObject();
	}
}