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
namespace Elastic.Clients.Elasticsearch.Ingest
{
	public sealed class IngestProcessorGrokRequestParameters : RequestParameters<IngestProcessorGrokRequestParameters>
	{
	}

	public partial class IngestProcessorGrokRequest : PlainRequestBase<IngestProcessorGrokRequestParameters>
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestProcessorGrok;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
	}

	public sealed partial class IngestProcessorGrokRequestDescriptor : RequestDescriptorBase<IngestProcessorGrokRequestDescriptor, IngestProcessorGrokRequestParameters>
	{
		internal IngestProcessorGrokRequestDescriptor(Action<IngestProcessorGrokRequestDescriptor> configure) => configure.Invoke(this);
		public IngestProcessorGrokRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestProcessorGrok;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override bool SupportsBody => false;
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}