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

public sealed partial class DisableUserProfileRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', it does nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class DisableUserProfileRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Disable a user profile.
/// </para>
/// <para>
/// Disable user profiles so that they are not visible in user profile searches.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// When you activate a user profile, its automatically enabled and visible in user profile searches. You can use the disable user profile API to disable a user profile so it’s not visible in these searches.
/// To re-enable a disabled user profile, use the enable user profile API .
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestConverter))]
public sealed partial class DisableUserProfileRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DisableUserProfileRequest(string uid) : base(r => r.Required("uid", uid))
	{
	}
#if NET7_0_OR_GREATER
	public DisableUserProfileRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DisableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityDisableUserProfile;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.disable_user_profile";

	/// <summary>
	/// <para>
	/// Unique identifier for the user profile.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Uid { get => P<string>("uid"); set => PR("uid", value); }

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', it does nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Disable a user profile.
/// </para>
/// <para>
/// Disable user profiles so that they are not visible in user profile searches.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// <para>
/// When you activate a user profile, its automatically enabled and visible in user profile searches. You can use the disable user profile API to disable a user profile so it’s not visible in these searches.
/// To re-enable a disabled user profile, use the enable user profile API .
/// </para>
/// </summary>
public readonly partial struct DisableUserProfileRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DisableUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest instance)
	{
		Instance = instance;
	}

	public DisableUserProfileRequestDescriptor(string uid)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest(uid);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public DisableUserProfileRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest instance) => new Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest(Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Unique identifier for the user profile.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor Uid(string value)
	{
		Instance.Uid = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If 'true', Elasticsearch refreshes the affected shards to make this operation visible to search.
	/// If 'wait_for', it waits for a refresh to make this operation visible to search.
	/// If 'false', it does nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DisableUserProfileRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}