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

public sealed partial class ExistsSourceRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
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
	/// If <c>true</c>, the request refreshes the relevant shards before retrieving the document.
	/// Setting it to <c>true</c> should be done after careful thought and verification that this does not cause a heavy load on the system (and slow down indexing).
	/// </para>
	/// </summary>
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// The version number for concurrency control.
	/// It must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }
}

internal sealed partial class ExistsSourceRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ExistsSourceRequest>
{
	public override Elastic.Clients.Elasticsearch.ExistsSourceRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.ExistsSourceRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ExistsSourceRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Check for a document source.
/// </para>
/// <para>
/// Check whether a document source exists in an index.
/// For example:
/// </para>
/// <code>
/// HEAD my-index-000001/_source/1
/// </code>
/// <para>
/// A document's source is not available if it is disabled in the mapping.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ExistsSourceRequestConverter))]
public sealed partial class ExistsSourceRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.ExistsSourceRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsSourceRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public ExistsSourceRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExistsSourceRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceExistsSource;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "exists_source";

	/// <summary>
	/// <para>
	/// A unique identifier for the document.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases.
	/// It supports wildcards (<c>*</c>).
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
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
	/// If <c>true</c>, the request refreshes the relevant shards before retrieving the document.
	/// Setting it to <c>true</c> should be done after careful thought and verification that this does not cause a heavy load on the system (and slow down indexing).
	/// </para>
	/// </summary>
	public bool? Refresh { get => Q<bool?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? Source { get => Q<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam?>("_source"); set => Q("_source", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceExcludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_excludes"); set => Q("_source_excludes", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? SourceIncludes { get => Q<Elastic.Clients.Elasticsearch.Fields?>("_source_includes"); set => Q("_source_includes", value); }

	/// <summary>
	/// <para>
	/// The version number for concurrency control.
	/// It must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public long? Version { get => Q<long?>("version"); set => Q("version", value); }

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.VersionType? VersionType { get => Q<Elastic.Clients.Elasticsearch.VersionType?>("version_type"); set => Q("version_type", value); }
}

/// <summary>
/// <para>
/// Check for a document source.
/// </para>
/// <para>
/// Check whether a document source exists in an index.
/// For example:
/// </para>
/// <code>
/// HEAD my-index-000001/_source/1
/// </code>
/// <para>
/// A document's source is not available if it is disabled in the mapping.
/// </para>
/// </summary>
public readonly partial struct ExistsSourceRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.ExistsSourceRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsSourceRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsSourceRequest instance)
	{
		Instance = instance;
	}

	public ExistsSourceRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExistsSourceRequest(index, id);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ExistsSourceRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsSourceRequest instance) => new Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExistsSourceRequest(Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique identifier for the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases.
	/// It supports wildcards (<c>*</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Realtime(bool? value = true)
	{
		Instance.Realtime = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes the relevant shards before retrieving the document.
	/// Setting it to <c>true</c> should be done after careful thought and verification that this does not cause a heavy load on the system (and slow down indexing).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Source<T>(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<T>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor SourceExcludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor SourceIncludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version number for concurrency control.
	/// It must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExistsSourceRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor(new Elastic.Clients.Elasticsearch.ExistsSourceRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Check for a document source.
/// </para>
/// <para>
/// Check whether a document source exists in an index.
/// For example:
/// </para>
/// <code>
/// HEAD my-index-000001/_source/1
/// </code>
/// <para>
/// A document's source is not available if it is disabled in the mapping.
/// </para>
/// </summary>
public readonly partial struct ExistsSourceRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.ExistsSourceRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsSourceRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsSourceRequest instance)
	{
		Instance = instance;
	}

	public ExistsSourceRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExistsSourceRequest(index, id);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ExistsSourceRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.ExistsSourceRequest instance) => new Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExistsSourceRequest(Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique identifier for the document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of data streams, indices, and aliases.
	/// It supports wildcards (<c>*</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Realtime(bool? value = true)
	{
		Instance.Realtime = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request refreshes the relevant shards before retrieving the document.
	/// Setting it to <c>true</c> should be done after careful thought and verification that this does not cause a heavy load on the system (and slow down indexing).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to exclude in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> SourceExcludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceExcludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of source fields to include in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> SourceIncludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version number for concurrency control.
	/// It must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExistsSourceRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.ExistsSourceRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsSourceRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}