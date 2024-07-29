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

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class PutIndicesSettingsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index<br/>alias, or `_all` value targets only missing or closed indices. This<br/>behavior applies even if the request targets other open indices. For<br/>example, a request targeting `foo*,bar*` returns an error if an index<br/>starts with `foo` but no index starts with `bar`.</para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match. If the request can target<br/>data streams, this argument determines whether wildcard expressions match<br/>hidden data streams. Supports comma-separated values, such as<br/>`open,hidden`.</para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>If `true`, returns settings in flat format.</para>
	/// </summary>
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	/// <summary>
	/// <para>If `true`, returns settings in flat format.</para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>Period to wait for a connection to the master node. If no response is<br/>received before the timeout expires, the request fails and returns an<br/>error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>If `true`, existing index settings remain unchanged.</para>
	/// </summary>
	public bool? PreserveExisting { get => Q<bool?>("preserve_existing"); set => Q("preserve_existing", value); }

	/// <summary>
	/// <para>Period to wait for a response. If no response is received before the<br/> timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>Update index settings.<br/>Changes dynamic index settings in real time. For data streams, index setting<br/>changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutIndicesSettingsRequest : PlainRequest<PutIndicesSettingsRequestParameters>, ISelfSerializable
{
	public PutIndicesSettingsRequest()
	{
	}

	public PutIndicesSettingsRequest(Elastic.Clients.Elasticsearch.Serverless.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutSettings;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_settings";

	/// <summary>
	/// <para>If `false`, the request returns an error if any wildcard expression, index<br/>alias, or `_all` value targets only missing or closed indices. This<br/>behavior applies even if the request targets other open indices. For<br/>example, a request targeting `foo*,bar*` returns an error if an index<br/>starts with `foo` but no index starts with `bar`.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match. If the request can target<br/>data streams, this argument determines whether wildcard expressions match<br/>hidden data streams. Supports comma-separated values, such as<br/>`open,hidden`.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>If `true`, returns settings in flat format.</para>
	/// </summary>
	[JsonIgnore]
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	/// <summary>
	/// <para>If `true`, returns settings in flat format.</para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>Period to wait for a connection to the master node. If no response is<br/>received before the timeout expires, the request fails and returns an<br/>error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>If `true`, existing index settings remain unchanged.</para>
	/// </summary>
	[JsonIgnore]
	public bool? PreserveExisting { get => Q<bool?>("preserve_existing"); set => Q("preserve_existing", value); }

	/// <summary>
	/// <para>Period to wait for a response. If no response is received before the<br/> timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings Settings { get; set; }

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, Settings, options);
	}
}

/// <summary>
/// <para>Update index settings.<br/>Changes dynamic index settings in real time. For data streams, index setting<br/>changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutIndicesSettingsRequestDescriptor<TDocument> : RequestDescriptor<PutIndicesSettingsRequestDescriptor<TDocument>, PutIndicesSettingsRequestParameters>
{
	internal PutIndicesSettingsRequestDescriptor(Action<PutIndicesSettingsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
	public PutIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings, Elastic.Clients.Elasticsearch.Serverless.Indices? indices) : base(r => r.Optional("index", indices)) => SettingsValue = settings;
	public PutIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings) => SettingsValue = settings;

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutSettings;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_settings";

	public PutIndicesSettingsRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public PutIndicesSettingsRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public PutIndicesSettingsRequestDescriptor<TDocument> FlatSettings(bool? flatSettings = true) => Qs("flat_settings", flatSettings);
	public PutIndicesSettingsRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public PutIndicesSettingsRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutIndicesSettingsRequestDescriptor<TDocument> PreserveExisting(bool? preserveExisting = true) => Qs("preserve_existing", preserveExisting);
	public PutIndicesSettingsRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public PutIndicesSettingsRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Serverless.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument> SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument>> SettingsDescriptorAction { get; set; }

	public PutIndicesSettingsRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public PutIndicesSettingsRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument> descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public PutIndicesSettingsRequestDescriptor<TDocument> Settings(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument>> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, SettingsValue, options);
	}
}

/// <summary>
/// <para>Update index settings.<br/>Changes dynamic index settings in real time. For data streams, index setting<br/>changes are applied to all backing indices by default.</para>
/// </summary>
public sealed partial class PutIndicesSettingsRequestDescriptor : RequestDescriptor<PutIndicesSettingsRequestDescriptor, PutIndicesSettingsRequestParameters>
{
	internal PutIndicesSettingsRequestDescriptor(Action<PutIndicesSettingsRequestDescriptor> configure) => configure.Invoke(this);
	public PutIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings, Elastic.Clients.Elasticsearch.Serverless.Indices? indices) : base(r => r.Optional("index", indices)) => SettingsValue = settings;
	public PutIndicesSettingsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings) => SettingsValue = settings;

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPutSettings;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.put_settings";

	public PutIndicesSettingsRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public PutIndicesSettingsRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.Serverless.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public PutIndicesSettingsRequestDescriptor FlatSettings(bool? flatSettings = true) => Qs("flat_settings", flatSettings);
	public PutIndicesSettingsRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public PutIndicesSettingsRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutIndicesSettingsRequestDescriptor PreserveExisting(bool? preserveExisting = true) => Qs("preserve_existing", preserveExisting);
	public PutIndicesSettingsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public PutIndicesSettingsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Serverless.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor> SettingsDescriptorAction { get; set; }

	public PutIndicesSettingsRequestDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public PutIndicesSettingsRequestDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public PutIndicesSettingsRequestDescriptor Settings(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, SettingsValue, options);
	}
}