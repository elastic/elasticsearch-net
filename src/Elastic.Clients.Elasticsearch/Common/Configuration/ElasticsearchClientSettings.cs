using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elastic.Transport;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch
{
	/// <inheritdoc cref="IElasticsearchClientSettings" />
	public class ElasticsearchClientSettings : ElasticsearchClientSettingsBase<ElasticsearchClientSettings>
	{
		/// <summary>
		///     A delegate used to construct a serializer to serialize CLR types representing documents and other types related to
		///     documents.
		///     By default, the internal serializer will be used to serializer all types.
		/// </summary>
		public delegate SerializerBase SourceSerializerFactory(SerializerBase builtIn,
			IElasticsearchClientSettings values);

		/// <summary> The default user agent for Elastic.Clients.Elasticsearch </summary>
		public static readonly UserAgent DefaultUserAgent =
			Elastic.Transport.UserAgent.Create("elasticsearch-net", typeof(IElasticsearchClientSettings));

		/// <summary>
		///     Creates a new instance of connection settings, if <paramref name="uri" /> is not specified will default to
		///     connecting to http://localhost:9200
		/// </summary>
		/// <param name="uri"></param>
		public ElasticsearchClientSettings(Uri? uri = null) : this(
			new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")))
		{
		}

		/// <summary>
		///     Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId" />,
		///     <para><see cref="CloudConnectionPool" /> documentation for more information on how to obtain your Cloud Id</para>
		/// </summary>
		public ElasticsearchClientSettings(string cloudId, IAuthenticationHeader credentials) : this(
			new CloudConnectionPool(cloudId, credentials))
		{
		}

		/// <summary>
		///     Instantiate connection settings using a <see cref="SingleNodeConnectionPool" /> using the provided
		///     <see cref="InMemoryConnection" /> that never uses any IO.
		/// </summary>
		public ElasticsearchClientSettings(InMemoryConnection connection)
			: this(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection)
		{
		}

		public ElasticsearchClientSettings(IConnectionPool connectionPool) : this(connectionPool, null, null) { }

		public ElasticsearchClientSettings(IConnectionPool connectionPool, SourceSerializerFactory sourceSerializer)
			: this(connectionPool, null, sourceSerializer)
		{
		}

		public ElasticsearchClientSettings(IConnectionPool connectionPool, IConnection connection) : this(
			connectionPool,
			connection, null)
		{
		}

		public ElasticsearchClientSettings(
			IConnectionPool connectionPool,
			IConnection connection,
			SourceSerializerFactory sourceSerializer) : base(connectionPool, connection, sourceSerializer)
		{
		}
	}

	/// <inheritdoc cref="IElasticsearchClientSettings" />
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class
		ElasticsearchClientSettingsBase<TConnectionSettings> : ConnectionConfigurationBase<TConnectionSettings>,
			IElasticsearchClientSettings
		where TConnectionSettings : ElasticsearchClientSettingsBase<TConnectionSettings>, IElasticsearchClientSettings
	{
		private readonly FluentDictionary<Type, string> _defaultIndices;
		private readonly FluentDictionary<Type, string> _defaultRelationNames;
		private readonly HashSet<Type> _disableIdInference = new();
		private readonly FluentDictionary<Type, string> _idProperties = new();

		private readonly Inferrer _inferrer;

		private readonly IPropertyMappingProvider _propertyMappingProvider;
		private readonly FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new();
		private readonly FluentDictionary<Type, string> _routeProperties = new();
		private readonly SerializerBase _sourceSerializer;
		private bool _experimentalEnableSerializeNullInferredValues;
		private ExperimentalSettings _experimentalSettings = new ();

		private bool _defaultDisableAllInference;
		private Func<string, string> _defaultFieldNameInferrer;
		private string _defaultIndex;

		protected ElasticsearchClientSettingsBase(
			IConnectionPool connectionPool,
			IConnection connection,
			ElasticsearchClientSettings.SourceSerializerFactory? sourceSerializerFactory)
			: base(connectionPool, connection, null, ElasticsearchClientProductRegistration.DefaultForElasticClientsElasticsearch)
		{
			var defaultSerializer = new DefaultRequestResponseSerializer(this);
			var sourceSerializer = sourceSerializerFactory?.Invoke(defaultSerializer, this) ?? new DefaultSourceSerializer(this);
			//var serializerAsMappingProvider = sourceSerializer as IPropertyMappingProvider;

			_propertyMappingProvider = /*propertyMappingProvider ?? serializerAsMappingProvider ??*/ new PropertyMappingProvider();

			//We wrap these in an internal proxy to facilitate serialization diagnostics
			_sourceSerializer = new DiagnosticsSerializerProxy(sourceSerializer, "source");

			UseThisRequestResponseSerializer = new DiagnosticsSerializerProxy(defaultSerializer);
			
			_defaultFieldNameInferrer = p => p.ToCamelCase();
			_defaultIndices = new FluentDictionary<Type, string>();
			_defaultRelationNames = new FluentDictionary<Type, string>();
			_inferrer = new Inferrer(this);

			UserAgent(ElasticsearchClientSettings.DefaultUserAgent);
		}

		public SerializerBase SourceSerializer { get; }

		bool IElasticsearchClientSettings.DefaultDisableIdInference => _defaultDisableAllInference;
		Func<string, string> IElasticsearchClientSettings.DefaultFieldNameInferrer => _defaultFieldNameInferrer;
		string IElasticsearchClientSettings.DefaultIndex => _defaultIndex;
		FluentDictionary<Type, string> IElasticsearchClientSettings.DefaultIndices => _defaultIndices;
		HashSet<Type> IElasticsearchClientSettings.DisableIdInference => _disableIdInference;
		FluentDictionary<Type, string> IElasticsearchClientSettings.DefaultRelationNames => _defaultRelationNames;
		FluentDictionary<Type, string> IElasticsearchClientSettings.IdProperties => _idProperties;

		Inferrer IElasticsearchClientSettings.Inferrer => _inferrer;

		IPropertyMappingProvider IElasticsearchClientSettings.PropertyMappingProvider => _propertyMappingProvider;
		FluentDictionary<MemberInfo, IPropertyMapping> IElasticsearchClientSettings.PropertyMappings => _propertyMappings;

		FluentDictionary<Type, string> IElasticsearchClientSettings.RouteProperties => _routeProperties;
		SerializerBase IElasticsearchClientSettings.SourceSerializer => _sourceSerializer;

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
		public TConnectionSettings DefaultFieldNameInferrer(Func<string, string> fieldNameInferrer) =>
			Assign(fieldNameInferrer, (a, v) => a._defaultFieldNameInferrer = v);

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

		private void ApplyPropertyMappings<TDocument>(IList<IClrPropertyMapping<TDocument>> mappings)
			where TDocument : class
		{
			foreach (var mapping in mappings)
			{
				var e = mapping.Property;
				var memberInfoResolver = new MemberInfoResolver(e);
				if (memberInfoResolver.Members.Count > 1)
					throw new ArgumentException($"{nameof(ApplyPropertyMappings)} can only map direct properties");

				if (memberInfoResolver.Members.Count == 0)
					throw new ArgumentException($"Expression {e} does contain any member access");

				var memberInfo = memberInfoResolver.Members[0];

				if (_propertyMappings.TryGetValue(memberInfo, out var propertyMapping))
				{
					var newName = mapping.NewName;
					var mappedAs = propertyMapping.Name;
					var typeName = typeof(TDocument).Name;
					if (mappedAs.IsNullOrEmpty() && newName.IsNullOrEmpty())
						throw new ArgumentException($"Property mapping '{e}' on type is already ignored");
					if (mappedAs.IsNullOrEmpty())
						throw new ArgumentException(
							$"Property mapping '{e}' on type {typeName} can not be mapped to '{newName}' it already has an ignore mapping");
					if (newName.IsNullOrEmpty())
						throw new ArgumentException(
							$"Property mapping '{e}' on type {typeName} can not be ignored it already has a mapping to '{mappedAs}'");

					throw new ArgumentException(
						$"Property mapping '{e}' on type {typeName} can not be mapped to '{newName}' already mapped as '{mappedAs}'");
				}
				_propertyMappings[memberInfo] = mapping.ToPropertyMapping();
			}
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

			if (inferMapping._properties != null)
				ApplyPropertyMappings(inferMapping._properties);

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

	/// <inheritdoc cref="IConnectionConfigurationValues" />
	public class ConnectionConfiguration : ConnectionConfigurationBase<ConnectionConfiguration>
	{
		/// <summary>
		///     The default user agent for Elasticsearch.Net
		/// </summary>
		public static readonly UserAgent DefaultUserAgent =
			Elastic.Transport.UserAgent.Create("elasticsearch-net", typeof(ITransportConfiguration));

		public ConnectionConfiguration(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")))
		{
		}

		public ConnectionConfiguration(InMemoryConnection connection)
			: this(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection)
		{
		}

		/// <summary>
		///     Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId" />,
		///     <para><see cref="CloudConnectionPool" /> documentation for more information on how to obtain your Cloud Id</para>
		/// </summary>
		public ConnectionConfiguration(string cloudId, IAuthenticationHeader credentials) : this(
			new CloudConnectionPool(cloudId, credentials))
		{
		}

		public ConnectionConfiguration(IConnectionPool connectionPool) : this(connectionPool, null, null) { }

		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection) : this(connectionPool,
			connection, null)
		{
		}

		public ConnectionConfiguration(IConnectionPool connectionPool, SerializerBase serializer) : this(
			connectionPool, null, serializer)
		{
		}

		public ConnectionConfiguration(IConnectionPool connectionPool, IConnection connection,
			SerializerBase serializer)
			: base(connectionPool, connection, serializer)
		{
		}
	}

	/// <inheritdoc cref="IConnectionConfigurationValues" />
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class
		ConnectionConfigurationBase<TConnectionConfiguration> : TransportConfigurationBase<TConnectionConfiguration>,
			IConnectionConfigurationValues
		where TConnectionConfiguration : ConnectionConfigurationBase<TConnectionConfiguration>,
		IConnectionConfigurationValues
	{
		private bool _includeServerStackTraceOnError;

		protected ConnectionConfigurationBase(IConnectionPool connectionPool, IConnection connection,
			SerializerBase? serializer,
			IProductRegistration registration = null)
			: base(connectionPool, connection, serializer, registration ?? new ElasticsearchProductRegistration(typeof(IElasticClient))) =>
				UserAgent(ConnectionConfiguration.DefaultUserAgent);

		bool IConnectionConfigurationValues.IncludeServerStackTraceOnError => _includeServerStackTraceOnError;

		public override TConnectionConfiguration EnableDebugMode(Action<IApiCallDetails> onRequestCompleted = null) =>
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

	public interface IConnectionConfigurationValues : ITransportConfiguration
	{
		/// <summary>
		///     Forces all requests to have ?error_trace=true querystring parameter appended,
		///     causing Elasticsearch to return stack traces as part of serialized exceptions
		///     Defaults to <c>false</c>
		/// </summary>
		bool IncludeServerStackTraceOnError { get; }
	}
}
