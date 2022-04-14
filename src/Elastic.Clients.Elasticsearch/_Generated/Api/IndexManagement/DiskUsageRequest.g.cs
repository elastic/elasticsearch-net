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
namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	public class DiskUsageRequestParameters : RequestParameters<DiskUsageRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? Flush { get => Q<bool?>("flush"); set => Q("flush", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TimeUnit? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.TimeUnit?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TimeUnit? Timeout { get => Q<Elastic.Clients.Elasticsearch.TimeUnit?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? RunExpensiveTasks { get => Q<bool?>("run_expensive_tasks"); set => Q("run_expensive_tasks", value); }

		[JsonIgnore]
		public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public partial class DiskUsageRequest : PlainRequestBase<DiskUsageRequestParameters>
	{
		public DiskUsageRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementDiskUsage;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? Flush { get => Q<bool?>("flush"); set => Q("flush", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TimeUnit? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.TimeUnit?>("master_timeout"); set => Q("master_timeout", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.TimeUnit? Timeout { get => Q<Elastic.Clients.Elasticsearch.TimeUnit?>("timeout"); set => Q("timeout", value); }

		[JsonIgnore]
		public bool? RunExpensiveTasks { get => Q<bool?>("run_expensive_tasks"); set => Q("run_expensive_tasks", value); }

		[JsonIgnore]
		public string? WaitForActiveShards { get => Q<string?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
	}

	public sealed partial class DiskUsageRequestDescriptor<TDocument> : RequestDescriptorBase<DiskUsageRequestDescriptor<TDocument>, DiskUsageRequestParameters>
	{
		internal DiskUsageRequestDescriptor(Action<DiskUsageRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public DiskUsageRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		public DiskUsageRequestDescriptor(TDocument document) : this(typeof(TDocument))
		{
		}

		internal DiskUsageRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementDiskUsage;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public DiskUsageRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public DiskUsageRequestDescriptor<TDocument> ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public DiskUsageRequestDescriptor<TDocument> Flush(bool? flush = true) => Qs("flush", flush);
		public DiskUsageRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public DiskUsageRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.TimeUnit? masterTimeout) => Qs("master_timeout", masterTimeout);
		public DiskUsageRequestDescriptor<TDocument> RunExpensiveTasks(bool? runExpensiveTasks = true) => Qs("run_expensive_tasks", runExpensiveTasks);
		public DiskUsageRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.TimeUnit? timeout) => Qs("timeout", timeout);
		public DiskUsageRequestDescriptor<TDocument> WaitForActiveShards(string? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		public DiskUsageRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class DiskUsageRequestDescriptor : RequestDescriptorBase<DiskUsageRequestDescriptor, DiskUsageRequestParameters>
	{
		internal DiskUsageRequestDescriptor(Action<DiskUsageRequestDescriptor> configure) => configure.Invoke(this);
		public DiskUsageRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
		{
		}

		internal DiskUsageRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementDiskUsage;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public DiskUsageRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public DiskUsageRequestDescriptor ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public DiskUsageRequestDescriptor Flush(bool? flush = true) => Qs("flush", flush);
		public DiskUsageRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public DiskUsageRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.TimeUnit? masterTimeout) => Qs("master_timeout", masterTimeout);
		public DiskUsageRequestDescriptor RunExpensiveTasks(bool? runExpensiveTasks = true) => Qs("run_expensive_tasks", runExpensiveTasks);
		public DiskUsageRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.TimeUnit? timeout) => Qs("timeout", timeout);
		public DiskUsageRequestDescriptor WaitForActiveShards(string? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
		public DiskUsageRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
		{
			RouteValues.Required("index", index);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}