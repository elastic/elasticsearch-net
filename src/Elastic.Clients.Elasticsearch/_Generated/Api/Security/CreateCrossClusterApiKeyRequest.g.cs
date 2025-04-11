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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class CreateCrossClusterApiKeyRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class CreateCrossClusterApiKeyRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAccess = System.Text.Json.JsonEncodedText.Encode("access");
	private static readonly System.Text.Json.JsonEncodedText PropExpiration = System.Text.Json.JsonEncodedText.Encode("expiration");
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");

	public override Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Security.Access> propAccess = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propExpiration = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMetadata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Name> propName = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAccess.TryReadProperty(ref reader, options, PropAccess, null))
			{
				continue;
			}

			if (propExpiration.TryReadProperty(ref reader, options, PropExpiration, null))
			{
				continue;
			}

			if (propMetadata.TryReadProperty(ref reader, options, PropMetadata, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
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
		return new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Access = propAccess.Value,
			Expiration = propExpiration.Value,
			Metadata = propMetadata.Value,
			Name = propName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAccess, value.Access, null, null);
		writer.WriteProperty(options, PropExpiration, value.Expiration, null, null);
		writer.WriteProperty(options, PropMetadata, value.Metadata, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a cross-cluster API key.
/// </para>
/// <para>
/// Create an API key of the <c>cross_cluster</c> type for the API key based remote cluster access.
/// A <c>cross_cluster</c> API key cannot be used to authenticate through the REST interface.
/// </para>
/// <para>
/// IMPORTANT: To authenticate this request you must use a credential that is not an API key. Even if you use an API key that has the required privilege, the API returns an error.
/// </para>
/// <para>
/// Cross-cluster API keys are created by the Elasticsearch API key service, which is automatically enabled.
/// </para>
/// <para>
/// NOTE: Unlike REST API keys, a cross-cluster API key does not capture permissions of the authenticated user. The API key’s effective permission is exactly as specified with the <c>access</c> property.
/// </para>
/// <para>
/// A successful request returns a JSON structure that contains the API key, its unique ID, and its name. If applicable, it also returns expiration information for the API key in milliseconds.
/// </para>
/// <para>
/// By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// <para>
/// Cross-cluster API keys can only be updated with the update cross-cluster API key API.
/// Attempting to update them with the update REST API key API or the bulk update REST API keys API will result in an error.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestConverter))]
public sealed partial class CreateCrossClusterApiKeyRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Security.Access access, Elastic.Clients.Elasticsearch.Name name)
	{
		Access = access;
		Name = name;
	}
#if NET7_0_OR_GREATER
	public CreateCrossClusterApiKeyRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The request contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public CreateCrossClusterApiKeyRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityCreateCrossClusterApiKey;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.create_cross_cluster_api_key";

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Security.Access Access { get; set; }

	/// <summary>
	/// <para>
	/// Expiration time for the API key.
	/// By default, API keys never expire.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Expiration { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get; set; }
}

/// <summary>
/// <para>
/// Create a cross-cluster API key.
/// </para>
/// <para>
/// Create an API key of the <c>cross_cluster</c> type for the API key based remote cluster access.
/// A <c>cross_cluster</c> API key cannot be used to authenticate through the REST interface.
/// </para>
/// <para>
/// IMPORTANT: To authenticate this request you must use a credential that is not an API key. Even if you use an API key that has the required privilege, the API returns an error.
/// </para>
/// <para>
/// Cross-cluster API keys are created by the Elasticsearch API key service, which is automatically enabled.
/// </para>
/// <para>
/// NOTE: Unlike REST API keys, a cross-cluster API key does not capture permissions of the authenticated user. The API key’s effective permission is exactly as specified with the <c>access</c> property.
/// </para>
/// <para>
/// A successful request returns a JSON structure that contains the API key, its unique ID, and its name. If applicable, it also returns expiration information for the API key in milliseconds.
/// </para>
/// <para>
/// By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// <para>
/// Cross-cluster API keys can only be updated with the update cross-cluster API key API.
/// Attempting to update them with the update REST API key API or the bulk update REST API keys API will result in an error.
/// </para>
/// </summary>
public readonly partial struct CreateCrossClusterApiKeyRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateCrossClusterApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest instance)
	{
		Instance = instance;
	}

	public CreateCrossClusterApiKeyRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest instance) => new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Access(Elastic.Clients.Elasticsearch.Security.Access value)
	{
		Instance.Access = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Access()
	{
		Instance.Access = Elastic.Clients.Elasticsearch.Security.AccessDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Access(System.Action<Elastic.Clients.Elasticsearch.Security.AccessDescriptor>? action)
	{
		Instance.Access = Elastic.Clients.Elasticsearch.Security.AccessDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Access<T>(System.Action<Elastic.Clients.Elasticsearch.Security.AccessDescriptor<T>>? action)
	{
		Instance.Access = Elastic.Clients.Elasticsearch.Security.AccessDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Expiration time for the API key.
	/// By default, API keys never expire.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Expiration(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Expiration = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Metadata(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Metadata()
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Metadata(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor AddMetadatum(string key, object value)
	{
		Instance.Metadata ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Metadata.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Create a cross-cluster API key.
/// </para>
/// <para>
/// Create an API key of the <c>cross_cluster</c> type for the API key based remote cluster access.
/// A <c>cross_cluster</c> API key cannot be used to authenticate through the REST interface.
/// </para>
/// <para>
/// IMPORTANT: To authenticate this request you must use a credential that is not an API key. Even if you use an API key that has the required privilege, the API returns an error.
/// </para>
/// <para>
/// Cross-cluster API keys are created by the Elasticsearch API key service, which is automatically enabled.
/// </para>
/// <para>
/// NOTE: Unlike REST API keys, a cross-cluster API key does not capture permissions of the authenticated user. The API key’s effective permission is exactly as specified with the <c>access</c> property.
/// </para>
/// <para>
/// A successful request returns a JSON structure that contains the API key, its unique ID, and its name. If applicable, it also returns expiration information for the API key in milliseconds.
/// </para>
/// <para>
/// By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// <para>
/// Cross-cluster API keys can only be updated with the update cross-cluster API key API.
/// Attempting to update them with the update REST API key API or the bulk update REST API keys API will result in an error.
/// </para>
/// </summary>
public readonly partial struct CreateCrossClusterApiKeyRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateCrossClusterApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest instance)
	{
		Instance = instance;
	}

	public CreateCrossClusterApiKeyRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest instance) => new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Access(Elastic.Clients.Elasticsearch.Security.Access value)
	{
		Instance.Access = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Access()
	{
		Instance.Access = Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The access to be granted to this API key.
	/// The access is composed of permissions for cross-cluster search and cross-cluster replication.
	/// At least one of them must be specified.
	/// </para>
	/// <para>
	/// NOTE: No explicit privileges should be specified for either search or replication access.
	/// The creation process automatically converts the access specification to a role descriptor which has relevant privileges assigned accordingly.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Access(System.Action<Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>>? action)
	{
		Instance.Access = Elastic.Clients.Elasticsearch.Security.AccessDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Expiration time for the API key.
	/// By default, API keys never expire.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Expiration(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Expiration = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Metadata(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Metadata()
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Metadata(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Metadata = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> AddMetadatum(string key, object value)
	{
		Instance.Metadata ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Metadata.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateCrossClusterApiKeyRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}