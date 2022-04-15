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
	public class MlDeleteModelSnapshotRequestParameters : RequestParameters<MlDeleteModelSnapshotRequestParameters>
	{
	}

	public partial class MlDeleteModelSnapshotRequest : PlainRequestBase<MlDeleteModelSnapshotRequestParameters>
	{
		public MlDeleteModelSnapshotRequest(Elastic.Clients.Elasticsearch.Id job_id, Elastic.Clients.Elasticsearch.Id snapshot_id) : base(r => r.Required("job_id", job_id).Required("snapshot_id", snapshot_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningDeleteModelSnapshot;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
	}

	public sealed partial class MlDeleteModelSnapshotRequestDescriptor : RequestDescriptorBase<MlDeleteModelSnapshotRequestDescriptor, MlDeleteModelSnapshotRequestParameters>
	{
		internal MlDeleteModelSnapshotRequestDescriptor(Action<MlDeleteModelSnapshotRequestDescriptor> configure) => configure.Invoke(this);
		public MlDeleteModelSnapshotRequestDescriptor(Elastic.Clients.Elasticsearch.Id job_id, Elastic.Clients.Elasticsearch.Id snapshot_id) : base(r => r.Required("job_id", job_id).Required("snapshot_id", snapshot_id))
		{
		}

		internal MlDeleteModelSnapshotRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningDeleteModelSnapshot;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public MlDeleteModelSnapshotRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id job_id)
		{
			RouteValues.Required("job_id", job_id);
			return Self;
		}

		public MlDeleteModelSnapshotRequestDescriptor SnapshotId(Elastic.Clients.Elasticsearch.Id snapshot_id)
		{
			RouteValues.Required("snapshot_id", snapshot_id);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}