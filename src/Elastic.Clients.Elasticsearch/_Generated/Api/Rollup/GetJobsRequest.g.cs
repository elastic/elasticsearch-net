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

namespace Elastic.Clients.Elasticsearch.Rollup;

public sealed partial class GetJobsRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetJobsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest>
{
	public override Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get rollup job information.
/// Get the configuration, stats, and status of rollup jobs.
/// </para>
/// <para>
/// NOTE: This API returns only active (both <c>STARTED</c> and <c>STOPPED</c>) jobs.
/// If a job was created, ran for a while, then was deleted, the API does not return any details about it.
/// For details about a historical rollup job, the rollup capabilities API may be more useful.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestConverter))]
public sealed partial class GetJobsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestParameters>
{
	public GetJobsRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetJobsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public GetJobsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetJobsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.RollupGetJobs;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "rollup.get_jobs";

	/// <summary>
	/// <para>
	/// Identifier for the rollup job.
	/// If it is <c>_all</c> or omitted, the API returns all rollup jobs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get => P<Elastic.Clients.Elasticsearch.Id?>("id"); set => PO("id", value); }
}

/// <summary>
/// <para>
/// Get rollup job information.
/// Get the configuration, stats, and status of rollup jobs.
/// </para>
/// <para>
/// NOTE: This API returns only active (both <c>STARTED</c> and <c>STOPPED</c>) jobs.
/// If a job was created, ran for a while, then was deleted, the API does not return any details about it.
/// For details about a historical rollup job, the rollup capabilities API may be more useful.
/// </para>
/// </summary>
public readonly partial struct GetJobsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetJobsRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest instance)
	{
		Instance = instance;
	}

	public GetJobsRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(id);
	}

	public GetJobsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest instance) => new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the rollup job.
	/// If it is <c>_all</c> or omitted, the API returns all rollup jobs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor(new Elastic.Clients.Elasticsearch.Rollup.GetJobsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.GetJobsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}