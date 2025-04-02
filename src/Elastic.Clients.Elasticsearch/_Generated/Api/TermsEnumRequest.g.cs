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

public sealed partial class TermsEnumRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class TermsEnumRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TermsEnumRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropIndexFilter = System.Text.Json.JsonEncodedText.Encode("index_filter");
	private static readonly System.Text.Json.JsonEncodedText PropSearchAfter = System.Text.Json.JsonEncodedText.Encode("search_after");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropString = System.Text.Json.JsonEncodedText.Encode("string");
	private static readonly System.Text.Json.JsonEncodedText PropTimeout = System.Text.Json.JsonEncodedText.Encode("timeout");

	public override Elastic.Clients.Elasticsearch.TermsEnumRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query?> propIndexFilter = default;
		LocalJsonValue<string?> propSearchAfter = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<string?> propString = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCaseInsensitive.TryReadProperty(ref reader, options, PropCaseInsensitive, null))
			{
				continue;
			}

			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propIndexFilter.TryReadProperty(ref reader, options, PropIndexFilter, null))
			{
				continue;
			}

			if (propSearchAfter.TryReadProperty(ref reader, options, PropSearchAfter, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propString.TryReadProperty(ref reader, options, PropString, null))
			{
				continue;
			}

			if (propTimeout.TryReadProperty(ref reader, options, PropTimeout, null))
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
		return new Elastic.Clients.Elasticsearch.TermsEnumRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CaseInsensitive = propCaseInsensitive.Value,
			Field = propField.Value,
			IndexFilter = propIndexFilter.Value,
			SearchAfter = propSearchAfter.Value,
			Size = propSize.Value,
			String = propString.Value,
			Timeout = propTimeout.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TermsEnumRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive, null, null);
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropIndexFilter, value.IndexFilter, null, null);
		writer.WriteProperty(options, PropSearchAfter, value.SearchAfter, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropString, value.String, null, null);
		writer.WriteProperty(options, PropTimeout, value.Timeout, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get terms in an index.
/// </para>
/// <para>
/// Discover terms that match a partial string in an index.
/// This API is designed for low-latency look-ups used in auto-complete scenarios.
/// </para>
/// <para>
/// info
/// The terms enum API may return terms from deleted documents. Deleted documents are initially only marked as deleted. It is not until their segments are merged that documents are actually deleted. Until that happens, the terms enum API will return terms from these documents.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TermsEnumRequestConverter))]
public sealed partial class TermsEnumRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.TermsEnumRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsEnumRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsEnumRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Field field) : base(r => r.Required("index", index))
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public TermsEnumRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TermsEnumRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceTermsEnum;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "terms_enum";

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and index aliases to search.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To search all data streams or indices, omit this parameter or use <c>*</c>  or <c>_all</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// When <c>true</c>, the provided search string is matched against index terms without case sensitivity.
	/// </para>
	/// </summary>
	public bool? CaseInsensitive { get; set; }

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms. If not provided, all terms in the field are considered.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? IndexFilter { get; set; }

	/// <summary>
	/// <para>
	/// The string after which terms in the index should be returned.
	/// It allows for a form of pagination if the last result from one request is passed as the <c>search_after</c> parameter for a subsequent request.
	/// </para>
	/// </summary>
	public string? SearchAfter { get; set; }

	/// <summary>
	/// <para>
	/// The number of matching terms to return.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms.
	/// If it is not provided, all terms in the field are considered.
	/// </para>
	/// <para>
	/// info
	/// The prefix string cannot be larger than the largest possible keyword value, which is Lucene's term byte-length limit of 32766.
	/// </para>
	/// </summary>
	public string? String { get; set; }

	/// <summary>
	/// <para>
	/// The maximum length of time to spend collecting results.
	/// If the timeout is exceeded the <c>complete</c> flag set to <c>false</c> in the response and the results may be partial or empty.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get; set; }
}

