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

public sealed partial class RenderSearchTemplateRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class RenderSearchTemplateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropFile = System.Text.Json.JsonEncodedText.Encode("file");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropParams = System.Text.Json.JsonEncodedText.Encode("params");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");

	public override Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propFile = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id?> propId = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propParams = default;
		LocalJsonValue<string?> propSource = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFile.TryReadProperty(ref reader, options, PropFile, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propParams.TryReadProperty(ref reader, options, PropParams, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
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
		return new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			File = propFile.Value,
			Id = propId.Value,
			Params = propParams.Value,
			Source = propSource.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFile, value.File, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropParams, value.Params, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Render a search template.
/// </para>
/// <para>
/// Render a search template as a search request body.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestConverter))]
public sealed partial class RenderSearchTemplateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestParameters>
{
#if NET7_0_OR_GREATER
	public RenderSearchTemplateRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RenderSearchTemplateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NoNamespaceRenderSearchTemplate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "render_search_template";

	public string? File { get; set; }

	/// <summary>
	/// <para>
	/// The ID of the search template to render.
	/// If no <c>source</c> is specified, this or the <c>&lt;template-id></c> request path parameter is required.
	/// If you specify both this parameter and the <c>&lt;template-id></c> parameter, the API uses only <c>&lt;template-id></c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Params { get; set; }

	/// <summary>
	/// <para>
	/// An inline search template.
	/// It supports the same parameters as the search API's request body.
	/// These parameters also support Mustache variables.
	/// If no <c>id</c> or <c>&lt;templated-id></c> is specified, this parameter is required.
	/// </para>
	/// </summary>
	public string? Source { get; set; }
}

/// <summary>
/// <para>
/// Render a search template.
/// </para>
/// <para>
/// Render a search template as a search request body.
/// </para>
/// </summary>
public readonly partial struct RenderSearchTemplateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RenderSearchTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest instance)
	{
		Instance = instance;
	}

	public RenderSearchTemplateRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest instance) => new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor File(string? value)
	{
		Instance.File = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ID of the search template to render.
	/// If no <c>source</c> is specified, this or the <c>&lt;template-id></c> request path parameter is required.
	/// If you specify both this parameter and the <c>&lt;template-id></c> parameter, the API uses only <c>&lt;template-id></c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? value)
	{
		Instance.Id = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Params(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Params = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Params()
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Params(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.Params = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor AddParam(string key, object value)
	{
		Instance.Params ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Params.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// An inline search template.
	/// It supports the same parameters as the search API's request body.
	/// These parameters also support Mustache variables.
	/// If no <c>id</c> or <c>&lt;templated-id></c> is specified, this parameter is required.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Source(string? value)
	{
		Instance.Source = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest Build(System.Action<Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor(new Elastic.Clients.Elasticsearch.RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.RenderSearchTemplateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}