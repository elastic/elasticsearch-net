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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class CloseJobRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class CloseJobRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowNoMatch = System.Text.Json.JsonEncodedText.Encode("allow_no_match");
	private static readonly System.Text.Json.JsonEncodedText PropForce = System.Text.Json.JsonEncodedText.Encode("force");
	private static readonly System.Text.Json.JsonEncodedText PropTimeout = System.Text.Json.JsonEncodedText.Encode("timeout");

	public override Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowNoMatch = default;
		LocalJsonValue<bool?> propForce = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowNoMatch.TryReadProperty(ref reader, options, PropAllowNoMatch, null))
			{
				continue;
			}

			if (propForce.TryReadProperty(ref reader, options, PropForce, null))
			{
				continue;
			}

			if (propTimeout.TryReadProperty(ref reader, options, PropTimeout, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowNoMatch = propAllowNoMatch.Value,
			Force = propForce.Value,
			Timeout = propTimeout.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowNoMatch, value.AllowNoMatch, null, null);
		writer.WriteProperty(options, PropForce, value.Force, null, null);
		writer.WriteProperty(options, PropTimeout, value.Timeout, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Close anomaly detection jobs.
/// </para>
/// <para>
/// A job can be opened and closed multiple times throughout its lifecycle. A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
/// When you close a job, it runs housekeeping tasks such as pruning the model history, flushing buffers, calculating final results and persisting the model snapshots. Depending upon the size of the job, it could take several minutes to close and the equivalent time to re-open. After it is closed, the job has a minimal overhead on the cluster except for maintaining its meta data. Therefore it is a best practice to close jobs that are no longer required to process data.
/// If you close an anomaly detection job whose datafeed is running, the request first tries to stop the datafeed. This behavior is equivalent to calling stop datafeed API with the same timeout and force parameters as the close job request.
/// When a datafeed that has a specified end date stops, it automatically closes its associated job.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestConverter))]
public sealed partial class CloseJobRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CloseJobRequest(Elastic.Clients.Elasticsearch.Id jobId) : base(r => r.Required("job_id", jobId))
	{
	}
#if NET7_0_OR_GREATER
	public CloseJobRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CloseJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningCloseJob;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.close_job";

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job. It can be a job identifier, a group name, or a wildcard expression. You can close multiple anomaly detection jobs in a single API request by using a group name, a comma-separated list of jobs, or a wildcard expression. You can close all jobs by using <c>_all</c> or by specifying <c>*</c> as the job identifier.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id JobId { get => P<Elastic.Clients.Elasticsearch.Id>("job_id"); set => PR("job_id", value); }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>allow_no_match</c> query parameter.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the descriptiion for the <c>force</c> query parameter.
	/// </para>
	/// </summary>
	public bool? Force { get; set; }

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>timeout</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get; set; }
}

/// <summary>
/// <para>
/// Close anomaly detection jobs.
/// </para>
/// <para>
/// A job can be opened and closed multiple times throughout its lifecycle. A closed job cannot receive data or perform analysis operations, but you can still explore and navigate results.
/// When you close a job, it runs housekeeping tasks such as pruning the model history, flushing buffers, calculating final results and persisting the model snapshots. Depending upon the size of the job, it could take several minutes to close and the equivalent time to re-open. After it is closed, the job has a minimal overhead on the cluster except for maintaining its meta data. Therefore it is a best practice to close jobs that are no longer required to process data.
/// If you close an anomaly detection job whose datafeed is running, the request first tries to stop the datafeed. This behavior is equivalent to calling stop datafeed API with the same timeout and force parameters as the close job request.
/// When a datafeed that has a specified end date stops, it automatically closes its associated job.
/// </para>
/// </summary>
public readonly partial struct CloseJobRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CloseJobRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest instance)
	{
		Instance = instance;
	}

	public CloseJobRequestDescriptor(Elastic.Clients.Elasticsearch.Id jobId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest(jobId);
	}

	[System.Obsolete("TODO")]
	public CloseJobRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest(Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job. It can be a job identifier, a group name, or a wildcard expression. You can close multiple anomaly detection jobs in a single API request by using a group name, a comma-separated list of jobs, or a wildcard expression. You can close all jobs by using <c>_all</c> or by specifying <c>*</c> as the job identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JobId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>allow_no_match</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor AllowNoMatch(bool? value = true)
	{
		Instance.AllowNoMatch = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the descriptiion for the <c>force</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor Force(bool? value = true)
	{
		Instance.Force = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Refer to the description for the <c>timeout</c> query parameter.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.CloseJobRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}