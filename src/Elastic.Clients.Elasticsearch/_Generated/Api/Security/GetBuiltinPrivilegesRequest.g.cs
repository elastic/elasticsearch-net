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

public sealed partial class GetBuiltinPrivilegesRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetBuiltinPrivilegesRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get builtin privileges.
/// </para>
/// <para>
/// Get the list of cluster privileges and index privileges that are available in this version of Elasticsearch.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestConverter))]
public sealed partial class GetBuiltinPrivilegesRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestParameters>
{
#if NET7_0_OR_GREATER
	public GetBuiltinPrivilegesRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetBuiltinPrivilegesRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityGetBuiltinPrivileges;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.get_builtin_privileges";
}

/// <summary>
/// <para>
/// Get builtin privileges.
/// </para>
/// <para>
/// Get the list of cluster privileges and index privileges that are available in this version of Elasticsearch.
/// </para>
/// </summary>
public readonly partial struct GetBuiltinPrivilegesRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetBuiltinPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest instance)
	{
		Instance = instance;
	}

	public GetBuiltinPrivilegesRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor(Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest instance) => new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.GetBuiltinPrivilegesRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}