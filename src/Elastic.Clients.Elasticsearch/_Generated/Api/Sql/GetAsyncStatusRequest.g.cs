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

namespace Elastic.Clients.Elasticsearch.Sql;

public sealed partial class GetAsyncStatusRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetAsyncStatusRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest>
{
	public override Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the async SQL search status.
/// Get the current status of an async SQL search or a stored synchronous SQL search.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestConverter))]
public sealed partial class GetAsyncStatusRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetAsyncStatusRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}
#if NET7_0_OR_GREATER
	public GetAsyncStatusRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetAsyncStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SqlGetAsyncStatus;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "sql.get_async_status";

	/// <summary>
	/// <para>
	/// The identifier for the search.
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
/// Get the async SQL search status.
/// Get the current status of an async SQL search or a stored synchronous SQL search.
/// </para>
/// </summary>
public readonly partial struct GetAsyncStatusRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetAsyncStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest instance)
	{
		Instance = instance;
	}

	public GetAsyncStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Id id)
	{
		Instance = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest(id);
	}

	[System.Obsolete("TODO")]
	public GetAsyncStatusRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest instance) => new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The identifier for the search.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.Id = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest Build(System.Action<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}