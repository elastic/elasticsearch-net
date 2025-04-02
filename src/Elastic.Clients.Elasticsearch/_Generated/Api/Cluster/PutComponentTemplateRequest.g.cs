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

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class PutComponentTemplateRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>true</c>, this request cannot replace or update existing component templates.
	/// </para>
	/// </summary>
	public bool? Create { get => Q<bool?>("create"); set => Q("create", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class PutComponentTemplateRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropDeprecated = System.Text.Json.JsonEncodedText.Encode("deprecated");
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("_meta");
	private static readonly System.Text.Json.JsonEncodedText PropTemplate = System.Text.Json.JsonEncodedText.Encode("template");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propDeprecated = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propMeta = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.IndexManagement.IndexState> propTemplate = default;
		LocalJsonValue<long?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDeprecated.TryReadProperty(ref reader, options, PropDeprecated, null))
			{
				continue;
			}

			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propTemplate.TryReadProperty(ref reader, options, PropTemplate, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Deprecated = propDeprecated.Value,
			Meta = propMeta.Value,
			Template = propTemplate.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDeprecated, value.Deprecated, null, null);
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropTemplate, value.Template, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create or update a component template.
/// Component templates are building blocks for constructing index templates that specify index mappings, settings, and aliases.
/// </para>
/// <para>
/// An index template can be composed of multiple component templates.
/// To use a component template, specify it in an index template’s <c>composed_of</c> list.
/// Component templates are only applied to new data streams and indices as part of a matching index template.
/// </para>
/// <para>
/// Settings and mappings specified directly in the index template or the create index request override any settings or mappings specified in a component template.
/// </para>
/// <para>
/// Component templates are only used during index creation.
/// For data streams, this includes data stream creation and the creation of a stream’s backing indices.
/// Changes to component templates do not affect existing indices, including a stream’s backing indices.
/// </para>
/// <para>
/// You can use C-style <c>/* *\/</c> block comments in component templates.
/// You can include comments anywhere in the request body except before the opening curly bracket.
/// </para>
/// <para>
/// <strong>Applying component templates</strong>
/// </para>
/// <para>
/// You cannot directly apply a component template to a data stream or index.
/// To be applied, a component template must be included in an index template's <c>composed_of</c> list.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestConverter))]
public sealed partial class PutComponentTemplateRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestParameters>
{
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("name", name))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Name name, Elastic.Clients.Elasticsearch.IndexManagement.IndexState template) : base(r => r.Required("name", name))
	{
		Template = template;
	}
#if NET7_0_OR_GREATER
	public PutComponentTemplateRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.ClusterPutComponentTemplate;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "cluster.put_component_template";

	/// <summary>
	/// <para>
	/// Name of the component template to create.
	/// Elasticsearch includes the following built-in component templates: <c>logs-mappings</c>; <c>logs-settings</c>; <c>metrics-mappings</c>; <c>metrics-settings</c>;<c>synthetics-mapping</c>; <c>synthetics-settings</c>.
	/// Elastic Agent uses these templates to configure backing indices for its data streams.
	/// If you use Elastic Agent and want to overwrite one of these templates, set the <c>version</c> for your replacement template higher than the current version.
	/// If you don’t use Elastic Agent and want to disable all built-in component and index templates, set <c>stack.templates.enabled</c> to <c>false</c> using the cluster update settings API.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Name Name { get => P<Elastic.Clients.Elasticsearch.Name>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, this request cannot replace or update existing component templates.
	/// </para>
	/// </summary>
	public bool? Create { get => Q<bool?>("create"); set => Q("create", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Marks this index template as deprecated. When creating or updating a non-deprecated index template
	/// that uses deprecated components, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public bool? Deprecated { get; set; }

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Meta { get; set; }

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexManagement.IndexState Template { get; set; }

	/// <summary>
	/// <para>
	/// Version number used to manage component templates externally.
	/// This number isn't automatically generated or incremented by Elasticsearch.
	/// To unset a version, replace the template without specifying a version.
	/// </para>
	/// </summary>
	public long? Version { get; set; }
}

/// <summary>
/// <para>
/// Create or update a component template.
/// Component templates are building blocks for constructing index templates that specify index mappings, settings, and aliases.
/// </para>
/// <para>
/// An index template can be composed of multiple component templates.
/// To use a component template, specify it in an index template’s <c>composed_of</c> list.
/// Component templates are only applied to new data streams and indices as part of a matching index template.
/// </para>
/// <para>
/// Settings and mappings specified directly in the index template or the create index request override any settings or mappings specified in a component template.
/// </para>
/// <para>
/// Component templates are only used during index creation.
/// For data streams, this includes data stream creation and the creation of a stream’s backing indices.
/// Changes to component templates do not affect existing indices, including a stream’s backing indices.
/// </para>
/// <para>
/// You can use C-style <c>/* *\/</c> block comments in component templates.
/// You can include comments anywhere in the request body except before the opening curly bracket.
/// </para>
/// <para>
/// <strong>Applying component templates</strong>
/// </para>
/// <para>
/// You cannot directly apply a component template to a data stream or index.
/// To be applied, a component template must be included in an index template's <c>composed_of</c> list.
/// </para>
/// </summary>
public readonly partial struct PutComponentTemplateRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest instance)
	{
		Instance = instance;
	}

	public PutComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(name);
