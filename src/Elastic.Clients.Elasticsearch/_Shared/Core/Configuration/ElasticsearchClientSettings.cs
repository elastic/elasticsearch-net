// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Esql;
using Elastic.Clients.Elasticsearch.Serverless.Fluent;
#else
using Elastic.Clients.Elasticsearch.Esql;
using Elastic.Clients.Elasticsearch.Fluent;
#endif

using Elastic.Clients.Elasticsearch.Serialization;

using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch;

/// <inheritdoc cref="IElasticsearchClientSettings" />
public class ElasticsearchClientSettings : ElasticsearchClientSettingsBase<ElasticsearchClientSettings>
{
	/// <summary>
	///     A delegate used to construct a serializer to serialize CLR types representing documents and other types related to
	///     documents.
	///     By default, the internal serializer will be used to serializer all types.
	/// </summary>
	public delegate Serializer SourceSerializerFactory(Serializer builtIn,
		IElasticsearchClientSettings values);

	/// <summary> The default user agent for Elastic.Clients.Elasticsearch </summary>
	public static readonly UserAgent DefaultUserAgent =
		Transport.UserAgent.Create("elasticsearch-net", typeof(IElasticsearchClientSettings));

	/// <summary>
	///     Creates a new instance of connection settings, if <paramref name="uri" /> is not specified will default to
	///     connecting to http://localhost:9200
	/// </summary>
	/// <param name="uri"></param>
	public ElasticsearchClientSettings(Uri? uri = null) : this(
		new SingleNodePool(uri ?? new Uri("http://localhost:9200")))
	{
	}

	/// <summary>
	///     Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId" />,
	///     <para><see cref="CloudNodePool" /> documentation for more information on how to obtain your Cloud Id</para>
	/// </summary>
	public ElasticsearchClientSettings(string cloudId, AuthorizationHeader credentials) : this(
		new CloudNodePool(cloudId, credentials))
	{
	}

	public ElasticsearchClientSettings(NodePool nodePool) : this(nodePool, null, null)
	{
	}

	public ElasticsearchClientSettings(NodePool nodePool, SourceSerializerFactory sourceSerializer)
		: this(nodePool, null, sourceSerializer) { }

	public ElasticsearchClientSettings(NodePool nodePool, IRequestInvoker requestInvoker) : this(nodePool, requestInvoker, null)
	{
	}

	public ElasticsearchClientSettings(NodePool nodePool, IRequestInvoker requestInvoker, SourceSerializerFactory sourceSerializer) : this(
		nodePool,
		requestInvoker, sourceSerializer, null)
	{
	}

	/// <summary>
	/// Instantiate connection settings using a <see cref="SingleNodePool" /> using the provided
	/// <see cref="InMemoryRequestInvoker" /> that never uses any IO.
	/// </summary>
	public ElasticsearchClientSettings(InMemoryRequestInvoker inMemoryTransportClient)
		: this(new SingleNodePool(new Uri("http://localhost:9200")), inMemoryTransportClient)
	{
	}

	public ElasticsearchClientSettings(
		NodePool nodePool,
		IRequestInvoker requestInvoker,
		SourceSerializerFactory sourceSerializer,
		IPropertyMappingProvider propertyMappingProvider) : base(nodePool, requestInvoker, sourceSerializer, propertyMappingProvider)
	{
	}
}

