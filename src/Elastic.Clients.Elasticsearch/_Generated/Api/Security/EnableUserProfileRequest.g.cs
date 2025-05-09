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

public sealed partial class EnableUserProfileRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation
	/// visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', nothing is done with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class EnableUserProfileRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Enable a user profile.
/// </para>
/// <para>
/// Enable user profiles to make them visible in user profile searches.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// When you activate a user profile, it's automatically enabled and visible in user profile searches.
/// If you later disable the user profile, you can use the enable user profile API to make the profile visible in these searches again.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestConverter))]
public sealed partial class EnableUserProfileRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnableUserProfileRequest(string uid) : base(r => r.Required("uid", uid))
	{
	}
#if NET7_0_OR_GREATER
	public EnableUserProfileRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EnableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityEnableUserProfile;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.enable_user_profile";

	/// <summary>
	/// <para>
	/// A unique identifier for the user profile.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Uid { get => P<string>("uid"); set => PR("uid", value); }

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation
	/// visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', nothing is done with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Enable a user profile.
/// </para>
/// <para>
/// Enable user profiles to make them visible in user profile searches.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// When you activate a user profile, it's automatically enabled and visible in user profile searches.
/// If you later disable the user profile, you can use the enable user profile API to make the profile visible in these searches again.
/// </para>
/// </summary>
public readonly partial struct EnableUserProfileRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnableUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest instance)
	{
		Instance = instance;
	}

	public EnableUserProfileRequestDescriptor(string uid)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest(uid);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public EnableUserProfileRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest instance) => new Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest(Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique identifier for the user profile.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor Uid(string value)
	{
		Instance.Uid = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation
	/// visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', nothing is done with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnableUserProfileRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}