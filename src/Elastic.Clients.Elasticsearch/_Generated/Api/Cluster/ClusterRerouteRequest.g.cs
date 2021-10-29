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

using Elastic.Clients.Elasticsearch.Experimental;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Cluster
{
	public class ClusterRerouteRequestParameters : RequestParameters<ClusterRerouteRequestParameters>
	{
		[JsonIgnore]
		public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

		[JsonIgnore]
		public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Metrics? Metric { get => Q<Elastic.Clients.Elasticsearch.Metrics?>("metric"); set => Q("metric", value); }

		[JsonIgnore]
		public bool? RetryFailed { get => Q<bool?>("retry_failed"); set => Q("retry_failed", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }
	}

	public partial class ClusterRerouteRequest : PlainRequestBase<ClusterRerouteRequestParameters>
	{
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterReroute;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		[JsonIgnore]
		public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

		[JsonIgnore]
		public bool? Explain { get => Q<bool?>("explain"); set => Q("explain", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Metrics? Metric { get => Q<Elastic.Clients.Elasticsearch.Metrics?>("metric"); set => Q("metric", value); }

		[JsonIgnore]
		public bool? RetryFailed { get => Q<bool?>("retry_failed"); set => Q("retry_failed", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Time? Timeout { get => Q<Elastic.Clients.Elasticsearch.Time?>("timeout"); set => Q("timeout", value); }

		[JsonInclude]
		[JsonPropertyName("commands")]
		public IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Reroute.Command>? Commands { get; set; }
	}

	[JsonConverter(typeof(ClusterRerouteRequestDescriptorConverter))]
	public partial class ClusterRerouteRequestDescriptor : RequestDescriptorBase<ClusterRerouteRequestDescriptor, ClusterRerouteRequestParameters>
	{
		internal IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Reroute.Command>? _commands;
		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterReroute;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public ClusterRerouteRequestDescriptor DryRun(bool? dryRun) => Qs("dry_run", dryRun);
		public ClusterRerouteRequestDescriptor Explain(bool? explain) => Qs("explain", explain);
		public ClusterRerouteRequestDescriptor Metric(Elastic.Clients.Elasticsearch.Metrics? metric) => Qs("metric", metric);
		public ClusterRerouteRequestDescriptor RetryFailed(bool? retryFailed) => Qs("retry_failed", retryFailed);
		public ClusterRerouteRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public ClusterRerouteRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		public ClusterRerouteRequestDescriptor Commands(IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Reroute.Command>? commands) => Assign(commands, (a, v) => a._commands = v);
	}

	internal sealed class ClusterRerouteRequestDescriptorConverter : JsonConverter<ClusterRerouteRequestDescriptor>
	{
		public override ClusterRerouteRequestDescriptor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, ClusterRerouteRequestDescriptor value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value._commands is not null)
			{
				writer.WritePropertyName("commands");
				JsonSerializer.Serialize(writer, value._commands, options);
			}

			writer.WriteEndObject();
		}
	}
}