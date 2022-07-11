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
	internal sealed class TTestAggregationConverter : JsonConverter<TTestAggregation>
	{
		public override TTestAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "t_test")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new TTestAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("a"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TestPopulation?>(ref reader, options);
						if (value is not null)
						{
							agg.a = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("b"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TestPopulation?>(ref reader, options);
						if (value is not null)
						{
							agg.b = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("type"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TTestType?>(ref reader, options);
						if (value is not null)
						{
							agg.Type = value;
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

		public override void Write(Utf8JsonWriter writer, TTestAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("t_test");
			writer.WriteStartObject();
			if (value.a is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, value.a, options);
			}

			if (value.b is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, value.b, options);
			}

			if (value.Type is not null)
			{
				writer.WritePropertyName("type");
				JsonSerializer.Serialize(writer, value.Type, options);
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

	[JsonConverter(typeof(TTestAggregationConverter))]
	public partial class TTestAggregation : AggregationBase
	{
		public TTestAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("a")]
		public Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a { get; set; }

		[JsonInclude]
		[JsonPropertyName("b")]
		public Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b { get; set; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public Elastic.Clients.Elasticsearch.Aggregations.TTestType? Type { get; set; }
	}

	public sealed partial class TTestAggregationDescriptor<TDocument> : SerializableDescriptorBase<TTestAggregationDescriptor<TDocument>>
	{
		internal TTestAggregationDescriptor(Action<TTestAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public TTestAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? aValue { get; set; }

		private TestPopulationDescriptor<TDocument> aDescriptor { get; set; }

		private Action<TestPopulationDescriptor<TDocument>> aDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? bValue { get; set; }

		private TestPopulationDescriptor<TDocument> bDescriptor { get; set; }

		private Action<TestPopulationDescriptor<TDocument>> bDescriptorAction { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TTestType? TypeValue { get; set; }

		public TTestAggregationDescriptor<TDocument> a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a)
		{
			aDescriptor = null;
			aDescriptorAction = null;
			aValue = a;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> a(TestPopulationDescriptor<TDocument> descriptor)
		{
			aValue = null;
			aDescriptorAction = null;
			aDescriptor = descriptor;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> a(Action<TestPopulationDescriptor<TDocument>> configure)
		{
			aValue = null;
			aDescriptor = null;
			aDescriptorAction = configure;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b)
		{
			bDescriptor = null;
			bDescriptorAction = null;
			bValue = b;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> b(TestPopulationDescriptor<TDocument> descriptor)
		{
			bValue = null;
			bDescriptorAction = null;
			bDescriptor = descriptor;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> b(Action<TestPopulationDescriptor<TDocument>> configure)
		{
			bValue = null;
			bDescriptor = null;
			bDescriptorAction = configure;
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public TTestAggregationDescriptor<TDocument> Type(Elastic.Clients.Elasticsearch.Aggregations.TTestType? type)
		{
			TypeValue = type;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("t_test");
			writer.WriteStartObject();
			if (aDescriptor is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, aDescriptor, options);
			}
			else if (aDescriptorAction is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, new TestPopulationDescriptor<TDocument>(aDescriptorAction), options);
			}
			else if (aValue is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, aValue, options);
			}

			if (bDescriptor is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, bDescriptor, options);
			}
			else if (bDescriptorAction is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, new TestPopulationDescriptor<TDocument>(bDescriptorAction), options);
			}
			else if (bValue is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, bValue, options);
			}

			if (TypeValue is not null)
			{
				writer.WritePropertyName("type");
				JsonSerializer.Serialize(writer, TypeValue, options);
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

	public sealed partial class TTestAggregationDescriptor : SerializableDescriptorBase<TTestAggregationDescriptor>
	{
		internal TTestAggregationDescriptor(Action<TTestAggregationDescriptor> configure) => configure.Invoke(this);
		public TTestAggregationDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? aValue { get; set; }

		private TestPopulationDescriptor aDescriptor { get; set; }

		private Action<TestPopulationDescriptor> aDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? bValue { get; set; }

		private TestPopulationDescriptor bDescriptor { get; set; }

		private Action<TestPopulationDescriptor> bDescriptorAction { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TTestType? TypeValue { get; set; }

		public TTestAggregationDescriptor a(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? a)
		{
			aDescriptor = null;
			aDescriptorAction = null;
			aValue = a;
			return Self;
		}

		public TTestAggregationDescriptor a(TestPopulationDescriptor descriptor)
		{
			aValue = null;
			aDescriptorAction = null;
			aDescriptor = descriptor;
			return Self;
		}

		public TTestAggregationDescriptor a(Action<TestPopulationDescriptor> configure)
		{
			aValue = null;
			aDescriptor = null;
			aDescriptorAction = configure;
			return Self;
		}

		public TTestAggregationDescriptor b(Elastic.Clients.Elasticsearch.Aggregations.TestPopulation? b)
		{
			bDescriptor = null;
			bDescriptorAction = null;
			bValue = b;
			return Self;
		}

		public TTestAggregationDescriptor b(TestPopulationDescriptor descriptor)
		{
			bValue = null;
			bDescriptorAction = null;
			bDescriptor = descriptor;
			return Self;
		}

		public TTestAggregationDescriptor b(Action<TestPopulationDescriptor> configure)
		{
			bValue = null;
			bDescriptor = null;
			bDescriptorAction = configure;
			return Self;
		}

		public TTestAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public TTestAggregationDescriptor Type(Elastic.Clients.Elasticsearch.Aggregations.TTestType? type)
		{
			TypeValue = type;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("t_test");
			writer.WriteStartObject();
			if (aDescriptor is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, aDescriptor, options);
			}
			else if (aDescriptorAction is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, new TestPopulationDescriptor(aDescriptorAction), options);
			}
			else if (aValue is not null)
			{
				writer.WritePropertyName("a");
				JsonSerializer.Serialize(writer, aValue, options);
			}

			if (bDescriptor is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, bDescriptor, options);
			}
			else if (bDescriptorAction is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, new TestPopulationDescriptor(bDescriptorAction), options);
			}
			else if (bValue is not null)
			{
				writer.WritePropertyName("b");
				JsonSerializer.Serialize(writer, bValue, options);
			}

			if (TypeValue is not null)
			{
				writer.WritePropertyName("type");
				JsonSerializer.Serialize(writer, TypeValue, options);
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