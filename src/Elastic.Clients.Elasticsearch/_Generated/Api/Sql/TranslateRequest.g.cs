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

namespace Elastic.Clients.Elasticsearch.Sql;

public sealed partial class TranslateRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class TranslateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Sql.TranslateRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropFetchSize = System.Text.Json.JsonEncodedText.Encode("fetch_size");
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropTimeZone = System.Text.Json.JsonEncodedText.Encode("time_zone");

	public override Elastic.Clients.Elasticsearch.Sql.TranslateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propFetchSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propFilter = default;
		LocalJsonValue<string> propQuery = default;
		LocalJsonValue<string?> propTimeZone = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFetchSize.TryReadProperty(ref reader, options, PropFetchSize, null))
			{
				continue;
			}

			if (propFilter.TryReadProperty(ref reader, options, PropFilter, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propTimeZone.TryReadProperty(ref reader, options, PropTimeZone, null))
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
		return new Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			FetchSize = propFetchSize.Value,
			Filter = propFilter.Value,
			Query = propQuery.Value,
			TimeZone = propTimeZone.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Sql.TranslateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFetchSize, value.FetchSize, null, null);
		writer.WriteProperty(options, PropFilter, value.Filter, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropTimeZone, value.TimeZone, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Translate SQL into Elasticsearch queries.
/// Translate an SQL search into a search API request containing Query DSL.
/// It accepts the same request body parameters as the SQL search API, excluding <c>cursor</c>.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Sql.TranslateRequestConverter))]
public sealed partial class TranslateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslateRequest(string query)
	{
		Query = query;
	}
#if NET7_0_OR_GREATER
	public TranslateRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The request contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TranslateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SqlTranslate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "sql.translate";

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	public int? FetchSize { get; set; }

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Query { get; set; }

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	public string? TimeZone { get; set; }
}

/// <summary>
/// <para>
/// Translate SQL into Elasticsearch queries.
/// Translate an SQL search into a search API request containing Query DSL.
/// It accepts the same request body parameters as the SQL search API, excluding <c>cursor</c>.
/// </para>
/// </summary>
public readonly partial struct TranslateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Sql.TranslateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslateRequestDescriptor(Elastic.Clients.Elasticsearch.Sql.TranslateRequest instance)
	{
		Instance = instance;
	}

	public TranslateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor(Elastic.Clients.Elasticsearch.Sql.TranslateRequest instance) => new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor FetchSize(int? value)
	{
		Instance.FetchSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Filter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Sql.TranslateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor(new Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Translate SQL into Elasticsearch queries.
/// Translate an SQL search into a search API request containing Query DSL.
/// It accepts the same request body parameters as the SQL search API, excluding <c>cursor</c>.
/// </para>
/// </summary>
public readonly partial struct TranslateRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Sql.TranslateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TranslateRequestDescriptor(Elastic.Clients.Elasticsearch.Sql.TranslateRequest instance)
	{
		Instance = instance;
	}

	public TranslateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Sql.TranslateRequest instance) => new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The maximum number of rows (or entries) to return in one response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> FetchSize(int? value)
	{
		Instance.FetchSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Filter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The Elasticsearch query DSL for additional filtering.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> Filter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Filter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The SQL query to run.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> Query(string value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ISO-8601 time zone ID for the search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Sql.TranslateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Sql.TranslateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}