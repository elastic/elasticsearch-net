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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class MultiGetRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Should this request force synthetic _source?
	/// Use this to test if the mapping supports synthetic _source and to get a sense of the worst case performance.
	/// Fetches with this enabled will be slower the enabling synthetic source natively in the index.
	/// </para>
	/// </summary>
	public bool? ForceSyntheticSource { get => Q<bool?>("force_synthetic_source"); set => Q("force_synthetic_source", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on. Random by default.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes relevant shards before retrieving documents.
	/// </para>
	/// </summary>
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }
}

/// <summary>
/// <para>
/// Get multiple documents.
/// </para>
/// <para>
/// Get multiple JSON documents by ID from one or more indices.
/// If you specify an index in the request URI, you only need to specify the document IDs in the request body.
/// To ensure fast responses, this multi get (mget) API responds with partial results if one or more shards fail.
/// </para>
/// <para>
/// <strong>Filter source fields</strong>
/// </para>
/// <para>
/// By default, the <c>_source</c> field is returned for every document (if stored).
/// Use the <c>_source</c> and <c>_source_include</c> or <c>source_exclude</c> attributes to filter what fields are returned for a particular document.
/// You can include the <c>_source</c>, <c>_source_includes</c>, and <c>_source_excludes</c> query parameters in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// <para>
/// <strong>Get stored fields</strong>
/// </para>
/// <para>
/// Use the <c>stored_fields</c> attribute to specify the set of stored fields you want to retrieve.
/// Any requested fields that are not stored are ignored.
/// You can include the <c>stored_fields</c> query parameter in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// </summary>
public sealed partial class MultiGetRequest : PlainRequest<MultiGetRequestParameters>
{
	public MultiGetRequest()
	{
	}

	public MultiGetRequest(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMultiGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	/// <summary>
	/// <para>
	/// Should this request force synthetic _source?
	/// Use this to test if the mapping supports synthetic _source and to get a sense of the worst case performance.
	/// Fetches with this enabled will be slower the enabling synthetic source natively in the index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? ForceSyntheticSource { get => Q<bool?>("force_synthetic_source"); set => Q("force_synthetic_source", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on. Random by default.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes relevant shards before retrieving documents.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("docs")]
	public ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? Docs { get; set; }

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ids")]
	public Elastic.Clients.Elasticsearch.Ids? Ids { get; set; }
}

/// <summary>
/// <para>
/// Get multiple documents.
/// </para>
/// <para>
/// Get multiple JSON documents by ID from one or more indices.
/// If you specify an index in the request URI, you only need to specify the document IDs in the request body.
/// To ensure fast responses, this multi get (mget) API responds with partial results if one or more shards fail.
/// </para>
/// <para>
/// <strong>Filter source fields</strong>
/// </para>
/// <para>
/// By default, the <c>_source</c> field is returned for every document (if stored).
/// Use the <c>_source</c> and <c>_source_include</c> or <c>source_exclude</c> attributes to filter what fields are returned for a particular document.
/// You can include the <c>_source</c>, <c>_source_includes</c>, and <c>_source_excludes</c> query parameters in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// <para>
/// <strong>Get stored fields</strong>
/// </para>
/// <para>
/// Use the <c>stored_fields</c> attribute to specify the set of stored fields you want to retrieve.
/// Any requested fields that are not stored are ignored.
/// You can include the <c>stored_fields</c> query parameter in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// </summary>
public sealed partial class MultiGetRequestDescriptor<TDocument> : RequestDescriptor<MultiGetRequestDescriptor<TDocument>, MultiGetRequestParameters>
{
	internal MultiGetRequestDescriptor(Action<MultiGetRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
	{
	}

	public MultiGetRequestDescriptor() : this(typeof(TDocument))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMultiGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	public MultiGetRequestDescriptor<TDocument> ForceSyntheticSource(bool? forceSyntheticSource = true) => Qs("force_synthetic_source", forceSyntheticSource);
	public MultiGetRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public MultiGetRequestDescriptor<TDocument> Realtime(bool? realtime = true) => Qs("realtime", realtime);
	public MultiGetRequestDescriptor<TDocument> Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public MultiGetRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public MultiGetRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public MultiGetRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public MultiGetRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public MultiGetRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Fields? storedFields) => Qs("stored_fields", storedFields);

