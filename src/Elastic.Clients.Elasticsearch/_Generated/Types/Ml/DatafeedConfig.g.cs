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
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class DatafeedConfig
	{
		[JsonInclude]
		[JsonPropertyName("aggregations")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? Aggregations { get; set; }

		[JsonInclude]
		[JsonPropertyName("chunking_config")]
		public Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("datafeed_id")]
		public Elastic.Clients.Elasticsearch.Id? DatafeedId { get; set; }

		[JsonInclude]
		[JsonPropertyName("delayed_data_check_config")]
		public Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("frequency")]
		public Elastic.Clients.Elasticsearch.Duration? Frequency { get; set; }

		[JsonInclude]
		[JsonPropertyName("indexes")]
		public IEnumerable<string>? Indexes { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices")]
		public IEnumerable<string> Indices { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices_options")]
		public Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptions { get; set; }

		[JsonInclude]
		[JsonPropertyName("job_id")]
		public Elastic.Clients.Elasticsearch.Id? JobId { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_empty_searches")]
		public int? MaxEmptySearches { get; set; }

		[JsonInclude]
		[JsonPropertyName("query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer Query { get; set; }

		[JsonInclude]
		[JsonPropertyName("query_delay")]
		public Elastic.Clients.Elasticsearch.Duration? QueryDelay { get; set; }

		[JsonInclude]
		[JsonPropertyName("runtime_mappings")]
		public Dictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>? RuntimeMappings { get; set; }

		[JsonInclude]
		[JsonPropertyName("script_fields")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFields { get; set; }

		[JsonInclude]
		[JsonPropertyName("scroll_size")]
		public int? ScrollSize { get; set; }
	}

	public sealed partial class DatafeedConfigDescriptor<TDocument> : SerializableDescriptorBase<DatafeedConfigDescriptor<TDocument>>
	{
		internal DatafeedConfigDescriptor(Action<DatafeedConfigDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DatafeedConfigDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor<TDocument> QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor<TDocument>> QueryDescriptorAction { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfigValue { get; set; }

		private ChunkingConfigDescriptor ChunkingConfigDescriptor { get; set; }

		private Action<ChunkingConfigDescriptor> ChunkingConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Id? DatafeedIdValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfigValue { get; set; }

		private DelayedDataCheckConfigDescriptor DelayedDataCheckConfigDescriptor { get; set; }

		private Action<DelayedDataCheckConfigDescriptor> DelayedDataCheckConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }

		private IEnumerable<string>? IndexesValue { get; set; }

		private IEnumerable<string> IndicesValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptionsValue { get; set; }

		private IndicesOptionsDescriptor IndicesOptionsDescriptor { get; set; }

		private Action<IndicesOptionsDescriptor> IndicesOptionsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Id? JobIdValue { get; set; }

		private int? MaxEmptySearchesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? QueryDelayValue { get; set; }

		private Dictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>? RuntimeMappingsValue { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

		private int? ScrollSizeValue { get; set; }

		public DatafeedConfigDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Query(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Query(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Aggregations(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>> selector)
		{
			AggregationsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>());
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> ChunkingConfig(Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? chunkingConfig)
		{
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigValue = chunkingConfig;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> ChunkingConfig(ChunkingConfigDescriptor descriptor)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> ChunkingConfig(Action<ChunkingConfigDescriptor> configure)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> DatafeedId(Elastic.Clients.Elasticsearch.Id? datafeedId)
		{
			DatafeedIdValue = datafeedId;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? delayedDataCheckConfig)
		{
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigValue = delayedDataCheckConfig;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> DelayedDataCheckConfig(DelayedDataCheckConfigDescriptor descriptor)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> DelayedDataCheckConfig(Action<DelayedDataCheckConfigDescriptor> configure)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
		{
			FrequencyValue = frequency;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Indexes(IEnumerable<string>? indexes)
		{
			IndexesValue = indexes;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> Indices(IEnumerable<string> indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> IndicesOptions(Elastic.Clients.Elasticsearch.IndicesOptions? indicesOptions)
		{
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsValue = indicesOptions;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> IndicesOptions(IndicesOptionsDescriptor descriptor)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> IndicesOptions(Action<IndicesOptionsDescriptor> configure)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id? jobId)
		{
			JobIdValue = jobId;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> MaxEmptySearches(int? maxEmptySearches)
		{
			MaxEmptySearchesValue = maxEmptySearches;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> QueryDelay(Elastic.Clients.Elasticsearch.Duration? queryDelay)
		{
			QueryDelayValue = queryDelay;
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>> selector)
		{
			RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>());
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> ScriptFields(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>> selector)
		{
			ScriptFieldsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>());
			return Self;
		}

		public DatafeedConfigDescriptor<TDocument> ScrollSize(int? scrollSize)
		{
			ScrollSizeValue = scrollSize;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (QueryDescriptor is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryDescriptor, options);
			}
			else if (QueryDescriptorAction is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<TDocument>(QueryDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryValue, options);
			}

			if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			if (ChunkingConfigDescriptor is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, ChunkingConfigDescriptor, options);
			}
			else if (ChunkingConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, new ChunkingConfigDescriptor(ChunkingConfigDescriptorAction), options);
			}
			else if (ChunkingConfigValue is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, ChunkingConfigValue, options);
			}

			if (DatafeedIdValue is not null)
			{
				writer.WritePropertyName("datafeed_id");
				JsonSerializer.Serialize(writer, DatafeedIdValue, options);
			}

			if (DelayedDataCheckConfigDescriptor is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, DelayedDataCheckConfigDescriptor, options);
			}
			else if (DelayedDataCheckConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, new DelayedDataCheckConfigDescriptor(DelayedDataCheckConfigDescriptorAction), options);
			}
			else if (DelayedDataCheckConfigValue is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, DelayedDataCheckConfigValue, options);
			}

			if (FrequencyValue is not null)
			{
				writer.WritePropertyName("frequency");
				JsonSerializer.Serialize(writer, FrequencyValue, options);
			}

			if (IndexesValue is not null)
			{
				writer.WritePropertyName("indexes");
				JsonSerializer.Serialize(writer, IndexesValue, options);
			}

			writer.WritePropertyName("indices");
			JsonSerializer.Serialize(writer, IndicesValue, options);
			if (IndicesOptionsDescriptor is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, IndicesOptionsDescriptor, options);
			}
			else if (IndicesOptionsDescriptorAction is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, new IndicesOptionsDescriptor(IndicesOptionsDescriptorAction), options);
			}
			else if (IndicesOptionsValue is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, IndicesOptionsValue, options);
			}

			if (JobIdValue is not null)
			{
				writer.WritePropertyName("job_id");
				JsonSerializer.Serialize(writer, JobIdValue, options);
			}

			if (MaxEmptySearchesValue.HasValue)
			{
				writer.WritePropertyName("max_empty_searches");
				writer.WriteNumberValue(MaxEmptySearchesValue.Value);
			}

			if (QueryDelayValue is not null)
			{
				writer.WritePropertyName("query_delay");
				JsonSerializer.Serialize(writer, QueryDelayValue, options);
			}

			if (RuntimeMappingsValue is not null)
			{
				writer.WritePropertyName("runtime_mappings");
				JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
			}

			if (ScriptFieldsValue is not null)
			{
				writer.WritePropertyName("script_fields");
				JsonSerializer.Serialize(writer, ScriptFieldsValue, options);
			}

			if (ScrollSizeValue.HasValue)
			{
				writer.WritePropertyName("scroll_size");
				writer.WriteNumberValue(ScrollSizeValue.Value);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class DatafeedConfigDescriptor : SerializableDescriptorBase<DatafeedConfigDescriptor>
	{
		internal DatafeedConfigDescriptor(Action<DatafeedConfigDescriptor> configure) => configure.Invoke(this);
		public DatafeedConfigDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor> QueryDescriptorAction { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfigValue { get; set; }

		private ChunkingConfigDescriptor ChunkingConfigDescriptor { get; set; }

		private Action<ChunkingConfigDescriptor> ChunkingConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Id? DatafeedIdValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfigValue { get; set; }

		private DelayedDataCheckConfigDescriptor DelayedDataCheckConfigDescriptor { get; set; }

		private Action<DelayedDataCheckConfigDescriptor> DelayedDataCheckConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }

		private IEnumerable<string>? IndexesValue { get; set; }

		private IEnumerable<string> IndicesValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptionsValue { get; set; }

		private IndicesOptionsDescriptor IndicesOptionsDescriptor { get; set; }

		private Action<IndicesOptionsDescriptor> IndicesOptionsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Id? JobIdValue { get; set; }

		private int? MaxEmptySearchesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? QueryDelayValue { get; set; }

		private Dictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>? RuntimeMappingsValue { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

		private int? ScrollSizeValue { get; set; }

		public DatafeedConfigDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public DatafeedConfigDescriptor Query(QueryDsl.QueryContainerDescriptor descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor Query(Action<QueryDsl.QueryContainerDescriptor> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor Aggregations(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>> selector)
		{
			AggregationsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>());
			return Self;
		}

		public DatafeedConfigDescriptor ChunkingConfig(Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? chunkingConfig)
		{
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigValue = chunkingConfig;
			return Self;
		}

		public DatafeedConfigDescriptor ChunkingConfig(ChunkingConfigDescriptor descriptor)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor ChunkingConfig(Action<ChunkingConfigDescriptor> configure)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor DatafeedId(Elastic.Clients.Elasticsearch.Id? datafeedId)
		{
			DatafeedIdValue = datafeedId;
			return Self;
		}

		public DatafeedConfigDescriptor DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? delayedDataCheckConfig)
		{
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigValue = delayedDataCheckConfig;
			return Self;
		}

		public DatafeedConfigDescriptor DelayedDataCheckConfig(DelayedDataCheckConfigDescriptor descriptor)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor DelayedDataCheckConfig(Action<DelayedDataCheckConfigDescriptor> configure)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
		{
			FrequencyValue = frequency;
			return Self;
		}

		public DatafeedConfigDescriptor Indexes(IEnumerable<string>? indexes)
		{
			IndexesValue = indexes;
			return Self;
		}

		public DatafeedConfigDescriptor Indices(IEnumerable<string> indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public DatafeedConfigDescriptor IndicesOptions(Elastic.Clients.Elasticsearch.IndicesOptions? indicesOptions)
		{
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsValue = indicesOptions;
			return Self;
		}

		public DatafeedConfigDescriptor IndicesOptions(IndicesOptionsDescriptor descriptor)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsDescriptor = descriptor;
			return Self;
		}

		public DatafeedConfigDescriptor IndicesOptions(Action<IndicesOptionsDescriptor> configure)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = configure;
			return Self;
		}

		public DatafeedConfigDescriptor JobId(Elastic.Clients.Elasticsearch.Id? jobId)
		{
			JobIdValue = jobId;
			return Self;
		}

		public DatafeedConfigDescriptor MaxEmptySearches(int? maxEmptySearches)
		{
			MaxEmptySearchesValue = maxEmptySearches;
			return Self;
		}

		public DatafeedConfigDescriptor QueryDelay(Elastic.Clients.Elasticsearch.Duration? queryDelay)
		{
			QueryDelayValue = queryDelay;
			return Self;
		}

		public DatafeedConfigDescriptor RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>> selector)
		{
			RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>());
			return Self;
		}

		public DatafeedConfigDescriptor ScriptFields(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>> selector)
		{
			ScriptFieldsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>());
			return Self;
		}

		public DatafeedConfigDescriptor ScrollSize(int? scrollSize)
		{
			ScrollSizeValue = scrollSize;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (QueryDescriptor is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryDescriptor, options);
			}
			else if (QueryDescriptorAction is not null)
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor(QueryDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("query");
				JsonSerializer.Serialize(writer, QueryValue, options);
			}

			if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			if (ChunkingConfigDescriptor is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, ChunkingConfigDescriptor, options);
			}
			else if (ChunkingConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, new ChunkingConfigDescriptor(ChunkingConfigDescriptorAction), options);
			}
			else if (ChunkingConfigValue is not null)
			{
				writer.WritePropertyName("chunking_config");
				JsonSerializer.Serialize(writer, ChunkingConfigValue, options);
			}

			if (DatafeedIdValue is not null)
			{
				writer.WritePropertyName("datafeed_id");
				JsonSerializer.Serialize(writer, DatafeedIdValue, options);
			}

			if (DelayedDataCheckConfigDescriptor is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, DelayedDataCheckConfigDescriptor, options);
			}
			else if (DelayedDataCheckConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, new DelayedDataCheckConfigDescriptor(DelayedDataCheckConfigDescriptorAction), options);
			}
			else if (DelayedDataCheckConfigValue is not null)
			{
				writer.WritePropertyName("delayed_data_check_config");
				JsonSerializer.Serialize(writer, DelayedDataCheckConfigValue, options);
			}

			if (FrequencyValue is not null)
			{
				writer.WritePropertyName("frequency");
				JsonSerializer.Serialize(writer, FrequencyValue, options);
			}

			if (IndexesValue is not null)
			{
				writer.WritePropertyName("indexes");
				JsonSerializer.Serialize(writer, IndexesValue, options);
			}

			writer.WritePropertyName("indices");
			JsonSerializer.Serialize(writer, IndicesValue, options);
			if (IndicesOptionsDescriptor is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, IndicesOptionsDescriptor, options);
			}
			else if (IndicesOptionsDescriptorAction is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, new IndicesOptionsDescriptor(IndicesOptionsDescriptorAction), options);
			}
			else if (IndicesOptionsValue is not null)
			{
				writer.WritePropertyName("indices_options");
				JsonSerializer.Serialize(writer, IndicesOptionsValue, options);
			}

			if (JobIdValue is not null)
			{
				writer.WritePropertyName("job_id");
				JsonSerializer.Serialize(writer, JobIdValue, options);
			}

			if (MaxEmptySearchesValue.HasValue)
			{
				writer.WritePropertyName("max_empty_searches");
				writer.WriteNumberValue(MaxEmptySearchesValue.Value);
			}

			if (QueryDelayValue is not null)
			{
				writer.WritePropertyName("query_delay");
				JsonSerializer.Serialize(writer, QueryDelayValue, options);
			}

			if (RuntimeMappingsValue is not null)
			{
				writer.WritePropertyName("runtime_mappings");
				JsonSerializer.Serialize(writer, RuntimeMappingsValue, options);
			}

			if (ScriptFieldsValue is not null)
			{
				writer.WritePropertyName("script_fields");
				JsonSerializer.Serialize(writer, ScriptFieldsValue, options);
			}

			if (ScrollSizeValue.HasValue)
			{
				writer.WritePropertyName("scroll_size");
				writer.WriteNumberValue(ScrollSizeValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}