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
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	internal sealed class TopMetricsAggregationConverter : JsonConverter<TopMetricsAggregation>
	{
		public override TopMetricsAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "top_metrics")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new TopMetricsAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("metrics"))
					{
						var value = JsonSerializer.Deserialize<IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>?>(ref reader, options);
						if (value is not null)
						{
							agg.Metrics = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("size"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.Size = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("sort"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Sort?>(ref reader, options);
						if (value is not null)
						{
							agg.Sort = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("field"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("script"))
					{
						var value = JsonSerializer.Deserialize<ScriptBase?>(ref reader, options);
						if (value is not null)
						{
							agg.Script = value;
						}

						continue;
					}
				}
			}

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("meta"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							agg.Meta = value;
						}

						continue;
					}
				}
			}

			reader.Read();
			return agg;
		}

		public override void Write(Utf8JsonWriter writer, TopMetricsAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("top_metrics");
			writer.WriteStartObject();
			if (value.Metrics is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, value.Metrics, options);
			}

			if (value.Size.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(value.Size.Value);
			}

			if (value.Sort is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, value.Sort, options);
			}

			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, value.Script, options);
			}

			writer.WriteEndObject();
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(TopMetricsAggregationConverter))]
	public partial class TopMetricsAggregation : MetricAggregationBase
	{
		public TopMetricsAggregation(string name, Field field) : base(name) => Field = field;
		public TopMetricsAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("metrics")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? Metrics { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort")]
		public Elastic.Clients.Elasticsearch.Sort? Sort { get; set; }
	}

	public sealed partial class TopMetricsAggregationDescriptor<TDocument> : SerializableDescriptorBase<TopMetricsAggregationDescriptor<TDocument>>
	{
		internal TopMetricsAggregationDescriptor(Action<TopMetricsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public TopMetricsAggregationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? MetricsValue { get; set; }

		private TopMetricsValueDescriptor<TDocument> MetricsDescriptor { get; set; }

		private Action<TopMetricsValueDescriptor<TDocument>> MetricsDescriptorAction { get; set; }

		private Action<TopMetricsValueDescriptor<TDocument>>[] MetricsDescriptorActions { get; set; }

		private ScriptBase? ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private int? SizeValue { get; set; }

		private Elastic.Clients.Elasticsearch.Sort? SortValue { get; set; }

		public TopMetricsAggregationDescriptor<TDocument> Metrics(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? metrics)
		{
			MetricsDescriptor = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = null;
			MetricsValue = metrics;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Metrics(TopMetricsValueDescriptor<TDocument> descriptor)
		{
			MetricsValue = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = null;
			MetricsDescriptor = descriptor;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Metrics(Action<TopMetricsValueDescriptor<TDocument>> configure)
		{
			MetricsValue = null;
			MetricsDescriptor = null;
			MetricsDescriptorActions = null;
			MetricsDescriptorAction = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Metrics(params Action<TopMetricsValueDescriptor<TDocument>>[] configure)
		{
			MetricsValue = null;
			MetricsDescriptor = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Script(ScriptBase? script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		public TopMetricsAggregationDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Sort? sort)
		{
			SortValue = sort;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("top_metrics");
			writer.WriteStartObject();
			if (MetricsDescriptor is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, MetricsDescriptor, options);
			}
			else if (MetricsDescriptorAction is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, new TopMetricsValueDescriptor<TDocument>(MetricsDescriptorAction), options);
			}
			else if (MetricsDescriptorActions is not null)
			{
				writer.WritePropertyName("metrics");
				writer.WriteStartArray();
				foreach (var action in MetricsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new TopMetricsValueDescriptor<TDocument>(action), options);
				}

				writer.WriteEndArray();
			}
			else if (MetricsValue is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, MetricsValue, options);
			}

			if (ScriptDescriptor is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptDescriptor, options);
			}
			else if (ScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ScriptDescriptorAction), options);
			}
			else if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			if (SortValue is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, SortValue, options);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class TopMetricsAggregationDescriptor : SerializableDescriptorBase<TopMetricsAggregationDescriptor>
	{
		internal TopMetricsAggregationDescriptor(Action<TopMetricsAggregationDescriptor> configure) => configure.Invoke(this);
		public TopMetricsAggregationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? MetricsValue { get; set; }

		private TopMetricsValueDescriptor MetricsDescriptor { get; set; }

		private Action<TopMetricsValueDescriptor> MetricsDescriptorAction { get; set; }

		private Action<TopMetricsValueDescriptor>[] MetricsDescriptorActions { get; set; }

		private ScriptBase? ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private int? SizeValue { get; set; }

		private Elastic.Clients.Elasticsearch.Sort? SortValue { get; set; }

		public TopMetricsAggregationDescriptor Metrics(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.TopMetricsValue>? metrics)
		{
			MetricsDescriptor = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = null;
			MetricsValue = metrics;
			return Self;
		}

		public TopMetricsAggregationDescriptor Metrics(TopMetricsValueDescriptor descriptor)
		{
			MetricsValue = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = null;
			MetricsDescriptor = descriptor;
			return Self;
		}

		public TopMetricsAggregationDescriptor Metrics(Action<TopMetricsValueDescriptor> configure)
		{
			MetricsValue = null;
			MetricsDescriptor = null;
			MetricsDescriptorActions = null;
			MetricsDescriptorAction = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor Metrics(params Action<TopMetricsValueDescriptor>[] configure)
		{
			MetricsValue = null;
			MetricsDescriptor = null;
			MetricsDescriptorAction = null;
			MetricsDescriptorActions = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor Script(ScriptBase? script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public TopMetricsAggregationDescriptor Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public TopMetricsAggregationDescriptor Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public TopMetricsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public TopMetricsAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public TopMetricsAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public TopMetricsAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public TopMetricsAggregationDescriptor Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		public TopMetricsAggregationDescriptor Sort(Elastic.Clients.Elasticsearch.Sort? sort)
		{
			SortValue = sort;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("top_metrics");
			writer.WriteStartObject();
			if (MetricsDescriptor is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, MetricsDescriptor, options);
			}
			else if (MetricsDescriptorAction is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, new TopMetricsValueDescriptor(MetricsDescriptorAction), options);
			}
			else if (MetricsDescriptorActions is not null)
			{
				writer.WritePropertyName("metrics");
				writer.WriteStartArray();
				foreach (var action in MetricsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new TopMetricsValueDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (MetricsValue is not null)
			{
				writer.WritePropertyName("metrics");
				JsonSerializer.Serialize(writer, MetricsValue, options);
			}

			if (ScriptDescriptor is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptDescriptor, options);
			}
			else if (ScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ScriptDescriptorAction), options);
			}
			else if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			if (SortValue is not null)
			{
				writer.WritePropertyName("sort");
				JsonSerializer.Serialize(writer, SortValue, options);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}
}