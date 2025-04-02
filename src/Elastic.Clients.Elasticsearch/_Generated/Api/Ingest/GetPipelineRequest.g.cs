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

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed partial class GetPipelineRequestParameters : Elastic.Transport.RequestParameters
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
	/// Return pipelines without their definitions (default: false)
	/// </para>
	/// </summary>
	public bool? Summary { get => Q<bool?>("summary"); set => Q("summary", value); }
}

internal sealed partial class GetPipelineRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest>
{
	public override Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get pipelines.
/// </para>
/// <para>
/// Get information about one or more ingest pipelines.
/// This API returns a local reference of the pipeline.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestConverter))]
public sealed partial class GetPipelineRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestParameters>
{
	public GetPipelineRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetPipelineRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetPipelineRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IngestGetPipeline;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.get_pipeline";

	/// <summary>
	/// <para>
	/// Comma-separated list of pipeline IDs to retrieve.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To get all ingest pipelines, omit this parameter or use <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => P<Elastic.Clients.Elasticsearch.Id?>("id"); set => PO("id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Return pipelines without their definitions (default: false)
	/// </para>
	/// </summary>
	public bool? Summary { get => Q<bool?>("summary"); set => Q("summary", value); }
}

/// <summary>
/// <para>
/// Get pipelines.
/// </para>
/// <para>
/// Get information about one or more ingest pipelines.
/// This API returns a local reference of the pipeline.
/// </para>
/// </summary>
public readonly partial struct GetPipelineRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest instance)
	{
		Instance = instance;
	}

	public GetPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(id);
	}

	public GetPipelineRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest instance) => new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of pipeline IDs to retrieve.
	/// Wildcard (<c>*</c>) expressions are supported.
	/// To get all ingest pipelines, omit this parameter or use <c>*</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Return pipelines without their definitions (default: false)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor Summary(bool? value = true)
	{
		Instance.Summary = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor(new Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Ingest.GetPipelineRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}