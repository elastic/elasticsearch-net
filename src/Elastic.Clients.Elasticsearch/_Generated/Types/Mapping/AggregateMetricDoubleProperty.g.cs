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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Mapping
{
	public sealed partial class AggregateMetricDoubleProperty
	{
		[JsonInclude]
		[JsonPropertyName("default_metric")]
		public string DefaultMetric { get; set; }

		[JsonInclude]
		[JsonPropertyName("dynamic")]
		public Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? Dynamic { get; set; }

		[JsonInclude]
		[JsonPropertyName("fields")]
		public Elastic.Clients.Elasticsearch.Mapping.Properties? Fields { get; set; }

		[JsonInclude]
		[JsonPropertyName("ignore_above")]
		public int? IgnoreAbove { get; set; }

		[JsonInclude]
		[JsonPropertyName("local_metadata")]
		public Dictionary<string, object>? LocalMetadata { get; set; }

		[JsonInclude]
		[JsonPropertyName("meta")]
		public Dictionary<string, string>? Meta { get; set; }

		[JsonInclude]
		[JsonPropertyName("metrics")]
		public IEnumerable<string> Metrics { get; set; }

		[JsonInclude]
		[JsonPropertyName("properties")]
		public Elastic.Clients.Elasticsearch.Mapping.Properties? Properties { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type => "aggregate_metric_double";
	}

	public sealed partial class AggregateMetricDoublePropertyDescriptor<TDocument> : SerializableDescriptorBase<AggregateMetricDoublePropertyDescriptor<TDocument>>, IBuildableDescriptor<AggregateMetricDoubleProperty>
	{
		internal AggregateMetricDoublePropertyDescriptor(Action<AggregateMetricDoublePropertyDescriptor<TDocument>> configure) => configure.Invoke(this);
		public AggregateMetricDoublePropertyDescriptor() : base()
		{
		}

		private string DefaultMetricValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private IEnumerable<string> MetricsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		public AggregateMetricDoublePropertyDescriptor<TDocument> DefaultMetric(string defaultMetric)
		{
			DefaultMetricValue = defaultMetric;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Fields(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Metrics(IEnumerable<string> metrics)
		{
			MetricsValue = metrics;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor<TDocument> Properties(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
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

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
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

			writer.WritePropertyName("type");
			writer.WriteStringValue("aggregate_metric_double");
			writer.WriteEndObject();
		}

		AggregateMetricDoubleProperty IBuildableDescriptor<AggregateMetricDoubleProperty>.Build() => new()
		{ DefaultMetric = DefaultMetricValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Metrics = MetricsValue, Properties = PropertiesValue };
	}

	public sealed partial class AggregateMetricDoublePropertyDescriptor : SerializableDescriptorBase<AggregateMetricDoublePropertyDescriptor>, IBuildableDescriptor<AggregateMetricDoubleProperty>
	{
		internal AggregateMetricDoublePropertyDescriptor(Action<AggregateMetricDoublePropertyDescriptor> configure) => configure.Invoke(this);
		public AggregateMetricDoublePropertyDescriptor() : base()
		{
		}

		private string DefaultMetricValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? DynamicValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? FieldsValue { get; set; }

		private int? IgnoreAboveValue { get; set; }

		private Dictionary<string, object>? LocalMetadataValue { get; set; }

		private Dictionary<string, string>? MetaValue { get; set; }

		private IEnumerable<string> MetricsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Mapping.Properties? PropertiesValue { get; set; }

		public AggregateMetricDoublePropertyDescriptor DefaultMetric(string defaultMetric)
		{
			DefaultMetricValue = defaultMetric;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Dynamic(Elastic.Clients.Elasticsearch.Mapping.DynamicMapping? dynamic)
		{
			DynamicValue = dynamic;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Fields(Elastic.Clients.Elasticsearch.Mapping.Properties? fields)
		{
			FieldsValue = fields;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Fields<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Fields<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			FieldsValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor IgnoreAbove(int? ignoreAbove)
		{
			IgnoreAboveValue = ignoreAbove;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor LocalMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			LocalMetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Meta(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, string>());
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Metrics(IEnumerable<string> metrics)
		{
			MetricsValue = metrics;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Properties(Elastic.Clients.Elasticsearch.Mapping.Properties? properties)
		{
			PropertiesValue = properties;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Properties<TDocument>(PropertiesDescriptor<TDocument> descriptor)
		{
			PropertiesValue = descriptor.PromisedValue;
			return Self;
		}

		public AggregateMetricDoublePropertyDescriptor Properties<TDocument>(Action<PropertiesDescriptor<TDocument>> configure)
		{
			var descriptor = new PropertiesDescriptor<TDocument>();
			configure?.Invoke(descriptor);
			PropertiesValue = descriptor.PromisedValue;
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

			if (LocalMetadataValue is not null)
			{
				writer.WritePropertyName("local_metadata");
				JsonSerializer.Serialize(writer, LocalMetadataValue, options);
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

			writer.WritePropertyName("type");
			writer.WriteStringValue("aggregate_metric_double");
			writer.WriteEndObject();
		}

		AggregateMetricDoubleProperty IBuildableDescriptor<AggregateMetricDoubleProperty>.Build() => new()
		{ DefaultMetric = DefaultMetricValue, Dynamic = DynamicValue, Fields = FieldsValue, IgnoreAbove = IgnoreAboveValue, LocalMetadata = LocalMetadataValue, Meta = MetaValue, Metrics = MetricsValue, Properties = PropertiesValue };
	}
}