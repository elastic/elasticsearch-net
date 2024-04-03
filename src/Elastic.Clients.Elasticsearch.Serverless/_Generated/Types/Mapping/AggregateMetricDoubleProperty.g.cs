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

namespace Elastic.Clients.Elasticsearch.Serverless.Mapping;

public sealed partial class AggregateMetricDoubleProperty : IProperty
{
	[JsonInclude, JsonPropertyName("default_metric")]
	public string DefaultMetric { get; set; }
	[JsonInclude, JsonPropertyName("dynamic")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? Dynamic { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? Fields { get; set; }
	[JsonInclude, JsonPropertyName("ignore_above")]
	public int? IgnoreAbove { get; set; }

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("meta")]
	public IDictionary<string, string>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("metrics")]
	public ICollection<string> Metrics { get; set; }
	[JsonInclude, JsonPropertyName("properties")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? Properties { get; set; }
	[JsonInclude, JsonPropertyName("time_series_metric")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.TimeSeriesMetricType? TimeSeriesMetric { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "aggregate_metric_double";
}

public sealed partial class AggregateMetricDoublePropertyDescriptor<TDocument> : SerializableDescriptor<AggregateMetricDoublePropertyDescriptor<TDocument>>, IBuildableDescriptor<AggregateMetricDoubleProperty>
{
	internal AggregateMetricDoublePropertyDescriptor(Action<AggregateMetricDoublePropertyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public AggregateMetricDoublePropertyDescriptor() : base()
	{
	}

	private string DefaultMetricValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private ICollection<string> MetricsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

	public AggregateMetricDoublePropertyDescriptor<TDocument> DefaultMetric(string defaultMetric)
	{
		DefaultMetricValue = defaultMetric;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	public AggregateMetricDoublePropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Metrics(ICollection<string> metrics)
	{
		MetricsValue = metrics;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor<TDocument> TimeSeriesMetric(Elastic.Clients.Elasticsearch.Serverless.Mapping.TimeSeriesMetricType? timeSeriesMetric)
	{
		TimeSeriesMetricValue = timeSeriesMetric;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("default_metric");
		writer.WriteStringValue(DefaultMetricValue);
		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (IgnoreAboveValue.HasValue)
		{
			writer.WritePropertyName("ignore_above");
			writer.WriteNumberValue(IgnoreAboveValue.Value);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		writer.WritePropertyName("metrics");
		JsonSerializer.Serialize(writer, MetricsValue, options);
		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (TimeSeriesMetricValue is not null)
		{
			writer.WritePropertyName("time_series_metric");
			JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("aggregate_metric_double");
		writer.WriteEndObject();
	}

	AggregateMetricDoubleProperty IBuildableDescriptor<AggregateMetricDoubleProperty>.Build() => new()
	{
		DefaultMetric = DefaultMetricValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Metrics = MetricsValue,
		Properties = PropertiesValue,
		TimeSeriesMetric = TimeSeriesMetricValue
	};
}

public sealed partial class AggregateMetricDoublePropertyDescriptor : SerializableDescriptor<AggregateMetricDoublePropertyDescriptor>, IBuildableDescriptor<AggregateMetricDoubleProperty>
{
	internal AggregateMetricDoublePropertyDescriptor(Action<AggregateMetricDoublePropertyDescriptor> configure) => configure.Invoke(this);

	public AggregateMetricDoublePropertyDescriptor() : base()
	{
	}

	private string DefaultMetricValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? DynamicValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? FieldsValue { get; set; }
	private int? IgnoreAboveValue { get; set; }
	private IDictionary<string, string>? MetaValue { get; set; }
	private ICollection<string> MetricsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? PropertiesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TimeSeriesMetricType? TimeSeriesMetricValue { get; set; }

	public AggregateMetricDoublePropertyDescriptor DefaultMetric(string defaultMetric)
	{
		DefaultMetricValue = defaultMetric;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Serverless.Mapping.DynamicMapping? dynamic)
	{
		DynamicValue = dynamic;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Fields<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Fields<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		FieldsValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor IgnoreAbove(int? ignoreAbove)
	{
		IgnoreAboveValue = ignoreAbove;
		return Self;
	}

	/// <summary>
	/// <para>Metadata about the field.</para>
	/// </summary>
	public AggregateMetricDoublePropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Metrics(ICollection<string> metrics)
	{
		MetricsValue = metrics;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Serverless.Mapping.Properties? properties)
	{
		PropertiesValue = properties;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Properties<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument> descriptor)
	{
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor Properties<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>> configure)
	{
		var descriptor = new Elastic.Clients.Elasticsearch.Serverless.Mapping.PropertiesDescriptor<TDocument>();
		configure?.Invoke(descriptor);
		PropertiesValue = descriptor.PromisedValue;
		return Self;
	}

	public AggregateMetricDoublePropertyDescriptor TimeSeriesMetric(Elastic.Clients.Elasticsearch.Serverless.Mapping.TimeSeriesMetricType? timeSeriesMetric)
	{
		TimeSeriesMetricValue = timeSeriesMetric;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("default_metric");
		writer.WriteStringValue(DefaultMetricValue);
		if (DynamicValue is not null)
		{
			writer.WritePropertyName("dynamic");
			JsonSerializer.Serialize(writer, DynamicValue, options);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (IgnoreAboveValue.HasValue)
		{
			writer.WritePropertyName("ignore_above");
			writer.WriteNumberValue(IgnoreAboveValue.Value);
		}

		if (MetaValue is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		writer.WritePropertyName("metrics");
		JsonSerializer.Serialize(writer, MetricsValue, options);
		if (PropertiesValue is not null)
		{
			writer.WritePropertyName("properties");
			JsonSerializer.Serialize(writer, PropertiesValue, options);
		}

		if (TimeSeriesMetricValue is not null)
		{
			writer.WritePropertyName("time_series_metric");
			JsonSerializer.Serialize(writer, TimeSeriesMetricValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("aggregate_metric_double");
		writer.WriteEndObject();
	}

	AggregateMetricDoubleProperty IBuildableDescriptor<AggregateMetricDoubleProperty>.Build() => new()
	{
		DefaultMetric = DefaultMetricValue,
		Dynamic = DynamicValue,
		Fields = FieldsValue,
		IgnoreAbove = IgnoreAboveValue,
		Meta = MetaValue,
		Metrics = MetricsValue,
		Properties = PropertiesValue,
		TimeSeriesMetric = TimeSeriesMetricValue
	};
}