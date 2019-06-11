using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	/// <inheritdoc cref="IConnectionSettingsValues" />
	public class ConnectionSettings : ConnectionSettingsBase<ConnectionSettings>
	{
		/// <summary>
		/// A delegate used to construct a serializer to serialize CLR types representing documents and other types related to
		/// documents.
		/// By default, the internal serializer will be used to serializer all types.
		/// </summary>
		public delegate IElasticsearchSerializer SourceSerializerFactory(IElasticsearchSerializer builtIn, IConnectionSettingsValues values);

		public ConnectionSettings(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200"))) { }

		/// <summary>
		/// Instantiate connection settings using a <see cref="SingleNodeConnectionPool" /> using the provided
		/// <see cref="InMemoryConnection" /> that never uses any IO.
		/// </summary>
		public ConnectionSettings(InMemoryConnection connection)
			: this(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection) { }

		public ConnectionSettings(IConnectionPool connectionPool) : this(connectionPool, null, null) { }

		public ConnectionSettings(IConnectionPool connectionPool, SourceSerializerFactory sourceSerializer)
			: this(connectionPool, null, sourceSerializer) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection) : this(connectionPool, connection, null) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection, SourceSerializerFactory sourceSerializer)
			: this(connectionPool, connection, sourceSerializer, null) { }

		public ConnectionSettings(
			IConnectionPool connectionPool,
			IConnection connection,
			SourceSerializerFactory sourceSerializer,
			IPropertyMappingProvider propertyMappingProvider
		)
			: base(connectionPool, connection, sourceSerializer, propertyMappingProvider) { }
	}

	/// <inheritdoc cref="IConnectionSettingsValues" />
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionSettingsBase<TConnectionSettings> : ConnectionConfiguration<TConnectionSettings>, IConnectionSettingsValues
		where TConnectionSettings : ConnectionSettingsBase<TConnectionSettings>, IConnectionSettingsValues
	{
		private readonly FluentDictionary<Type, string> _defaultIndices;

		private readonly FluentDictionary<Type, string> _defaultRelationNames;
		private readonly HashSet<Type> _disableIdInference = new HashSet<Type>();

		private readonly FluentDictionary<Type, string> _idProperties = new FluentDictionary<Type, string>();

		private readonly Inferrer _inferrer;

		private readonly IPropertyMappingProvider _propertyMappingProvider;

		private readonly FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new FluentDictionary<MemberInfo, IPropertyMapping>();

		private readonly FluentDictionary<Type, string> _routeProperties = new FluentDictionary<Type, string>();

		private readonly IElasticsearchSerializer _sourceSerializer;
		private bool _defaultDisableAllInference;

		private Func<string, string> _defaultFieldNameInferrer;
		private string _defaultIndex;

		protected ConnectionSettingsBase(
			IConnectionPool connectionPool,
			IConnection connection,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory,
			IPropertyMappingProvider propertyMappingProvider
		)
			: base(connectionPool, connection, null)
		{
			var formatterResolver = new NestFormatterResolver(this);
			var defaultSerializer = new InternalSerializer(formatterResolver);
			_sourceSerializer = sourceSerializerFactory?.Invoke(defaultSerializer, this) ?? defaultSerializer;
			UseThisRequestResponseSerializer = defaultSerializer;
			var serializerAsMappingProvider = _sourceSerializer as IPropertyMappingProvider;
			_propertyMappingProvider = propertyMappingProvider ?? serializerAsMappingProvider ?? new PropertyMappingProvider();

			_defaultFieldNameInferrer = p => p.ToCamelCase();
			_defaultIndices = new FluentDictionary<Type, string>();
			_defaultRelationNames = new FluentDictionary<Type, string>();
			_inferrer = new Inferrer(this);
		}
		bool IConnectionSettingsValues.DefaultDisableIdInference => _defaultDisableAllInference;

		Func<string, string> IConnectionSettingsValues.DefaultFieldNameInferrer => _defaultFieldNameInferrer;
		string IConnectionSettingsValues.DefaultIndex => _defaultIndex;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultIndices => _defaultIndices;
		HashSet<Type> IConnectionSettingsValues.DisableIdInference => _disableIdInference;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultRelationNames => _defaultRelationNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.IdProperties => _idProperties;
		Inferrer IConnectionSettingsValues.Inferrer => _inferrer;
		IPropertyMappingProvider IConnectionSettingsValues.PropertyMappingProvider => _propertyMappingProvider;
		FluentDictionary<MemberInfo, IPropertyMapping> IConnectionSettingsValues.PropertyMappings => _propertyMappings;
		FluentDictionary<Type, string> IConnectionSettingsValues.RouteProperties => _routeProperties;
		IElasticsearchSerializer IConnectionSettingsValues.SourceSerializer => _sourceSerializer;

		/// <inheritdoc cref="IConnectionSettingsValues.DefaultIndex"/>
		public TConnectionSettings DefaultIndex(string defaultIndex) => Assign(defaultIndex, (a, v) => a._defaultIndex = v);

		/// <inheritdoc cref="IConnectionSettingsValues.DefaultFieldNameInferrer"/>
		public TConnectionSettings DefaultFieldNameInferrer(Func<string, string> fieldNameInferrer) =>
			Assign(fieldNameInferrer, (a, v) => a._defaultFieldNameInferrer = v);

		/// <inheritdoc cref="IConnectionSettingsValues.DefaultDisableIdInference" />
		public TConnectionSettings DefaultDisableIdInference(bool disable = true) => Assign(disable, (a, v) => a._defaultDisableAllInference = v);

		/// <inheritdoc cref="IConnectionSettingsValues.IdProperties"/>
		private void MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull(nameof(objectPath));

			var memberInfo = new MemberInfoResolver(objectPath);
			var fieldName = memberInfo.Members.Single().Name;

			if (_idProperties.TryGetValue(typeof(TDocument), out var idPropertyFieldName))
			{
				if (idPropertyFieldName.Equals(fieldName)) return;

				throw new ArgumentException(
					$"Cannot map '{fieldName}' as the id property for type '{typeof(TDocument).Name}': it already has '{_idProperties[typeof(TDocument)]}' mapped.");
			}

			_idProperties.Add(typeof(TDocument), fieldName);
		}

		/// <inheritdoc cref="IConnectionSettingsValues.RouteProperties"/>
		private void MapRoutePropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull(nameof(objectPath));

			var memberInfo = new MemberInfoResolver(objectPath);
			var fieldName = memberInfo.Members.Single().Name;

			if (_routeProperties.TryGetValue(typeof(TDocument), out var routePropertyFieldName))
			{
				if (routePropertyFieldName.Equals(fieldName)) return;

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

				if (memberInfoResolver.Members.Count < 1)
					throw new ArgumentException($"Expression {e} does contain any member access");

				var memberInfo = memberInfoResolver.Members.Last();

				// memberInfo will be the declaringType, which may not be TDocument in the case of an inherited property.
				// Get the correct memberinfo
				if (typeof(TDocument) != memberInfo.DeclaringType)
				{
					var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
					memberInfo = typeof(TDocument).GetMember(memberInfo.Name, bindingFlags).First();
				}

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
		/// Specify how the mapping is inferred for a given CLR type.
		/// The mapping can infer the index, type, id and relation name for a given CLR type, as well as control
		/// serialization behaviour for CLR properties.
		/// </summary>
		public TConnectionSettings DefaultMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>> selector)
			where TDocument : class
		{
			var inferMapping = selector(new ClrTypeMappingDescriptor<TDocument>());
			if (!inferMapping.IndexName.IsNullOrEmpty())
				_defaultIndices[inferMapping.ClrType] = inferMapping.IndexName;

			if (!inferMapping.RelationName.IsNullOrEmpty())
				_defaultRelationNames[inferMapping.ClrType] = inferMapping.RelationName;

			if (!string.IsNullOrWhiteSpace(inferMapping.IdPropertyName))
				_idProperties[inferMapping.ClrType] = inferMapping.IdPropertyName;

			if (inferMapping.IdProperty != null)
				MapIdPropertyFor(inferMapping.IdProperty);

			if (inferMapping.RoutingProperty != null)
				MapRoutePropertyFor(inferMapping.RoutingProperty);

			if (inferMapping.Properties != null)
				ApplyPropertyMappings(inferMapping.Properties);

			if (inferMapping.DisableIdInference) _disableIdInference.Add(inferMapping.ClrType);
			else _disableIdInference.Remove(inferMapping.ClrType);

			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify how the mapping is inferred for a given CLR type.
		/// The mapping can infer the index, type and relation name for a given CLR type.
		/// </summary>
		public TConnectionSettings DefaultMappingFor(Type documentType, Func<ClrTypeMappingDescriptor, IClrTypeMapping> selector)
		{
			var inferMapping = selector(new ClrTypeMappingDescriptor(documentType));
			if (!inferMapping.IndexName.IsNullOrEmpty())
				_defaultIndices[inferMapping.ClrType] = inferMapping.IndexName;

			if (!inferMapping.RelationName.IsNullOrEmpty())
				_defaultRelationNames[inferMapping.ClrType] = inferMapping.RelationName;

			if (!string.IsNullOrWhiteSpace(inferMapping.IdPropertyName))
				_idProperties[inferMapping.ClrType] = inferMapping.IdPropertyName;

			return (TConnectionSettings)this;
		}

		/// <inheritdoc cref="DefaultMappingFor(Type, Func{ClrTypeMappingDescriptor,IClrTypeMapping})" />
		public TConnectionSettings DefaultMappingFor(IEnumerable<IClrTypeMapping> typeMappings)
		{
			if (typeMappings == null) return (TConnectionSettings)this;

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
}
