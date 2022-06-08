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
namespace Elastic.Clients.Elasticsearch
{
	public sealed class MultiSearchRequestParameters : RequestParameters<MultiSearchRequestParameters>
	{
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public long? MaxConcurrentSearches { get => Q<long?>("max_concurrent_searches"); set => Q("max_concurrent_searches", value); }

		[JsonIgnore]
		public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

		[JsonIgnore]
		public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

		[JsonIgnore]
		public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

		[JsonIgnore]
		public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }
	}

	public partial class MultiSearchRequest : PlainRequestBase<MultiSearchRequestParameters>
	{
		public MultiSearchRequest()
		{
		}

		public MultiSearchRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceMsearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		[JsonIgnore]
		public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

		[JsonIgnore]
		public bool? CcsMinimizeRoundtrips { get => Q<bool?>("ccs_minimize_roundtrips"); set => Q("ccs_minimize_roundtrips", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.ExpandWildcards? ExpandWildcards { get => Q<Elastic.Clients.Elasticsearch.ExpandWildcards?>("expand_wildcards"); set => Q("expand_wildcards", value); }

		[JsonIgnore]
		public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

		[JsonIgnore]
		public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

		[JsonIgnore]
		public long? MaxConcurrentSearches { get => Q<long?>("max_concurrent_searches"); set => Q("max_concurrent_searches", value); }

		[JsonIgnore]
		public long? MaxConcurrentShardRequests { get => Q<long?>("max_concurrent_shard_requests"); set => Q("max_concurrent_shard_requests", value); }

		[JsonIgnore]
		public long? PreFilterShardSize { get => Q<long?>("pre_filter_shard_size"); set => Q("pre_filter_shard_size", value); }

		[JsonIgnore]
		public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

		[JsonIgnore]
		public Elastic.Clients.Elasticsearch.SearchType? SearchType { get => Q<Elastic.Clients.Elasticsearch.SearchType?>("search_type"); set => Q("search_type", value); }

		[JsonIgnore]
		public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }
	}

	public sealed partial class MultiSearchRequestDescriptor<TDocument> : RequestDescriptorBase<MultiSearchRequestDescriptor<TDocument>, MultiSearchRequestParameters>
	{
		internal MultiSearchRequestDescriptor(Action<MultiSearchRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MultiSearchRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceMsearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public MultiSearchRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public MultiSearchRequestDescriptor<TDocument> CcsMinimizeRoundtrips(bool? ccsMinimizeRoundtrips = true) => Qs("ccs_minimize_roundtrips", ccsMinimizeRoundtrips);
		public MultiSearchRequestDescriptor<TDocument> ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public MultiSearchRequestDescriptor<TDocument> IgnoreThrottled(bool? ignoreThrottled = true) => Qs("ignore_throttled", ignoreThrottled);
		public MultiSearchRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public MultiSearchRequestDescriptor<TDocument> MaxConcurrentSearches(long? maxConcurrentSearches) => Qs("max_concurrent_searches", maxConcurrentSearches);
		public MultiSearchRequestDescriptor<TDocument> MaxConcurrentShardRequests(long? maxConcurrentShardRequests) => Qs("max_concurrent_shard_requests", maxConcurrentShardRequests);
		public MultiSearchRequestDescriptor<TDocument> PreFilterShardSize(long? preFilterShardSize) => Qs("pre_filter_shard_size", preFilterShardSize);
		public MultiSearchRequestDescriptor<TDocument> RestTotalHitsAsInt(bool? restTotalHitsAsInt = true) => Qs("rest_total_hits_as_int", restTotalHitsAsInt);
		public MultiSearchRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
		public MultiSearchRequestDescriptor<TDocument> SearchType(Elastic.Clients.Elasticsearch.SearchType? searchType) => Qs("search_type", searchType);
		public MultiSearchRequestDescriptor<TDocument> TypedKeys(bool? typedKeys = true) => Qs("typed_keys", typedKeys);
		public MultiSearchRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
		{
			RouteValues.Optional("index", indices);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}

	public sealed partial class MultiSearchRequestDescriptor : RequestDescriptorBase<MultiSearchRequestDescriptor, MultiSearchRequestParameters>
	{
		internal MultiSearchRequestDescriptor(Action<MultiSearchRequestDescriptor> configure) => configure.Invoke(this);
		public MultiSearchRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceMsearch;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override bool SupportsBody => false;
		public MultiSearchRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
		public MultiSearchRequestDescriptor CcsMinimizeRoundtrips(bool? ccsMinimizeRoundtrips = true) => Qs("ccs_minimize_roundtrips", ccsMinimizeRoundtrips);
		public MultiSearchRequestDescriptor ExpandWildcards(Elastic.Clients.Elasticsearch.ExpandWildcards? expandWildcards) => Qs("expand_wildcards", expandWildcards);
		public MultiSearchRequestDescriptor IgnoreThrottled(bool? ignoreThrottled = true) => Qs("ignore_throttled", ignoreThrottled);
		public MultiSearchRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
		public MultiSearchRequestDescriptor MaxConcurrentSearches(long? maxConcurrentSearches) => Qs("max_concurrent_searches", maxConcurrentSearches);
		public MultiSearchRequestDescriptor MaxConcurrentShardRequests(long? maxConcurrentShardRequests) => Qs("max_concurrent_shard_requests", maxConcurrentShardRequests);
		public MultiSearchRequestDescriptor PreFilterShardSize(long? preFilterShardSize) => Qs("pre_filter_shard_size", preFilterShardSize);
		public MultiSearchRequestDescriptor RestTotalHitsAsInt(bool? restTotalHitsAsInt = true) => Qs("rest_total_hits_as_int", restTotalHitsAsInt);
		public MultiSearchRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
		public MultiSearchRequestDescriptor SearchType(Elastic.Clients.Elasticsearch.SearchType? searchType) => Qs("search_type", searchType);
		public MultiSearchRequestDescriptor TypedKeys(bool? typedKeys = true) => Qs("typed_keys", typedKeys);
		public MultiSearchRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
		{
			RouteValues.Optional("index", indices);
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
		}
	}
}