#pragma warning restore CS0618
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PutComponentTemplateRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the component template to create.
	/// Elasticsearch includes the following built-in component templates: <c>logs-mappings</c>; <c>logs-settings</c>; <c>metrics-mappings</c>; <c>metrics-settings</c>;<c>synthetics-mapping</c>; <c>synthetics-settings</c>.
	/// Elastic Agent uses these templates to configure backing indices for its data streams.
	/// If you use Elastic Agent and want to overwrite one of these templates, set the <c>version</c> for your replacement template higher than the current version.
	/// If you don’t use Elastic Agent and want to disable all built-in component and index templates, set <c>stack.templates.enabled</c> to <c>false</c> using the cluster update settings API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, this request cannot replace or update existing component templates.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Create(bool? value = true)
	{
		Instance.Create = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Marks this index template as deprecated. When creating or updating a non-deprecated index template
	/// that uses deprecated components, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Template(Elastic.Clients.Elasticsearch.IndexManagement.IndexState value)
	{
		Instance.Template = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Template()
	{
		Instance.Template = Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Template(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor>? action)
	{
		Instance.Template = Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Template<T>(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor<T>>? action)
	{
		Instance.Template = Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Version number used to manage component templates externally.
	/// This number isn't automatically generated or incremented by Elasticsearch.
	/// To unset a version, replace the template without specifying a version.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor(new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Create or update a component template.
/// Component templates are building blocks for constructing index templates that specify index mappings, settings, and aliases.
/// </para>
/// <para>
/// An index template can be composed of multiple component templates.
/// To use a component template, specify it in an index template’s <c>composed_of</c> list.
/// Component templates are only applied to new data streams and indices as part of a matching index template.
/// </para>
/// <para>
/// Settings and mappings specified directly in the index template or the create index request override any settings or mappings specified in a component template.
/// </para>
/// <para>
/// Component templates are only used during index creation.
/// For data streams, this includes data stream creation and the creation of a stream’s backing indices.
/// Changes to component templates do not affect existing indices, including a stream’s backing indices.
/// </para>
/// <para>
/// You can use C-style <c>/* *\/</c> block comments in component templates.
/// You can include comments anywhere in the request body except before the opening curly bracket.
/// </para>
/// <para>
/// <strong>Applying component templates</strong>
/// </para>
/// <para>
/// You cannot directly apply a component template to a data stream or index.
/// To be applied, a component template must be included in an index template's <c>composed_of</c> list.
/// </para>
/// </summary>
public readonly partial struct PutComponentTemplateRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest instance)
	{
		Instance = instance;
	}

	public PutComponentTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Name name)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(name);
#pragma warning restore CS0618
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PutComponentTemplateRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest instance) => new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the component template to create.
	/// Elasticsearch includes the following built-in component templates: <c>logs-mappings</c>; <c>logs-settings</c>; <c>metrics-mappings</c>; <c>metrics-settings</c>;<c>synthetics-mapping</c>; <c>synthetics-settings</c>.
	/// Elastic Agent uses these templates to configure backing indices for its data streams.
	/// If you use Elastic Agent and want to overwrite one of these templates, set the <c>version</c> for your replacement template higher than the current version.
	/// If you don’t use Elastic Agent and want to disable all built-in component and index templates, set <c>stack.templates.enabled</c> to <c>false</c> using the cluster update settings API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name value)
	{
		Instance.Name = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, this request cannot replace or update existing component templates.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Create(bool? value = true)
	{
		Instance.Create = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Marks this index template as deprecated. When creating or updating a non-deprecated index template
	/// that uses deprecated components, Elasticsearch will emit a deprecation warning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Deprecated(bool? value = true)
	{
		Instance.Deprecated = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Meta(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Meta = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Meta()
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Optional user metadata about the component template.
	/// It may have any contents. This map is not automatically generated by Elasticsearch.
	/// This information is stored in the cluster state, so keeping it short is preferable.
	/// To unset <c>_meta</c>, replace the template without specifying this information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Meta(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.Meta = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> AddMeta(string key, object value)
	{
		Instance.Meta ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Meta.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Template(Elastic.Clients.Elasticsearch.IndexManagement.IndexState value)
	{
		Instance.Template = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Template()
	{
		Instance.Template = Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The template to be applied which includes mappings, settings, or aliases configuration.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Template(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor<TDocument>>? action)
	{
		Instance.Template = Elastic.Clients.Elasticsearch.IndexManagement.IndexStateDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Version number used to manage component templates externally.
	/// This number isn't automatically generated or incremented by Elasticsearch.
	/// To unset a version, replace the template without specifying a version.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Version(long? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest Build(System.Action<Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Cluster.PutComponentTemplateRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}