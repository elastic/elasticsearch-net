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

public sealed partial class CreateServiceTokenRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class CreateServiceTokenRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a service account token.
/// </para>
/// <para>
/// Create a service accounts token for access without requiring basic authentication.
/// </para>
/// <para>
/// NOTE: Service account tokens never expire.
/// You must actively delete them if they are no longer needed.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestConverter))]
public sealed partial class CreateServiceTokenRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateServiceTokenRequest(string @namespace, string service, Elastic.Clients.Elasticsearch.Name? name) : base(r => r.Required("namespace", @namespace).Required("service", service).Optional("name", name))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateServiceTokenRequest(string @namespace, string service) : base(r => r.Required("namespace", @namespace).Required("service", service))
	{
	}
#if NET7_0_OR_GREATER
	public CreateServiceTokenRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CreateServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityCreateServiceToken;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.create_service_token";

	/// <summary>
	/// <para>
	/// The name for the service account token.
	/// If omitted, a random name will be generated.
	/// </para>
	/// <para>
	/// Token names must be at least one and no more than 256 characters.
	/// They can contain alphanumeric characters (a-z, A-Z, 0-9), dashes (<c>-</c>), and underscores (<c>_</c>), but cannot begin with an underscore.
	/// </para>
	/// <para>
	/// NOTE: Token names must be unique in the context of the associated service account.
	/// They must also be globally unique with their fully qualified names, which are comprised of the service account principal and token name, such as <c>&lt;namespace>/&lt;service>/&lt;token-name></c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Name? Name { get => P<Elastic.Clients.Elasticsearch.Name?>("name"); set => PO("name", value); }

	/// <summary>
	/// <para>
	/// The name of the namespace, which is a top-level grouping of service accounts.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Namespace { get => P<string>("namespace"); set => PR("namespace", value); }

	/// <summary>
	/// <para>
	/// The name of the service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Service { get => P<string>("service"); set => PR("service", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

/// <summary>
/// <para>
/// Create a service account token.
/// </para>
/// <para>
/// Create a service accounts token for access without requiring basic authentication.
/// </para>
/// <para>
/// NOTE: Service account tokens never expire.
/// You must actively delete them if they are no longer needed.
/// </para>
/// </summary>
public readonly partial struct CreateServiceTokenRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CreateServiceTokenRequestDescriptor(Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest instance)
	{
		Instance = instance;
	}

	public CreateServiceTokenRequestDescriptor(string @namespace, string service, Elastic.Clients.Elasticsearch.Name? name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest(@namespace, service, name);
	}

	public CreateServiceTokenRequestDescriptor(string @namespace, string service)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest(@namespace, service);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public CreateServiceTokenRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor(Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest instance) => new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest(Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name for the service account token.
	/// If omitted, a random name will be generated.
	/// </para>
	/// <para>
	/// Token names must be at least one and no more than 256 characters.
	/// They can contain alphanumeric characters (a-z, A-Z, 0-9), dashes (<c>-</c>), and underscores (<c>_</c>), but cannot begin with an underscore.
	/// </para>
	/// <para>
	/// NOTE: Token names must be unique in the context of the associated service account.
	/// They must also be globally unique with their fully qualified names, which are comprised of the service account principal and token name, such as <c>&lt;namespace>/&lt;service>/&lt;token-name></c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the namespace, which is a top-level grouping of service accounts.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Namespace(string value)
	{
		Instance.Namespace = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Service(string value)
	{
		Instance.Service = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.CreateServiceTokenRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}