/// <inheritdoc cref="IElasticsearchClientSettings" />
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class ElasticsearchClientSettingsBase<TConnectionSettings> :
	ConnectionConfigurationBase<TConnectionSettings>,
	IElasticsearchClientSettings
	where TConnectionSettings : ElasticsearchClientSettingsBase<TConnectionSettings>, IElasticsearchClientSettings
{
	private readonly FluentDictionary<Type, string> _defaultIndices;
	private readonly FluentDictionary<Type, string> _defaultRelationNames;
	private readonly HashSet<Type> _disableIdInference = new();
	private readonly FluentDictionary<Type, string> _idProperties = new();

	private readonly Inferrer _inferrer;

	private readonly IPropertyMappingProvider _propertyMappingProvider;
	private readonly FluentDictionary<MemberInfo, PropertyMapping> _propertyMappings = new();
	private readonly FluentDictionary<Type, string> _routeProperties = new();
	private readonly Serializer _sourceSerializer;
	private bool _experimentalEnableSerializeNullInferredValues;
	private ExperimentalSettings _experimentalSettings = new();

	private bool _defaultDisableAllInference;
	private Func<string, string> _defaultFieldNameInferrer;
	private string _defaultIndex;

	protected ElasticsearchClientSettingsBase(
		NodePool nodePool,
		IRequestInvoker requestInvoker,
		ElasticsearchClientSettings.SourceSerializerFactory? sourceSerializerFactory,
		IPropertyMappingProvider propertyMappingProvider)
		: base(nodePool, requestInvoker, null, ElasticsearchClientProductRegistration.DefaultForElasticsearchClientsElasticsearch)
	{
		var requestResponseSerializer = new DefaultRequestResponseSerializer(this);
		var sourceSerializer = new DefaultSourceSerializer(this);

		UseThisRequestResponseSerializer = requestResponseSerializer;

		_sourceSerializer = sourceSerializerFactory?.Invoke(sourceSerializer, this) ?? sourceSerializer;
		_propertyMappingProvider = propertyMappingProvider ?? sourceSerializer as IPropertyMappingProvider ?? new DefaultPropertyMappingProvider();
		_defaultFieldNameInferrer = _sourceSerializer.TryGetJsonSerializerOptions(out var options)
			? p => options.PropertyNamingPolicy?.ConvertName(p) ?? p
			: p => p.ToCamelCase();
		_defaultIndices = new FluentDictionary<Type, string>();
		_defaultRelationNames = new FluentDictionary<Type, string>();
		_inferrer = new Inferrer(this);

		UserAgent(ElasticsearchClientSettings.DefaultUserAgent);
	}

	public Serializer SourceSerializer => _sourceSerializer;

	bool IElasticsearchClientSettings.DefaultDisableIdInference => _defaultDisableAllInference;
	Func<string, string> IElasticsearchClientSettings.DefaultFieldNameInferrer => _defaultFieldNameInferrer;
	string IElasticsearchClientSettings.DefaultIndex => _defaultIndex;
	FluentDictionary<Type, string> IElasticsearchClientSettings.DefaultIndices => _defaultIndices;
	HashSet<Type> IElasticsearchClientSettings.DisableIdInference => _disableIdInference;
	FluentDictionary<Type, string> IElasticsearchClientSettings.DefaultRelationNames => _defaultRelationNames;
	FluentDictionary<Type, string> IElasticsearchClientSettings.IdProperties => _idProperties;

	Inferrer IElasticsearchClientSettings.Inferrer => _inferrer;

	IPropertyMappingProvider IElasticsearchClientSettings.PropertyMappingProvider => _propertyMappingProvider;
	FluentDictionary<MemberInfo, PropertyMapping> IElasticsearchClientSettings.PropertyMappings => _propertyMappings;

	FluentDictionary<Type, string> IElasticsearchClientSettings.RouteProperties => _routeProperties;
	Serializer IElasticsearchClientSettings.SourceSerializer => _sourceSerializer;

	ExperimentalSettings IElasticsearchClientSettings.Experimental => _experimentalSettings;

	bool IElasticsearchClientSettings.ExperimentalEnableSerializeNullInferredValues => _experimentalEnableSerializeNullInferredValues;

	/// <summary>
	///     The default index to use for a request when no index has been explicitly specified
	///     and no default indices are specified for the given CLR type specified for the request.
	/// </summary>
	public TConnectionSettings DefaultIndex(string defaultIndex) =>
		Assign(defaultIndex, (a, v) => a._defaultIndex = v);

	/// <summary>
	///     Specifies how field names are inferred from CLR property names.
	///     <para></para>
	///     By default, Elastic.Clients.Elasticsearch camel cases property names.
	/// </summary>
	/// <example>
	///     CLR property EmailAddress will be inferred as "emailAddress" Elasticsearch document field name
	/// </example>
	public TConnectionSettings DefaultFieldNameInferrer(Func<string, string> fieldNameInferrer)
	{
		if (_sourceSerializer.TryGetJsonSerializerOptions(out var options, SerializationFormatting.None))
			options.PropertyNamingPolicy = new CustomizedNamingPolicy(fieldNameInferrer);

		if (_sourceSerializer.TryGetJsonSerializerOptions(out var indentedOptions, SerializationFormatting.Indented))
			indentedOptions.PropertyNamingPolicy = new CustomizedNamingPolicy(fieldNameInferrer);

		return Assign(fieldNameInferrer, (a, v) => a._defaultFieldNameInferrer = v);
	}

	public TConnectionSettings ExperimentalEnableSerializeNullInferredValues(bool enabled = true) =>
		Assign(enabled, (a, v) => a._experimentalEnableSerializeNullInferredValues = v);

	public TConnectionSettings Experimental(ExperimentalSettings settings) =>
		Assign(settings, (a, v) => a._experimentalSettings = v);

	/// <summary>
	///     Disables automatic Id inference for given CLR types.
	///     <para></para>
	///     Elastic.Clients.Elasticsearch by default will use the value of a property named Id on a CLR type as the _id to send to Elasticsearch. Adding
	///     a type
	///     will disable this behaviour for that CLR type. If Id inference should be disabled for all CLR types, use
	///     <see cref="DefaultDisableIdInference" />
	/// </summary>
	public TConnectionSettings DefaultDisableIdInference(bool disable = true) =>
		Assign(disable, (a, v) => a._defaultDisableAllInference = v);

	private void MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
	{
		objectPath.ThrowIfNull(nameof(objectPath));

		var memberInfo = new MemberInfoResolver(objectPath);
		var fieldName = memberInfo.Members.Single().Name;

		if (_idProperties.TryGetValue(typeof(TDocument), out var idPropertyFieldName))
		{
			if (idPropertyFieldName.Equals(fieldName))
				return;

			throw new ArgumentException(
				$"Cannot map '{fieldName}' as the id property for type '{typeof(TDocument).Name}': it already has '{_idProperties[typeof(TDocument)]}' mapped.");
		}

		_idProperties.Add(typeof(TDocument), fieldName);
	}

	/// <inheritdoc cref="IElasticsearchClientSettings.RouteProperties" />
	private void MapRoutePropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
	{
		objectPath.ThrowIfNull(nameof(objectPath));

		var memberInfo = new MemberInfoResolver(objectPath);
		var fieldName = memberInfo.Members.Single().Name;

		if (_routeProperties.TryGetValue(typeof(TDocument), out var routePropertyFieldName))
		{
			if (routePropertyFieldName.Equals(fieldName))
				return;

			throw new ArgumentException(
				$"Cannot map '{fieldName}' as the route property for type '{typeof(TDocument).Name}': it already has '{_routeProperties[typeof(TDocument)]}' mapped.");
		}

		_routeProperties.Add(typeof(TDocument), fieldName);
	}

	/// <summary>
	///     Specify how the mapping is inferred for a given CLR type.
	///     The mapping can infer the index, id and relation name for a given CLR type, as well as control
	///     serialization behaviour for CLR properties.
	/// </summary>
	public TConnectionSettings DefaultMappingFor<TDocument>(
		Action<ClrTypeMappingDescriptor<TDocument>> selector)
		where TDocument : class
	{
		var inferMapping = new ClrTypeMappingDescriptor<TDocument>();
		selector(inferMapping);

		if (!inferMapping._indexName.IsNullOrEmpty())
			_defaultIndices[inferMapping._clrType] = inferMapping._indexName;

		if (!inferMapping._relationName.IsNullOrEmpty())
			_defaultRelationNames[inferMapping._clrType] = inferMapping._relationName;

		if (!string.IsNullOrWhiteSpace(inferMapping._idProperty))
			_idProperties[inferMapping._clrType] = inferMapping._idProperty;

		if (inferMapping._idPropertyExpression != null)
			MapIdPropertyFor(inferMapping._idPropertyExpression);

		if (inferMapping._routingPropertyExpression != null)
			MapRoutePropertyFor(inferMapping._routingPropertyExpression);

		if (inferMapping._disableIdInference)
			_disableIdInference.Add(inferMapping._clrType);
		else
			_disableIdInference.Remove(inferMapping._clrType);

		return (TConnectionSettings)this;
	}

	/// <summary>
	///     Specify how the mapping is inferred for a given CLR type.
	///     The mapping can infer the index and relation name for a given CLR type.
	/// </summary>
	public TConnectionSettings DefaultMappingFor(Type documentType,
		Action<ClrTypeMappingDescriptor> selector)
	{
		var inferMapping = new ClrTypeMappingDescriptor(documentType);

		selector(inferMapping);
		if (!inferMapping._indexName.IsNullOrEmpty())
			_defaultIndices[inferMapping._clrType] = inferMapping._indexName;

		if (!inferMapping._relationName.IsNullOrEmpty())
			_defaultRelationNames[inferMapping._clrType] = inferMapping._relationName;

		if (!string.IsNullOrWhiteSpace(inferMapping._idProperty))
			_idProperties[inferMapping._clrType] = inferMapping._idProperty;

		return (TConnectionSettings)this;
	}

	/// <summary>
	///     Specify how the mapping is inferred for a given CLR type.
	///     The mapping can infer the index and relation name for a given CLR type.
	/// </summary>
	public TConnectionSettings DefaultMappingFor(IEnumerable<ClrTypeMapping> typeMappings)
	{
		if (typeMappings == null)
			return (TConnectionSettings)this;

		foreach (var inferMapping in typeMappings)
		{
			if (!inferMapping.IndexName.IsNullOrEmpty())
				_defaultIndices[inferMapping.ClrType] = inferMapping.IndexName;

			if (!inferMapping.RelationName.IsNullOrEmpty())
				_defaultRelationNames[inferMapping.ClrType] = inferMapping.RelationName;
		}

		return (TConnectionSettings)this;
	}
}

