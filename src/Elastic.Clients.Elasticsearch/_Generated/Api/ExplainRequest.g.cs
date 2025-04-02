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

public sealed partial class ExplainRequestParameters : Elastic.Transport.RequestParameters
{
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
	/// The field to use as default where no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// It is random by default.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// The query in the Lucene query string syntax.
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
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }
}

internal sealed partial class ExplainRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ExplainRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");

	public override Elastic.Clients.Elasticsearch.ExplainRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.ExplainRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Query = propQuery.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ExplainRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Explain a document match result.
/// Get information about why a specific document matches, or doesn't match, a query.
/// It computes a score explanation for a query and a specific document.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ExplainRequestConverter))]
public sealed partial class ExplainRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.ExplainRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExplainRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public ExplainRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExplainRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceExplain;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "explain";

	/// <summary>
	/// <para>
	/// The document identifier.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Index names that are used to limit the request.
	/// Only a single index name can be provided to this parameter.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

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
	/// The field to use as default where no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public string? Df { get => Q<string?>("df"); set => Q("df", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public bool? Lenient { get => Q<bool?>("lenient"); set => Q("lenient", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// It is random by default.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// The query in the Lucene query string syntax.
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
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Query { get; set; }
}

/// <summary>
/// <para>
/// Explain a document match result.
/// Get information about why a specific document matches, or doesn't match, a query.
/// It computes a score explanation for a query and a specific document.
/// </para>
/// </summary>
public readonly partial struct ExplainRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.ExplainRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.ExplainRequest instance)
	{
		Instance = instance;
	}

	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExplainRequest(index, id);
	}

	[System.Obsolete("TODO")]
	public ExplainRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.ExplainRequest instance) => new Elastic.Clients.Elasticsearch.ExplainRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExplainRequest(Elastic.Clients.Elasticsearch.ExplainRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The document identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Index names that are used to limit the request.
	/// Only a single index name can be provided to this parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Analyzer(string? value)
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
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor AnalyzeWildcard(bool? value = true)
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
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? value)
	{
		Instance.DefaultOperator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use as default where no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Df(string? value)
	{
		Instance.Df = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Lenient(bool? value = true)
	{
		Instance.Lenient = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// It is random by default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query in the Lucene query string syntax.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor QueryLuceneSyntax(string? value)
	{
		Instance.QueryLuceneSyntax = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Source<T>(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<T>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor SourceExcludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor SourceIncludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor StoredFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExplainRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExplainRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExplainRequestDescriptor(new Elastic.Clients.Elasticsearch.ExplainRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Explain a document match result.
/// Get information about why a specific document matches, or doesn't match, a query.
/// It computes a score explanation for a query and a specific document.
/// </para>
/// </summary>
public readonly partial struct ExplainRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.ExplainRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.ExplainRequest instance)
	{
		Instance = instance;
	}

	public ExplainRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExplainRequest(index, id);
	}

	[System.Obsolete("TODO")]
	public ExplainRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.ExplainRequest instance) => new Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExplainRequest(Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The document identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Index names that are used to limit the request.
	/// Only a single index name can be provided to this parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The analyzer to use for the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Analyzer(string? value)
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
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> AnalyzeWildcard(bool? value = true)
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
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> DefaultOperator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? value)
	{
		Instance.DefaultOperator = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to use as default where no field prefix is given in the query string.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Df(string? value)
	{
		Instance.Df = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, format-based query failures (such as providing text to a numeric field) in the query string will be ignored.
	/// This parameter can be used only when the <c>q</c> query string parameter is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Lenient(bool? value = true)
	{
		Instance.Lenient = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// It is random by default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query in the Lucene query string syntax.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> QueryLuceneSyntax(string? value)
	{
		Instance.QueryLuceneSyntax = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// <c>True</c> or <c>false</c> to return the <c>_source</c> field or not or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> SourceExcludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned.
	/// You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> SourceIncludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> StoredFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query? value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Defines the search definition using the Query DSL.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExplainRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.ExplainRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExplainRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}