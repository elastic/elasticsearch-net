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

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

public sealed partial class GetAsyncSearchRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The length of time that the async search should be available in the cluster.
	/// When not specified, the <c>keep_alive</c> set with the corresponding submit async request will be used.
	/// Otherwise, it is possible to override the value and extend the validity of the request.
	/// When this period expires, the search, if still running, is cancelled.
	/// If the search is completed, its saved results are deleted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

	/// <summary>
	/// <para>
	/// Specifies to wait for the search to be completed up until the provided timeout.
	/// Final results will be returned if available before the timeout expires, otherwise the currently available results will be returned once the timeout expires.
	/// By default no timeout is set meaning that the currently available results will be returned without any additional wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

internal sealed partial class GetAsyncSearchRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest>
{
	public override Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get async search results.
/// </para>
/// <para>
/// Retrieve the results of a previously submitted asynchronous search request.
/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestConverter))]
public partial class GetAsyncSearchRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetAsyncSearchRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.AsyncSearchGet;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "async_search.get";

	/// <summary>
	/// <para>
	/// A unique identifier for the async search.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }

	/// <summary>
	/// <para>
	/// The length of time that the async search should be available in the cluster.
	/// When not specified, the <c>keep_alive</c> set with the corresponding submit async request will be used.
	/// Otherwise, it is possible to override the value and extend the validity of the request.
	/// When this period expires, the search, if still running, is cancelled.
	/// If the search is completed, its saved results are deleted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? KeepAlive { get => Q<Elastic.Clients.Elasticsearch.Duration?>("keep_alive"); set => Q("keep_alive", value); }

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public bool? TypedKeys { get => Q<bool?>("typed_keys"); set => Q("typed_keys", value); }

	/// <summary>
	/// <para>
	/// Specifies to wait for the search to be completed up until the provided timeout.
	/// Final results will be returned if available before the timeout expires, otherwise the currently available results will be returned once the timeout expires.
	/// By default no timeout is set meaning that the currently available results will be returned without any additional wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? WaitForCompletionTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("wait_for_completion_timeout"); set => Q("wait_for_completion_timeout", value); }
}

/// <summary>
/// <para>
/// Get async search results.
/// </para>
/// <para>
/// Retrieve the results of a previously submitted asynchronous search request.
/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
/// </para>
/// </summary>
public readonly partial struct GetAsyncSearchRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest instance)
	{
		Instance = instance;
	}

	public GetAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest(id);
	}

	[System.Obsolete("TODO")]
	public GetAsyncSearchRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor(Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest instance) => new Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A unique identifier for the async search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The length of time that the async search should be available in the cluster.
	/// When not specified, the <c>keep_alive</c> set with the corresponding submit async request will be used.
	/// Otherwise, it is possible to override the value and extend the validity of the request.
	/// When this period expires, the search, if still running, is cancelled.
	/// If the search is completed, its saved results are deleted.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor KeepAlive(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.KeepAlive = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specify whether aggregation and suggester names should be prefixed by their respective types in the response
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor TypedKeys(bool? value = true)
	{
		Instance.TypedKeys = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies to wait for the search to be completed up until the provided timeout.
	/// Final results will be returned if available before the timeout expires, otherwise the currently available results will be returned once the timeout expires.
	/// By default no timeout is set meaning that the currently available results will be returned without any additional wait.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor WaitForCompletionTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.WaitForCompletionTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest Build(System.Action<Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor(new Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.AsyncSearch.GetAsyncSearchRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}