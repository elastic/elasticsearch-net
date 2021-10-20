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
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class IndexPutAliasRequestParameters : RequestParameters<IndexPutAliasRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }
	}

	[InterfaceConverterAttribute(typeof(IndexPutAliasRequestDescriptorConverter<IndexPutAliasRequest>))]
	public partial interface IIndexPutAliasRequest : IRequest<IndexPutAliasRequestParameters>
	{
		QueryDsl.IQueryContainer? Filter { get; set; }

		string? IndexRouting { get; set; }

		bool? IsWriteIndex { get; set; }

		string? Routing { get; set; }

		string? SearchRouting { get; set; }
	}

	public partial class IndexPutAliasRequest : PlainRequestBase<IndexPutAliasRequestParameters>, IIndexPutAliasRequest
	{
		public IndexPutAliasRequest(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("indices", indices).Required("name", name))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutAlias;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonInclude]
		[JsonPropertyName("filter")]
		public QueryDsl.IQueryContainer? Filter { get; set; }

		[JsonInclude]
		[JsonPropertyName("index_routing")]
		public string? IndexRouting { get; set; }

		[JsonInclude]
		[JsonPropertyName("is_write_index")]
		public bool? IsWriteIndex { get; set; }

		[JsonInclude]
		[JsonPropertyName("routing")]
		public string? Routing { get; set; }

		[JsonInclude]
		[JsonPropertyName("search_routing")]
		public string? SearchRouting { get; set; }
	}

	public partial class IndexPutAliasRequestDescriptor : RequestDescriptorBase<IndexPutAliasRequestDescriptor, IndexPutAliasRequestParameters, IIndexPutAliasRequest>, IIndexPutAliasRequest
	{
		///<summary>/{index}/_alias/{name}</summary>
        public IndexPutAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("indices", indices).Required("name", name))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutAlias;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		QueryDsl.IQueryContainer? IIndexPutAliasRequest.Filter { get; set; }

		string? IIndexPutAliasRequest.IndexRouting { get; set; }

		bool? IIndexPutAliasRequest.IsWriteIndex { get; set; }

		string? IIndexPutAliasRequest.Routing { get; set; }

		string? IIndexPutAliasRequest.SearchRouting { get; set; }

		public IndexPutAliasRequestDescriptor Filter(QueryDsl.IQueryContainer? filter) => Assign(filter, (a, v) => a.Filter = v);
		public IndexPutAliasRequestDescriptor IndexRouting(string? indexRouting) => Assign(indexRouting, (a, v) => a.IndexRouting = v);
		public IndexPutAliasRequestDescriptor IsWriteIndex(bool? isWriteIndex = true) => Assign(isWriteIndex, (a, v) => a.IsWriteIndex = v);
		public IndexPutAliasRequestDescriptor Routing(string? routing) => Assign(routing, (a, v) => a.Routing = v);
		public IndexPutAliasRequestDescriptor SearchRouting(string? searchRouting) => Assign(searchRouting, (a, v) => a.SearchRouting = v);
		public IndexPutAliasRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public IndexPutAliasRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
	}

	internal sealed class IndexPutAliasRequestDescriptorConverter<TReadAs> : JsonConverter<IIndexPutAliasRequest> where TReadAs : class, IIndexPutAliasRequest
	{
		public override IIndexPutAliasRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => JsonSerializer.Deserialize<TReadAs>(ref reader, options);
		public override void Write(Utf8JsonWriter writer, IIndexPutAliasRequest value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.Filter is not null)
			{
				writer.WritePropertyName("filter");
				JsonSerializer.Serialize(writer, value.Filter, options);
			}

			if (value.IndexRouting is not null)
			{
				writer.WritePropertyName("index_routing");
				JsonSerializer.Serialize(writer, value.IndexRouting, options);
			}

			if (value.IsWriteIndex.HasValue)
			{
				writer.WritePropertyName("is_write_index");
				writer.WriteBooleanValue(value.IsWriteIndex.Value);
			}

			if (value.Routing is not null)
			{
				writer.WritePropertyName("routing");
				JsonSerializer.Serialize(writer, value.Routing, options);
			}

			if (value.SearchRouting is not null)
			{
				writer.WritePropertyName("search_routing");
				JsonSerializer.Serialize(writer, value.SearchRouting, options);
			}

			writer.WriteEndObject();
		}
	}
}