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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class RollupSearchRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Indicates whether hits.total should be rendered as an integer or an object in the rest search response
	/// </para>
	/// </summary>
	public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }
}

internal sealed partial class RollupSearchRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropAggregations1 = System.Text.Json.JsonEncodedText.Encode("aggs");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");

	public override Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>?> propAggregations = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propQuery = default;
		LocalJsonValue<int?> propSize = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryReadProperty(ref reader, options, PropAggregations, static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, null, null)) || propAggregations.TryReadProperty(ref reader, options, PropAggregations1, static System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, null, null)))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
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
		return new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = propAggregations.Value,
			Query = propQuery.Value,
			Size = propSize.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>(o, v, null, null));
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Search rolled-up data.
/// The rollup search endpoint is needed because, internally, rolled-up documents utilize a different document structure than the original data.
/// It rewrites standard Query DSL into a format that matches the rollup documents then takes the response and rewrites it back to what a client would expect given the original query.
/// </para>
/// <para>
/// The request body supports a subset of features from the regular search API.
/// The following functionality is not available:
/// </para>
/// <para>
/// <c>size</c>: Because rollups work on pre-aggregated data, no search hits can be returned and so size must be set to zero or omitted entirely.
/// <c>highlighter</c>, <c>suggestors</c>, <c>post_filter</c>, <c>profile</c>, <c>explain</c>: These are similarly disallowed.
/// </para>
/// <para>
/// <strong>Searching both historical rollup and non-rollup data</strong>
/// </para>
/// <para>
/// The rollup search API has the capability to search across both "live" non-rollup data and the aggregated rollup data.
/// This is done by simply adding the live indices to the URI. For example:
/// </para>
/// <code>
/// GET sensor-1,sensor_rollup/_rollup_search
/// {
///   "size": 0,
///   "aggregations": {
///      "max_temperature": {
///       "max": {
///         "field": "temperature"
///       }
///     }
///   }
/// }
/// </code>
/// <para>
/// The rollup search endpoint does two things when the search runs:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The original request is sent to the non-rollup index unaltered.
/// </para>
/// </item>
/// <item>
/// <para>
/// A rewritten version of the original request is sent to the rollup index.
/// </para>
/// </item>
/// </list>
/// <para>
/// When the two responses are received, the endpoint rewrites the rollup response and merges the two together.
/// During the merging process, if there is any overlap in buckets between the two responses, the buckets from the non-rollup index are used.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestConverter))]
public sealed partial class RollupSearchRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RollupSearchRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public RollupSearchRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RollupSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.RollupRollupSearch;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "rollup.rollup_search";

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams and indices used to limit the request.
	/// This parameter has the following rules:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// At least one data stream, index, or wildcard expression must be specified. This target can include a rollup or non-rollup index. For data streams, the stream's backing indices can only serve as non-rollup indices. Omitting the parameter or using <c>_all</c> are not permitted.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Multiple non-rollup indices may be specified.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Only one rollup index may be specified. If more than one are supplied, an exception occurs.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Wildcard expressions (<c>*</c>) may be used. If they match more than one rollup index, an exception occurs. However, you can use an expression to match multiple non-rollup indices or data streams.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Indices Indices { get => P<Elastic.Clients.Elasticsearch.Indices>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// Indicates whether hits.total should be rendered as an integer or an object in the rest search response
	/// </para>
	/// </summary>
	public bool? RestTotalHitsAsInt { get => Q<bool?>("rest_total_hits_as_int"); set => Q("rest_total_hits_as_int", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? Aggregations { get; set; }

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }

	/// <summary>
	/// <para>
	/// Must be zero if set, as rollups work on pre-aggregated data.
	/// </para>
	/// </summary>
	public int? Size { get; set; }
}

