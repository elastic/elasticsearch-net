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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class CountRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, wildcard and prefix queries are analyzed.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>
	/// The default operator for query string query: <c>AND</c> or <c>OR</c>.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>
	/// The field to use as a default when no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, concrete, expanded, or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	[System.Obsolete("Deprecated in '7.16.0'.")]
	public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>
	/// The minimum <c>_score</c> value that documents must have to be included in the result.
	/// </para>
	/// </summary>
	public double? MinScore { get => Q<double?>("min_score"); set => Q("min_score", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// The query in Lucene query string syntax. This parameter cannot be used with a request body.
	/// </para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// The maximum number of documents to collect for each shard.
	/// If a query reaches this limit, Elasticsearch terminates the query early.
	/// Elasticsearch collects documents before sorting.
	/// </para>
	/// <para>
	/// IMPORTANT: Use with caution.
	/// Elasticsearch applies this parameter to each shard handling the request.
	/// When possible, let Elasticsearch perform early termination automatically.
	/// Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.
	/// </para>
	/// </summary>
	public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }
}

internal sealed partial class CountRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CountRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");

	public override Elastic.Clients.Elasticsearch.CountRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propQuery = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Query = propQuery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CountRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Count search results.
/// Get the number of documents matching a query.
/// </para>
/// <para>
/// The query can be provided either by using a simple query string as a parameter, or by defining Query DSL within the request body.
/// The query is optional. When no query is provided, the API uses <c>match_all</c> to count all the documents.
/// </para>
/// <para>
/// The count API supports multi-target syntax. You can run a single count API search across multiple data streams and indices.
/// </para>
/// <para>
/// The operation is broadcast across all shards.
/// For each shard ID group, a replica is chosen and the search is run against it.
/// This means that replicas increase the scalability of the count.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CountRequestConverter))]
public partial class CountRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.CountRequestParameters>
{
	public CountRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public CountRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public CountRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceCount;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "count";

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases to search.
	/// It supports wildcards (<c>*</c>).
	/// To search all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Analyzer { get => Q<string?>("analyzer"); set => Q("analyzer", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, wildcard and prefix queries are analyzed.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? AnalyzeWildcard { get => Q<bool?>("analyze_wildcard"); set => Q("analyze_wildcard", value); }

	/// <summary>
	/// <para>
	/// The default operator for query string query: <c>AND</c> or <c>OR</c>.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Operator? DefaultOperator { get => Q<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>("default_operator"); set => Q("default_operator", value); }

	/// <summary>
	/// <para>
	/// The field to use as a default when no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, concrete, expanded, or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	[System.Obsolete("Deprecated in '7.16.0'.")]
	public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>
	/// The minimum <c>_score</c> value that documents must have to be included in the result.
	/// </para>
	/// </summary>
	public double? MinScore { get => Q<double?>("min_score"); set => Q("min_score", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// The query in Lucene query string syntax. This parameter cannot be used with a request body.
	/// </para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// The maximum number of documents to collect for each shard.
	/// If a query reaches this limit, Elasticsearch terminates the query early.
	/// Elasticsearch collects documents before sorting.
	/// </para>
	/// <para>
	/// IMPORTANT: Use with caution.
	/// Elasticsearch applies this parameter to each shard handling the request.
	/// When possible, let Elasticsearch perform early termination automatically.
	/// Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.
	/// </para>
	/// </summary>
	public long? TerminateAfter { get => Q<long?>("terminate_after"); set => Q("terminate_after", value); }

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }
}

/// <summary>
/// <para>
/// Count search results.
/// Get the number of documents matching a query.
/// </para>
/// <para>
/// The query can be provided either by using a simple query string as a parameter, or by defining Query DSL within the request body.
/// The query is optional. When no query is provided, the API uses <c>match_all</c> to count all the documents.
/// </para>
/// <para>
/// The count API supports multi-target syntax. You can run a single count API search across multiple data streams and indices.
/// </para>
/// <para>
/// The operation is broadcast across all shards.
/// For each shard ID group, a replica is chosen and the search is run against it.
/// This means that replicas increase the scalability of the count.
/// </para>
/// </summary>
public readonly partial struct CountRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.CountRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CountRequestDescriptor(Elastic.Clients.Elasticsearch.CountRequest instance)
	{
		Instance = instance;
	}

