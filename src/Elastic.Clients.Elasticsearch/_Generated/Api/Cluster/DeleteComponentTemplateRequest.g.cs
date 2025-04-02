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

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class DeleteComponentTemplateRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class DeleteComponentTemplateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest>
{
	public override Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete component templates.
/// Component templates are building blocks for constructing index templates that specify index mappings, settings, and aliases.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestConverter))]
public sealed partial class DeleteComponentTemplateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteComponentTemplateRequest(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteComponentTemplateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.ClusterDeleteComponentTemplate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.delete_component_template";

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expression of component template names used to limit the request.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Names Name { get => P<Elastic.Clients.Elasticsearch.Names>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Delete component templates.
/// Component templates are building blocks for constructing index templates that specify index mappings, settings, and aliases.
/// </para>
/// </summary>
public readonly partial struct DeleteComponentTemplateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest instance)
	{
		Instance = instance;
	}

	public DeleteComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Names name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest(name);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DeleteComponentTemplateRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest(Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expression of component template names used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor(new Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.DeleteComponentTemplateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}