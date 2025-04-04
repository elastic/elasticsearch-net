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

namespace Elastic.Clients.Elasticsearch.Eql;

public sealed partial class EqlGetRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period for which the search and its results are stored on the cluster.
	/// Defaults to the keep_alive value set by the search’s EQL search API request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Timeout duration to wait for the request to finish.
	/// Defaults to no timeout, meaning the request waits for complete search results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

internal sealed partial class EqlGetRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Eql.EqlGetRequest>
{
	public override Elastic.Clients.Elasticsearch.Eql.EqlGetRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Eql.EqlGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.EqlGetRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get async EQL search results.
/// Get the current status and available results for an async EQL search or a stored synchronous EQL search.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Eql.EqlGetRequestConverter))]
public sealed partial class EqlGetRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Eql.EqlGetRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlGetRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public EqlGetRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EqlGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.EqlGet;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "eql.get";

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// Period for which the search and its results are stored on the cluster.
	/// Defaults to the keep_alive value set by the search’s EQL search API request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Timeout duration to wait for the request to finish.
	/// Defaults to no timeout, meaning the request waits for complete search results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Get async EQL search results.
/// Get the current status and available results for an async EQL search or a stored synchronous EQL search.
/// </para>
/// </summary>
public readonly partial struct EqlGetRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Eql.EqlGetRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlGetRequestDescriptor(Elastic.Clients.Elasticsearch.Eql.EqlGetRequest instance)
	{
		Instance = instance;
	}

	public EqlGetRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Eql.EqlGetRequest(id);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public EqlGetRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor(Elastic.Clients.Elasticsearch.Eql.EqlGetRequest instance) => new Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Eql.EqlGetRequest(Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period for which the search and its results are stored on the cluster.
	/// Defaults to the keep_alive value set by the search’s EQL search API request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Timeout duration to wait for the request to finish.
	/// Defaults to no timeout, meaning the request waits for complete search results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.WaitForCompletionTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Eql.EqlGetRequest Build(System.Action<Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor(new Elastic.Clients.Elasticsearch.Eql.EqlGetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Eql.EqlGetRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}