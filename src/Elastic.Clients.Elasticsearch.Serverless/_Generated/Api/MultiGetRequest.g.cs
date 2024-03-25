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

namespace Elastic.Clients.Elasticsearch.Serverless;

public sealed partial class MultiGetRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on. Random by default.</para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>If `true`, the request is real-time as opposed to near-real-time.</para>
	/// </summary>
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>If `true`, the request refreshes relevant shards before retrieving documents.</para>
	/// </summary>
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Serverless.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>True or false to return the `_source` field or not, or a list of fields to return.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to exclude from the response.<br/>You can also use this parameter to exclude fields from the subset specified in `_source_includes` query parameter.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to include in the response.<br/>If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the `_source_excludes` query parameter.<br/>If the `_source` parameter is `false`, this parameter is ignored.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>If `true`, retrieves the document fields stored in the index rather than the document `_source`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("stored_fields"); set => Q("stored_fields", value); }
}

/// <summary>
/// <para>Allows to get multiple documents in one request.</para>
/// </summary>
public sealed partial class MultiGetRequest : PlainRequest<MultiGetRequestParameters>
{
	public MultiGetRequest()
	{
	}

	public MultiGetRequest(Elastic.Clients.Elasticsearch.Serverless.IndexName? index) : base(r => r.Optional("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMget;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on. Random by default.</para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>If `true`, the request is real-time as opposed to near-real-time.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Realtime { get => Q<bool?>("realtime"); set => Q("realtime", value); }

	/// <summary>
	/// <para>If `true`, the request refreshes relevant shards before retrieving documents.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Serverless.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>True or false to return the `_source` field or not, or a list of fields to return.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to exclude from the response.<br/>You can also use this parameter to exclude fields from the subset specified in `_source_includes` query parameter.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to include in the response.<br/>If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the `_source_excludes` query parameter.<br/>If the `_source` parameter is `false`, this parameter is ignored.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>If `true`, retrieves the document fields stored in the index rather than the document `_source`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>The documents you want to retrieve. Required if no index is specified in the request URI.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("docs")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Core.MGet.MultiGetOperation>? Docs { get; set; }

	/// <summary>
	/// <para>The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ids")]
	public Elastic.Clients.Elasticsearch.Serverless.Ids? Ids { get; set; }
}

/// <summary>
/// <para>Allows to get multiple documents in one request.</para>
/// </summary>
public sealed partial class MultiGetRequestDescriptor<TDocument> : RequestDescriptor<MultiGetRequestDescriptor<TDocument>, MultiGetRequestParameters>
{
	internal MultiGetRequestDescriptor(Action<MultiGetRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public MultiGetRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMget;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	public MultiGetRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public MultiGetRequestDescriptor<TDocument> Realtime(bool? realtime = true) => Qs("realtime", realtime);
	public MultiGetRequestDescriptor<TDocument> Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public MultiGetRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Serverless.Routing? routing) => Qs("routing", routing);
	public MultiGetRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public MultiGetRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public MultiGetRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public MultiGetRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Serverless.Fields? storedFields) => Qs("stored_fields", storedFields);

	public MultiGetRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.Serverless.IndexName? index)
	{
		RouteValues.Optional("index", index);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Core.MGet.MultiGetOperation>? DocsValue { get; set; }
	private Core.MGet.MultiGetOperationDescriptor DocsDescriptor { get; set; }
	private Action<Core.MGet.MultiGetOperationDescriptor> DocsDescriptorAction { get; set; }
	private Action<Core.MGet.MultiGetOperationDescriptor>[] DocsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ids? IdsValue { get; set; }

	/// <summary>
	/// <para>The documents you want to retrieve. Required if no index is specified in the request URI.</para>
	/// </summary>
	public MultiGetRequestDescriptor<TDocument> Docs(ICollection<Elastic.Clients.Elasticsearch.Serverless.Core.MGet.MultiGetOperation>? docs)
	{
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsValue = docs;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(Core.MGet.MultiGetOperationDescriptor descriptor)
	{
		DocsValue = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsDescriptor = descriptor;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(Action<Core.MGet.MultiGetOperationDescriptor> configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorActions = null;
		DocsDescriptorAction = configure;
		return Self;
	}

	public MultiGetRequestDescriptor<TDocument> Docs(params Action<Core.MGet.MultiGetOperationDescriptor>[] configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.</para>
	/// </summary>
	public MultiGetRequestDescriptor<TDocument> Ids(Elastic.Clients.Elasticsearch.Serverless.Ids? ids)
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
			JsonSerializer.Serialize(writer, new Core.MGet.MultiGetOperationDescriptor(DocsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorActions is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var action in DocsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Core.MGet.MultiGetOperationDescriptor(action), options);
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
/// <para>Allows to get multiple documents in one request.</para>
/// </summary>
public sealed partial class MultiGetRequestDescriptor : RequestDescriptor<MultiGetRequestDescriptor, MultiGetRequestParameters>
{
	internal MultiGetRequestDescriptor(Action<MultiGetRequestDescriptor> configure) => configure.Invoke(this);

	public MultiGetRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceMget;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	public MultiGetRequestDescriptor Preference(string? preference) => Qs("preference", preference);
	public MultiGetRequestDescriptor Realtime(bool? realtime = true) => Qs("realtime", realtime);
	public MultiGetRequestDescriptor Refresh(bool? refresh = true) => Qs("refresh", refresh);
	public MultiGetRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Serverless.Routing? routing) => Qs("routing", routing);
	public MultiGetRequestDescriptor Source(Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public MultiGetRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public MultiGetRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public MultiGetRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Serverless.Fields? storedFields) => Qs("stored_fields", storedFields);

	public MultiGetRequestDescriptor Index(Elastic.Clients.Elasticsearch.Serverless.IndexName? index)
	{
		RouteValues.Optional("index", index);
		return Self;
	}

	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Core.MGet.MultiGetOperation>? DocsValue { get; set; }
	private Core.MGet.MultiGetOperationDescriptor DocsDescriptor { get; set; }
	private Action<Core.MGet.MultiGetOperationDescriptor> DocsDescriptorAction { get; set; }
	private Action<Core.MGet.MultiGetOperationDescriptor>[] DocsDescriptorActions { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Ids? IdsValue { get; set; }

	/// <summary>
	/// <para>The documents you want to retrieve. Required if no index is specified in the request URI.</para>
	/// </summary>
	public MultiGetRequestDescriptor Docs(ICollection<Elastic.Clients.Elasticsearch.Serverless.Core.MGet.MultiGetOperation>? docs)
	{
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsValue = docs;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(Core.MGet.MultiGetOperationDescriptor descriptor)
	{
		DocsValue = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsDescriptor = descriptor;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(Action<Core.MGet.MultiGetOperationDescriptor> configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorActions = null;
		DocsDescriptorAction = configure;
		return Self;
	}

	public MultiGetRequestDescriptor Docs(params Action<Core.MGet.MultiGetOperationDescriptor>[] configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.</para>
	/// </summary>
	public MultiGetRequestDescriptor Ids(Elastic.Clients.Elasticsearch.Serverless.Ids? ids)
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
			JsonSerializer.Serialize(writer, new Core.MGet.MultiGetOperationDescriptor(DocsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorActions is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var action in DocsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Core.MGet.MultiGetOperationDescriptor(action), options);
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