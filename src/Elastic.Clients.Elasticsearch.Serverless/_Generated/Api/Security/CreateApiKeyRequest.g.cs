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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Security;

public sealed partial class CreateApiKeyRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Create an API key.
/// Creates an API key for access without requiring basic authentication.
/// A successful request returns a JSON structure that contains the API key, its unique id, and its name.
/// If applicable, it also returns expiration information for the API key in milliseconds.
/// NOTE: By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// </summary>
public sealed partial class CreateApiKeyRequest : PlainRequest<CreateApiKeyRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityCreateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.create_api_key";

	/// <summary>
	/// <para>
	/// If <c>true</c> (the default) then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Serverless.Refresh?>("refresh"); set => Q("refresh", value); }

	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("expiration")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Expiration { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key. It supports nested data structure. Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("metadata")]
	public IDictionary<string, object>? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("name")]
	public Elastic.Clients.Elasticsearch.Serverless.Name? Name { get; set; }

	/// <summary>
	/// <para>
	/// An array of role descriptors for this API key. This parameter is optional. When it is not specified or is an empty array, then the API key will have a point in time snapshot of permissions of the authenticated user. If you supply role descriptors then the resultant permissions would be an intersection of API keys permissions and authenticated user’s permissions thereby limiting the access scope for API keys. The structure of role descriptor is the same as the request for create role API. For more details, see create or update roles API.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("role_descriptors")]
	public IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptor>? RoleDescriptors { get; set; }
}

/// <summary>
/// <para>
/// Create an API key.
/// Creates an API key for access without requiring basic authentication.
/// A successful request returns a JSON structure that contains the API key, its unique id, and its name.
/// If applicable, it also returns expiration information for the API key in milliseconds.
/// NOTE: By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// </summary>
public sealed partial class CreateApiKeyRequestDescriptor<TDocument> : RequestDescriptor<CreateApiKeyRequestDescriptor<TDocument>, CreateApiKeyRequestParameters>
{
	internal CreateApiKeyRequestDescriptor(Action<CreateApiKeyRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CreateApiKeyRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityCreateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.create_api_key";

	public CreateApiKeyRequestDescriptor<TDocument> Refresh(Elastic.Clients.Elasticsearch.Serverless.Refresh? refresh) => Qs("refresh", refresh);

	private Elastic.Clients.Elasticsearch.Serverless.Duration? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Name? NameValue { get; set; }
	private IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor<TDocument>> RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor<TDocument> Expiration(Elastic.Clients.Elasticsearch.Serverless.Duration? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key. It supports nested data structure. Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor<TDocument> Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Serverless.Name? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An array of role descriptors for this API key. This parameter is optional. When it is not specified or is an empty array, then the API key will have a point in time snapshot of permissions of the authenticated user. If you supply role descriptors then the resultant permissions would be an intersection of API keys permissions and authenticated user’s permissions thereby limiting the access scope for API keys. The structure of role descriptor is the same as the request for create role API. For more details, see create or update roles API.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor<TDocument> RoleDescriptors(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor<TDocument>>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor<TDocument>>> selector)
	{
		RoleDescriptorsValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor<TDocument>>());
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

		if (NameValue is not null)
		{
			writer.WritePropertyName("name");
			JsonSerializer.Serialize(writer, NameValue, options);
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
/// Create an API key.
/// Creates an API key for access without requiring basic authentication.
/// A successful request returns a JSON structure that contains the API key, its unique id, and its name.
/// If applicable, it also returns expiration information for the API key in milliseconds.
/// NOTE: By default, API keys never expire. You can specify expiration information when you create the API keys.
/// </para>
/// </summary>
public sealed partial class CreateApiKeyRequestDescriptor : RequestDescriptor<CreateApiKeyRequestDescriptor, CreateApiKeyRequestParameters>
{
	internal CreateApiKeyRequestDescriptor(Action<CreateApiKeyRequestDescriptor> configure) => configure.Invoke(this);

	public CreateApiKeyRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityCreateApiKey;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.create_api_key";

	public CreateApiKeyRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Serverless.Refresh? refresh) => Qs("refresh", refresh);

	private Elastic.Clients.Elasticsearch.Serverless.Duration? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Name? NameValue { get; set; }
	private IDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor> RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor Expiration(Elastic.Clients.Elasticsearch.Serverless.Duration? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key. It supports nested data structure. Within the metadata object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies the name for this API key.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor Name(Elastic.Clients.Elasticsearch.Serverless.Name? name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>
	/// An array of role descriptors for this API key. This parameter is optional. When it is not specified or is an empty array, then the API key will have a point in time snapshot of permissions of the authenticated user. If you supply role descriptors then the resultant permissions would be an intersection of API keys permissions and authenticated user’s permissions thereby limiting the access scope for API keys. The structure of role descriptor is the same as the request for create role API. For more details, see create or update roles API.
	/// </para>
	/// </summary>
	public CreateApiKeyRequestDescriptor RoleDescriptors(Func<FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor>, FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor>> selector)
	{
		RoleDescriptorsValue = selector?.Invoke(new FluentDescriptorDictionary<string, Elastic.Clients.Elasticsearch.Serverless.Security.RoleDescriptorDescriptor>());
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

		if (NameValue is not null)
		{
			writer.WritePropertyName("name");
			JsonSerializer.Serialize(writer, NameValue, options);
		}

		if (RoleDescriptorsValue is not null)
		{
			writer.WritePropertyName("role_descriptors");
			JsonSerializer.Serialize(writer, RoleDescriptorsValue, options);
		}

		writer.WriteEndObject();
	}
}