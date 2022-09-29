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
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public sealed partial class Alias
	{
		[JsonInclude]
		[JsonPropertyName("filter")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("index_routing")]
		public Elastic.Clients.Elasticsearch.Routing? IndexRouting { get; set; }

		[JsonInclude]
		[JsonPropertyName("is_hidden")]
		public bool? IsHidden { get; set; }

		[JsonInclude]
		[JsonPropertyName("is_write_index")]
		public bool? IsWriteIndex { get; set; }

		[JsonInclude]
		[JsonPropertyName("routing")]
		public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }

		[JsonInclude]
		[JsonPropertyName("search_routing")]
		public Elastic.Clients.Elasticsearch.Routing? SearchRouting { get; set; }
	}

	public sealed partial class AliasDescriptor<TDocument> : SerializableDescriptorBase<AliasDescriptor<TDocument>>
	{
		internal AliasDescriptor(Action<AliasDescriptor<TDocument>> configure) => configure.Invoke(this);
		public AliasDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryDsl.QueryContainerDescriptor<TDocument> FilterDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor<TDocument>> FilterDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? IndexRoutingValue { get; set; }

		private bool? IsHiddenValue { get; set; }

		private bool? IsWriteIndexValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? SearchRoutingValue { get; set; }

		public AliasDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public AliasDescriptor<TDocument> Filter(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public AliasDescriptor<TDocument> Filter(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public AliasDescriptor<TDocument> IndexRouting(Elastic.Clients.Elasticsearch.Routing? indexRouting)
		{
			IndexRoutingValue = indexRouting;
			return Self;
		}

		public AliasDescriptor<TDocument> IsHidden(bool? isHidden = true)
		{
			IsHiddenValue = isHidden;
			return Self;
		}

		public AliasDescriptor<TDocument> IsWriteIndex(bool? isWriteIndex = true)
		{
			IsWriteIndexValue = isWriteIndex;
			return Self;
		}

		public AliasDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing)
		{
			RoutingValue = routing;
			return Self;
		}

		public AliasDescriptor<TDocument> SearchRouting(Elastic.Clients.Elasticsearch.Routing? searchRouting)
		{
			SearchRoutingValue = searchRouting;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FilterDescriptor is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterDescriptor, options);
			}
			else if (FilterDescriptorAction is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor<TDocument>(FilterDescriptorAction), options);
			}
			else if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (IndexRoutingValue is not null)
			{
				writer.WritePropertyName("index_routing");
				JsonSerializer.Serialize(writer, IndexRoutingValue, options);
			}

			if (IsHiddenValue.HasValue)
			{
				writer.WritePropertyName("is_hidden");
				writer.WriteBooleanValue(IsHiddenValue.Value);
			}

			if (IsWriteIndexValue.HasValue)
			{
				writer.WritePropertyName("is_write_index");
				writer.WriteBooleanValue(IsWriteIndexValue.Value);
			}

			if (RoutingValue is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, RoutingValue, options);
			}

			if (SearchRoutingValue is not null)
			{
				writer.WritePropertyName("search_routing");
				JsonSerializer.Serialize(writer, SearchRoutingValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class AliasDescriptor : SerializableDescriptorBase<AliasDescriptor>
	{
		internal AliasDescriptor(Action<AliasDescriptor> configure) => configure.Invoke(this);
		public AliasDescriptor() : base()
		{
		}

		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? FilterValue { get; set; }

		private QueryDsl.QueryContainerDescriptor FilterDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor> FilterDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? IndexRoutingValue { get; set; }

		private bool? IsHiddenValue { get; set; }

		private bool? IsWriteIndexValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }

		private Elastic.Clients.Elasticsearch.Routing? SearchRoutingValue { get; set; }

		public AliasDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? filter)
		{
			FilterDescriptor = null;
			FilterDescriptorAction = null;
			FilterValue = filter;
			return Self;
		}

		public AliasDescriptor Filter(QueryDsl.QueryContainerDescriptor descriptor)
		{
			FilterValue = null;
			FilterDescriptorAction = null;
			FilterDescriptor = descriptor;
			return Self;
		}

		public AliasDescriptor Filter(Action<QueryDsl.QueryContainerDescriptor> configure)
		{
			FilterValue = null;
			FilterDescriptor = null;
			FilterDescriptorAction = configure;
			return Self;
		}

		public AliasDescriptor IndexRouting(Elastic.Clients.Elasticsearch.Routing? indexRouting)
		{
			IndexRoutingValue = indexRouting;
			return Self;
		}

		public AliasDescriptor IsHidden(bool? isHidden = true)
		{
			IsHiddenValue = isHidden;
			return Self;
		}

		public AliasDescriptor IsWriteIndex(bool? isWriteIndex = true)
		{
			IsWriteIndexValue = isWriteIndex;
			return Self;
		}

		public AliasDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing)
		{
			RoutingValue = routing;
			return Self;
		}

		public AliasDescriptor SearchRouting(Elastic.Clients.Elasticsearch.Routing? searchRouting)
		{
			SearchRoutingValue = searchRouting;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (FilterDescriptor is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterDescriptor, options);
			}
			else if (FilterDescriptorAction is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, new QueryDsl.QueryContainerDescriptor(FilterDescriptorAction), options);
			}
			else if (FilterValue is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, FilterValue, options);
			}

			if (IndexRoutingValue is not null)
			{
				writer.WritePropertyName("index_routing");
				JsonSerializer.Serialize(writer, IndexRoutingValue, options);
			}

			if (IsHiddenValue.HasValue)
			{
				writer.WritePropertyName("is_hidden");
				writer.WriteBooleanValue(IsHiddenValue.Value);
			}

			if (IsWriteIndexValue.HasValue)
			{
				writer.WritePropertyName("is_write_index");
				writer.WriteBooleanValue(IsWriteIndexValue.Value);
			}

			if (RoutingValue is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, RoutingValue, options);
			}

			if (SearchRoutingValue is not null)
			{
				writer.WritePropertyName("search_routing");
				JsonSerializer.Serialize(writer, SearchRoutingValue, options);
			}

			writer.WriteEndObject();
		}
	}
}