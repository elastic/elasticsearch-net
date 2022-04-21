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
namespace Elastic.Clients.Elasticsearch.Eql
{
	public sealed class EqlGetStatusRequestParameters : RequestParameters<EqlGetStatusRequestParameters>
	{
	}

	public partial class EqlGetStatusRequest : PlainRequestBase<EqlGetStatusRequestParameters>
	{
		public EqlGetStatusRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.EqlGetStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
	}

	public sealed partial class EqlGetStatusRequestDescriptor<TDocument> : RequestDescriptorBase<EqlGetStatusRequestDescriptor<TDocument>, EqlGetStatusRequestParameters>
	{
		internal EqlGetStatusRequestDescriptor(Action<EqlGetStatusRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public EqlGetStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal EqlGetStatusRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.EqlGetStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		public EqlGetStatusRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class EqlGetStatusRequestDescriptor : RequestDescriptorBase<EqlGetStatusRequestDescriptor, EqlGetStatusRequestParameters>
	{
		internal EqlGetStatusRequestDescriptor(Action<EqlGetStatusRequestDescriptor> configure) => configure.Invoke(this);
		public EqlGetStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal EqlGetStatusRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.EqlGetStatus;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		public EqlGetStatusRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}