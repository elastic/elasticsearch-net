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
namespace Elastic.Clients.Elasticsearch.Security
{
	public sealed class SecurityQueryApiKeysRequestParameters : RequestParameters<SecurityQueryApiKeysRequestParameters>
	{
	}

	public partial class SecurityQueryApiKeysRequest : PlainRequestBase<SecurityQueryApiKeysRequestParameters>
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityQueryApiKeys;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonInclude]
		[JsonPropertyName("query")]
		public Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? Query { get; set; }

		[JsonInclude]
		[JsonPropertyName("from")]
		public int? From { get; set; }

		[JsonInclude]
		[JsonPropertyName("sort")]
		public Elastic.Clients.Elasticsearch.Sort? Sort { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }

		[JsonInclude]
		[JsonPropertyName("search_after")]
		public IEnumerable<object>? SearchAfter { get; set; }
	}

	public sealed partial class SecurityQueryApiKeysRequestDescriptor<TDocument> : RequestDescriptorBase<SecurityQueryApiKeysRequestDescriptor<TDocument>, SecurityQueryApiKeysRequestParameters>
	{
		internal SecurityQueryApiKeysRequestDescriptor(Action<SecurityQueryApiKeysRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public SecurityQueryApiKeysRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityQueryApiKeys;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor<TDocument> QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor<TDocument>> QueryDescriptorAction { get; set; }

		private int? FromValue { get; set; }

		private IEnumerable<object>? SearchAfterValue { get; set; }

		private int? SizeValue { get; set; }

		private Elastic.Clients.Elasticsearch.Sort? SortValue { get; set; }

		public SecurityQueryApiKeysRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> Query(QueryDsl.QueryContainerDescriptor<TDocument> descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> Query(Action<QueryDsl.QueryContainerDescriptor<TDocument>> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> From(int? from)
		{
			FromValue = from;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> SearchAfter(IEnumerable<object>? searchAfter)
		{
			SearchAfterValue = searchAfter;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.Sort? sort)
		{
			SortValue = sort;
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

			if (FromValue.HasValue)
			{
				writer.WritePropertyName("from");
				writer.WriteNumberValue(FromValue.Value);
			}

			if (SearchAfterValue is not null)
			{
				writer.WritePropertyName("search_after");
				JsonSerializer.Serialize(writer, SearchAfterValue, options);
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
		}
	}

	public sealed partial class SecurityQueryApiKeysRequestDescriptor : RequestDescriptorBase<SecurityQueryApiKeysRequestDescriptor, SecurityQueryApiKeysRequestParameters>
	{
		internal SecurityQueryApiKeysRequestDescriptor(Action<SecurityQueryApiKeysRequestDescriptor> configure) => configure.Invoke(this);
		public SecurityQueryApiKeysRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.SecurityQueryApiKeys;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		private Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? QueryValue { get; set; }

		private QueryDsl.QueryContainerDescriptor QueryDescriptor { get; set; }

		private Action<QueryDsl.QueryContainerDescriptor> QueryDescriptorAction { get; set; }

		private int? FromValue { get; set; }

		private IEnumerable<object>? SearchAfterValue { get; set; }

		private int? SizeValue { get; set; }

		private Elastic.Clients.Elasticsearch.Sort? SortValue { get; set; }

		public SecurityQueryApiKeysRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer? query)
		{
			QueryDescriptor = null;
			QueryDescriptorAction = null;
			QueryValue = query;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor Query(QueryDsl.QueryContainerDescriptor descriptor)
		{
			QueryValue = null;
			QueryDescriptorAction = null;
			QueryDescriptor = descriptor;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor Query(Action<QueryDsl.QueryContainerDescriptor> configure)
		{
			QueryValue = null;
			QueryDescriptor = null;
			QueryDescriptorAction = configure;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor From(int? from)
		{
			FromValue = from;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor SearchAfter(IEnumerable<object>? searchAfter)
		{
			SearchAfterValue = searchAfter;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		public SecurityQueryApiKeysRequestDescriptor Sort(Elastic.Clients.Elasticsearch.Sort? sort)
		{
			SortValue = sort;
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

			if (FromValue.HasValue)
			{
				writer.WritePropertyName("from");
				writer.WriteNumberValue(FromValue.Value);
			}

			if (SearchAfterValue is not null)
			{
				writer.WritePropertyName("search_after");
				JsonSerializer.Serialize(writer, SearchAfterValue, options);
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
		}
	}
}