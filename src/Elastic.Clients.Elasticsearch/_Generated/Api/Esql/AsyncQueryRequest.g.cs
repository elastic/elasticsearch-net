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

namespace Elastic.Clients.Elasticsearch.Esql;

public sealed partial class AsyncQueryRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c>, partial results will be returned if there are shard failures, but the query can continue to execute on other clusters and shards.
	/// </para>
	/// </summary>
	public bool? AllowPartialResults { get => Q<bool?>("allow_partial_results"); set => Q("allow_partial_results", value); }

	/// <summary>
	/// <para>
	/// The character to use between values within a CSV row.
	/// It is valid only for the CSV format.
	/// </para>
	/// </summary>
	public string? Delimiter { get => Q<string?>("delimiter"); set => Q("delimiter", value); }

	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	public bool? DropNullColumns { get => Q<bool?>("drop_null_columns"); set => Q("drop_null_columns", value); }

	/// <summary>
	/// <para>
	/// A short version of the Accept header, for example <c>json</c> or <c>yaml</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.EsqlFormat? Format { get => Q<Elastic.Clients.Elasticsearch.Esql.EsqlFormat?>("format"); set => Q("format", value); }

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// The default period is five days.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// If the <c>keep_on_completion</c> parameter is false, Elasticsearch only stores async queries that do not complete within the period set by the <c>wait_for_completion_timeout</c> parameter, regardless of this value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Indicates whether the query and its results are stored in the cluster.
	/// If false, the query and its results are stored in the cluster only if the request does not complete during the period set by the <c>wait_for_completion_timeout</c> parameter.
	/// </para>
	/// </summary>
	public bool? KeepOnCompletion { get => Q<bool?>("keep_on_completion"); set => Q("keep_on_completion", value); }
}

