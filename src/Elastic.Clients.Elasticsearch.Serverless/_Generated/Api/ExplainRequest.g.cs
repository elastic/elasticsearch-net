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

public sealed class ExplainRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Analyzer to use for the query string.<br/>This parameter can only be used when the `q` query string parameter is specified.</para>
	/// </summary>
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>If `true`, wildcard and prefix queries are analyzed.</para>
	/// </summary>
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>The default operator for query string query: `AND` or `OR`.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>Field to use as default where no field prefix is given in the query string.</para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>If `true`, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.</para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on.<br/>Random by default.</para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>Custom value used to route operations to a specific shard.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Serverless.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>True or false to return the `_source` field or not, or a list of fields to return.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to exclude from the response.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to include in the response.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>A comma-separated list of stored fields to return in the response.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>Query in the Lucene query string syntax.</para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }
}

/// <summary>
/// <para>Returns information about why a specific document matches (or doesn’t match) a query.</para>
/// </summary>
public sealed partial class ExplainRequest : PlainRequest<ExplainRequestParameters>
{
	public ExplainRequest(Elastic.Clients.Elasticsearch.Serverless.IndexName index, Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceExplain;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "explain";

	/// <summary>
	/// <para>Analyzer to use for the query string.<br/>This parameter can only be used when the `q` query string parameter is specified.</para>
	/// </summary>
	[JsonIgnore]
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>If `true`, wildcard and prefix queries are analyzed.</para>
	/// </summary>
	[JsonIgnore]
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>The default operator for query string query: `AND` or `OR`.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>Field to use as default where no field prefix is given in the query string.</para>
	/// </summary>
	[JsonIgnore]
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>If `true`, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.</para>
	/// </summary>
	[JsonIgnore]
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>Specifies the node or shard the operation should be performed on.<br/>Random by default.</para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

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
	/// <para>A comma-separated list of source fields to exclude from the response.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>A comma-separated list of source fields to include in the response.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>A comma-separated list of stored fields to return in the response.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Serverless.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>Query in the Lucene query string syntax.</para>
	/// </summary>
	[JsonIgnore]
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>Defines the search definition using the Query DSL.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? Query { get; set; }
}

/// <summary>
/// <para>Returns information about why a specific document matches (or doesn’t match) a query.</para>
/// </summary>
public sealed partial class ExplainRequestDescriptor<TDocument> : RequestDescriptor<ExplainRequestDescriptor<TDocument>, ExplainRequestParameters>
{
	internal ExplainRequestDescriptor(Action<ExplainRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index, Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	public ExplainRequestDescriptor(TDocument document) : this(typeof(TDocument), Serverless.Id.From(document))
	{
	}

	public ExplainRequestDescriptor(TDocument document, IndexName index, Id id) : this(index, id)
	{
	}

	public ExplainRequestDescriptor(TDocument document, IndexName index) : this(index, Serverless.Id.From(document))
	{
	}

	public ExplainRequestDescriptor(TDocument document, Id id) : this(typeof(TDocument), id)
	{
	}

	public ExplainRequestDescriptor(Id id) : this(typeof(TDocument), id)
	{
	}

	internal ExplainRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceExplain;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "explain";

	public ExplainRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public ExplainRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public ExplainRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public ExplainRequestDescriptor<TDocument> AnalyzeWildcard(bool? analyzeWildcard = true) => Qs("analyze_wildcard", analyzeWildcard);
	public ExplainRequestDescriptor<TDocument> Analyzer(string? analyzer) => Qs("analyzer", analyzer);
	public ExplainRequestDescriptor<TDocument> DefaultOperator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? defaultOperator) => Qs("default_operator", defaultOperator);
	public ExplainRequestDescriptor<TDocument> Df(string? df) => Qs("df", df);
	public ExplainRequestDescriptor<TDocument> Lenient(bool? lenient = true) => Qs("lenient", lenient);
	public ExplainRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public ExplainRequestDescriptor<TDocument> QueryLuceneSyntax(string? q) => Qs("q", q);
	public ExplainRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Serverless.Routing? routing) => Qs("routing", routing);
	public ExplainRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Serverless.Fields? storedFields) => Qs("stored_fields", storedFields);

	public ExplainRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	public ExplainRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.Serverless.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }

	/// <summary>
	/// <para>Defines the search definition using the Query DSL.</para>
	/// </summary>
	public ExplainRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public ExplainRequestDescriptor<TDocument> Query(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public ExplainRequestDescriptor<TDocument> Query(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>Returns information about why a specific document matches (or doesn’t match) a query.</para>
/// </summary>
public sealed partial class ExplainRequestDescriptor : RequestDescriptor<ExplainRequestDescriptor, ExplainRequestParameters>
{
	internal ExplainRequestDescriptor(Action<ExplainRequestDescriptor> configure) => configure.Invoke(this);

	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index, Elastic.Clients.Elasticsearch.Serverless.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}

	internal ExplainRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceExplain;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "explain";

	public ExplainRequestDescriptor Source(Elastic.Clients.Elasticsearch.Serverless.Core.Search.SourceConfigParam? source) => Qs("_source", source);
	public ExplainRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceExcludes) => Qs("_source_excludes", sourceExcludes);
	public ExplainRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Serverless.Fields? sourceIncludes) => Qs("_source_includes", sourceIncludes);
	public ExplainRequestDescriptor AnalyzeWildcard(bool? analyzeWildcard = true) => Qs("analyze_wildcard", analyzeWildcard);
	public ExplainRequestDescriptor Analyzer(string? analyzer) => Qs("analyzer", analyzer);
	public ExplainRequestDescriptor DefaultOperator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? defaultOperator) => Qs("default_operator", defaultOperator);
	public ExplainRequestDescriptor Df(string? df) => Qs("df", df);
	public ExplainRequestDescriptor Lenient(bool? lenient = true) => Qs("lenient", lenient);
	public ExplainRequestDescriptor Preference(string? preference) => Qs("preference", preference);
	public ExplainRequestDescriptor QueryLuceneSyntax(string? q) => Qs("q", q);
	public ExplainRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Serverless.Routing? routing) => Qs("routing", routing);
	public ExplainRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Serverless.Fields? storedFields) => Qs("stored_fields", storedFields);

	public ExplainRequestDescriptor Id(Elastic.Clients.Elasticsearch.Serverless.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	public ExplainRequestDescriptor Index(Elastic.Clients.Elasticsearch.Serverless.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }

	/// <summary>
	/// <para>Defines the search definition using the Query DSL.</para>
	/// </summary>
	public ExplainRequestDescriptor Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public ExplainRequestDescriptor Query(QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public ExplainRequestDescriptor Query(Action<QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		writer.WriteEndObject();
	}
}