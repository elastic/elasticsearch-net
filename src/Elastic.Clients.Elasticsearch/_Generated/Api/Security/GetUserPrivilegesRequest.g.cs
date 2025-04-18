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

public sealed partial class GetUserPrivilegesRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The name of the application. Application privileges are always associated with exactly one application. If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Application { get => Q<Elastic.Clients.Elasticsearch.Name?>("application"); set => Q("application", value); }

	/// <summary>
	/// <para>
	/// The name of the privilege. If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Priviledge { get => Q<Elastic.Clients.Elasticsearch.Name?>("priviledge"); set => Q("priviledge", value); }
	public Elastic.Clients.Elasticsearch.Name? Username { get => Q<Elastic.Clients.Elasticsearch.Name?>("username"); set => Q("username", value); }
}

internal sealed partial class GetUserPrivilegesRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get user privileges.
/// </para>
/// <para>
/// Get the security privileges for the logged in user.
/// All users can use this API, but only to determine their own privileges.
/// To check the privileges of other users, you must use the run as feature.
/// To check whether a user has a specific list of privileges, use the has privileges API.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestConverter))]
public sealed partial class GetUserPrivilegesRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestParameters>
{
#if NET7_0_OR_GREATER
	public GetUserPrivilegesRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetUserPrivilegesRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityGetUserPrivileges;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_user_privileges";

	/// <summary>
	/// <para>
	/// The name of the application. Application privileges are always associated with exactly one application. If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Application { get => Q<Elastic.Clients.Elasticsearch.Name?>("application"); set => Q("application", value); }

	/// <summary>
	/// <para>
	/// The name of the privilege. If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Priviledge { get => Q<Elastic.Clients.Elasticsearch.Name?>("priviledge"); set => Q("priviledge", value); }
	public Elastic.Clients.Elasticsearch.Name? Username { get => Q<Elastic.Clients.Elasticsearch.Name?>("username"); set => Q("username", value); }
}

/// <summary>
/// <para>
/// Get user privileges.
/// </para>
/// <para>
/// Get the security privileges for the logged in user.
/// All users can use this API, but only to determine their own privileges.
/// To check the privileges of other users, you must use the run as feature.
/// To check whether a user has a specific list of privileges, use the has privileges API.
/// </para>
/// </summary>
public readonly partial struct GetUserPrivilegesRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetUserPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest instance)
	{
		Instance = instance;
	}

	public GetUserPrivilegesRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest instance) => new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the application. Application privileges are always associated with exactly one application. If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor Application(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Application = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the privilege. If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor Priviledge(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Priviledge = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor Username(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Username = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetUserPrivilegesRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}