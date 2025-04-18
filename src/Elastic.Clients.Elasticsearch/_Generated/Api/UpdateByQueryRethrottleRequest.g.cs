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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class UpdateByQueryRethrottleRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The throttle for this request in sub-requests per second.
	/// To turn off throttling, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }
}

internal sealed partial class UpdateByQueryRethrottleRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest>
{
	public override Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Throttle an update by query operation.
/// </para>
/// <para>
/// Change the number of requests per second for a particular update by query operation.
/// Rethrottling that speeds up the query takes effect immediately but rethrotting that slows down the query takes effect after completing the current batch to prevent scroll timeouts.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestConverter))]
public sealed partial class UpdateByQueryRethrottleRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.Id taskId) : base(r => r.Required("task_id", taskId))
	{
	}
#if NET7_0_OR_GREATER
	public UpdateByQueryRethrottleRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceUpdateByQueryRethrottle;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "update_by_query_rethrottle";

	/// <summary>
	/// <para>
	/// The ID for the task.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id TaskId { get => P<Elastic.Clients.Elasticsearch.Id>("task_id"); set => PR("task_id", value); }

	/// <summary>
	/// <para>
	/// The throttle for this request in sub-requests per second.
	/// To turn off throttling, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public float? RequestsPerSecond { get => Q<float?>("requests_per_second"); set => Q("requests_per_second", value); }
}

/// <summary>
/// <para>
/// Throttle an update by query operation.
/// </para>
/// <para>
/// Change the number of requests per second for a particular update by query operation.
/// Rethrottling that speeds up the query takes effect immediately but rethrotting that slows down the query takes effect after completing the current batch to prevent scroll timeouts.
/// </para>
/// </summary>
public readonly partial struct UpdateByQueryRethrottleRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateByQueryRethrottleRequestDescriptor(Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest instance)
	{
		Instance = instance;
	}

	public UpdateByQueryRethrottleRequestDescriptor(Elastic.Clients.Elasticsearch.Id taskId)
	{
		Instance = new Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest(taskId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public UpdateByQueryRethrottleRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor(Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest instance) => new Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The ID for the task.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor TaskId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.TaskId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The throttle for this request in sub-requests per second.
	/// To turn off throttling, set it to <c>-1</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor RequestsPerSecond(float? value)
	{
		Instance.RequestsPerSecond = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest Build(System.Action<Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor(new Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.UpdateByQueryRethrottleRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}