/// <summary>
/// <para>
/// Get terms in an index.
/// </para>
/// <para>
/// Discover terms that match a partial string in an index.
/// This API is designed for low-latency look-ups used in auto-complete scenarios.
/// </para>
/// <para>
/// info
/// The terms enum API may return terms from deleted documents. Deleted documents are initially only marked as deleted. It is not until their segments are merged that documents are actually deleted. Until that happens, the terms enum API will return terms from these documents.
/// </para>
/// </summary>
public readonly partial struct TermsEnumRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.TermsEnumRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.TermsEnumRequest instance)
	{
		Instance = instance;
	}

	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.TermsEnumRequest(index);
#pragma warning restore CS0618
	}

	[System.Obsolete("TODO")]
	public TermsEnumRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.TermsEnumRequest instance) => new Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TermsEnumRequest(Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and index aliases to search.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To search all data streams or indices, omit this parameter or use <c>*</c>  or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, the provided search string is matched against index terms without case sensitivity.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor CaseInsensitive(bool? value = true)
	{
		Instance.CaseInsensitive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms. If not provided, all terms in the field are considered.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms. If not provided, all terms in the field are considered.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.IndexFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor IndexFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor IndexFilter<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The string after which terms in the index should be returned.
	/// It allows for a form of pagination if the last result from one request is passed as the <c>search_after</c> parameter for a subsequent request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor SearchAfter(string? value)
	{
		Instance.SearchAfter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of matching terms to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms.
	/// If it is not provided, all terms in the field are considered.
	/// </para>
	/// <para>
	/// info
	/// The prefix string cannot be larger than the largest possible keyword value, which is Lucene's term byte-length limit of 32766.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor String(string? value)
	{
		Instance.String = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum length of time to spend collecting results.
	/// If the timeout is exceeded the <c>complete</c> flag set to <c>false</c> in the response and the results may be partial or empty.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TermsEnumRequest Build(System.Action<Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor(new Elastic.Clients.Elasticsearch.TermsEnumRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get terms in an index.
/// </para>
/// <para>
/// Discover terms that match a partial string in an index.
/// This API is designed for low-latency look-ups used in auto-complete scenarios.
/// </para>
/// <para>
/// info
/// The terms enum API may return terms from deleted documents. Deleted documents are initially only marked as deleted. It is not until their segments are merged that documents are actually deleted. Until that happens, the terms enum API will return terms from these documents.
/// </para>
/// </summary>
public readonly partial struct TermsEnumRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.TermsEnumRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.TermsEnumRequest instance)
	{
		Instance = instance;
	}

	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.TermsEnumRequest(index);
#pragma warning restore CS0618
	}

	public TermsEnumRequestDescriptor()
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.TermsEnumRequest(typeof(TDocument));
#pragma warning restore CS0618
	}

	public static explicit operator Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.TermsEnumRequest instance) => new Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.TermsEnumRequest(Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and index aliases to search.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To search all data streams or indices, omit this parameter or use <c>*</c>  or <c>_all</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When <c>true</c>, the provided search string is matched against index terms without case sensitivity.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> CaseInsensitive(bool? value = true)
	{
		Instance.CaseInsensitive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms. If not provided, all terms in the field are considered.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms. If not provided, all terms in the field are considered.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.IndexFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Filter an index shard if the provided query rewrites to <c>match_none</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> IndexFilter(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.IndexFilter = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The string after which terms in the index should be returned.
	/// It allows for a form of pagination if the last result from one request is passed as the <c>search_after</c> parameter for a subsequent request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> SearchAfter(string? value)
	{
		Instance.SearchAfter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of matching terms to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The string to match at the start of indexed terms.
	/// If it is not provided, all terms in the field are considered.
	/// </para>
	/// <para>
	/// info
	/// The prefix string cannot be larger than the largest possible keyword value, which is Lucene's term byte-length limit of 32766.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> String(string? value)
	{
		Instance.String = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum length of time to spend collecting results.
	/// If the timeout is exceeded the <c>complete</c> flag set to <c>false</c> in the response and the results may be partial or empty.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.TermsEnumRequest Build(System.Action<Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.TermsEnumRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.TermsEnumRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}