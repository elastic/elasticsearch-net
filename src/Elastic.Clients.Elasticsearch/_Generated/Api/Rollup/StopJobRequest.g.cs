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

public sealed partial class StopJobRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>wait_for_completion</c> is <c>true</c>, the API blocks for (at maximum) the specified duration while waiting for the job to stop.
	/// If more than <c>timeout</c> time has passed, the API throws a timeout exception.
	/// NOTE: Even if a timeout occurs, the stop request is still processing and eventually moves the job to STOPPED.
	/// The timeout simply means the API call itself timed out while waiting for the status change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If set to <c>true</c>, causes the API to block until the indexer state completely stops.
	/// If set to <c>false</c>, the API returns immediately and the indexer is stopped asynchronously in the background.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class StopJobRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Rollup.StopJobRequest>
{
	public override Elastic.Clients.Elasticsearch.Rollup.StopJobRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Rollup.StopJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Rollup.StopJobRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Stop rollup jobs.
/// If you try to stop a job that does not exist, an exception occurs.
/// If you try to stop a job that is already stopped, nothing happens.
/// </para>
/// <para>
/// Since only a stopped job can be deleted, it can be useful to block the API until the indexer has fully stopped.
/// This is accomplished with the <c>wait_for_completion</c> query parameter, and optionally a timeout. For example:
/// </para>
/// <code>
/// POST _rollup/job/sensor/_stop?wait_for_completion=true&amp;timeout=10s
/// </code>
/// <para>
/// The parameter blocks the API call from returning until either the job has moved to STOPPED or the specified time has elapsed.
/// If the specified time elapses without the job moving to STOPPED, a timeout exception occurs.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Rollup.StopJobRequestConverter))]
public sealed partial class StopJobRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Rollup.StopJobRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StopJobRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public StopJobRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StopJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.RollupStopJob;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "rollup.stop_job";

	/// <summary>
	/// <para>
	/// Identifier for the rollup job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// If <c>wait_for_completion</c> is <c>true</c>, the API blocks for (at maximum) the specified duration while waiting for the job to stop.
	/// If more than <c>timeout</c> time has passed, the API throws a timeout exception.
	/// NOTE: Even if a timeout occurs, the stop request is still processing and eventually moves the job to STOPPED.
	/// The timeout simply means the API call itself timed out while waiting for the status change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If set to <c>true</c>, causes the API to block until the indexer state completely stops.
	/// If set to <c>false</c>, the API returns immediately and the indexer is stopped asynchronously in the background.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>
/// Stop rollup jobs.
/// If you try to stop a job that does not exist, an exception occurs.
/// If you try to stop a job that is already stopped, nothing happens.
/// </para>
/// <para>
/// Since only a stopped job can be deleted, it can be useful to block the API until the indexer has fully stopped.
/// This is accomplished with the <c>wait_for_completion</c> query parameter, and optionally a timeout. For example:
/// </para>
/// <code>
/// POST _rollup/job/sensor/_stop?wait_for_completion=true&amp;timeout=10s
/// </code>
/// <para>
/// The parameter blocks the API call from returning until either the job has moved to STOPPED or the specified time has elapsed.
/// If the specified time elapses without the job moving to STOPPED, a timeout exception occurs.
/// </para>
/// </summary>
public readonly partial struct StopJobRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Rollup.StopJobRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StopJobRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.StopJobRequest instance)
	{
		Instance = instance;
	}

	public StopJobRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Rollup.StopJobRequest(id);
	}

	[System.Obsolete("TODO")]
	public StopJobRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor(Elastic.Clients.Elasticsearch.Rollup.StopJobRequest instance) => new Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Rollup.StopJobRequest(Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the rollup job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>wait_for_completion</c> is <c>true</c>, the API blocks for (at maximum) the specified duration while waiting for the job to stop.
	/// If more than <c>timeout</c> time has passed, the API throws a timeout exception.
	/// NOTE: Even if a timeout occurs, the stop request is still processing and eventually moves the job to STOPPED.
	/// The timeout simply means the API call itself timed out while waiting for the status change.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If set to <c>true</c>, causes the API to block until the indexer state completely stops.
	/// If set to <c>false</c>, the API returns immediately and the indexer is stopped asynchronously in the background.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Rollup.StopJobRequest Build(System.Action<Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor(new Elastic.Clients.Elasticsearch.Rollup.StopJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Rollup.StopJobRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}