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

namespace Elastic.Clients.Elasticsearch.Esql;

public sealed partial class AsyncQueryDeleteRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class AsyncQueryDeleteRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest>
{
	public override Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete an async ES|QL query.
/// If the query is still running, it is cancelled.
/// Otherwise, the stored results are deleted.
/// </para>
/// <para>
/// If the Elasticsearch security features are enabled, only the following users can use this API to delete a query:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The authenticated user that submitted the original query request
/// </para>
/// </item>
/// <item>
/// <para>
/// Users with the <c>cancel_task</c> cluster privilege
/// </para>
/// </item>
/// </list>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestConverter))]
public sealed partial class AsyncQueryDeleteRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AsyncQueryDeleteRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public AsyncQueryDeleteRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AsyncQueryDeleteRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.EsqlAsyncQueryDelete;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "esql.async_query_delete";

	/// <summary>
	/// <para>
	/// The unique identifier of the query.
	/// A query ID is provided in the ES|QL async query API response for a query that does not complete in the designated time.
	/// A query ID is also provided when the request was submitted with the <c>keep_on_completion</c> parameter set to <c>true</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id Id { get => P<Elastic.Clients.Elasticsearch.Id>("id"); set => PR("id", value); }
}

/// <summary>
/// <para>
/// Delete an async ES|QL query.
/// If the query is still running, it is cancelled.
/// Otherwise, the stored results are deleted.
/// </para>
/// <para>
/// If the Elasticsearch security features are enabled, only the following users can use this API to delete a query:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The authenticated user that submitted the original query request
/// </para>
/// </item>
/// <item>
/// <para>
/// Users with the <c>cancel_task</c> cluster privilege
/// </para>
/// </item>
/// </list>
/// </summary>
public readonly partial struct AsyncQueryDeleteRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AsyncQueryDeleteRequestDescriptor(Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest instance)
	{
		Instance = instance;
	}

	public AsyncQueryDeleteRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest(id);
	}

	[System.Obsolete("TODO")]
	public AsyncQueryDeleteRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor(Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest instance) => new Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest(Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the query.
	/// A query ID is provided in the ES|QL async query API response for a query that does not complete in the designated time.
	/// A query ID is also provided when the request was submitted with the <c>keep_on_completion</c> parameter set to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest Build(System.Action<Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor(new Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Esql.AsyncQueryDeleteRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}