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

public sealed partial class DeleteSearchApplicationRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class DeleteSearchApplicationRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest>
{
	public override Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete a search application.
/// </para>
/// <para>
/// Remove a search application and its associated alias. Indices attached to the search application are not removed.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestConverter))]
public sealed partial class DeleteSearchApplicationRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteSearchApplicationRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteSearchApplicationRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteSearchApplicationRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SearchApplicationDelete;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "search_application.delete";

	/// <summary>
	/// <para>
	/// The name of the search application to delete.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("name"); set => PR("name", value); }
}

/// <summary>
/// <para>
/// Delete a search application.
/// </para>
/// <para>
/// Remove a search application and its associated alias. Indices attached to the search application are not removed.
/// </para>
/// </summary>
public readonly partial struct DeleteSearchApplicationRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteSearchApplicationRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest instance)
	{
		Instance = instance;
	}

	public DeleteSearchApplicationRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
		Instance = new Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest(name);
	}

	[System.Obsolete("TODO")]
	public DeleteSearchApplicationRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest instance) => new Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest(Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the search application to delete.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest Build(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor(new Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.DeleteSearchApplicationRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}