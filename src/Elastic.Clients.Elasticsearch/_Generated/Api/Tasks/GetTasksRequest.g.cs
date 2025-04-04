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

namespace Elastic.Clients.Elasticsearch.Tasks;

public sealed partial class GetTasksRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the task has completed.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class GetTasksRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest>
{
	public override Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get task information.
/// Get information about a task currently running in the cluster.
/// </para>
/// <para>
/// WARNING: The task management API is new and should still be considered a beta feature.
/// The API may change in ways that are not backwards compatible.
/// </para>
/// <para>
/// If the task identifier is not found, a 404 response code indicates that there are no resources that match the request.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestConverter))]
public sealed partial class GetTasksRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetTasksRequest(Elastic.Clients.Elasticsearch.Id taskId) : base(r => r.Required("task_id", taskId))
	{
	}
#if NET7_0_OR_GREATER
	public GetTasksRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetTasksRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.TasksGet;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "tasks.get";

	/// <summary>
	/// <para>
	/// The task identifier.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id TaskId { get => P<Elastic.Clients.Elasticsearch.Id>("task_id"); set => PR("task_id", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the task has completed.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>
/// Get task information.
/// Get information about a task currently running in the cluster.
/// </para>
/// <para>
/// WARNING: The task management API is new and should still be considered a beta feature.
/// The API may change in ways that are not backwards compatible.
/// </para>
/// <para>
/// If the task identifier is not found, a 404 response code indicates that there are no resources that match the request.
/// </para>
/// </summary>
public readonly partial struct GetTasksRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetTasksRequestDescriptor(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest instance)
	{
		Instance = instance;
	}

	public GetTasksRequestDescriptor(Elastic.Clients.Elasticsearch.Id taskId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest(taskId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetTasksRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest instance) => new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The task identifier.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor TaskId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.TaskId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the task has completed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest Build(System.Action<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}