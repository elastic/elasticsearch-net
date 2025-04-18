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

public sealed partial class RenderQueryRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class RenderQueryRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropParams = System.Text.Json.JsonEncodedText.Encode("params");

	public override Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propParams = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propParams.TryReadProperty(ref reader, options, PropParams, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Params = propParams.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropParams, value.Params, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Render a search application query.
/// Generate an Elasticsearch query using the specified query parameters and the search template associated with the search application or a default template if none is specified.
/// If a parameter used in the search template is not specified in <c>params</c>, the parameter's default value will be used.
/// The API returns the specific Elasticsearch query that would be generated and run by calling the search application search API.
/// </para>
/// <para>
/// You must have <c>read</c> privileges on the backing alias of the search application.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestConverter))]
public sealed partial class RenderQueryRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RenderQueryRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}
#if NET7_0_OR_GREATER
	public RenderQueryRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RenderQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SearchApplicationRenderQuery;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "search_application.render_query";

	/// <summary>
	/// <para>
	/// The name of the search application to render teh query for.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("name"); set => PR("name", value); }
	public System.Collections.Generic.IDictionary<string, object>? Params { get; set; }
}

/// <summary>
/// <para>
/// Render a search application query.
/// Generate an Elasticsearch query using the specified query parameters and the search template associated with the search application or a default template if none is specified.
/// If a parameter used in the search template is not specified in <c>params</c>, the parameter's default value will be used.
/// The API returns the specific Elasticsearch query that would be generated and run by calling the search application search API.
/// </para>
/// <para>
/// You must have <c>read</c> privileges on the backing alias of the search application.
/// </para>
/// </summary>
public readonly partial struct RenderQueryRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RenderQueryRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest instance)
	{
		Instance = instance;
	}

	public RenderQueryRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
		Instance = new Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest(name);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public RenderQueryRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor(Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest instance) => new Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest(Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the search application to render teh query for.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Params(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Params = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor AddParam(string key, object value)
	{
		Instance.Params ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Params.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest Build(System.Action<Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor(new Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.SearchApplication.RenderQueryRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}