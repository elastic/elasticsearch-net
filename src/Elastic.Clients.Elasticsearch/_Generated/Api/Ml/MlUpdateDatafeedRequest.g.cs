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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public sealed class MlUpdateDatafeedRequestParameters : RequestParameters<MlUpdateDatafeedRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
	}

	public sealed partial class MlUpdateDatafeedRequest : PlainRequestBase<MlUpdateDatafeedRequestParameters>
	{
		public MlUpdateDatafeedRequest(Elastic.Clients.Elasticsearch.Id datafeed_id) : base(r => r.Required("datafeed_id", datafeed_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningUpdateDatafeed;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonInclude]
		[JsonPropertyName("aggregations")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? Aggregations { get; set; }

		[JsonInclude]
		[JsonPropertyName("chunking_config")]
		public Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("delayed_data_check_config")]
		public Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("frequency")]
		public Elastic.Clients.Elasticsearch.Duration? Frequency { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices")]
		public IEnumerable<string>? Indices { get; set; }

		[JsonInclude]
		[JsonPropertyName("indices_options")]
		public Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptions { get; set; }

		[JsonInclude]
		[JsonPropertyName("max_empty_searches")]
		public int? MaxEmptySearches { get; set; }

		[JsonInclude]
		[JsonPropertyName("query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Query { get; set; }

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

	public sealed partial class MlUpdateDatafeedRequestDescriptor<TDocument> : RequestDescriptorBase<MlUpdateDatafeedRequestDescriptor<TDocument>, MlUpdateDatafeedRequestParameters>
	{
		internal MlUpdateDatafeedRequestDescriptor(Action<MlUpdateDatafeedRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MlUpdateDatafeedRequestDescriptor(Elastic.Clients.Elasticsearch.Id datafeed_id) : base(r => r.Required("datafeed_id", datafeed_id))
		{
		}

		internal MlUpdateDatafeedRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningUpdateDatafeed;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public MlUpdateDatafeedRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public MlUpdateDatafeedRequestDescriptor<TDocument> ExpandWildcards(IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public MlUpdateDatafeedRequestDescriptor<TDocument> IgnoreThrottled(bool? ignoreThrottled = true) => Qs("ignore_throttled", ignoreThrottled);
		public MlUpdateDatafeedRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public MlUpdateDatafeedRequestDescriptor<TDocument> DatafeedId(Elastic.Clients.Elasticsearch.Id datafeed_id)
		{
			RouteValues.Required("datafeed_id", datafeed_id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor<TDocument> QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor<TDocument>> QueryDescriptorAction { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfigValue { get; set; }

		private ChunkingConfigDescriptor ChunkingConfigDescriptor { get; set; }

		private Action<ChunkingConfigDescriptor> ChunkingConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfigValue { get; set; }

		private DelayedDataCheckConfigDescriptor DelayedDataCheckConfigDescriptor { get; set; }

		private Action<DelayedDataCheckConfigDescriptor> DelayedDataCheckConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }

		private IEnumerable<string>? IndicesValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptionsValue { get; set; }

		private IndicesOptionsDescriptor IndicesOptionsDescriptor { get; set; }

		private Action<IndicesOptionsDescriptor> IndicesOptionsDescriptorAction { get; set; }

		private int? MaxEmptySearchesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? QueryDelayValue { get; set; }

		private Dictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>? RuntimeMappingsValue { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

		private int? ScrollSizeValue { get; set; }

		public MlUpdateDatafeedRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> Query(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> Query(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> Aggregations(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>> selector)
		{
			AggregationsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> ChunkingConfig(Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? chunkingConfig)
		{
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigValue = chunkingConfig;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> ChunkingConfig(ChunkingConfigDescriptor descriptor)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> ChunkingConfig(Action<ChunkingConfigDescriptor> configure)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? delayedDataCheckConfig)
		{
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigValue = delayedDataCheckConfig;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> DelayedDataCheckConfig(DelayedDataCheckConfigDescriptor descriptor)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> DelayedDataCheckConfig(Action<DelayedDataCheckConfigDescriptor> configure)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
		{
			FrequencyValue = frequency;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> Indices(IEnumerable<string>? indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> IndicesOptions(Elastic.Clients.Elasticsearch.IndicesOptions? indicesOptions)
		{
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsValue = indicesOptions;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> IndicesOptions(IndicesOptionsDescriptor descriptor)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> IndicesOptions(Action<IndicesOptionsDescriptor> configure)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> MaxEmptySearches(int? maxEmptySearches)
		{
			MaxEmptySearchesValue = maxEmptySearches;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> QueryDelay(Elastic.Clients.Elasticsearch.Duration? queryDelay)
		{
			QueryDelayValue = queryDelay;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>> selector)
		{
			RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> ScriptFields(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>> selector)
		{
			ScriptFieldsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor<TDocument> ScrollSize(int? scrollSize)
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
			else if (QueryValue is not null)
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

			if (IndicesValue is not null)
			{
				writer.WritePropertyName("indices");
				JsonSerializer.Serialize(writer, IndicesValue, options);
			}

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

	public sealed partial class MlUpdateDatafeedRequestDescriptor : RequestDescriptorBase<MlUpdateDatafeedRequestDescriptor, MlUpdateDatafeedRequestParameters>
	{
		internal MlUpdateDatafeedRequestDescriptor(Action<MlUpdateDatafeedRequestDescriptor> configure) => configure.Invoke(this);
		public MlUpdateDatafeedRequestDescriptor(Elastic.Clients.Elasticsearch.Id datafeed_id) : base(r => r.Required("datafeed_id", datafeed_id))
		{
		}

		internal MlUpdateDatafeedRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningUpdateDatafeed;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public MlUpdateDatafeedRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public MlUpdateDatafeedRequestDescriptor ExpandWildcards(IEnumerable<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public MlUpdateDatafeedRequestDescriptor IgnoreThrottled(bool? ignoreThrottled = true) => Qs("ignore_throttled", ignoreThrottled);
		public MlUpdateDatafeedRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public MlUpdateDatafeedRequestDescriptor DatafeedId(Elastic.Clients.Elasticsearch.Id datafeed_id)
		{
			RouteValues.Required("datafeed_id", datafeed_id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor> QueryDescriptorAction { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>? AggregationsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? ChunkingConfigValue { get; set; }

		private ChunkingConfigDescriptor ChunkingConfigDescriptor { get; set; }

		private Action<ChunkingConfigDescriptor> ChunkingConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? DelayedDataCheckConfigValue { get; set; }

		private DelayedDataCheckConfigDescriptor DelayedDataCheckConfigDescriptor { get; set; }

		private Action<DelayedDataCheckConfigDescriptor> DelayedDataCheckConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? FrequencyValue { get; set; }

		private IEnumerable<string>? IndicesValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndicesOptions? IndicesOptionsValue { get; set; }

		private IndicesOptionsDescriptor IndicesOptionsDescriptor { get; set; }

		private Action<IndicesOptionsDescriptor> IndicesOptionsDescriptorAction { get; set; }

		private int? MaxEmptySearchesValue { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? QueryDelayValue { get; set; }

		private Dictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>? RuntimeMappingsValue { get; set; }

		private Dictionary<string, Elastic.Clients.Elasticsearch.ScriptField>? ScriptFieldsValue { get; set; }

		private int? ScrollSizeValue { get; set; }

		public MlUpdateDatafeedRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor Query(QueryDsl.QueryContainerDescriptor descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor Query(Action<QueryDsl.QueryContainerDescriptor> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor Aggregations(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>, FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>> selector)
		{
			AggregationsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.AggregationContainer>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor ChunkingConfig(Elastic.Clients.Elasticsearch.Ml.ChunkingConfig? chunkingConfig)
		{
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigValue = chunkingConfig;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor ChunkingConfig(ChunkingConfigDescriptor descriptor)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptorAction = null;
			ChunkingConfigDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor ChunkingConfig(Action<ChunkingConfigDescriptor> configure)
		{
			ChunkingConfigValue = null;
			ChunkingConfigDescriptor = null;
			ChunkingConfigDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor DelayedDataCheckConfig(Elastic.Clients.Elasticsearch.Ml.DelayedDataCheckConfig? delayedDataCheckConfig)
		{
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigValue = delayedDataCheckConfig;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor DelayedDataCheckConfig(DelayedDataCheckConfigDescriptor descriptor)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptorAction = null;
			DelayedDataCheckConfigDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor DelayedDataCheckConfig(Action<DelayedDataCheckConfigDescriptor> configure)
		{
			DelayedDataCheckConfigValue = null;
			DelayedDataCheckConfigDescriptor = null;
			DelayedDataCheckConfigDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor Frequency(Elastic.Clients.Elasticsearch.Duration? frequency)
		{
			FrequencyValue = frequency;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor Indices(IEnumerable<string>? indices)
		{
			IndicesValue = indices;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor IndicesOptions(Elastic.Clients.Elasticsearch.IndicesOptions? indicesOptions)
		{
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsValue = indicesOptions;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor IndicesOptions(IndicesOptionsDescriptor descriptor)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptorAction = null;
			IndicesOptionsDescriptor = descriptor;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor IndicesOptions(Action<IndicesOptionsDescriptor> configure)
		{
			IndicesOptionsValue = null;
			IndicesOptionsDescriptor = null;
			IndicesOptionsDescriptorAction = configure;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor MaxEmptySearches(int? maxEmptySearches)
		{
			MaxEmptySearchesValue = maxEmptySearches;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor QueryDelay(Elastic.Clients.Elasticsearch.Duration? queryDelay)
		{
			QueryDelayValue = queryDelay;
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor RuntimeMappings(Func<FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>, FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>> selector)
		{
			RuntimeMappingsValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.Field, IEnumerable<Elastic.Clients.Elasticsearch.Mapping.RuntimeField>>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor ScriptFields(Func<FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>, FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>> selector)
		{
			ScriptFieldsValue = selector?.Invoke(new FluentDictionary<string, Elastic.Clients.Elasticsearch.ScriptField>());
			return Self;
		}

		public MlUpdateDatafeedRequestDescriptor ScrollSize(int? scrollSize)
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
			else if (QueryValue is not null)
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

			if (IndicesValue is not null)
			{
				writer.WritePropertyName("indices");
				JsonSerializer.Serialize(writer, IndicesValue, options);
			}

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