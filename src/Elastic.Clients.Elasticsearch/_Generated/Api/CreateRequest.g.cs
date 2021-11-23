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
namespace Elastic.Clients.Elasticsearch
{
	public class CreateRequestParameters : RequestParameters<CreateRequestParameters>
	{
		[JsonIgnore]
		public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public long? Version { get => Q<long?>("version"); set => Q("version", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public partial class CreateRequest<TDocument> : PlainRequestBase<CreateRequestParameters>
	{
		public CreateRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceCreate;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public TDocument Document { get; set; }

		[JsonIgnore]
		public string? Pipeline { get => Q<string?>("pipeline"); set => Q("pipeline", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }

		[JsonIgnore]
		public string? Routing { get => Q<string?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public long? Version { get => Q<long?>("version"); set => Q("version", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public sealed partial class CreateRequestDescriptor<TDocument> : RequestDescriptorBase<CreateRequestDescriptor<TDocument>, CreateRequestParameters>
	{

		public CreateRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		internal CreateRequestDescriptor(Action<CreateRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceCreate;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		internal TDocument DocumentValue { get; private set; }

		public CreateRequestDescriptor<TDocument> Pipeline(string? pipeline) => Qs("pipeline", pipeline);
		public CreateRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Refresh? refresh) => Qs("refresh", refresh);
		public CreateRequestDescriptor<TDocument> Routing(string? routing) => Qs("routing", routing);
		public CreateRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public CreateRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
		public CreateRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? versionType) => Qs("version_type", versionType);
		public CreateRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WriteEndObject();
		}
	}
}
