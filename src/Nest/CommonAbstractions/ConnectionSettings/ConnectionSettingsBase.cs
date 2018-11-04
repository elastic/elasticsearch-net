using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Provides the connection settings for NEST's <see cref="ElasticClient" />
	/// </summary>
	public class ConnectionSettings : ConnectionSettingsBase<ConnectionSettings>
	{
		public ConnectionSettings(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200"))) { }

		/// <summary>
		/// Instantiate connection settings using a <see cref="SingleNodeConnectionPool" /> using the provided
		/// <see cref="InMemoryConnection" /> that never uses any IO.
		/// </summary>
		public ConnectionSettings(InMemoryConnection connection)
			: this(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection) { }

		public ConnectionSettings(IConnectionPool connectionPool)
			: this(connectionPool, null, new SerializerFactory()) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection)
			: this(connectionPool, connection, new SerializerFactory()) { }

		public ConnectionSettings(IConnectionPool connectionPool, Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory)
#pragma warning disable CS0618 // Type or member is obsolete
			: this(connectionPool, null, serializerFactory) { }
#pragma warning restore CS0618 // Type or member is obsolete

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection, ISerializerFactory serializerFactory)
			: base(connectionPool, connection, serializerFactory, s => serializerFactory?.Create(s)) { }

		[Obsolete("Please use the constructor taking ISerializerFactory instead of a Func")]
		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection,
			Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory
		)
			: base(connectionPool, connection, null, s => serializerFactory?.Invoke(s)) { }
	}

	/// <summary>
	/// Provides the connection settings for NEST's <see cref="ElasticClient" />
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionSettingsBase<TConnectionSettings> : ConnectionConfiguration<TConnectionSettings>, IConnectionSettingsValues
		where TConnectionSettings : ConnectionSettingsBase<TConnectionSettings>, IConnectionSettingsValues
	{
		private readonly FluentDictionary<Type, string> _defaultIndices;
		private readonly FluentDictionary<Type, string> _defaultTypeNames;
		private readonly FluentDictionary<Type, string> _idProperties = new FluentDictionary<Type, string>();
		private readonly Inferrer _inferrer;
		private readonly FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new FluentDictionary<MemberInfo, IPropertyMapping>();
		private readonly ISerializerFactory _serializerFactory;
		private Func<string, string> _defaultFieldNameInferrer;
		private string _defaultIndex;
		private Func<Type, string> _defaultTypeNameInferrer;

		protected ConnectionSettingsBase(
			IConnectionPool connectionPool,
			IConnection connection,
			ISerializerFactory serializerFactory,
			Func<TConnectionSettings, IElasticsearchSerializer> serializerFactoryFunc
		)
			: base(connectionPool, connection, serializerFactoryFunc)
		{
			_defaultTypeNameInferrer = t => t.Name.ToLowerInvariant();
			_defaultFieldNameInferrer = p => p.ToCamelCase();
			_defaultIndices = new FluentDictionary<Type, string>();
			_defaultTypeNames = new FluentDictionary<Type, string>();
			_serializerFactory = serializerFactory ?? new SerializerFactory();

			_inferrer = new Inferrer(this);
		}

		protected ConnectionSettingsBase(
			IConnectionPool connectionPool,
			IConnection connection,
			Func<TConnectionSettings, IElasticsearchSerializer> serializerFactoryFunc
		)
			: this(connectionPool, connection, null, serializerFactoryFunc) { }

		Func<string, string> IConnectionSettingsValues.DefaultFieldNameInferrer => _defaultFieldNameInferrer;
		string IConnectionSettingsValues.DefaultIndex => _defaultIndex;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultIndices => _defaultIndices;
		Func<Type, string> IConnectionSettingsValues.DefaultTypeNameInferrer => _defaultTypeNameInferrer;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultTypeNames => _defaultTypeNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.IdProperties => _idProperties;
		Inferrer IConnectionSettingsValues.Inferrer => _inferrer;
		FluentDictionary<MemberInfo, IPropertyMapping> IConnectionSettingsValues.PropertyMappings => _propertyMappings;
		ISerializerFactory IConnectionSettingsValues.SerializerFactory => _serializerFactory;

		IElasticsearchSerializer IConnectionSettingsValues.StatefulSerializer(JsonConverter converter) =>
			_serializerFactory.CreateStateful(this, converter);

		/// <summary>
		/// The default serializer for requests and responses
		/// </summary>
		/// <returns></returns>
		protected override IElasticsearchSerializer DefaultSerializer(TConnectionSettings settings) => new JsonNetSerializer(settings);

		/// <summary>
		/// Pluralize type names when inferring from POCO type names.
		/// <para></para>
		/// This calls <see cref="DefaultTypeNameInferrer" /> with an implementation that will pluralize type names.
		/// This used to be the default prior to Nest 0.90
		/// </summary>
		public TConnectionSettings PluralizeTypeNames()
		{
			_defaultTypeNameInferrer = LowerCaseAndPluralizeTypeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// The default index to use when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">
		/// When null/empty/not set might throw
		/// <see cref="NullReferenceException" /> later on when not specifying index explicitly while indexing.
		/// </param>
		public TConnectionSettings DefaultIndex(string defaultIndex)
		{
			_defaultIndex = defaultIndex;
			return (TConnectionSettings)this;
		}

		private string LowerCaseAndPluralizeTypeNameInferrer(Type type)
		{
			type.ThrowIfNull(nameof(type));
			return type.Name.MakePlural().ToLowerInvariant();
		}

		/// <summary>
		/// Specify how field names are inferred from POCO property names.
		/// <para></para>
		/// By default, NEST camel cases property names
		/// e.g. EmailAddress POCO property => "emailAddress" Elasticsearch document field name
		/// </summary>
		public TConnectionSettings DefaultFieldNameInferrer(Func<string, string> fieldNameInferrer)
		{
			_defaultFieldNameInferrer = fieldNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify how type names are inferred from POCO types.
		/// By default, type names are inferred by calling <see cref="string.ToLowerInvariant" />
		/// on the type's name.
		/// </summary>
		public TConnectionSettings DefaultTypeNameInferrer(Func<Type, string> typeNameInferrer)
		{
			typeNameInferrer.ThrowIfNull(nameof(typeNameInferrer));
			_defaultTypeNameInferrer = typeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify the default index names for a given POCO type.
		/// Takes precedence over the global <see cref="DefaultIndex" />
		/// </summary>
		/// <remarks>Removed in 6.x.</remarks>
		public TConnectionSettings MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull(nameof(mappingSelector));
			mappingSelector(_defaultIndices);
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify the default type names for a given POCO type.
		/// Takes precedence over the global <see cref="DefaultTypeNameInferrer" />
		/// </summary>
		/// <remarks>Removed in 6.x.</remarks>
		public TConnectionSettings MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull(nameof(mappingSelector));
			mappingSelector(_defaultTypeNames);
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify which property on a given POCO should be used to infer the id of the document when
		/// indexed in Elasticsearch.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="objectPath">The object path.</param>
		/// <returns></returns>
		/// <remarks>Removed in 6.x.</remarks>
		public TConnectionSettings MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull(nameof(objectPath));

			var memberInfo = new MemberInfoResolver(objectPath);
			var fieldName = memberInfo.Members.Single().Name;

			if (_idProperties.ContainsKey(typeof(TDocument)))
			{
				if (_idProperties[typeof(TDocument)].Equals(fieldName))
					return (TConnectionSettings)this;

				throw new ArgumentException(
					$"Cannot map '{fieldName}' as the id property for type '{typeof(TDocument).Name}': it already has '{_idProperties[typeof(TDocument)]}' mapped.");
			}

			_idProperties.Add(typeof(TDocument), fieldName);

			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify how the properties are mapped for a given POCO type.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="propertiesSelector">The properties selector.</param>
		/// <returns></returns>
		/// <remarks>Removed in 6.x.</remarks>
		public TConnectionSettings MapPropertiesFor<TDocument>(Action<PropertyMappingDescriptor<TDocument>> propertiesSelector)
			where TDocument : class
		{
			propertiesSelector.ThrowIfNull(nameof(propertiesSelector));
			var mapper = new PropertyMappingDescriptor<TDocument>();
			propertiesSelector(mapper);
			ApplyPropertyMappings(mapper.Mappings);
			return (TConnectionSettings)this;
		}

		private void ApplyPropertyMappings<TDocument>(IList<IClrTypePropertyMapping<TDocument>> mappings)
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
				if (_propertyMappings.ContainsKey(memberInfo))
				{
					var newName = mapping.NewName;
					var mappedAs = _propertyMappings[memberInfo].Name;
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
				_propertyMappings.Add(memberInfo, mapping.ToPropertyMapping());
			}
		}

		/// <summary>
		/// Specify how the mapping is inferred for a given POCO type.
		/// Can be used to infer the index, type, id property and properties for the POCO.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns></returns>
		public TConnectionSettings InferMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>> selector)
			where TDocument : class
		{
			var inferMapping = selector(new ClrTypeMappingDescriptor<TDocument>());
			if (!inferMapping.IndexName.IsNullOrEmpty())
				_defaultIndices.Add(inferMapping.Type, inferMapping.IndexName);

			if (!inferMapping.TypeName.IsNullOrEmpty())
				_defaultTypeNames.Add(inferMapping.Type, inferMapping.TypeName);

			if (inferMapping.IdProperty != null)
#pragma warning disable CS0618 // Type or member is obsolete but will be private in the future OK to call here
				MapIdPropertyFor<TDocument>(inferMapping.IdProperty);
#pragma warning restore CS0618

			if (inferMapping.Properties != null)
				ApplyPropertyMappings<TDocument>(inferMapping.Properties);

			return (TConnectionSettings)this;
		}
	}
}
