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
namespace Elastic.Clients.Elasticsearch.Cluster
{
	public sealed class ClusterRerouteRequestParameters : RequestParameters<ClusterRerouteRequestParameters>
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
		public IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Command>? Commands { get; set; }
	}

	public sealed partial class ClusterRerouteRequestDescriptor : RequestDescriptorBase<ClusterRerouteRequestDescriptor, ClusterRerouteRequestParameters>
	{
		internal ClusterRerouteRequestDescriptor(Action<ClusterRerouteRequestDescriptor> configure) => configure.Invoke(this);
		public ClusterRerouteRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.ClusterReroute;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => true;
		public ClusterRerouteRequestDescriptor DryRun(bool? dryRun = true) => Qs("dry_run", dryRun);
		public ClusterRerouteRequestDescriptor Explain(bool? explain = true) => Qs("explain", explain);
		public ClusterRerouteRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Time? masterTimeout) => Qs("master_timeout", masterTimeout);
		public ClusterRerouteRequestDescriptor Metric(Elastic.Clients.Elasticsearch.Metrics? metric) => Qs("metric", metric);
		public ClusterRerouteRequestDescriptor RetryFailed(bool? retryFailed = true) => Qs("retry_failed", retryFailed);
		public ClusterRerouteRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Time? timeout) => Qs("timeout", timeout);
		private IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Command>? CommandsValue { get; set; }

		private CommandDescriptor CommandsDescriptor { get; set; }

		private Action<CommandDescriptor> CommandsDescriptorAction { get; set; }

		private Action<CommandDescriptor>[] CommandsDescriptorActions { get; set; }

		public ClusterRerouteRequestDescriptor Commands(IEnumerable<Elastic.Clients.Elasticsearch.Cluster.Command>? commands)
		{
			CommandsDescriptor = null;
			CommandsDescriptorAction = null;
			CommandsDescriptorActions = null;
			CommandsValue = commands;
			return Self;
		}

		public ClusterRerouteRequestDescriptor Commands(CommandDescriptor descriptor)
		{
			CommandsValue = null;
			CommandsDescriptorAction = null;
			CommandsDescriptorActions = null;
			CommandsDescriptor = descriptor;
			return Self;
		}

		public ClusterRerouteRequestDescriptor Commands(Action<CommandDescriptor> configure)
		{
			CommandsValue = null;
			CommandsDescriptor = null;
			CommandsDescriptorActions = null;
			CommandsDescriptorAction = configure;
			return Self;
		}

		public ClusterRerouteRequestDescriptor Commands(params Action<CommandDescriptor>[] configure)
		{
			CommandsValue = null;
			CommandsDescriptor = null;
			CommandsDescriptorAction = null;
			CommandsDescriptorActions = configure;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (CommandsDescriptor is not null)
			{
				writer.WritePropertyName("commands");
				JsonSerializer.Serialize(writer, CommandsDescriptor, options);
			}
			else if (CommandsDescriptorAction is not null)
			{
				writer.WritePropertyName("commands");
				JsonSerializer.Serialize(writer, new CommandDescriptor(CommandsDescriptorAction), options);
			}
			else if (CommandsDescriptorActions is not null)
			{
				writer.WritePropertyName("commands");
				writer.WriteStartArray();
				foreach (var action in CommandsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new CommandDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else if (CommandsValue is not null)
			{
				writer.WritePropertyName("commands");
				JsonSerializer.Serialize(writer, CommandsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}