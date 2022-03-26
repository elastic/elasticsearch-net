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
namespace Elastic.Clients.Elasticsearch.Ccr
{
	public class CcrPauseFollowRequestParameters : RequestParameters<CcrPauseFollowRequestParameters>
	{
	}

	public partial class CcrPauseFollowRequest : PlainRequestBase<CcrPauseFollowRequestParameters>
	{
		public CcrPauseFollowRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationPauseFollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public sealed partial class CcrPauseFollowRequestDescriptor<TDocument> : RequestDescriptorBase<CcrPauseFollowRequestDescriptor<TDocument>, CcrPauseFollowRequestParameters>
	{
		internal CcrPauseFollowRequestDescriptor(Action<CcrPauseFollowRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public CcrPauseFollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		public CcrPauseFollowRequestDescriptor(TDocument document) : this(typeof(TDocument))
		{
		}

		internal CcrPauseFollowRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationPauseFollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public CcrPauseFollowRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class CcrPauseFollowRequestDescriptor : RequestDescriptorBase<CcrPauseFollowRequestDescriptor, CcrPauseFollowRequestParameters>
	{
		internal CcrPauseFollowRequestDescriptor(Action<CcrPauseFollowRequestDescriptor> configure) => configure.Invoke(this);
		public CcrPauseFollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal CcrPauseFollowRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationPauseFollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public CcrPauseFollowRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}