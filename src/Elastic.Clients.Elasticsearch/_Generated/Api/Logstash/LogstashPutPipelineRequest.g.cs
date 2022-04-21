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
namespace Elastic.Clients.Elasticsearch.Logstash
{
	public sealed class LogstashPutPipelineRequestParameters : RequestParameters<LogstashPutPipelineRequestParameters>
	{
	}

	public partial class LogstashPutPipelineRequest : PlainRequestBase<LogstashPutPipelineRequestParameters>
	{
		public LogstashPutPipelineRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.LogstashPutPipeline;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
	}

	public sealed partial class LogstashPutPipelineRequestDescriptor<TDocument> : RequestDescriptorBase<LogstashPutPipelineRequestDescriptor<TDocument>, LogstashPutPipelineRequestParameters>
	{
		internal LogstashPutPipelineRequestDescriptor(Action<LogstashPutPipelineRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public LogstashPutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal LogstashPutPipelineRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.LogstashPutPipeline;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		public LogstashPutPipelineRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class LogstashPutPipelineRequestDescriptor : RequestDescriptorBase<LogstashPutPipelineRequestDescriptor, LogstashPutPipelineRequestParameters>
	{
		internal LogstashPutPipelineRequestDescriptor(Action<LogstashPutPipelineRequestDescriptor> configure) => configure.Invoke(this);
		public LogstashPutPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
		{
		}

		internal LogstashPutPipelineRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.LogstashPutPipeline;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => false;
		public LogstashPutPipelineRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
		{
			RouteValues.Required("id", id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}