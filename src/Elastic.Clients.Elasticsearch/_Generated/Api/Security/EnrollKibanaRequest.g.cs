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

public sealed partial class EnrollKibanaRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class EnrollKibanaRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest>
{
	public override Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Enroll Kibana.
/// </para>
/// <para>
/// Enable a Kibana instance to configure itself for communication with a secured Elasticsearch cluster.
/// </para>
/// <para>
/// NOTE: This API is currently intended for internal use only by Kibana.
/// Kibana uses this API internally to configure itself for communications with an Elasticsearch cluster that already has security features enabled.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestConverter))]
public sealed partial class EnrollKibanaRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestParameters>
{
#if NET7_0_OR_GREATER
	public EnrollKibanaRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public EnrollKibanaRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SecurityEnrollKibana;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.enroll_kibana";
}

/// <summary>
/// <para>
/// Enroll Kibana.
/// </para>
/// <para>
/// Enable a Kibana instance to configure itself for communication with a secured Elasticsearch cluster.
/// </para>
/// <para>
/// NOTE: This API is currently intended for internal use only by Kibana.
/// Kibana uses this API internally to configure itself for communications with an Elasticsearch cluster that already has security features enabled.
/// </para>
/// </summary>
public readonly partial struct EnrollKibanaRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnrollKibanaRequestDescriptor(Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest instance)
	{
		Instance = instance;
	}

	public EnrollKibanaRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor(Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest instance) => new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor descriptor) => descriptor.Instance;

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest Build(System.Action<Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor(new Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Security.EnrollKibanaRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}