/// <inheritdoc cref="TransportClientConfigurationValues" />
public class ConnectionConfiguration : ConnectionConfigurationBase<ConnectionConfiguration>
{
	/// <summary>
	///     The default user agent for Elasticsearch.Net
	/// </summary>
	public static readonly UserAgent DefaultUserAgent =
		Transport.UserAgent.Create("elasticsearch-net", typeof(ITransportConfiguration));

	public ConnectionConfiguration(Uri uri = null)
		: this(new SingleNodePool(uri ?? new Uri("http://localhost:9200")))
	{
	}

	/// <summary>
	///     Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId" />,
	///     <para><see cref="CloudNodePool" /> documentation for more information on how to obtain your Cloud Id</para>
	/// </summary>
	public ConnectionConfiguration(string cloudId, AuthorizationHeader credentials) : this(
		new CloudNodePool(cloudId, credentials))
	{
	}

	public ConnectionConfiguration(NodePool nodePool) : this(nodePool, null, null)
	{
	}

	public ConnectionConfiguration(NodePool nodePool, IRequestInvoker requestInvoker) : this(nodePool,
		requestInvoker, null)
	{
	}

	public ConnectionConfiguration(NodePool nodePool, Serializer serializer) : this(
		nodePool, null, serializer)
	{
	}