	public CountRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.CountRequest(indices);
	}

	public CountRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CountRequestDescriptor(Elastic.Clients.Elasticsearch.CountRequest instance) => new Elastic.Clients.Elasticsearch.CountRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.CountRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases to search.
	/// It supports wildcards (<c>*</c>).
	/// To search all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, wildcard and prefix queries are analyzed.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor AnalyzeWildcard(bool? value = true)
	{
		Instance.AnalyzeWildcard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default operator for query string query: <c>AND</c> or <c>OR</c>.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? value)
	{
		Instance.DefaultOperator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use as a default when no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Df(string? value)
	{
		Instance.Df = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	[System.Obsolete("Deprecated in '7.16.0'.")]
	/// <summary>
	/// <para>
	/// If <c>true</c>, concrete, expanded, or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor IgnoreThrottled(bool? value = true)
	{
		Instance.IgnoreThrottled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Lenient(bool? value = true)
	{
		Instance.Lenient = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum <c>_score</c> value that documents must have to be included in the result.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor MinScore(double? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query in Lucene query string syntax. This parameter cannot be used with a request body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor QueryLuceneSyntax(string? value)
	{
		Instance.QueryLuceneSyntax = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of documents to collect for each shard.
	/// If a query reaches this limit, Elasticsearch terminates the query early.
	/// Elasticsearch collects documents before sorting.
	/// </para>
	/// <para>
	/// IMPORTANT: Use with caution.
	/// Elasticsearch applies this parameter to each shard handling the request.
	/// When possible, let Elasticsearch perform early termination automatically.
	/// Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor TerminateAfter(long? value)
	{
		Instance.TerminateAfter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CountRequest Build(System.Action<Elastic.Clients.Elasticsearch.CountRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.CountRequestDescriptor(new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Count search results.
/// Get the number of documents matching a query.
/// </para>
/// <para>
/// The query can be provided either by using a simple query string as a parameter, or by defining Query DSL within the request body.
/// The query is optional. When no query is provided, the API uses <c>match_all</c> to count all the documents.
/// </para>
/// <para>
/// The count API supports multi-target syntax. You can run a single count API search across multiple data streams and indices.
/// </para>
/// <para>
/// The operation is broadcast across all shards.
/// For each shard ID group, a replica is chosen and the search is run against it.
/// This means that replicas increase the scalability of the count.
/// </para>
/// </summary>
public readonly partial struct CountRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.CountRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CountRequestDescriptor(Elastic.Clients.Elasticsearch.CountRequest instance)
	{
		Instance = instance;
	}

	public CountRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.CountRequest(indices);
	}

	public CountRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.CountRequest instance) => new Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases to search.
	/// It supports wildcards (<c>*</c>).
	/// To search all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Analyzer(string? value)
	{
		Instance.Analyzer = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, wildcard and prefix queries are analyzed.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> AnalyzeWildcard(bool? value = true)
	{
		Instance.AnalyzeWildcard = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The default operator for query string query: <c>AND</c> or <c>OR</c>.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? value)
	{
		Instance.DefaultOperator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use as a default when no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Df(string? value)
	{
		Instance.Df = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// It supports comma-separated values, such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	[System.Obsolete("Deprecated in '7.16.0'.")]
	/// <summary>
	/// <para>
	/// If <c>true</c>, concrete, expanded, or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> IgnoreThrottled(bool? value = true)
	{
		Instance.IgnoreThrottled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Lenient(bool? value = true)
	{
		Instance.Lenient = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum <c>_score</c> value that documents must have to be included in the result.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> MinScore(double? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, it is random.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query in Lucene query string syntax. This parameter cannot be used with a request body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> QueryLuceneSyntax(string? value)
	{
		Instance.QueryLuceneSyntax = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of documents to collect for each shard.
	/// If a query reaches this limit, Elasticsearch terminates the query early.
	/// Elasticsearch collects documents before sorting.
	/// </para>
	/// <para>
	/// IMPORTANT: Use with caution.
	/// Elasticsearch applies this parameter to each shard handling the request.
	/// When possible, let Elasticsearch perform early termination automatically.
	/// Avoid specifying this parameter for requests that target data streams with backing indices across multiple data tiers.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> TerminateAfter(long? value)
	{
		Instance.TerminateAfter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search query using Query DSL. A request body query cannot be used
	/// with the <c>q</c> query string parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CountRequest Build(System.Action<Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.CountRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CountRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}