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

public sealed partial class GetApiKeyRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys that are currently active. An API key is considered active if it is neither invalidated, nor expired at query time. You can specify this together with other parameters such as <c>owner</c> or <c>name</c>. If <c>active_only</c> is false, the response will include both active and inactive (expired or invalidated) keys.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// An API key id.
	/// This parameter cannot be used with any of <c>name</c>, <c>realm_name</c> or <c>username</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => Q<Elastic.Clients.Elasticsearch.Id?>("id"); set => Q("id", value); }

	/// <summary>
	/// <para>
	/// An API key name.
	/// This parameter cannot be used with any of <c>id</c>, <c>realm_name</c> or <c>username</c>.
	/// It supports prefix search with wildcard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Name { get => Q<Elastic.Clients.Elasticsearch.Name?>("name"); set => Q("name", value); }

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys owned by the currently authenticated user.
	/// The <c>realm_name</c> or <c>username</c> parameters cannot be specified when this parameter is set to <c>true</c> as they are assumed to be the currently authenticated ones.
	/// </para>
	/// </summary>
	public bool? Owner { get => Q<bool?>("owner"); set => Q("owner", value); }

	/// <summary>
	/// <para>
	/// The name of an authentication realm.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? RealmName { get => Q<Elastic.Clients.Elasticsearch.Name?>("realm_name"); set => Q("realm_name", value); }

	/// <summary>
	/// <para>
	/// The username of a user.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Username? Username { get => Q<Elastic.Clients.Elasticsearch.Username?>("username"); set => Q("username", value); }

	/// <summary>
	/// <para>
	/// Return the snapshot of the owner user's role descriptors
	/// associated with the API key. An API key's actual
	/// permission is the intersection of its assigned role
	/// descriptors and the owner user's role descriptors.
	/// </para>
	/// </summary>
	public bool? WithLimitedBy { get => Q<bool?>("with_limited_by"); set => Q("with_limited_by", value); }

	/// <summary>
	/// <para>
	/// Determines whether to also retrieve the profile uid, for the API key owner principal, if it exists.
	/// </para>
	/// </summary>
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }
}

internal sealed partial class GetApiKeyRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get API key information.
/// </para>
/// <para>
/// Retrieves information for one or more API keys.
/// NOTE: If you have only the <c>manage_own_api_key</c> privilege, this API returns only the API keys that you own.
/// If you have <c>read_security</c>, <c>manage_api_key</c> or greater privileges (including <c>manage_security</c>), this API returns all API keys regardless of ownership.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestConverter))]
public sealed partial class GetApiKeyRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestParameters>
{
#if NET7_0_OR_GREATER
	public GetApiKeyRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetApiKeyRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityGetApiKey;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_api_key";

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys that are currently active. An API key is considered active if it is neither invalidated, nor expired at query time. You can specify this together with other parameters such as <c>owner</c> or <c>name</c>. If <c>active_only</c> is false, the response will include both active and inactive (expired or invalidated) keys.
	/// </para>
	/// </summary>
	public bool? ActiveOnly { get => Q<bool?>("active_only"); set => Q("active_only", value); }

	/// <summary>
	/// <para>
	/// An API key id.
	/// This parameter cannot be used with any of <c>name</c>, <c>realm_name</c> or <c>username</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => Q<Elastic.Clients.Elasticsearch.Id?>("id"); set => Q("id", value); }

	/// <summary>
	/// <para>
	/// An API key name.
	/// This parameter cannot be used with any of <c>id</c>, <c>realm_name</c> or <c>username</c>.
	/// It supports prefix search with wildcard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Name { get => Q<Elastic.Clients.Elasticsearch.Name?>("name"); set => Q("name", value); }

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys owned by the currently authenticated user.
	/// The <c>realm_name</c> or <c>username</c> parameters cannot be specified when this parameter is set to <c>true</c> as they are assumed to be the currently authenticated ones.
	/// </para>
	/// </summary>
	public bool? Owner { get => Q<bool?>("owner"); set => Q("owner", value); }

	/// <summary>
	/// <para>
	/// The name of an authentication realm.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? RealmName { get => Q<Elastic.Clients.Elasticsearch.Name?>("realm_name"); set => Q("realm_name", value); }

	/// <summary>
	/// <para>
	/// The username of a user.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Username? Username { get => Q<Elastic.Clients.Elasticsearch.Username?>("username"); set => Q("username", value); }

	/// <summary>
	/// <para>
	/// Return the snapshot of the owner user's role descriptors
	/// associated with the API key. An API key's actual
	/// permission is the intersection of its assigned role
	/// descriptors and the owner user's role descriptors.
	/// </para>
	/// </summary>
	public bool? WithLimitedBy { get => Q<bool?>("with_limited_by"); set => Q("with_limited_by", value); }

	/// <summary>
	/// <para>
	/// Determines whether to also retrieve the profile uid, for the API key owner principal, if it exists.
	/// </para>
	/// </summary>
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }
}

/// <summary>
/// <para>
/// Get API key information.
/// </para>
/// <para>
/// Retrieves information for one or more API keys.
/// NOTE: If you have only the <c>manage_own_api_key</c> privilege, this API returns only the API keys that you own.
/// If you have <c>read_security</c>, <c>manage_api_key</c> or greater privileges (including <c>manage_security</c>), this API returns all API keys regardless of ownership.
/// </para>
/// </summary>
public readonly partial struct GetApiKeyRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest instance)
	{
		Instance = instance;
	}

	public GetApiKeyRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest instance) => new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest(Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys that are currently active. An API key is considered active if it is neither invalidated, nor expired at query time. You can specify this together with other parameters such as <c>owner</c> or <c>name</c>. If <c>active_only</c> is false, the response will include both active and inactive (expired or invalidated) keys.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor ActiveOnly(bool? value = true)
	{
		Instance.ActiveOnly = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An API key id.
	/// This parameter cannot be used with any of <c>name</c>, <c>realm_name</c> or <c>username</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// An API key name.
	/// This parameter cannot be used with any of <c>id</c>, <c>realm_name</c> or <c>username</c>.
	/// It supports prefix search with wildcard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A boolean flag that can be used to query API keys owned by the currently authenticated user.
	/// The <c>realm_name</c> or <c>username</c> parameters cannot be specified when this parameter is set to <c>true</c> as they are assumed to be the currently authenticated ones.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Owner(bool? value = true)
	{
		Instance.Owner = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of an authentication realm.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor RealmName(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.RealmName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The username of a user.
	/// This parameter cannot be used with either <c>id</c> or <c>name</c> or when <c>owner</c> flag is set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Username(Elastic.Clients.Elasticsearch.Username? value)
	{
		Instance.Username = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Return the snapshot of the owner user's role descriptors
	/// associated with the API key. An API key's actual
	/// permission is the intersection of its assigned role
	/// descriptors and the owner user's role descriptors.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor WithLimitedBy(bool? value = true)
	{
		Instance.WithLimitedBy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines whether to also retrieve the profile uid, for the API key owner principal, if it exists.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor WithProfileUid(bool? value = true)
	{
		Instance.WithProfileUid = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.GetApiKeyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetApiKeyRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}