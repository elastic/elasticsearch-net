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
namespace Elastic.Clients.Elasticsearch.AsyncSearch
{
	public sealed class DeleteAsyncSearchRequestParameters : RequestParameters<DeleteAsyncSearchRequestParameters>
	{
	}

	public sealed partial class DeleteAsyncSearchRequest : PlainRequestBase<DeleteAsyncSearchRequestParameters>
	{
		public DeleteAsyncSearchRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.AsyncSearchDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
	}

	public sealed partial class DeleteAsyncSearchRequestDescriptor<TDocument> : RequestDescriptorBase<DeleteAsyncSearchRequestDescriptor<TDocument>, DeleteAsyncSearchRequestParameters>
	{
		internal DeleteAsyncSearchRequestDescriptor(Action<DeleteAsyncSearchRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DeleteAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal DeleteAsyncSearchRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.AsyncSearchDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public DeleteAsyncSearchRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class DeleteAsyncSearchRequestDescriptor : RequestDescriptorBase<DeleteAsyncSearchRequestDescriptor, DeleteAsyncSearchRequestParameters>
	{
		internal DeleteAsyncSearchRequestDescriptor(Action<DeleteAsyncSearchRequestDescriptor> configure) => configure.Invoke(this);
		public DeleteAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal DeleteAsyncSearchRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.AsyncSearchDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public DeleteAsyncSearchRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}