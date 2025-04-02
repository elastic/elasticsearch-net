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

public sealed partial class DeleteServiceTokenRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Refresh? Refresh { get => Q<Elastic.Clients.Elasticsearch.Refresh?>("refresh"); set => Q("refresh", value); }
}

internal sealed partial class DeleteServiceTokenRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete service account tokens.
/// </para>
/// <para>
/// Delete service account tokens for a service in a specified namespace.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestConverter))]
public sealed partial class DeleteServiceTokenRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteServiceTokenRequest(string @namespace, string service, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("namespace", @namespace).Required("service", service).Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteServiceTokenRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityDeleteServiceToken;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.delete_service_token";

	/// <summary>
	/// <para>
	/// The name of the service account token.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// The namespace, which is a top-level grouping of service accounts.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Namespace { get => P<string>("namespace"); set => PR("namespace", value); }

	/// <summary>
	/// <para>
	/// The service name.
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
/// Delete service account tokens.
/// </para>
/// <para>
/// Delete service account tokens for a service in a specified namespace.
/// </para>
/// </summary>
public readonly partial struct DeleteServiceTokenRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteServiceTokenRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest instance)
	{
		Instance = instance;
	}

	public DeleteServiceTokenRequestDescriptor(string @namespace, string service, Elastic.Clients.Elasticsearch.Name name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest(@namespace, service, name);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DeleteServiceTokenRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor(Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest instance) => new Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest(Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the service account token.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The namespace, which is a top-level grouping of service accounts.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Namespace(string value)
	{
		Instance.Namespace = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The service name.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Service(string value)
	{
		Instance.Service = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c> then refresh the affected shards to make this operation visible to search, if <c>wait_for</c> (the default) then wait for a refresh to make this operation visible to search, if <c>false</c> then do nothing with refreshes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Refresh(Elastic.Clients.Elasticsearch.Refresh? value)
	{
		Instance.Refresh = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.DeleteServiceTokenRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}