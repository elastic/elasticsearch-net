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

public sealed partial class ListRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Actions { get => Q<System.Collections.Generic.ICollection<string>?>("actions"); set => Q("actions", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about the running tasks.
	/// This information is useful to distinguish tasks from each other but is more costly to run.
	/// </para>
	/// </summary>
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }

	/// <summary>
	/// <para>
	/// A key that is used to group tasks in the response.
	/// The task lists can be grouped either by nodes or by parent tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.GroupBy? GroupBy { get => Q<Elastic.Clients.Elasticsearch.Tasks.GroupBy?>("group_by"); set => Q("group_by", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of node IDs or names that is used to limit the returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? Nodes { get => Q<Elastic.Clients.Elasticsearch.NodeIds?>("nodes"); set => Q("nodes", value); }

	/// <summary>
	/// <para>
	/// A parent task identifier that is used to limit returned information.
	/// To return all tasks, omit this parameter or use a value of <c>-1</c>.
	/// If the parent task is not found, the API does not return a 404 response code.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? ParentTaskId { get => Q<Elastic.Clients.Elasticsearch.Id?>("parent_task_id"); set => Q("parent_task_id", value); }

	/// <summary>
	/// <para>
	/// The period to wait for each node to respond.
	/// If a node does not respond before its timeout expires, the response does not include its information.
	/// However, timed out nodes are included in the <c>node_failures</c> property.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the operation is complete.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

internal sealed partial class ListRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Tasks.ListRequest>
{
	public override Elastic.Clients.Elasticsearch.Tasks.ListRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Tasks.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Tasks.ListRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get all tasks.
/// Get information about the tasks currently running on one or more nodes in the cluster.
/// </para>
/// <para>
/// WARNING: The task management API is new and should still be considered a beta feature.
/// The API may change in ways that are not backwards compatible.
/// </para>
/// <para>
/// <strong>Identifying running tasks</strong>
/// </para>
/// <para>
/// The <c>X-Opaque-Id header</c>, when provided on the HTTP request header, is going to be returned as a header in the response as well as in the headers field for in the task information.
/// This enables you to track certain calls or associate certain tasks with the client that started them.
/// For example:
/// </para>
/// <code>
/// curl -i -H "X-Opaque-Id: 123456" "http://localhost:9200/_tasks?group_by=parents"
/// </code>
/// <para>
/// The API returns the following result:
/// </para>
/// <code>
/// HTTP/1.1 200 OK
/// X-Opaque-Id: 123456
/// content-type: application/json; charset=UTF-8
/// content-length: 831
/// 
/// {
///   "tasks" : {
///     "u5lcZHqcQhu-rUoFaqDphA:45" : {
///       "node" : "u5lcZHqcQhu-rUoFaqDphA",
///       "id" : 45,
///       "type" : "transport",
///       "action" : "cluster:monitor/tasks/lists",
///       "start_time_in_millis" : 1513823752749,
///       "running_time_in_nanos" : 293139,
///       "cancellable" : false,
///       "headers" : {
///         "X-Opaque-Id" : "123456"
///       },
///       "children" : [
///         {
///           "node" : "u5lcZHqcQhu-rUoFaqDphA",
///           "id" : 46,
///           "type" : "direct",
///           "action" : "cluster:monitor/tasks/lists[n]",
///           "start_time_in_millis" : 1513823752750,
///           "running_time_in_nanos" : 92133,
///           "cancellable" : false,
///           "parent_task_id" : "u5lcZHqcQhu-rUoFaqDphA:45",
///           "headers" : {
///             "X-Opaque-Id" : "123456"
///           }
///         }
///       ]
///     }
///   }
///  }
/// </code>
/// <para>
/// In this example, <c>X-Opaque-Id: 123456</c> is the ID as a part of the response header.
/// The <c>X-Opaque-Id</c> in the task <c>headers</c> is the ID for the task that was initiated by the REST request.
/// The <c>X-Opaque-Id</c> in the children <c>headers</c> is the child task of the task that was initiated by the REST request.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Tasks.ListRequestConverter))]
public sealed partial class ListRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>
{
#if NET7_0_OR_GREATER
	public ListRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ListRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.TasksList;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "tasks.list";

	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Actions { get => Q<System.Collections.Generic.ICollection<string>?>("actions"); set => Q("actions", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about the running tasks.
	/// This information is useful to distinguish tasks from each other but is more costly to run.
	/// </para>
	/// </summary>
	public bool? Detailed { get => Q<bool?>("detailed"); set => Q("detailed", value); }

	/// <summary>
	/// <para>
	/// A key that is used to group tasks in the response.
	/// The task lists can be grouped either by nodes or by parent tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.GroupBy? GroupBy { get => Q<Elastic.Clients.Elasticsearch.Tasks.GroupBy?>("group_by"); set => Q("group_by", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of node IDs or names that is used to limit the returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? Nodes { get => Q<Elastic.Clients.Elasticsearch.NodeIds?>("nodes"); set => Q("nodes", value); }

	/// <summary>
	/// <para>
	/// A parent task identifier that is used to limit returned information.
	/// To return all tasks, omit this parameter or use a value of <c>-1</c>.
	/// If the parent task is not found, the API does not return a 404 response code.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? ParentTaskId { get => Q<Elastic.Clients.Elasticsearch.Id?>("parent_task_id"); set => Q("parent_task_id", value); }

	/// <summary>
	/// <para>
	/// The period to wait for each node to respond.
	/// If a node does not respond before its timeout expires, the response does not include its information.
	/// However, timed out nodes are included in the <c>node_failures</c> property.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the operation is complete.
	/// </para>
	/// </summary>
	public bool? WaitForCompletion { get => Q<bool?>("wait_for_completion"); set => Q("wait_for_completion", value); }
}

/// <summary>
/// <para>
/// Get all tasks.
/// Get information about the tasks currently running on one or more nodes in the cluster.
/// </para>
/// <para>
/// WARNING: The task management API is new and should still be considered a beta feature.
/// The API may change in ways that are not backwards compatible.
/// </para>
/// <para>
/// <strong>Identifying running tasks</strong>
/// </para>
/// <para>
/// The <c>X-Opaque-Id header</c>, when provided on the HTTP request header, is going to be returned as a header in the response as well as in the headers field for in the task information.
/// This enables you to track certain calls or associate certain tasks with the client that started them.
/// For example:
/// </para>
/// <code>
/// curl -i -H "X-Opaque-Id: 123456" "http://localhost:9200/_tasks?group_by=parents"
/// </code>
/// <para>
/// The API returns the following result:
/// </para>
/// <code>
/// HTTP/1.1 200 OK
/// X-Opaque-Id: 123456
/// content-type: application/json; charset=UTF-8
/// content-length: 831
/// 
/// {
///   "tasks" : {
///     "u5lcZHqcQhu-rUoFaqDphA:45" : {
///       "node" : "u5lcZHqcQhu-rUoFaqDphA",
///       "id" : 45,
///       "type" : "transport",
///       "action" : "cluster:monitor/tasks/lists",
///       "start_time_in_millis" : 1513823752749,
///       "running_time_in_nanos" : 293139,
///       "cancellable" : false,
///       "headers" : {
///         "X-Opaque-Id" : "123456"
///       },
///       "children" : [
///         {
///           "node" : "u5lcZHqcQhu-rUoFaqDphA",
///           "id" : 46,
///           "type" : "direct",
///           "action" : "cluster:monitor/tasks/lists[n]",
///           "start_time_in_millis" : 1513823752750,
///           "running_time_in_nanos" : 92133,
///           "cancellable" : false,
///           "parent_task_id" : "u5lcZHqcQhu-rUoFaqDphA:45",
///           "headers" : {
///             "X-Opaque-Id" : "123456"
///           }
///         }
///       ]
///     }
///   }
///  }
/// </code>
/// <para>
/// In this example, <c>X-Opaque-Id: 123456</c> is the ID as a part of the response header.
/// The <c>X-Opaque-Id</c> in the task <c>headers</c> is the ID for the task that was initiated by the REST request.
/// The <c>X-Opaque-Id</c> in the children <c>headers</c> is the child task of the task that was initiated by the REST request.
/// </para>
/// </summary>
public readonly partial struct ListRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Tasks.ListRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ListRequestDescriptor(Elastic.Clients.Elasticsearch.Tasks.ListRequest instance)
	{
		Instance = instance;
	}

	public ListRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Tasks.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor(Elastic.Clients.Elasticsearch.Tasks.ListRequest instance) => new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Tasks.ListRequest(Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Actions(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Actions = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Actions()
	{
		Instance.Actions = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Actions(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.Actions = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list or wildcard expression of actions used to limit the request.
	/// For example, you can use <c>cluser:*</c> to retrieve all cluster-related tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Actions(params string[] values)
	{
		Instance.Actions = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes detailed information about the running tasks.
	/// This information is useful to distinguish tasks from each other but is more costly to run.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Detailed(bool? value = true)
	{
		Instance.Detailed = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A key that is used to group tasks in the response.
	/// The task lists can be grouped either by nodes or by parent tasks.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor GroupBy(Elastic.Clients.Elasticsearch.Tasks.GroupBy? value)
	{
		Instance.GroupBy = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of node IDs or names that is used to limit the returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Nodes(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.Nodes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A parent task identifier that is used to limit returned information.
	/// To return all tasks, omit this parameter or use a value of <c>-1</c>.
	/// If the parent task is not found, the API does not return a 404 response code.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor ParentTaskId(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.ParentTaskId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for each node to respond.
	/// If a node does not respond before its timeout expires, the response does not include its information.
	/// However, timed out nodes are included in the <c>node_failures</c> property.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request blocks until the operation is complete.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor WaitForCompletion(bool? value = true)
	{
		Instance.WaitForCompletion = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Tasks.ListRequest Build(System.Action<Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Tasks.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor(new Elastic.Clients.Elasticsearch.Tasks.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}