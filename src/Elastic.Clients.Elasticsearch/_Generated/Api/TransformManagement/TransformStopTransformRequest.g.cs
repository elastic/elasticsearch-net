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
namespace Elastic.Clients.Elasticsearch.TransformManagement
{
	public sealed class TransformStopTransformRequestParameters : RequestParameters<TransformStopTransformRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

		[JsonIgnore]
		public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? WaitForCheckpoint { get => Q<bool?>("wait_for_checkpoint"); set => Q("wait_for_checkpoint", value); }

		[JsonIgnore]
		public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
	}

	public partial class TransformStopTransformRequest : PlainRequestBase<TransformStopTransformRequestParameters>
	{
		public TransformStopTransformRequest(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementStopTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		[JsonIgnore]
		public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

		[JsonIgnore]
		public bool? Force { get => Q<bool?>("force"); set => Q("force", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? WaitForCheckpoint { get => Q<bool?>("wait_for_checkpoint"); set => Q("wait_for_checkpoint", value); }

		[JsonIgnore]
		public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
	}

	public sealed partial class TransformStopTransformRequestDescriptor : RequestDescriptorBase<TransformStopTransformRequestDescriptor, TransformStopTransformRequestParameters>
	{
		internal TransformStopTransformRequestDescriptor(Action<TransformStopTransformRequestDescriptor> configure) => configure.Invoke(this);
		public TransformStopTransformRequestDescriptor(Elastic.Clients.Elasticsearch.Name transform_id) : base(r => r.Required("transform_id", transform_id))
		{
		}

		internal TransformStopTransformRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.TransformManagementStopTransform;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public TransformStopTransformRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);
		public TransformStopTransformRequestDescriptor Force(bool? force = true) => Qs("force", force);
		public TransformStopTransformRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
		public TransformStopTransformRequestDescriptor WaitForCheckpoint(bool? waitForCheckpoint = true) => Qs("wait_for_checkpoint", waitForCheckpoint);
		public TransformStopTransformRequestDescriptor WaitForCompletion(bool? waitForCompletion = true) => Qs("wait_for_completion", waitForCompletion);
		public TransformStopTransformRequestDescriptor TransformId(Elastic.Clients.Elasticsearch.Name transform_id)
		{
			RouteValues.Required("transform_id", transform_id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}