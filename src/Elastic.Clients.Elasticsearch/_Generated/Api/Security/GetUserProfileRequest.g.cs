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

public sealed partial class GetUserProfileRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// A comma-separated list of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>.
	/// To return a subset of content use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Data { get => Q<System.Collections.Generic.ICollection<string>?>("data"); set => Q("data", value); }
}

internal sealed partial class GetUserProfileRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get a user profile.
/// </para>
/// <para>
/// Get a user's profile using the unique profile ID.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestConverter))]
public sealed partial class GetUserProfileRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetUserProfileRequest(System.Collections.Generic.ICollection<string> uid) : base(r => r.Required("uid", uid))
	{
	}
#if NET7_0_OR_GREATER
	public GetUserProfileRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityGetUserProfile;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_profile";

	/// <summary>
	/// <para>
	/// A unique identifier for the user profile.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> Uid { get => P<System.Collections.Generic.ICollection<string>>("uid"); set => PR("uid", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>.
	/// To return a subset of content use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Data { get => Q<System.Collections.Generic.ICollection<string>?>("data"); set => Q("data", value); }
}

/// <summary>
/// <para>
/// Get a user profile.
/// </para>
/// <para>
/// Get a user's profile using the unique profile ID.
/// </para>
/// <para>
/// NOTE: The user profile feature is designed only for use by Kibana and Elastic's Observability, Enterprise Search, and Elastic Security solutions.
/// Individual users and external applications should not call this API directly.
/// Elastic reserves the right to change or remove this feature in future releases without prior notice.
/// </para>
/// </summary>
public readonly partial struct GetUserProfileRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest instance)
	{
		Instance = instance;
	}

	public GetUserProfileRequestDescriptor(System.Collections.Generic.ICollection<string> uid)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest(uid);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetUserProfileRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest instance) => new Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest(Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique identifier for the user profile.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Uid(System.Collections.Generic.ICollection<string> value)
	{
		Instance.Uid = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A unique identifier for the user profile.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Uid(params string[] values)
	{
		Instance.Uid = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>.
	/// To return a subset of content use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Data(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Data = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of filters for the <c>data</c> field of the profile document.
	/// To return all content use <c>data=*</c>.
	/// To return a subset of content use <c>data=&lt;key></c> to retrieve content nested under the specified <c>&lt;key></c>.
	/// By default returns no <c>data</c> content.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Data(params string[] values)
	{
		Instance.Data = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.GetUserProfileRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserProfileRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}