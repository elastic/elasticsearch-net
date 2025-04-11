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

namespace Elastic.Clients.Elasticsearch.SearchApplication;

public sealed partial class ListRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Starting offset.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Query in the Lucene query string syntax.
	/// </para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>
	/// Specifies a max number of results to get.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

internal sealed partial class ListRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.ListRequest>
{
	public override Elastic.Clients.Elasticsearch.SearchApplication.ListRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.SearchApplication.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.ListRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get search applications.
/// Get information about search applications.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.ListRequestConverter))]
public sealed partial class ListRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.SearchApplication.ListRequestParameters>
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

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SearchApplicationList;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "search_application.list";

	/// <summary>
	/// <para>
	/// Starting offset.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Query in the Lucene query string syntax.
	/// </para>
	/// </summary>
	public string? QueryLuceneSyntax { get => Q<string?>("q"); set => Q("q", value); }

	/// <summary>
	/// <para>
	/// Specifies a max number of results to get.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get search applications.
/// Get information about search applications.
/// </para>
/// </summary>
public readonly partial struct ListRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.SearchApplication.ListRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ListRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.ListRequest instance)
	{
		Instance = instance;
	}

	public ListRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.SearchApplication.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.ListRequest instance) => new Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.SearchApplication.ListRequest(Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Starting offset.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor From(int? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query in the Lucene query string syntax.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor QueryLuceneSyntax(string? value)
	{
		Instance.QueryLuceneSyntax = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies a max number of results to get.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.SearchApplication.ListRequest Build(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.SearchApplication.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor(new Elastic.Clients.Elasticsearch.SearchApplication.ListRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.ListRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}