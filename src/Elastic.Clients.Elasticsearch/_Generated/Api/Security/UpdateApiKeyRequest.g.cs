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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class UpdateApiKeyRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Update an API key.
/// </para>
/// <para>
/// Update attributes of an existing API key.
/// This API supports updates to an API key's access scope, expiration, and metadata.
/// </para>
/// <para>
/// To use this API, you must have at least the <c>manage_own_api_key</c> cluster privilege.
/// Users can only update API keys that they created or that were granted to them.
/// To update another user’s API key, use the <c>run_as</c> feature to submit a request on behalf of another user.
/// </para>
/// <para>
/// IMPORTANT: It's not possible to use an API key as the authentication credential for this API. The owner user’s credentials are required.
/// </para>
/// <para>
/// Use this API to update API keys created by the create API key or grant API Key APIs.
/// If you need to apply the same update to many API keys, you can use the bulk update API keys API to reduce overhead.
/// It's not possible to update expired API keys or API keys that have been invalidated by the invalidate API key API.
/// </para>
/// <para>
/// The access scope of an API key is derived from the <c>role_descriptors</c> you specify in the request and a snapshot of the owner user's permissions at the time of the request.
/// The snapshot of the owner's permissions is updated automatically on every call.
/// </para>
/// <para>
/// IMPORTANT: If you don't specify <c>role_descriptors</c> in the request, a call to this API might still change the API key's access scope.
/// This change can occur if the owner user's permissions have changed since the API key was created or last modified.
/// </para>
/// </summary>
public sealed partial class UpdateApiKeyRequest : PlainRequest<UpdateApiKeyRequestParameters>
{
	public UpdateApiKeyRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityUpdateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.update_api_key";