internal sealed partial class AsyncQueryRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropColumnar = System.Text.Json.JsonEncodedText.Encode("columnar");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropIncludeCcsMetadata = System.Text.Json.JsonEncodedText.Encode("include_ccs_metadata");
	private static readonly System.Text.Json.JsonEncodedText PropLocale = System.Text.Json.JsonEncodedText.Encode("locale");
	private static readonly System.Text.Json.JsonEncodedText PropParams = System.Text.Json.JsonEncodedText.Encode("params");
	private static readonly System.Text.Json.JsonEncodedText PropProfile = System.Text.Json.JsonEncodedText.Encode("profile");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropWaitForCompletionTimeout = System.Text.Json.JsonEncodedText.Encode("wait_for_completion_timeout");

	public override Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propColumnar = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propFilter = default;
		LocalJsonValue<bool?> propIncludeCcsMetadata = default;
		LocalJsonValue<string?> propLocale = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>?> propParams = default;
		LocalJsonValue<bool?> propProfile = default;
		LocalJsonValue<string> propQuery = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propWaitForCompletionTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propColumnar.TryReadProperty(ref reader, options, PropColumnar, null))
			{
				continue;
			}

			if (propFilter.TryReadProperty(ref reader, options, PropFilter, null))
			{
				continue;
			}

			if (propIncludeCcsMetadata.TryReadProperty(ref reader, options, PropIncludeCcsMetadata, null))
			{
				continue;
			}

			if (propLocale.TryReadProperty(ref reader, options, PropLocale, null))
			{
				continue;
			}

			if (propParams.TryReadProperty(ref reader, options, PropParams, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)))
			{
				continue;
			}

			if (propProfile.TryReadProperty(ref reader, options, PropProfile, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propWaitForCompletionTimeout.TryReadProperty(ref reader, options, PropWaitForCompletionTimeout, null))
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
		return new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Columnar = propColumnar.Value,
			Filter = propFilter.Value,
			IncludeCcsMetadata = propIncludeCcsMetadata.Value,
			Locale = propLocale.Value,
			Params = propParams.Value,
			Profile = propProfile.Value,
			Query = propQuery.Value,
			WaitForCompletionTimeout = propWaitForCompletionTimeout.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropColumnar, value.Columnar, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, null);
		writer.WriteProperty(options, PropIncludeCcsMetadata, value.IncludeCcsMetadata, null, null);
		writer.WriteProperty(options, PropLocale, value.Locale, null, null);
		writer.WriteProperty(options, PropParams, value.Params, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null));
		writer.WriteProperty(options, PropProfile, value.Profile, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropWaitForCompletionTimeout, value.WaitForCompletionTimeout, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Run an async ES|QL query.
/// Asynchronously run an ES|QL (Elasticsearch query language) query, monitor its progress, and retrieve results when they become available.
/// </para>
/// <para>
/// The API accepts the same parameters and request body as the synchronous query API, along with additional async related properties.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestConverter))]
public sealed partial class AsyncQueryRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AsyncQueryRequest(string query)
	{
		Query = query;
	}
#if NET7_0_OR_GREATER
	public AsyncQueryRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AsyncQueryRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.EsqlAsyncQuery;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "esql.async_query";

	/// <summary>
	/// <para>
	/// If <c>true</c>, partial results will be returned if there are shard failures, but the query can continue to execute on other clusters and shards.
	/// </para>
	/// </summary>
	public bool? AllowPartialResults { get => Q<bool?>("allow_partial_results"); set => Q("allow_partial_results", value); }

	/// <summary>
	/// <para>
	/// The character to use between values within a CSV row.
	/// It is valid only for the CSV format.
	/// </para>
	/// </summary>
	public string? Delimiter { get => Q<string?>("delimiter"); set => Q("delimiter", value); }

	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	public bool? DropNullColumns { get => Q<bool?>("drop_null_columns"); set => Q("drop_null_columns", value); }

	/// <summary>
	/// <para>
	/// A short version of the Accept header, for example <c>json</c> or <c>yaml</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.EsqlFormat? Format { get => Q<Elastic.Clients.Elasticsearch.Esql.EsqlFormat?>("format"); set => Q("format", value); }

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// The default period is five days.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// If the <c>keep_on_completion</c> parameter is false, Elasticsearch only stores async queries that do not complete within the period set by the <c>wait_for_completion_timeout</c> parameter, regardless of this value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Indicates whether the query and its results are stored in the cluster.
	/// If false, the query and its results are stored in the cluster only if the request does not complete during the period set by the <c>wait_for_completion_timeout</c> parameter.
	/// </para>
	/// </summary>
	public bool? KeepOnCompletion { get => Q<bool?>("keep_on_completion"); set => Q("keep_on_completion", value); }

	/// <summary>
	/// <para>
	/// By default, ES|QL returns results as rows. For example, FROM returns each individual document as one row. For the JSON, YAML, CBOR and smile formats, ES|QL can return the results in a columnar fashion where one row represents all the values of a certain column in the results.
	/// </para>
	/// </summary>
	public bool? Columnar { get; set; }

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }

	/// <summary>
	/// <para>
	/// When set to <c>true</c> and performing a cross-cluster query, the response will include an extra <c>_clusters</c>
	/// object with information about the clusters that participated in the search along with info such as shards
	/// count.
	/// </para>
	/// </summary>
	public bool? IncludeCcsMetadata { get; set; }
	public string? Locale { get; set; }

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>? Params { get; set; }

	/// <summary>
	/// <para>
	/// If provided and <c>true</c> the response will include an extra <c>profile</c> object
	/// with information on how the query was executed. This information is for human debugging
	/// and its format can change at any time but it can give some insight into the performance
	/// of each part of the query.
	/// </para>
	/// </summary>
	public bool? Profile { get; set; }

	/// <summary>
	/// <para>
	/// The ES|QL query API accepts an ES|QL query string in the query parameter, runs it, and returns the results.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Query { get; set; }

	/// <summary>
	/// <para>
	/// The period to wait for the request to finish.
	/// By default, the request waits for 1 second for the query results.
	/// If the query completes during this period, results are returned
	/// Otherwise, a query ID is returned that can later be used to retrieve the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get; set; }
}

