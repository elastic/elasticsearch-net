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
	public class IngestSimulateRequestParameters : RequestParameters<IngestSimulateRequestParameters>
	{
		[JsonIgnore]
		public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }
	}

	public partial class IngestSimulateRequest : PlainRequestBase<IngestSimulateRequestParameters>
	{
		public IngestSimulateRequest()
		{
		}

		public IngestSimulateRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestSimulate;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? Verbose { get => Q<bool?>("verbose"); set => Q("verbose", value); }

		[JsonInclude]
		[JsonPropertyName("docs")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Ingest.Simulate.Document>? Docs { get; set; }

		[JsonInclude]
		[JsonPropertyName("pipeline")]
		public Elastic.Clients.Elasticsearch.Ingest.Pipeline? Pipeline { get; set; }
	}

	public sealed partial class IngestSimulateRequestDescriptor<TDocument> : RequestDescriptorBase<IngestSimulateRequestDescriptor<TDocument>, IngestSimulateRequestParameters>
	{
		internal IngestSimulateRequestDescriptor(Action<IngestSimulateRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public IngestSimulateRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestSimulate;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public IngestSimulateRequestDescriptor<TDocument> Verbose(bool? verbose = true) => Qs("verbose", verbose);
		public IngestSimulateRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? id)
		{
			RouteValues.Optional("id", id);
			return Self;
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.Simulate.Document>? DocsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ingest.Pipeline? PipelineValue { get; set; }

		private PipelineDescriptor PipelineDescriptor { get; set; }

		private Action<PipelineDescriptor> PipelineDescriptorAction { get; set; }

		public IngestSimulateRequestDescriptor<TDocument> Docs(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.Simulate.Document>? docs)
		{
			DocsValue = docs;
			return Self;
		}

		public IngestSimulateRequestDescriptor<TDocument> Pipeline(Elastic.Clients.Elasticsearch.Ingest.Pipeline? pipeline)
		{
			PipelineDescriptor = null;
			PipelineDescriptorAction = null;
			PipelineValue = pipeline;
			return Self;
		}

		public IngestSimulateRequestDescriptor<TDocument> Pipeline(Ingest.PipelineDescriptor descriptor)
		{
			PipelineValue = null;
			PipelineDescriptorAction = null;
			PipelineDescriptor = descriptor;
			return Self;
		}

		public IngestSimulateRequestDescriptor<TDocument> Pipeline(Action<Ingest.PipelineDescriptor> configure)
		{
			PipelineValue = null;
			PipelineDescriptorAction = null;
			PipelineDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (DocsValue is not null)
			{
				writer.WritePropertyName("docs");
				JsonSerializer.Serialize(writer, DocsValue, options);
			}

			if (PipelineDescriptor is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, PipelineDescriptor, options);
			}
			else if (PipelineDescriptorAction is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, new Ingest.PipelineDescriptor(PipelineDescriptorAction), options);
			}
			else if (PipelineValue is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, PipelineValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class IngestSimulateRequestDescriptor : RequestDescriptorBase<IngestSimulateRequestDescriptor, IngestSimulateRequestParameters>
	{
		internal IngestSimulateRequestDescriptor(Action<IngestSimulateRequestDescriptor> configure) => configure.Invoke(this);
		public IngestSimulateRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestSimulate;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public IngestSimulateRequestDescriptor Verbose(bool? verbose = true) => Qs("verbose", verbose);
		public IngestSimulateRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? id)
		{
			RouteValues.Optional("id", id);
			return Self;
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Ingest.Simulate.Document>? DocsValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ingest.Pipeline? PipelineValue { get; set; }

		private PipelineDescriptor PipelineDescriptor { get; set; }

		private Action<PipelineDescriptor> PipelineDescriptorAction { get; set; }

		public IngestSimulateRequestDescriptor Docs(IEnumerable<Elastic.Clients.Elasticsearch.Ingest.Simulate.Document>? docs)
		{
			DocsValue = docs;
			return Self;
		}

		public IngestSimulateRequestDescriptor Pipeline(Elastic.Clients.Elasticsearch.Ingest.Pipeline? pipeline)
		{
			PipelineDescriptor = null;
			PipelineDescriptorAction = null;
			PipelineValue = pipeline;
			return Self;
		}

		public IngestSimulateRequestDescriptor Pipeline(Ingest.PipelineDescriptor descriptor)
		{
			PipelineValue = null;
			PipelineDescriptorAction = null;
			PipelineDescriptor = descriptor;
			return Self;
		}

		public IngestSimulateRequestDescriptor Pipeline(Action<Ingest.PipelineDescriptor> configure)
		{
			PipelineValue = null;
			PipelineDescriptorAction = null;
			PipelineDescriptorAction = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (DocsValue is not null)
			{
				writer.WritePropertyName("docs");
				JsonSerializer.Serialize(writer, DocsValue, options);
			}

			if (PipelineDescriptor is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, PipelineDescriptor, options);
			}
			else if (PipelineDescriptorAction is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, new Ingest.PipelineDescriptor(PipelineDescriptorAction), options);
			}
			else if (PipelineValue is not null)
			{
				writer.WritePropertyName("pipeline");
				JsonSerializer.Serialize(writer, PipelineValue, options);
			}

			writer.WriteEndObject();
		}
	}
}