	/// <summary>
	/// <para>
	/// The expiration time for the API key.
	/// By default, API keys never expire.
	/// This property can be omitted to leave the expiration unchanged.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("expiration")]
	public Elastic.Clients.Elasticsearch.Duration? Expiration { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports a nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// When specified, this value fully replaces the metadata previously associated with the API key.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("metadata")]
	public IDictionary<string, object>? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// The role descriptors to assign to this API key.
	/// The API key's effective permissions are an intersection of its assigned privileges and the point in time snapshot of permissions of the owner user.
	/// You can assign new privileges by specifying them in this parameter.
	/// To remove assigned privileges, you can supply an empty <c>role_descriptors</c> parameter, that is to say, an empty object <c>{}</c>.
	/// If an API key has no assigned privileges, it inherits the owner user's full permissions.
	/// The snapshot of the owner's permissions is always updated, whether you supply the <c>role_descriptors</c> parameter or not.
	/// The structure of a role descriptor is the same as the request for the create API keys API.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("role_descriptors")]
	public IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>? RoleDescriptors { get; set; }
}

/// <summary>
/// <para>
/// Update an API key.
/// </para>
/// <para>
/// Update attributes of an existing API key.
/// This API supports updates to an API key's access scope, expiration, and metadata.
/// </para>
/// <para>
/// To use this API, you must have at least the <c>manage_own_api_key</c> cluster privilege.
/// Users can only update API keys that they created or that were granted to them.
/// To update another user’s API key, use the <c>run_as</c> feature to submit a request on behalf of another user.
/// </para>
/// <para>
/// IMPORTANT: It's not possible to use an API key as the authentication credential for this API. The owner user’s credentials are required.
/// </para>
/// <para>
/// Use this API to update API keys created by the create API key or grant API Key APIs.
/// If you need to apply the same update to many API keys, you can use the bulk update API keys API to reduce overhead.
/// It's not possible to update expired API keys or API keys that have been invalidated by the invalidate API key API.
/// </para>
/// <para>
/// The access scope of an API key is derived from the <c>role_descriptors</c> you specify in the request and a snapshot of the owner user's permissions at the time of the request.
/// The snapshot of the owner's permissions is updated automatically on every call.
/// </para>
/// <para>
/// IMPORTANT: If you don't specify <c>role_descriptors</c> in the request, a call to this API might still change the API key's access scope.
/// This change can occur if the owner user's permissions have changed since the API key was created or last modified.
/// </para>
/// </summary>
public sealed partial class UpdateApiKeyRequestDescriptor<TDocument> : RequestDescriptor<UpdateApiKeyRequestDescriptor<TDocument>, UpdateApiKeyRequestParameters>
{
	internal UpdateApiKeyRequestDescriptor(Action<UpdateApiKeyRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public UpdateApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityUpdateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.update_api_key";

	public UpdateApiKeyRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Duration? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>> RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// The expiration time for the API key.
	/// By default, API keys never expire.
	/// This property can be omitted to leave the expiration unchanged.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor<TDocument> Expiration(Elastic.Clients.Elasticsearch.Duration? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports a nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// When specified, this value fully replaces the metadata previously associated with the API key.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor<TDocument> Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The role descriptors to assign to this API key.
	/// The API key's effective permissions are an intersection of its assigned privileges and the point in time snapshot of permissions of the owner user.
	/// You can assign new privileges by specifying them in this parameter.
	/// To remove assigned privileges, you can supply an empty <c>role_descriptors</c> parameter, that is to say, an empty object <c>{}</c>.
	/// If an API key has no assigned privileges, it inherits the owner user's full permissions.
	/// The snapshot of the owner's permissions is always updated, whether you supply the <c>role_descriptors</c> parameter or not.
	/// The structure of a role descriptor is the same as the request for the create API keys API.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor<TDocument> RoleDescriptors(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>> selector)
	{
		RoleDescriptorsValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor<TDocument>>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExpirationValue is not null)
		{
			writer.WritePropertyName("expiration");
			JsonSerializer.Serialize(writer, ExpirationValue, options);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		if (RoleDescriptorsValue is not null)
		{
			writer.WritePropertyName("role_descriptors");
			JsonSerializer.Serialize(writer, RoleDescriptorsValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Update an API key.
/// </para>
/// <para>
/// Update attributes of an existing API key.
/// This API supports updates to an API key's access scope, expiration, and metadata.
/// </para>
/// <para>
/// To use this API, you must have at least the <c>manage_own_api_key</c> cluster privilege.
/// Users can only update API keys that they created or that were granted to them.
/// To update another user’s API key, use the <c>run_as</c> feature to submit a request on behalf of another user.
/// </para>
/// <para>
/// IMPORTANT: It's not possible to use an API key as the authentication credential for this API. The owner user’s credentials are required.
/// </para>
/// <para>
/// Use this API to update API keys created by the create API key or grant API Key APIs.
/// If you need to apply the same update to many API keys, you can use the bulk update API keys API to reduce overhead.
/// It's not possible to update expired API keys or API keys that have been invalidated by the invalidate API key API.
/// </para>
/// <para>
/// The access scope of an API key is derived from the <c>role_descriptors</c> you specify in the request and a snapshot of the owner user's permissions at the time of the request.
/// The snapshot of the owner's permissions is updated automatically on every call.
/// </para>
/// <para>
/// IMPORTANT: If you don't specify <c>role_descriptors</c> in the request, a call to this API might still change the API key's access scope.
/// This change can occur if the owner user's permissions have changed since the API key was created or last modified.
/// </para>
/// </summary>
public sealed partial class UpdateApiKeyRequestDescriptor : RequestDescriptor<UpdateApiKeyRequestDescriptor, UpdateApiKeyRequestParameters>
{
	internal UpdateApiKeyRequestDescriptor(Action<UpdateApiKeyRequestDescriptor> configure) => configure.Invoke(this);

	public UpdateApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityUpdateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.update_api_key";

	public UpdateApiKeyRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.Duration? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor> RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// The expiration time for the API key.
	/// By default, API keys never expire.
	/// This property can be omitted to leave the expiration unchanged.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor Expiration(Elastic.Clients.Elasticsearch.Duration? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports a nested data structure.
	/// Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// When specified, this value fully replaces the metadata previously associated with the API key.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// The role descriptors to assign to this API key.
	/// The API key's effective permissions are an intersection of its assigned privileges and the point in time snapshot of permissions of the owner user.
	/// You can assign new privileges by specifying them in this parameter.
	/// To remove assigned privileges, you can supply an empty <c>role_descriptors</c> parameter, that is to say, an empty object <c>{}</c>.
	/// If an API key has no assigned privileges, it inherits the owner user's full permissions.
	/// The snapshot of the owner's permissions is always updated, whether you supply the <c>role_descriptors</c> parameter or not.
	/// The structure of a role descriptor is the same as the request for the create API keys API.
	/// </para>
	/// </summary>
	public UpdateApiKeyRequestDescriptor RoleDescriptors(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>> selector)
	{
		RoleDescriptorsValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptorDescriptor>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExpirationValue is not null)
		{
			writer.WritePropertyName("expiration");
			JsonSerializer.Serialize(writer, ExpirationValue, options);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		if (RoleDescriptorsValue is not null)
		{
			writer.WritePropertyName("role_descriptors");
			JsonSerializer.Serialize(writer, RoleDescriptorsValue, options);
		}

		writer.WriteEndObject();
	}
}