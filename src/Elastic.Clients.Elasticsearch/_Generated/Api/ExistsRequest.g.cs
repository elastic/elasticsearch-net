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

public sealed partial class ExistsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
	/// </para>
	/// <para>
	/// If it is set to <c>_local</c>, the operation will prefer to be run on a local allocated shard when possible.
	/// If it is set to a custom value, the value is used to guarantee that the same shards will be used for the same custom value.
	/// This can help with "jumping values" when hitting different shards in different refresh states.
	/// A sample value can be something like the web session ID or the user name.
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
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
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

internal sealed partial class ExistsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ExistsRequest>
{
	public override Elastic.Clients.Elasticsearch.ExistsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.ExistsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ExistsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Check a document.
/// </para>
/// <para>
/// Verify that a document exists.
/// For example, check to see if a document with the <c>_id</c> 0 exists:
/// </para>
/// <code>
/// HEAD my-index-000001/_doc/0
/// </code>
/// <para>
/// If the document exists, the API returns a status code of <c>200 - OK</c>.
/// If the document doesn’t exist, the API returns <c>404 - Not Found</c>.
/// </para>
/// <para>
/// <strong>Versioning support</strong>
/// </para>
/// <para>
/// You can use the <c>version</c> parameter to check the document only if its current version is equal to the specified one.
/// </para>
/// <para>
/// Internally, Elasticsearch has marked the old document as deleted and added an entirely new document.
/// The old version of the document doesn't disappear immediately, although you won't be able to access it.
/// Elasticsearch cleans up deleted documents in the background as you continue to index more data.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ExistsRequestConverter))]
public partial class ExistsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.ExistsRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("index", index).Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public ExistsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExistsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceExists;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "exists";

	/// <summary>
	/// <para>
	/// A unique document identifier.
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
	/// <para>
	/// If it is set to <c>_local</c>, the operation will prefer to be run on a local allocated shard when possible.
	/// If it is set to a custom value, the value is used to guarantee that the same shards will be used for the same custom value.
	/// This can help with "jumping values" when hitting different shards in different refresh states.
	/// A sample value can be something like the web session ID or the user name.
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
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? StoredFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("stored_fields"); set => Q("stored_fields", value); }

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
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
/// Check a document.
/// </para>
/// <para>
/// Verify that a document exists.
/// For example, check to see if a document with the <c>_id</c> 0 exists:
/// </para>
/// <code>
/// HEAD my-index-000001/_doc/0
/// </code>
/// <para>
/// If the document exists, the API returns a status code of <c>200 - OK</c>.
/// If the document doesn’t exist, the API returns <c>404 - Not Found</c>.
/// </para>
/// <para>
/// <strong>Versioning support</strong>
/// </para>
/// <para>
/// You can use the <c>version</c> parameter to check the document only if its current version is equal to the specified one.
/// </para>
/// <para>
/// Internally, Elasticsearch has marked the old document as deleted and added an entirely new document.
/// The old version of the document doesn't disappear immediately, although you won't be able to access it.
/// Elasticsearch cleans up deleted documents in the background as you continue to index more data.
/// </para>
/// </summary>
public readonly partial struct ExistsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.ExistsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsRequest instance)
	{
		Instance = instance;
	}

	public ExistsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExistsRequest(index, id);
	}

	[System.Obsolete("TODO")]
	public ExistsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExistsRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsRequest instance) => new Elastic.Clients.Elasticsearch.ExistsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExistsRequest(Elastic.Clients.Elasticsearch.ExistsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique document identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
	/// </para>
	/// <para>
	/// If it is set to <c>_local</c>, the operation will prefer to be run on a local allocated shard when possible.
	/// If it is set to a custom value, the value is used to guarantee that the same shards will be used for the same custom value.
	/// This can help with "jumping values" when hitting different shards in different refresh states.
	/// A sample value can be something like the web session ID or the user name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Realtime(bool? value = true)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
	{
		Instance.Source = Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Source<T>(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<T>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor SourceExcludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor SourceIncludes<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor StoredFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExistsRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExistsRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExistsRequestDescriptor(new Elastic.Clients.Elasticsearch.ExistsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Check a document.
/// </para>
/// <para>
/// Verify that a document exists.
/// For example, check to see if a document with the <c>_id</c> 0 exists:
/// </para>
/// <code>
/// HEAD my-index-000001/_doc/0
/// </code>
/// <para>
/// If the document exists, the API returns a status code of <c>200 - OK</c>.
/// If the document doesn’t exist, the API returns <c>404 - Not Found</c>.
/// </para>
/// <para>
/// <strong>Versioning support</strong>
/// </para>
/// <para>
/// You can use the <c>version</c> parameter to check the document only if its current version is equal to the specified one.
/// </para>
/// <para>
/// Internally, Elasticsearch has marked the old document as deleted and added an entirely new document.
/// The old version of the document doesn't disappear immediately, although you won't be able to access it.
/// Elasticsearch cleans up deleted documents in the background as you continue to index more data.
/// </para>
/// </summary>
public readonly partial struct ExistsRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.ExistsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsRequestDescriptor(Elastic.Clients.Elasticsearch.ExistsRequest instance)
	{
		Instance = instance;
	}

	public ExistsRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.ExistsRequest(index, id);
	}

	[System.Obsolete("TODO")]
	public ExistsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.ExistsRequest instance) => new Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.ExistsRequest(Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique document identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The node or shard the operation should be performed on.
	/// By default, the operation is randomized between the shard replicas.
	/// </para>
	/// <para>
	/// If it is set to <c>_local</c>, the operation will prefer to be run on a local allocated shard when possible.
	/// If it is set to a custom value, the value is used to guarantee that the same shards will be used for the same custom value.
	/// This can help with "jumping values" when hitting different shards in different refresh states.
	/// A sample value can be something like the web session ID or the user name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Preference(string? value)
	{
		Instance.Preference = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request is real-time as opposed to near-real-time.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Realtime(bool? value = true)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Refresh(bool? value = true)
	{
		Instance.Refresh = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? value)
	{
		Instance.Routing = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Source(Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam? value)
	{
		Instance.Source = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether to return the <c>_source</c> field (<c>true</c> or <c>false</c>) or lists the fields to return.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Source(System.Func<Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParamBuilder<TDocument>, Elastic.Clients.Elasticsearch.Core.Search.SourceConfigParam> action)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> SourceExcludes(Elastic.Clients.Elasticsearch.Fields? value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> SourceExcludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> SourceIncludes(Elastic.Clients.Elasticsearch.Fields? value)
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
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> SourceIncludes(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.SourceIncludes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> StoredFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of stored fields to return as part of a hit.
	/// If no fields are specified, no stored fields are included in the response.
	/// If this field is specified, the <c>_source</c> parameter defaults to <c>false</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> StoredFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.StoredFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Explicit version number for concurrency control.
	/// The specified version must match the current version of the document for the request to succeed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The version type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> VersionType(Elastic.Clients.Elasticsearch.VersionType? value)
	{
		Instance.VersionType = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.ExistsRequest Build(System.Action<Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.ExistsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.ExistsRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}