using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides the connection settings for NEST's <see cref="ElasticClient"/>
	/// </summary>
	public class ConnectionSettings : ConnectionSettingsBase<ConnectionSettings>
	{
		public ConnectionSettings(Uri uri = null)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200"))) { }

		public ConnectionSettings(IConnectionPool connectionPool)
			: this(connectionPool, null, null) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection)
			: this(connectionPool, connection, null) { }

		public ConnectionSettings(IConnectionPool connectionPool, IElasticsearchSerializer sourceSerializer)
			: this(connectionPool, null, sourceSerializer) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection, IElasticsearchSerializer sourceSerializer)
			: base(connectionPool, connection, sourceSerializer, null) { }

		public ConnectionSettings(
			IConnectionPool connectionPool,
			IConnection connection,
			IElasticsearchSerializer sourceSerializer,
			IPropertyMappingProvider propertyMappingProvider)
			: base(connectionPool, connection, sourceSerializer, propertyMappingProvider) { }

	}

	/// <summary>
	/// Provides the connection settings for NEST's <see cref="ElasticClient"/>
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionSettingsBase<TConnectionSettings> : ConnectionConfiguration<TConnectionSettings>, IConnectionSettingsValues
		where TConnectionSettings : ConnectionSettingsBase<TConnectionSettings>, IConnectionSettingsValues
	{
		private string _defaultIndex;
		string IConnectionSettingsValues.DefaultIndex => this._defaultIndex;

		private readonly Inferrer _inferrer;
		Inferrer IConnectionSettingsValues.Inferrer => _inferrer;

		private Func<Type, string> _defaultTypeNameInferrer;
		Func<Type, string> IConnectionSettingsValues.DefaultTypeNameInferrer => _defaultTypeNameInferrer;

		private readonly FluentDictionary<Type, string> _defaultIndices;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultIndices => _defaultIndices;

		private readonly FluentDictionary<Type, string> _defaultTypeNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultTypeNames => _defaultTypeNames;

		private readonly FluentDictionary<Type, string> _defaultRelationNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultRelationNames => _defaultRelationNames;

		private Func<string, string> _defaultFieldNameInferrer;
		Func<string, string> IConnectionSettingsValues.DefaultFieldNameInferrer => _defaultFieldNameInferrer;

		private readonly FluentDictionary<Type, string> _idProperties = new FluentDictionary<Type, string>();
		FluentDictionary<Type, string> IConnectionSettingsValues.IdProperties => _idProperties;

		private readonly FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new FluentDictionary<MemberInfo, IPropertyMapping>();
		FluentDictionary<MemberInfo, IPropertyMapping> IConnectionSettingsValues.PropertyMappings => _propertyMappings;

		private readonly  IElasticsearchSerializer _sourceSerializer;
		IElasticsearchSerializer IConnectionSettingsValues.SourceSerializer => _sourceSerializer;

		private readonly IPropertyMappingProvider _propertyMappingProvider;
		IPropertyMappingProvider IConnectionSettingsValues.PropertyMappingProvider => _propertyMappingProvider;

		//todo hacky
		internal StatefulSerializerFactory SerializerFactory { get; }

		protected ConnectionSettingsBase(
			IConnectionPool connectionPool,
			IConnection connection,
			IElasticsearchSerializer sourceSerializer,
			IPropertyMappingProvider propertyMappingProvider
			)
			: base(connectionPool, connection, null)
		{
			var defaultSerializer = new JsonNetSerializer(this);
			this._sourceSerializer = sourceSerializer ?? defaultSerializer;
			this._requestResponseSerializer = defaultSerializer;
			this._propertyMappingProvider = propertyMappingProvider ?? new PropertyMappingProvider(this);

			this._defaultTypeNameInferrer = (t => t.Name.ToLowerInvariant());
			this._defaultFieldNameInferrer = (p => p.ToCamelCase());
			this._defaultIndices = new FluentDictionary<Type, string>();
			this._defaultTypeNames = new FluentDictionary<Type, string>();
			this._defaultRelationNames = new FluentDictionary<Type, string>();

			this.SerializerFactory = new StatefulSerializerFactory();

			this._inferrer = new Inferrer(this);
		}

		/// <summary>
		/// Pluralize type names when inferring from POCO type names.
		/// <para></para>
		/// This calls <see cref="DefaultTypeNameInferrer"/> with an implementation that will pluralize type names.
		/// This used to be the default prior to Nest 0.90
		/// </summary>
		public TConnectionSettings PluralizeTypeNames()
		{
			this._defaultTypeNameInferrer = this.LowerCaseAndPluralizeTypeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// The default index to use when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw
		/// <see cref="NullReferenceException"/> later on when not specifying index explicitly while indexing.
		/// </param>
		public TConnectionSettings DefaultIndex(string defaultIndex)
		{
			this._defaultIndex = defaultIndex;
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
			this._defaultFieldNameInferrer = fieldNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Specify how type names are inferred from POCO types.
		/// By default, type names are inferred by calling <see cref="string.ToLowerInvariant"/>
		///  on the type's name.
		/// </summary>
		public TConnectionSettings DefaultTypeNameInferrer(Func<Type, string> typeNameInferrer)
		{
			typeNameInferrer.ThrowIfNull(nameof(typeNameInferrer));
			this._defaultTypeNameInferrer = typeNameInferrer;
			return (TConnectionSettings)this;
		}

        /// <summary>
        /// Specify which property on a given POCO should be used to infer the id of the document when
        /// indexed in Elasticsearch.
        /// </summary>
        /// <typeparam name="TDocument">The type of the document.</typeparam>
        /// <param name="objectPath">The object path.</param>
        /// <returns></returns>
        private TConnectionSettings MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull(nameof(objectPath));

			var memberInfo = new MemberInfoResolver(objectPath);
			var fieldName = memberInfo.Members.Single().Name;

			if (this._idProperties.ContainsKey(typeof(TDocument)))
			{
				if (this._idProperties[typeof(TDocument)].Equals(fieldName))
					return (TConnectionSettings)this;

				throw new ArgumentException($"Cannot map '{fieldName}' as the id property for type '{typeof(TDocument).Name}': it already has '{this._idProperties[typeof(TDocument)]}' mapped.");
			}

			this._idProperties.Add(typeof(TDocument), fieldName);

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
						throw new ArgumentException($"Property mapping '{e}' on type {typeName} can not be mapped to '{newName}' it already has an ignore mapping");
					if (newName.IsNullOrEmpty())
						throw new ArgumentException($"Property mapping '{e}' on type {typeName} can not be ignored it already has a mapping to '{mappedAs}'");
					throw new ArgumentException($"Property mapping '{e}' on type {typeName} can not be mapped to '{newName}' already mapped as '{mappedAs}'");
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
				this._defaultIndices.Add(inferMapping.Type, inferMapping.IndexName);

			if (!inferMapping.TypeName.IsNullOrEmpty())
				this._defaultTypeNames.Add(inferMapping.Type, inferMapping.TypeName);

			if (!inferMapping.RelationName.IsNullOrEmpty())
				this._defaultRelationNames.Add(inferMapping.Type, inferMapping.RelationName);

			if (inferMapping.IdProperty != null)
				this.MapIdPropertyFor<TDocument>(inferMapping.IdProperty);

			if (inferMapping.Properties != null)
				this.ApplyPropertyMappings<TDocument>(inferMapping.Properties);

			return (TConnectionSettings)this;
		}

	}
}
