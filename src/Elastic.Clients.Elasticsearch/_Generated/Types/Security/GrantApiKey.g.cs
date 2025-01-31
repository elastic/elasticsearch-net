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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

internal sealed partial class GrantApiKeyConverter : System.Text.Json.Serialization.JsonConverter<GrantApiKey>
{
	private static readonly System.Text.Json.JsonEncodedText PropExpiration = System.Text.Json.JsonEncodedText.Encode("expiration");
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropRoleDescriptors = System.Text.Json.JsonEncodedText.Encode("role_descriptors");

	public override GrantApiKey Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propExpiration = default;
		LocalJsonValue<IDictionary<string, object>?> propMetadata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Name> propName = default;
		LocalJsonValue<ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>?> propRoleDescriptors = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExpiration.TryRead(ref reader, options, PropExpiration))
			{
				continue;
			}

			if (propMetadata.TryRead(ref reader, options, PropMetadata))
			{
				continue;
			}

			if (propName.TryRead(ref reader, options, PropName))
			{
				continue;
			}

			if (propRoleDescriptors.TryRead(ref reader, options, PropRoleDescriptors, typeof(SingleOrManyMarker<ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>?, IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>)))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new GrantApiKey
		{
			Expiration = propExpiration.Value
,
			Metadata = propMetadata.Value
,
			Name = propName.Value
,
			RoleDescriptors = propRoleDescriptors.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GrantApiKey value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExpiration, value.Expiration);
		writer.WriteProperty(options, PropMetadata, value.Metadata);
		writer.WriteProperty(options, PropName, value.Name);
		writer.WriteProperty(options, PropRoleDescriptors, value.RoleDescriptors, null, typeof(SingleOrManyMarker<ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>?, IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GrantApiKeyConverter))]
public sealed partial class GrantApiKey
{
	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	public string? Expiration { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the <c>metadata</c> object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public IDictionary<string, object>? Metadata { get; set; }
	public Elastic.Clients.Elasticsearch.Name Name { get; set; }

	/// <summary>
	/// <para>
	/// The role descriptors for this API key.
	/// This parameter is optional.
	/// When it is not specified or is an empty array, the API key has a point in time snapshot of permissions of the specified user or access token.
	/// If you supply role descriptors, the resultant permissions are an intersection of API keys permissions and the permissions of the user or access token.
	/// </para>
	/// </summary>
	public ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>? RoleDescriptors { get; set; }
}

public sealed partial class GrantApiKeyDescriptor<TDocument> : SerializableDescriptor<GrantApiKeyDescriptor<TDocument>>
{
	internal GrantApiKeyDescriptor(Action<GrantApiKeyDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GrantApiKeyDescriptor() : base()
	{
	}

	private string? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private Elastic.Clients.Elasticsearch.Name NameValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>? RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor<TDocument> Expiration(string? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the <c>metadata</c> object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor<TDocument> Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public GrantApiKeyDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The role descriptors for this API key.
	/// This parameter is optional.
	/// When it is not specified or is an empty array, the API key has a point in time snapshot of permissions of the specified user or access token.
	/// If you supply role descriptors, the resultant permissions are an intersection of API keys permissions and the permissions of the user or access token.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor<TDocument> RoleDescriptors(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>? roleDescriptors)
	{
		RoleDescriptorsValue = roleDescriptors;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ExpirationValue))
		{
			writer.WritePropertyName("expiration");
			writer.WriteStringValue(ExpirationValue);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		writer.WritePropertyName("name");
		JsonSerializer.Serialize(writer, NameValue, options);
		if (RoleDescriptorsValue is not null)
		{
			writer.WritePropertyName("role_descriptors");
			SingleOrManySerializationHelper.Serialize<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>(RoleDescriptorsValue, writer, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class GrantApiKeyDescriptor : SerializableDescriptor<GrantApiKeyDescriptor>
{
	internal GrantApiKeyDescriptor(Action<GrantApiKeyDescriptor> configure) => configure.Invoke(this);

	public GrantApiKeyDescriptor() : base()
	{
	}

	private string? ExpirationValue { get; set; }
	private IDictionary<string, object>? MetadataValue { get; set; }
	private Elastic.Clients.Elasticsearch.Name NameValue { get; set; }
	private ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>? RoleDescriptorsValue { get; set; }

	/// <summary>
	/// <para>
	/// Expiration time for the API key. By default, API keys never expire.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor Expiration(string? expiration)
	{
		ExpirationValue = expiration;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Arbitrary metadata that you want to associate with the API key.
	/// It supports nested data structure.
	/// Within the <c>metadata</c> object, keys beginning with <c>_</c> are reserved for system usage.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetadataValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public GrantApiKeyDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		NameValue = name;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The role descriptors for this API key.
	/// This parameter is optional.
	/// When it is not specified or is an empty array, the API key has a point in time snapshot of permissions of the specified user or access token.
	/// If you supply role descriptors, the resultant permissions are an intersection of API keys permissions and the permissions of the user or access token.
	/// </para>
	/// </summary>
	public GrantApiKeyDescriptor RoleDescriptors(ICollection<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>? roleDescriptors)
	{
		RoleDescriptorsValue = roleDescriptors;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ExpirationValue))
		{
			writer.WritePropertyName("expiration");
			writer.WriteStringValue(ExpirationValue);
		}

		if (MetadataValue is not null)
		{
			writer.WritePropertyName("metadata");
			JsonSerializer.Serialize(writer, MetadataValue, options);
		}

		writer.WritePropertyName("name");
		JsonSerializer.Serialize(writer, NameValue, options);
		if (RoleDescriptorsValue is not null)
		{
			writer.WritePropertyName("role_descriptors");
			SingleOrManySerializationHelper.Serialize<IDictionary<string, Elastic.Clients.Elasticsearch.Security.RoleDescriptor>>(RoleDescriptorsValue, writer, options);
		}

		writer.WriteEndObject();
	}
}