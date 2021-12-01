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
	public abstract partial class MetricAggregationBase : Aggregations.AggregationBase
	{
		protected MetricAggregationBase(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing")]
		public Elastic.Clients.Elasticsearch.Aggregations.Missing? Missing { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	}

	public sealed partial class MetricAggregationBaseDescriptor<T> : DescriptorBase<MetricAggregationBaseDescriptor<T>>
	{
		public MetricAggregationBaseDescriptor()
		{
		}

		internal MetricAggregationBaseDescriptor(Action<MetricAggregationBaseDescriptor<T>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Field? FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.Missing? MissingValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? ScriptValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		internal string? NameValue { get; private set; }

		public MetricAggregationBaseDescriptor<T> Field(Elastic.Clients.Elasticsearch.Field? field) => Assign(field, (a, v) => a.FieldValue = v);
		public MetricAggregationBaseDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public MetricAggregationBaseDescriptor<T> Missing(Elastic.Clients.Elasticsearch.Aggregations.Missing? missing) => Assign(missing, (a, v) => a.MissingValue = v);
		public MetricAggregationBaseDescriptor<T> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a.ScriptValue = v);
		public MetricAggregationBaseDescriptor<T> Meta(Func<FluentDictionary<string?, object?>, FluentDictionary<string?, object?>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string?, object?>()));
		public MetricAggregationBaseDescriptor<T> Name(string? name) => Assign(name, (a, v) => a.NameValue = v);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (MissingValue is not null)
			{
				writer.WritePropertyName("missing");
				JsonSerializer.Serialize(writer, MissingValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
			}

			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			if (!string.IsNullOrEmpty(NameValue))
			{
				writer.WritePropertyName("name");
				writer.WriteStringValue(NameValue);
			}

			writer.WriteEndObject();
		}
	}
}