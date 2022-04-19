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
namespace Elastic.Clients.Elasticsearch.Ml
{
	public class MlInferTrainedModelDeploymentRequestParameters : RequestParameters<MlInferTrainedModelDeploymentRequestParameters>
	{
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }
	}

	public partial class MlInferTrainedModelDeploymentRequest : PlainRequestBase<MlInferTrainedModelDeploymentRequestParameters>
	{
		public MlInferTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.Id model_id) : base(r => r.Required("model_id", model_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningInferTrainedModelDeployment;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonInclude]
		[JsonPropertyName("docs")]
		public IEnumerable<Dictionary<string, string>> Docs { get; set; }
	}

	public sealed partial class MlInferTrainedModelDeploymentRequestDescriptor : RequestDescriptorBase<MlInferTrainedModelDeploymentRequestDescriptor, MlInferTrainedModelDeploymentRequestParameters>
	{
		internal MlInferTrainedModelDeploymentRequestDescriptor(Action<MlInferTrainedModelDeploymentRequestDescriptor> configure) => configure.Invoke(this);
		public MlInferTrainedModelDeploymentRequestDescriptor(Elastic.Clients.Elasticsearch.Id model_id) : base(r => r.Required("model_id", model_id))
		{
		}

		internal MlInferTrainedModelDeploymentRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningInferTrainedModelDeployment;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public MlInferTrainedModelDeploymentRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public MlInferTrainedModelDeploymentRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Id model_id)
		{
			RouteValues.Required("model_id", model_id);
			return Self;
		}

		private IEnumerable<Dictionary<string, string>> DocsValue { get; set; }

		public MlInferTrainedModelDeploymentRequestDescriptor Docs(IEnumerable<Dictionary<string, string>> docs)
		{
			DocsValue = docs;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("docs");
			JsonSerializer.Serialize(writer, DocsValue, options);
			writer.WriteEndObject();
		}
	}
}