/// <summary>
/// <para>
/// Search rolled-up data.
/// The rollup search endpoint is needed because, internally, rolled-up documents utilize a different document structure than the original data.
/// It rewrites standard Query DSL into a format that matches the rollup documents then takes the response and rewrites it back to what a client would expect given the original query.
/// </para>
/// <para>
/// The request body supports a subset of features from the regular search API.
/// The following functionality is not available:
/// </para>
/// <para>
/// <c>size</c>: Because rollups work on pre-aggregated data, no search hits can be returned and so size must be set to zero or omitted entirely.
/// <c>highlighter</c>, <c>suggestors</c>, <c>post_filter</c>, <c>profile</c>, <c>explain</c>: These are similarly disallowed.
/// </para>
/// <para>
/// <strong>Searching both historical rollup and non-rollup data</strong>
/// </para>
/// <para>
/// The rollup search API has the capability to search across both "live" non-rollup data and the aggregated rollup data.
/// This is done by simply adding the live indices to the URI. For example:
/// </para>
/// <code>
/// GET sensor-1,sensor_rollup/_rollup_search
/// {
///   "size": 0,
///   "aggregations": {
///      "max_temperature": {
///       "max": {
///         "field": "temperature"
///       }
///     }
///   }
/// }
/// </code>
/// <para>
/// The rollup search endpoint does two things when the search runs:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The original request is sent to the non-rollup index unaltered.
/// </para>
/// </item>
/// <item>
/// <para>
/// A rewritten version of the original request is sent to the rollup index.
/// </para>
/// </item>
/// </list>
/// <para>
/// When the two responses are received, the endpoint rewrites the rollup response and merges the two together.
/// During the merging process, if there is any overlap in buckets between the two responses, the buckets from the non-rollup index are used.
/// </para>
/// </summary>
public readonly partial struct RollupSearchRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RollupSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest instance)
	{
		Instance = instance;
	}

	public RollupSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(indices);
	}

	[System.Obsolete("TODO")]
	public RollupSearchRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest instance) => new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams and indices used to limit the request.
	/// This parameter has the following rules:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// At least one data stream, index, or wildcard expression must be specified. This target can include a rollup or non-rollup index. For data streams, the stream's backing indices can only serve as non-rollup indices. Omitting the parameter or using <c>_all</c> are not permitted.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Multiple non-rollup indices may be specified.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Only one rollup index may be specified. If more than one are supplied, an exception occurs.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Wildcard expressions (<c>*</c>) may be used. If they match more than one rollup index, an exception occurs. However, you can use an expression to match multiple non-rollup indices or data streams.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether hits.total should be rendered as an integer or an object in the rest search response
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor RestTotalHitsAsInt(bool? value = true)
	{
		Instance.RestTotalHitsAsInt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor TypedKeys(bool? value = true)
	{
		Instance.TypedKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Aggregations(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? value)
	{
		Instance.Aggregations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Aggregations()
	{
		Instance.Aggregations = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Aggregations(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation>? action)
	{
		Instance.Aggregations = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Aggregations<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation<T>>? action)
	{
		Instance.Aggregations = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor AddAggregation(string key, Elastic.Clients.Elasticsearch.Aggregations.Aggregation value)
	{
		Instance.Aggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>();
		Instance.Aggregations.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor AddAggregation(string key, System.Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor> action)
	{
		Instance.Aggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>();
		Instance.Aggregations.Add(key, Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor AddAggregation<T>(string key, System.Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<T>> action)
	{
		Instance.Aggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>();
		Instance.Aggregations.Add(key, Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<T>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Must be zero if set, as rollups work on pre-aggregated data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest Build(System.Action<Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor(new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Search rolled-up data.
/// The rollup search endpoint is needed because, internally, rolled-up documents utilize a different document structure than the original data.
/// It rewrites standard Query DSL into a format that matches the rollup documents then takes the response and rewrites it back to what a client would expect given the original query.
/// </para>
/// <para>
/// The request body supports a subset of features from the regular search API.
/// The following functionality is not available:
/// </para>
/// <para>
/// <c>size</c>: Because rollups work on pre-aggregated data, no search hits can be returned and so size must be set to zero or omitted entirely.
/// <c>highlighter</c>, <c>suggestors</c>, <c>post_filter</c>, <c>profile</c>, <c>explain</c>: These are similarly disallowed.
/// </para>
/// <para>
/// <strong>Searching both historical rollup and non-rollup data</strong>
/// </para>
/// <para>
/// The rollup search API has the capability to search across both "live" non-rollup data and the aggregated rollup data.
/// This is done by simply adding the live indices to the URI. For example:
/// </para>
/// <code>
/// GET sensor-1,sensor_rollup/_rollup_search
/// {
///   "size": 0,
///   "aggregations": {
///      "max_temperature": {
///       "max": {
///         "field": "temperature"
///       }
///     }
///   }
/// }
/// </code>
/// <para>
/// The rollup search endpoint does two things when the search runs:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The original request is sent to the non-rollup index unaltered.
/// </para>
/// </item>
/// <item>
/// <para>
/// A rewritten version of the original request is sent to the rollup index.
/// </para>
/// </item>
/// </list>
/// <para>
/// When the two responses are received, the endpoint rewrites the rollup response and merges the two together.
/// During the merging process, if there is any overlap in buckets between the two responses, the buckets from the non-rollup index are used.
/// </para>
/// </summary>
public readonly partial struct RollupSearchRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RollupSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest instance)
	{
		Instance = instance;
	}

	public RollupSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(indices);
	}

	public RollupSearchRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(typeof(TDocument));
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest instance) => new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams and indices used to limit the request.
	/// This parameter has the following rules:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// At least one data stream, index, or wildcard expression must be specified. This target can include a rollup or non-rollup index. For data streams, the stream's backing indices can only serve as non-rollup indices. Omitting the parameter or using <c>_all</c> are not permitted.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Multiple non-rollup indices may be specified.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Only one rollup index may be specified. If more than one are supplied, an exception occurs.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Wildcard expressions (<c>*</c>) may be used. If they match more than one rollup index, an exception occurs. However, you can use an expression to match multiple non-rollup indices or data streams.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether hits.total should be rendered as an integer or an object in the rest search response
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> RestTotalHitsAsInt(bool? value = true)
	{
		Instance.RestTotalHitsAsInt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> TypedKeys(bool? value = true)
	{
		Instance.TypedKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Aggregations(System.Collections.Generic.IDictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>? value)
	{
		Instance.Aggregations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Aggregations()
	{
		Instance.Aggregations = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies aggregations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Aggregations(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation<TDocument>>? action)
	{
		Instance.Aggregations = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringAggregation<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> AddAggregation(string key, Elastic.Clients.Elasticsearch.Aggregations.Aggregation value)
	{
		Instance.Aggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>();
		Instance.Aggregations.Add(key, value);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> AddAggregation(string key, System.Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>> action)
	{
		Instance.Aggregations ??= new System.Collections.Generic.Dictionary<string, Elastic.Clients.Elasticsearch.Aggregations.Aggregation>();
		Instance.Aggregations.Add(key, Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a DSL query that is subject to some limitations.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Must be zero if set, as rollups work on pre-aggregated data.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest Build(System.Action<Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.RollupSearchRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}