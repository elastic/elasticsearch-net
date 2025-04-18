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

public sealed partial class ExistsComponentTemplateRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only.
	/// Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class ExistsComponentTemplateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest>
{
	public override Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Check component templates.
/// Returns information about whether a particular component template exists.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestConverter))]
public sealed partial class ExistsComponentTemplateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsComponentTemplateRequest(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public ExistsComponentTemplateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExistsComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.ClusterExistsComponentTemplate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.exists_component_template";

	/// <summary>
	/// <para>
	/// Comma-separated list of component template names used to limit the request.
	/// Wildcard (*) expressions are supported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Names Name { get => P<Elastic.Clients.Elasticsearch.Names>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only.
	/// Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Check component templates.
/// Returns information about whether a particular component template exists.
/// </para>
/// </summary>
public readonly partial struct ExistsComponentTemplateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExistsComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest instance)
	{
		Instance = instance;
	}

	public ExistsComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Names name)
	{
		Instance = new Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest(name);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public ExistsComponentTemplateRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest(Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of component template names used to limit the request.
	/// Wildcard (*) expressions are supported.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the request retrieves information from the local node only.
	/// Defaults to false, which means information is retrieved from the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor Local(bool? value = true)
	{
		Instance.Local = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is
	/// received before the timeout expires, the request fails and returns an
	/// error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor(new Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.ExistsComponentTemplateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}