	public MultiGetRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName? index)
	{
		RouteValues.Optional("index", index);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? DocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> DocsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>> DocsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>>[] DocsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Ids? IdsValue { get; set; }

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public MultiGetRequestDescriptor<TDocument> Docs(ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? docs)
	{
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsValue = docs;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument> descriptor)
	{
		DocsValue = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsDescriptor = descriptor;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>> configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorActions = null;
		DocsDescriptorAction = configure;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(params Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>>[] configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
	public MultiGetRequestDescriptor<TDocument> Ids(Elastic.Clients.Elasticsearch.Ids? ids)
	{
		IdsValue = ids;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DocsDescriptor is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DocsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorAction is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>(DocsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorActions is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var action in DocsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DocsValue is not null)
		{
			writer.WritePropertyName("docs");
			JsonSerializer.Serialize(writer, DocsValue, options);
		}

		if (IdsValue is not null)
		{
			writer.WritePropertyName("ids");
			JsonSerializer.Serialize(writer, IdsValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get multiple documents.
/// </para>
/// <para>
/// Get multiple JSON documents by ID from one or more indices.
/// If you specify an index in the request URI, you only need to specify the document IDs in the request body.
/// To ensure fast responses, this multi get (mget) API responds with partial results if one or more shards fail.
/// </para>
/// <para>
/// <strong>Filter source fields</strong>
/// </para>
/// <para>
/// By default, the <c>_source</c> field is returned for every document (if stored).
/// Use the <c>_source</c> and <c>_source_include</c> or <c>source_exclude</c> attributes to filter what fields are returned for a particular document.
/// You can include the <c>_source</c>, <c>_source_includes</c>, and <c>_source_excludes</c> query parameters in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// <para>
/// <strong>Get stored fields</strong>
/// </para>
/// <para>
/// Use the <c>stored_fields</c> attribute to specify the set of stored fields you want to retrieve.
/// Any requested fields that are not stored are ignored.
/// You can include the <c>stored_fields</c> query parameter in the request URI to specify the defaults to use when there are no per-document instructions.
/// </para>
/// </summary>
public sealed partial class MultiGetRequestDescriptor : RequestDescriptor<MultiGetRequestDescriptor, MultiGetRequestParameters>
{
	internal MultiGetRequestDescriptor(Action<MultiGetRequestDescriptor> configure) => configure.Invoke(this);

	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
	{
	}

	public MultiGetRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMultiGet;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	public MultiGetRequestDescriptor ForceSyntheticSource(bool? forceSyntheticSource = true) => Qs("force_synthetic_source", forceSyntheticSource);
	public MultiGetRequestDescriptor Preference(string? preference) => Qs("preference", preference);
	public MultiGetRequestDescriptor Realtime(bool? realtime = true) => Qs("realtime", realtime);
	public MultiGetRequestDescriptor Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public MultiGetRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);
	public MultiGetRequestDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public MultiGetRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public MultiGetRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public MultiGetRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Fields? storedFields) => Qs("stored_fields", storedFields);

	public MultiGetRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? index)
	{
		RouteValues.Optional("index", index);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? DocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor DocsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor> DocsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor>[] DocsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Ids? IdsValue { get; set; }

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public MultiGetRequestDescriptor Docs(ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? docs)
	{
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsValue = docs;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor descriptor)
	{
		DocsValue = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsDescriptor = descriptor;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor> configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorActions = null;
		DocsDescriptorAction = configure;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(params Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor>[] configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
	public MultiGetRequestDescriptor Ids(Elastic.Clients.Elasticsearch.Ids? ids)
	{
		IdsValue = ids;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DocsDescriptor is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DocsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorAction is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor(DocsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorActions is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var action in DocsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DocsValue is not null)
		{
			writer.WritePropertyName("docs");
			JsonSerializer.Serialize(writer, DocsValue, options);
		}

		if (IdsValue is not null)
		{
			writer.WritePropertyName("ids");
			JsonSerializer.Serialize(writer, IdsValue, options);
		}

		writer.WriteEndObject();
	}
}