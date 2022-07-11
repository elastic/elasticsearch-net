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
	internal sealed class ScriptedMetricAggregationConverter : JsonConverter<ScriptedMetricAggregation>
	{
		public override ScriptedMetricAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "scripted_metric")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new ScriptedMetricAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("combine_script"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<ScriptBase?>(ref reader, options);
						if (value is not null)
						{
							agg.CombineScript = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("init_script"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<ScriptBase?>(ref reader, options);
						if (value is not null)
						{
							agg.InitScript = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("map_script"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<ScriptBase?>(ref reader, options);
						if (value is not null)
						{
							agg.MapScript = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("params"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Dictionary<string, object>?>(ref reader, options);
						if (value is not null)
						{
							agg.Params = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("reduce_script"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<ScriptBase?>(ref reader, options);
						if (value is not null)
						{
							agg.ReduceScript = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("field"))
					{
						reader.Read();
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("script"))
					{
						reader.Read();
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

		public override void Write(Utf8JsonWriter writer, ScriptedMetricAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("scripted_metric");
			writer.WriteStartObject();
			if (value.CombineScript is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, value.CombineScript, options);
			}

			if (value.InitScript is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, value.InitScript, options);
			}

			if (value.MapScript is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, value.MapScript, options);
			}

			if (value.Params is not null)
			{
				writer.WritePropertyName("params");
				JsonSerializer.Serialize(writer, value.Params, options);
			}

			if (value.ReduceScript is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, value.ReduceScript, options);
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

	[JsonConverter(typeof(ScriptedMetricAggregationConverter))]
	public partial class ScriptedMetricAggregation : MetricAggregationBase
	{
		public ScriptedMetricAggregation(string name, Field field) : base(name) => Field = field;
		public ScriptedMetricAggregation(string name) : base(name)
		{
		}

		[JsonInclude]
		[JsonPropertyName("combine_script")]
		public ScriptBase? CombineScript { get; set; }

		[JsonInclude]
		[JsonPropertyName("init_script")]
		public ScriptBase? InitScript { get; set; }

		[JsonInclude]
		[JsonPropertyName("map_script")]
		public ScriptBase? MapScript { get; set; }

		[JsonInclude]
		[JsonPropertyName("params")]
		public Dictionary<string, object>? Params { get; set; }

		[JsonInclude]
		[JsonPropertyName("reduce_script")]
		public ScriptBase? ReduceScript { get; set; }
	}

	public sealed partial class ScriptedMetricAggregationDescriptor<TDocument> : SerializableDescriptorBase<ScriptedMetricAggregationDescriptor<TDocument>>
	{
		internal ScriptedMetricAggregationDescriptor(Action<ScriptedMetricAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public ScriptedMetricAggregationDescriptor() : base()
		{
		}

		private ScriptBase? CombineScriptValue { get; set; }

		private ScriptDescriptor CombineScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> CombineScriptDescriptorAction { get; set; }

		private ScriptBase? InitScriptValue { get; set; }

		private ScriptDescriptor InitScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> InitScriptDescriptorAction { get; set; }

		private ScriptBase? MapScriptValue { get; set; }

		private ScriptDescriptor MapScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> MapScriptDescriptorAction { get; set; }

		private ScriptBase? ReduceScriptValue { get; set; }

		private ScriptDescriptor ReduceScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ReduceScriptDescriptorAction { get; set; }

		private ScriptBase? ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Dictionary<string, object>? ParamsValue { get; set; }

		public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(ScriptBase? combineScript)
		{
			CombineScriptDescriptor = null;
			CombineScriptDescriptorAction = null;
			CombineScriptValue = combineScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(ScriptDescriptor descriptor)
		{
			CombineScriptValue = null;
			CombineScriptDescriptorAction = null;
			CombineScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> CombineScript(Action<ScriptDescriptor> configure)
		{
			CombineScriptValue = null;
			CombineScriptDescriptor = null;
			CombineScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> InitScript(ScriptBase? initScript)
		{
			InitScriptDescriptor = null;
			InitScriptDescriptorAction = null;
			InitScriptValue = initScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> InitScript(ScriptDescriptor descriptor)
		{
			InitScriptValue = null;
			InitScriptDescriptorAction = null;
			InitScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> InitScript(Action<ScriptDescriptor> configure)
		{
			InitScriptValue = null;
			InitScriptDescriptor = null;
			InitScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> MapScript(ScriptBase? mapScript)
		{
			MapScriptDescriptor = null;
			MapScriptDescriptorAction = null;
			MapScriptValue = mapScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> MapScript(ScriptDescriptor descriptor)
		{
			MapScriptValue = null;
			MapScriptDescriptorAction = null;
			MapScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> MapScript(Action<ScriptDescriptor> configure)
		{
			MapScriptValue = null;
			MapScriptDescriptor = null;
			MapScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(ScriptBase? reduceScript)
		{
			ReduceScriptDescriptor = null;
			ReduceScriptDescriptorAction = null;
			ReduceScriptValue = reduceScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(ScriptDescriptor descriptor)
		{
			ReduceScriptValue = null;
			ReduceScriptDescriptorAction = null;
			ReduceScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> ReduceScript(Action<ScriptDescriptor> configure)
		{
			ReduceScriptValue = null;
			ReduceScriptDescriptor = null;
			ReduceScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Script(ScriptBase? script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public ScriptedMetricAggregationDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("scripted_metric");
			writer.WriteStartObject();
			if (CombineScriptDescriptor is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, CombineScriptDescriptor, options);
			}
			else if (CombineScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(CombineScriptDescriptorAction), options);
			}
			else if (CombineScriptValue is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, CombineScriptValue, options);
			}

			if (InitScriptDescriptor is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, InitScriptDescriptor, options);
			}
			else if (InitScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(InitScriptDescriptorAction), options);
			}
			else if (InitScriptValue is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, InitScriptValue, options);
			}

			if (MapScriptDescriptor is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, MapScriptDescriptor, options);
			}
			else if (MapScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(MapScriptDescriptorAction), options);
			}
			else if (MapScriptValue is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, MapScriptValue, options);
			}

			if (ReduceScriptDescriptor is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, ReduceScriptDescriptor, options);
			}
			else if (ReduceScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ReduceScriptDescriptorAction), options);
			}
			else if (ReduceScriptValue is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, ReduceScriptValue, options);
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

			if (ParamsValue is not null)
			{
				writer.WritePropertyName("params");
				JsonSerializer.Serialize(writer, ParamsValue, options);
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

	public sealed partial class ScriptedMetricAggregationDescriptor : SerializableDescriptorBase<ScriptedMetricAggregationDescriptor>
	{
		internal ScriptedMetricAggregationDescriptor(Action<ScriptedMetricAggregationDescriptor> configure) => configure.Invoke(this);
		public ScriptedMetricAggregationDescriptor() : base()
		{
		}

		private ScriptBase? CombineScriptValue { get; set; }

		private ScriptDescriptor CombineScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> CombineScriptDescriptorAction { get; set; }

		private ScriptBase? InitScriptValue { get; set; }

		private ScriptDescriptor InitScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> InitScriptDescriptorAction { get; set; }

		private ScriptBase? MapScriptValue { get; set; }

		private ScriptDescriptor MapScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> MapScriptDescriptorAction { get; set; }

		private ScriptBase? ReduceScriptValue { get; set; }

		private ScriptDescriptor ReduceScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ReduceScriptDescriptorAction { get; set; }

		private ScriptBase? ScriptValue { get; set; }

		private ScriptDescriptor ScriptDescriptor { get; set; }

		private Action<ScriptDescriptor> ScriptDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private Dictionary<string, object>? ParamsValue { get; set; }

		public ScriptedMetricAggregationDescriptor CombineScript(ScriptBase? combineScript)
		{
			CombineScriptDescriptor = null;
			CombineScriptDescriptorAction = null;
			CombineScriptValue = combineScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor CombineScript(ScriptDescriptor descriptor)
		{
			CombineScriptValue = null;
			CombineScriptDescriptorAction = null;
			CombineScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor CombineScript(Action<ScriptDescriptor> configure)
		{
			CombineScriptValue = null;
			CombineScriptDescriptor = null;
			CombineScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor InitScript(ScriptBase? initScript)
		{
			InitScriptDescriptor = null;
			InitScriptDescriptorAction = null;
			InitScriptValue = initScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor InitScript(ScriptDescriptor descriptor)
		{
			InitScriptValue = null;
			InitScriptDescriptorAction = null;
			InitScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor InitScript(Action<ScriptDescriptor> configure)
		{
			InitScriptValue = null;
			InitScriptDescriptor = null;
			InitScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor MapScript(ScriptBase? mapScript)
		{
			MapScriptDescriptor = null;
			MapScriptDescriptorAction = null;
			MapScriptValue = mapScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor MapScript(ScriptDescriptor descriptor)
		{
			MapScriptValue = null;
			MapScriptDescriptorAction = null;
			MapScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor MapScript(Action<ScriptDescriptor> configure)
		{
			MapScriptValue = null;
			MapScriptDescriptor = null;
			MapScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor ReduceScript(ScriptBase? reduceScript)
		{
			ReduceScriptDescriptor = null;
			ReduceScriptDescriptorAction = null;
			ReduceScriptValue = reduceScript;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor ReduceScript(ScriptDescriptor descriptor)
		{
			ReduceScriptValue = null;
			ReduceScriptDescriptorAction = null;
			ReduceScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor ReduceScript(Action<ScriptDescriptor> configure)
		{
			ReduceScriptValue = null;
			ReduceScriptDescriptor = null;
			ReduceScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Script(ScriptBase? script)
		{
			ScriptDescriptor = null;
			ScriptDescriptorAction = null;
			ScriptValue = script;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Script(ScriptDescriptor descriptor)
		{
			ScriptValue = null;
			ScriptDescriptorAction = null;
			ScriptDescriptor = descriptor;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Script(Action<ScriptDescriptor> configure)
		{
			ScriptValue = null;
			ScriptDescriptor = null;
			ScriptDescriptorAction = configure;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public ScriptedMetricAggregationDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("scripted_metric");
			writer.WriteStartObject();
			if (CombineScriptDescriptor is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, CombineScriptDescriptor, options);
			}
			else if (CombineScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(CombineScriptDescriptorAction), options);
			}
			else if (CombineScriptValue is not null)
			{
				writer.WritePropertyName("combine_script");
				JsonSerializer.Serialize(writer, CombineScriptValue, options);
			}

			if (InitScriptDescriptor is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, InitScriptDescriptor, options);
			}
			else if (InitScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(InitScriptDescriptorAction), options);
			}
			else if (InitScriptValue is not null)
			{
				writer.WritePropertyName("init_script");
				JsonSerializer.Serialize(writer, InitScriptValue, options);
			}

			if (MapScriptDescriptor is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, MapScriptDescriptor, options);
			}
			else if (MapScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(MapScriptDescriptorAction), options);
			}
			else if (MapScriptValue is not null)
			{
				writer.WritePropertyName("map_script");
				JsonSerializer.Serialize(writer, MapScriptValue, options);
			}

			if (ReduceScriptDescriptor is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, ReduceScriptDescriptor, options);
			}
			else if (ReduceScriptDescriptorAction is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, new ScriptDescriptor(ReduceScriptDescriptorAction), options);
			}
			else if (ReduceScriptValue is not null)
			{
				writer.WritePropertyName("reduce_script");
				JsonSerializer.Serialize(writer, ReduceScriptValue, options);
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

			if (ParamsValue is not null)
			{
				writer.WritePropertyName("params");
				JsonSerializer.Serialize(writer, ParamsValue, options);
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