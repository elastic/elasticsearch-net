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

public sealed partial class AuthenticateRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class AuthenticateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.AuthenticateRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.AuthenticateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.AuthenticateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.AuthenticateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Authenticate a user.
/// </para>
/// <para>
/// Authenticates a user and returns information about the authenticated user.
/// Include the user information in a <a href="https://en.wikipedia.org/wiki/Basic_access_authentication">basic auth header</a>.
/// A successful call returns a JSON structure that shows user information such as their username, the roles that are assigned to the user, any assigned metadata, and information about the realms that authenticated and authorized the user.
/// If the user cannot be authenticated, this API returns a 401 status code.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.AuthenticateRequestConverter))]
public sealed partial class AuthenticateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.AuthenticateRequestParameters>
{
#if NET7_0_OR_GREATER
	public AuthenticateRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public AuthenticateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AuthenticateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityAuthenticate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.authenticate";
}

/// <summary>
/// <para>
/// Authenticate a user.
/// </para>
/// <para>
/// Authenticates a user and returns information about the authenticated user.
/// Include the user information in a <a href="https://en.wikipedia.org/wiki/Basic_access_authentication">basic auth header</a>.
/// A successful call returns a JSON structure that shows user information such as their username, the roles that are assigned to the user, any assigned metadata, and information about the realms that authenticated and authorized the user.
/// If the user cannot be authenticated, this API returns a 401 status code.
/// </para>
/// </summary>
public readonly partial struct AuthenticateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.AuthenticateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AuthenticateRequestDescriptor(Elastic.Clients.Elasticsearch.Security.AuthenticateRequest instance)
	{
		Instance = instance;
	}

	public AuthenticateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.AuthenticateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor(Elastic.Clients.Elasticsearch.Security.AuthenticateRequest instance) => new Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.AuthenticateRequest(Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.AuthenticateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.AuthenticateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.AuthenticateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.AuthenticateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}