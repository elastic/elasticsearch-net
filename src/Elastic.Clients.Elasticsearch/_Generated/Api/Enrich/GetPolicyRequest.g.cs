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

namespace Elastic.Clients.Elasticsearch.Enrich;

public sealed partial class GetPolicyRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class GetPolicyRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest>
{
	public override Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get an enrich policy.
/// Returns information about an enrich policy.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestConverter))]
public sealed partial class GetPolicyRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestParameters>
{
	public GetPolicyRequest(Elastic.Clients.Elasticsearch.Names? name) : base(r => r.Optional("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public GetPolicyRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetPolicyRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetPolicyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.EnrichGetPolicy;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "enrich.get_policy";

	/// <summary>
	/// <para>
	/// Comma-separated list of enrich policy names used to limit the request.
	/// To return information for all enrich policies, omit this parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Names? Name { get => P<Elastic.Clients.Elasticsearch.Names?>("name"); set => PO("name", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get an enrich policy.
/// Returns information about an enrich policy.
/// </para>
/// </summary>
public readonly partial struct GetPolicyRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetPolicyRequestDescriptor(Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest instance)
	{
		Instance = instance;
	}

	public GetPolicyRequestDescriptor(Elastic.Clients.Elasticsearch.Names? name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(name);
	}

	public GetPolicyRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor(Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest instance) => new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of enrich policy names used to limit the request.
	/// To return information for all enrich policies, omit this parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names? value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest Build(System.Action<Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor(new Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Enrich.GetPolicyRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}