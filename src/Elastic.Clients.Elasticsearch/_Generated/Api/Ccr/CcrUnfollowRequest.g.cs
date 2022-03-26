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
	public class CcrUnfollowRequestParameters : RequestParameters<CcrUnfollowRequestParameters>
	{
	}

	public partial class CcrUnfollowRequest : PlainRequestBase<CcrUnfollowRequestParameters>
	{
		public CcrUnfollowRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationUnfollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
	}

	public sealed partial class CcrUnfollowRequestDescriptor<TDocument> : RequestDescriptorBase<CcrUnfollowRequestDescriptor<TDocument>, CcrUnfollowRequestParameters>
	{
		internal CcrUnfollowRequestDescriptor(Action<CcrUnfollowRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public CcrUnfollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		public CcrUnfollowRequestDescriptor(TDocument document) : this(typeof(TDocument))
		{
		}

		internal CcrUnfollowRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationUnfollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public CcrUnfollowRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class CcrUnfollowRequestDescriptor : RequestDescriptorBase<CcrUnfollowRequestDescriptor, CcrUnfollowRequestParameters>
	{
		internal CcrUnfollowRequestDescriptor(Action<CcrUnfollowRequestDescriptor> configure) => configure.Invoke(this);
		public CcrUnfollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal CcrUnfollowRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.CrossClusterReplicationUnfollow;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public CcrUnfollowRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}