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
	internal sealed class BoxplotAggregationConverter : JsonConverter<BoxplotAggregation>
	{
		public override BoxplotAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new BoxplotAggregation();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "compression")
					{
						variant.Compression = JsonSerializer.Deserialize<double?>(ref reader, options);
						continue;
					}

					if (property == "field")
					{
						variant.Field = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						continue;
					}

					if (property == "meta")
					{
						variant.Meta = JsonSerializer.Deserialize<Dictionary<string, object>?>(ref reader, options);
						continue;
					}

					if (property == "name")
					{
						variant.Name = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "script")
					{
						variant.Script = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Script?>(ref reader, options);
						continue;
					}
				}
			}

			return variant;
		}

		public override void Write(Utf8JsonWriter writer, BoxplotAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Compression.HasValue)
			{
				writer.WritePropertyName("compression");
				writer.WriteNumberValue(value.Compression.Value);
			}

			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (!string.IsNullOrEmpty(value.Name))
			{
				writer.WritePropertyName("name");
				writer.WriteStringValue(value.Name);
			}

			if (value.Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, value.Script, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(BoxplotAggregationConverter))]
	public sealed partial class BoxplotAggregation : Aggregation
	{
		public BoxplotAggregation(string name, Field field) => Field = field;
		public BoxplotAggregation(string name) => Name = name;
		internal BoxplotAggregation()
		{
		}

		public double? Compression { get; set; }

		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		public Dictionary<string, object>? Meta { get; set; }

		public override string? Name { get; internal set; }

		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
	}

	public sealed partial class BoxplotAggregationDescriptor<TDocument> : SerializableDescriptorBase<BoxplotAggregationDescriptor<TDocument>>
	{
		internal BoxplotAggregationDescriptor(Action<BoxplotAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public BoxplotAggregationDescriptor() : base()
		{
		}

		private double? CompressionValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

		public BoxplotAggregationDescriptor<TDocument> Compression(double? compression)
		{
			CompressionValue = compression;
			return Self;
		}

		public BoxplotAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public BoxplotAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public BoxplotAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public BoxplotAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
		{
			ScriptValue = script;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("boxplot");
			writer.WriteStartObject();
			if (CompressionValue.HasValue)
			{
				writer.WritePropertyName("compression");
				writer.WriteNumberValue(CompressionValue.Value);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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

	public sealed partial class BoxplotAggregationDescriptor : SerializableDescriptorBase<BoxplotAggregationDescriptor>
	{
		internal BoxplotAggregationDescriptor(Action<BoxplotAggregationDescriptor> configure) => configure.Invoke(this);
		public BoxplotAggregationDescriptor() : base()
		{
		}

		private double? CompressionValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

		public BoxplotAggregationDescriptor Compression(double? compression)
		{
			CompressionValue = compression;
			return Self;
		}

		public BoxplotAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public BoxplotAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public BoxplotAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public BoxplotAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public BoxplotAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
		{
			ScriptValue = script;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("boxplot");
			writer.WriteStartObject();
			if (CompressionValue.HasValue)
			{
				writer.WritePropertyName("compression");
				writer.WriteNumberValue(CompressionValue.Value);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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