/// <summary>
/// <para>
/// Run an async ES|QL query.
/// Asynchronously run an ES|QL (Elasticsearch query language) query, monitor its progress, and retrieve results when they become available.
/// </para>
/// <para>
/// The API accepts the same parameters and request body as the synchronous query API, along with additional async related properties.
/// </para>
/// </summary>
public readonly partial struct AsyncQueryRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AsyncQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest instance)
	{
		Instance = instance;
	}

	public AsyncQueryRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest instance) => new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If <c>true</c>, partial results will be returned if there are shard failures, but the query can continue to execute on other clusters and shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor AllowPartialResults(bool? value = true)
	{
		Instance.AllowPartialResults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The character to use between values within a CSV row.
	/// It is valid only for the CSV format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Delimiter(string? value)
	{
		Instance.Delimiter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor DropNullColumns(bool? value = true)
	{
		Instance.DropNullColumns = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A short version of the Accept header, for example <c>json</c> or <c>yaml</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Format(Elastic.Clients.Elasticsearch.Esql.EsqlFormat? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// The default period is five days.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// If the <c>keep_on_completion</c> parameter is false, Elasticsearch only stores async queries that do not complete within the period set by the <c>wait_for_completion_timeout</c> parameter, regardless of this value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the query and its results are stored in the cluster.
	/// If false, the query and its results are stored in the cluster only if the request does not complete during the period set by the <c>wait_for_completion_timeout</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor KeepOnCompletion(bool? value = true)
	{
		Instance.KeepOnCompletion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// By default, ES|QL returns results as rows. For example, FROM returns each individual document as one row. For the JSON, YAML, CBOR and smile formats, ES|QL can return the results in a columnar fashion where one row represents all the values of a certain column in the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Columnar(bool? value = true)
	{
		Instance.Columnar = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c> and performing a cross-cluster query, the response will include an extra <c>_clusters</c>
	/// object with information about the clusters that participated in the search along with info such as shards
	/// count.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor IncludeCcsMetadata(bool? value = true)
	{
		Instance.IncludeCcsMetadata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Params(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>? value)
	{
		Instance.Params = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Params(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Params = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If provided and <c>true</c> the response will include an extra <c>profile</c> object
	/// with information on how the query was executed. This information is for human debugging
	/// and its format can change at any time but it can give some insight into the performance
	/// of each part of the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Profile(bool? value = true)
	{
		Instance.Profile = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ES|QL query API accepts an ES|QL query string in the query parameter, runs it, and returns the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for the request to finish.
	/// By default, the request waits for 1 second for the query results.
	/// If the query completes during this period, results are returned
	/// Otherwise, a query ID is returned that can later be used to retrieve the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.WaitForCompletionTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest Build(System.Action<Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor(new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Run an async ES|QL query.
/// Asynchronously run an ES|QL (Elasticsearch query language) query, monitor its progress, and retrieve results when they become available.
/// </para>
/// <para>
/// The API accepts the same parameters and request body as the synchronous query API, along with additional async related properties.
/// </para>
/// </summary>
public readonly partial struct AsyncQueryRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AsyncQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest instance)
	{
		Instance = instance;
	}

	public AsyncQueryRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest instance) => new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If <c>true</c>, partial results will be returned if there are shard failures, but the query can continue to execute on other clusters and shards.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> AllowPartialResults(bool? value = true)
	{
		Instance.AllowPartialResults = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The character to use between values within a CSV row.
	/// It is valid only for the CSV format.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Delimiter(string? value)
	{
		Instance.Delimiter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether columns that are entirely <c>null</c> will be removed from the <c>columns</c> and <c>values</c> portion of the results.
	/// If <c>true</c>, the response will include an extra section under the name <c>all_columns</c> which has the name of all the columns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> DropNullColumns(bool? value = true)
	{
		Instance.DropNullColumns = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A short version of the Accept header, for example <c>json</c> or <c>yaml</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Format(Elastic.Clients.Elasticsearch.Esql.EsqlFormat? value)
	{
		Instance.Format = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period for which the query and its results are stored in the cluster.
	/// The default period is five days.
	/// When this period expires, the query and its results are deleted, even if the query is still ongoing.
	/// If the <c>keep_on_completion</c> parameter is false, Elasticsearch only stores async queries that do not complete within the period set by the <c>wait_for_completion_timeout</c> parameter, regardless of this value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the query and its results are stored in the cluster.
	/// If false, the query and its results are stored in the cluster only if the request does not complete during the period set by the <c>wait_for_completion_timeout</c> parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> KeepOnCompletion(bool? value = true)
	{
		Instance.KeepOnCompletion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// By default, ES|QL returns results as rows. For example, FROM returns each individual document as one row. For the JSON, YAML, CBOR and smile formats, ES|QL can return the results in a columnar fashion where one row represents all the values of a certain column in the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Columnar(bool? value = true)
	{
		Instance.Columnar = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify a Query DSL query in the filter parameter to filter the set of documents that an ES|QL query runs on.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c> and performing a cross-cluster query, the response will include an extra <c>_clusters</c>
	/// object with information about the clusters that participated in the search along with info such as shards
	/// count.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> IncludeCcsMetadata(bool? value = true)
	{
		Instance.IncludeCcsMetadata = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Locale(string? value)
	{
		Instance.Locale = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Params(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.FieldValue>? value)
	{
		Instance.Params = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfFieldValue.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// To avoid any attempts of hacking or code injection, extract the values in a separate list of parameters. Use question mark placeholders (?) in the query string for each of the parameters.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Params(params Elastic.Clients.Elasticsearch.FieldValue[] values)
	{
		Instance.Params = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If provided and <c>true</c> the response will include an extra <c>profile</c> object
	/// with information on how the query was executed. This information is for human debugging
	/// and its format can change at any time but it can give some insight into the performance
	/// of each part of the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Profile(bool? value = true)
	{
		Instance.Profile = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ES|QL query API accepts an ES|QL query string in the query parameter, runs it, and returns the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for the request to finish.
	/// By default, the request waits for 1 second for the query results.
	/// If the query completes during this period, results are returned
	/// Otherwise, a query ID is returned that can later be used to retrieve the results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.WaitForCompletionTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest Build(System.Action<Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}