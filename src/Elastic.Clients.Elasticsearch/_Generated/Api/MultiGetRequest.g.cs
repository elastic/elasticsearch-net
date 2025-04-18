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

public sealed partial class MultiGetRequestParameters : Elastic.Transport.RequestParameters
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

internal sealed partial class MultiGetRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MultiGetRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocs = System.Text.Json.JsonEncodedText.Encode("docs");
	private static readonly System.Text.Json.JsonEncodedText PropIds = System.Text.Json.JsonEncodedText.Encode("ids");

	public override Elastic.Clients.Elasticsearch.MultiGetRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>?> propDocs = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Ids?> propIds = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocs.TryReadProperty(ref reader, options, PropDocs, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>(o, null)))
			{
				continue;
			}

			if (propIds.TryReadProperty(ref reader, options, PropIds, null))
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
		return new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Docs = propDocs.Value,
			Ids = propIds.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MultiGetRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocs, value.Docs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>(o, v, null));
		writer.WriteProperty(options, PropIds, value.Ids, null, null);
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
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MultiGetRequestConverter))]
public sealed partial class MultiGetRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MultiGetRequestParameters>
{
	public MultiGetRequest(Elastic.Clients.Elasticsearch.IndexName? index) : base(r => r.Optional("index", index))
	{
	}
#if NET7_0_OR_GREATER
	public MultiGetRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public MultiGetRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceMultiGet;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "mget";

	/// <summary>
	/// <para>
	/// Name of the index to retrieve documents from when <c>ids</c> are specified, or when a document in the <c>docs</c> array does not specify an index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexName? Index { get => P<Elastic.Clients.Elasticsearch.IndexName?>("index"); set => PO("index", value); }

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

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? Docs { get; set; }

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
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
public readonly partial struct MultiGetRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MultiGetRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.MultiGetRequest instance)
	{
		Instance = instance;
	}

	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName? index)
	{
		Instance = new Elastic.Clients.Elasticsearch.MultiGetRequest(index);
	}

	public MultiGetRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.MultiGetRequest instance) => new Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the index to retrieve documents from when <c>ids</c> are specified, or when a document in the <c>docs</c> array does not specify an index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should this request force synthetic _source?
	/// Use this to test if the mapping supports synthetic _source and to get a sense of the worst case performance.
	/// Fetches with this enabled will be slower the enabling synthetic source natively in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor ForceSyntheticSource(bool? value = true)
	{
		Instance.ForceSyntheticSource = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on. Random by default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Realtime(bool? value = true)
	{
		Instance.Realtime = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes relevant shards before retrieving documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Source<T>(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<T>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor SourceExcludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor SourceIncludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor StoredFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Docs(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? value)
	{
		Instance.Docs = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Docs(params Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation[] values)
	{
		Instance.Docs = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Docs(params System.Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor.Build(action));
		}

		Instance.Docs = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Docs<T>(params System.Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<T>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<T>.Build(action));
		}

		Instance.Docs = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Ids(Elastic.Clients.Elasticsearch.Ids? value)
	{
		Instance.Ids = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MultiGetRequest Build(System.Action<Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor(new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
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
public readonly partial struct MultiGetRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.MultiGetRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.MultiGetRequest instance)
	{
		Instance = instance;
	}

	public MultiGetRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName? index)
	{
		Instance = new Elastic.Clients.Elasticsearch.MultiGetRequest(index);
	}

	public MultiGetRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MultiGetRequest(typeof(TDocument));
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.MultiGetRequest instance) => new Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the index to retrieve documents from when <c>ids</c> are specified, or when a document in the <c>docs</c> array does not specify an index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName? value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should this request force synthetic _source?
	/// Use this to test if the mapping supports synthetic _source and to get a sense of the worst case performance.
	/// Fetches with this enabled will be slower the enabling synthetic source natively in the index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> ForceSyntheticSource(bool? value = true)
	{
		Instance.ForceSyntheticSource = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on. Random by default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Realtime(bool? value = true)
	{
		Instance.Realtime = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes relevant shards before retrieving documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// True or false to return the <c>_source</c> field or not, or a list of fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamFactory<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude from the response.
	/// You can also use this parameter to exclude fields from the subset specified in <c>_source_includes</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> SourceExcludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// If this parameter is specified, only these source fields are returned. You can exclude fields from this subset using the <c>_source_excludes</c> query parameter.
	/// If the <c>_source</c> parameter is <c>false</c>, this parameter is ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> SourceIncludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, retrieves the document fields stored in the index rather than the document <c>_source</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> StoredFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Docs(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>? value)
	{
		Instance.Docs = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Docs(params Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation[] values)
	{
		Instance.Docs = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// The documents you want to retrieve. Required if no index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Docs(params System.Action<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperation>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.Core.MGet.MultiGetOperationDescriptor<TDocument>.Build(action));
		}

		Instance.Docs = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The IDs of the documents you want to retrieve. Allowed when the index is specified in the request URI.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Ids(Elastic.Clients.Elasticsearch.Ids? value)
	{
		Instance.Ids = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MultiGetRequest Build(System.Action<Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.MultiGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MultiGetRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}