	public ConnectionConfiguration(NodePool nodePool, IRequestInvoker requestInvoker,
		Serializer serializer)
		: base(nodePool, requestInvoker, serializer)
	{
	}
}

/// <inheritdoc cref="TransportClientConfigurationValues" />
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class ConnectionConfigurationBase<TConnectionConfiguration> :
	TransportConfigurationDescriptorBase<TConnectionConfiguration>,
	TransportClientConfigurationValues
	where TConnectionConfiguration : ConnectionConfigurationBase<TConnectionConfiguration>, TransportClientConfigurationValues
{
	private bool _includeServerStackTraceOnError;

	protected ConnectionConfigurationBase(NodePool nodePool, IRequestInvoker requestInvoker,
		Serializer? serializer,
		ProductRegistration registration = null)
		: base(nodePool, requestInvoker, serializer, registration ?? new ElasticsearchProductRegistration(typeof(ElasticsearchClient)))
	{
		UserAgent(ConnectionConfiguration.DefaultUserAgent);
		ResponseBuilder(new EsqlResponseBuilder());
	}

	bool TransportClientConfigurationValues.IncludeServerStackTraceOnError => _includeServerStackTraceOnError;

	public override TConnectionConfiguration EnableDebugMode(Action<ApiCallDetails> onRequestCompleted = null) =>
		base.EnableDebugMode(onRequestCompleted)
			.PrettyJson()
			.IncludeServerStackTraceOnError();

	/// <summary>
	///     Forces all requests to have ?pretty=true querystring parameter appended,
	///     causing Elasticsearch to return formatted JSON.
	///     Defaults to <c>false</c>
	/// </summary>
	public override TConnectionConfiguration PrettyJson(bool b = true) =>
		base.PrettyJson(b).UpdateGlobalQueryString("pretty", "true", b);

	/// <summary>
	///     Forces all requests to have ?error_trace=true querystring parameter appended,
	///     causing Elasticsearch to return stack traces as part of serialized exceptions
	///     Defaults to <c>false</c>
	/// </summary>
	public TConnectionConfiguration IncludeServerStackTraceOnError(bool b = true) => Assign(b, (a, v) =>
	{
		a._includeServerStackTraceOnError = true;
		const string key = "error_trace";
		UpdateGlobalQueryString(key, "true", v);
	});
}

public interface TransportClientConfigurationValues : ITransportConfiguration
{
	/// <summary>
	///     Forces all requests to have ?error_trace=true querystring parameter appended,
	///     causing Elasticsearch to return stack traces as part of serialized exceptions
	///     Defaults to <c>false</c>
	/// </summary>
	bool IncludeServerStackTraceOnError { get; }
}
