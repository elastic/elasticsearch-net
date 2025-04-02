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

public sealed partial class GetPrivilegesRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetPrivilegesRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>read_security</c> cluster privilege (or a greater privilege such as <c>manage_security</c> or <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestConverter))]
public sealed partial class GetPrivilegesRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestParameters>
{
	public GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Name? application) : base(r => r.Optional("application", application))
	{
	}

	public GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Name? application, Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("application", application).Optional("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public GetPrivilegesRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetPrivilegesRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityGetPrivileges;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_privileges";

	/// <summary>
	/// <para>
	/// The name of the application.
	/// Application privileges are always associated with exactly one application.
	/// If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Application { get => P<Elastic.Clients.Elasticsearch.Name?>("application"); set => PO("application", value); }

	/// <summary>
	/// <para>
	/// The name of the privilege.
	/// If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Names? Name { get => P<Elastic.Clients.Elasticsearch.Names?>("name"); set => PO("name", value); }
}

/// <summary>
/// <para>
/// Get application privileges.
/// </para>
/// <para>
/// To use this API, you must have one of the following privileges:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The <c>read_security</c> cluster privilege (or a greater privilege such as <c>manage_security</c> or <c>all</c>).
/// </para>
/// </item>
/// <item>
/// <para>
/// The "Manage Application Privileges" global privilege for the application being referenced in the request.
/// </para>
/// </item>
/// </list>
/// </summary>
public readonly partial struct GetPrivilegesRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest instance)
	{
		Instance = instance;
	}

	public GetPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Name application)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(application);
	}

	public GetPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Name application, Elastic.Clients.Elasticsearch.Names name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(application, name);
	}

	public GetPrivilegesRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest instance) => new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the application.
	/// Application privileges are always associated with exactly one application.
	/// If you do not specify this parameter, the API returns information about all privileges for all applications.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor Application(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Application = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the privilege.
	/// If you do not specify this parameter, the API returns information about all privileges for the requested application.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names? value)
	{
		Instance.Name = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetPrivilegesRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}