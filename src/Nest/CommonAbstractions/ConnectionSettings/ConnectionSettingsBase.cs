using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;

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

		public ConnectionSettings(IConnectionPool connectionPool, Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory)
			: this(connectionPool, null, serializerFactory) { }

		public ConnectionSettings(IConnectionPool connectionPool, IConnection connection, Func<ConnectionSettings, IElasticsearchSerializer> serializerFactory)
			: base(connectionPool, connection, serializerFactory) { }
	}

	/// <summary>
	/// Provides the connection settings for NEST's <see cref="ElasticClient"/>
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ConnectionSettingsBase<TConnectionSettings> : ConnectionConfiguration<TConnectionSettings>, IConnectionSettingsValues
		where TConnectionSettings : ConnectionSettingsBase<TConnectionSettings>
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

		private Func<string, string> _defaultFieldNameInferrer;
		Func<string, string> IConnectionSettingsValues.DefaultFieldNameInferrer => _defaultFieldNameInferrer;

		private readonly FluentDictionary<Type, string> _idProperties = new FluentDictionary<Type, string>();
		FluentDictionary<Type, string> IConnectionSettingsValues.IdProperties => _idProperties;

		private readonly FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new FluentDictionary<MemberInfo, IPropertyMapping>();
		FluentDictionary<MemberInfo, IPropertyMapping> IConnectionSettingsValues.PropertyMappings => _propertyMappings;

		protected ConnectionSettingsBase(IConnectionPool connectionPool, IConnection connection, Func<TConnectionSettings, IElasticsearchSerializer> serializerFactory)
			: base(connectionPool, connection, serializerFactory)
		{
			this._defaultTypeNameInferrer = (t => t.Name.ToLowerInvariant());
			this._defaultFieldNameInferrer = (p => p.ToCamelCase());
			this._defaultIndices = new FluentDictionary<Type, string>();
			this._defaultTypeNames = new FluentDictionary<Type, string>();

			this._inferrer = new Inferrer(this);
		}

		/// <summary>
		/// The default serializer for requests and responses
		/// </summary>
		/// <returns></returns>
		protected override IElasticsearchSerializer DefaultSerializer(TConnectionSettings settings) => new JsonNetSerializer(settings);

		/// <summary>
		/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names. This used to be the default prior to Nest 0.90
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
		/// By default NEST camelCases property name (EmailAddress => emailAddress) expressions
		/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
		/// <pre>
		/// Here you can register a function that transforms these expressions (default casing, pre- or suffixing)
		/// </pre>
		/// </summary>
		public TConnectionSettings DefaultFieldNameInferrer(Func<string, string> fieldNameInferrer)
		{
			this._defaultFieldNameInferrer = fieldNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Allows you to override how type names should be represented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		public TConnectionSettings DefaultTypeNameInferrer(Func<Type, string> typeNameInferrer)
		{
			typeNameInferrer.ThrowIfNull(nameof(typeNameInferrer));
			this._defaultTypeNameInferrer = typeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Map types to a index names. Takes precedence over DefaultIndex().
		/// </summary>
		public TConnectionSettings MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull(nameof(mappingSelector));
			mappingSelector(this._defaultIndices);
			return (TConnectionSettings)this;
		}
		/// <summary>
		/// Allows you to override typenames, takes priority over the global DefaultTypeNameInferrer()
		/// </summary>
		public TConnectionSettings MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull(nameof(mappingSelector));
			mappingSelector(this._defaultTypeNames);
			return (TConnectionSettings)this;
		}

		public TConnectionSettings MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull(nameof(objectPath));

			var memberInfo = new MemberInfoResolver(objectPath);
			var fieldName = memberInfo.Members.Single().Name;

			if (this._idProperties.ContainsKey(typeof(TDocument)))
			{
				if (this._idProperties[typeof(TDocument)].Equals(fieldName))
					return (TConnectionSettings)this;

				throw new ArgumentException("Cannot map '{0}' as the id property for type '{1}': it already has '{2}' mapped."
					.F(fieldName, typeof(TDocument).Name, this._idProperties[typeof(TDocument)]));
			}

			this._idProperties.Add(typeof(TDocument), fieldName);

			return (TConnectionSettings)this;
		}

		public TConnectionSettings MapPropertiesFor<TDocument>(Action<PropertyMappingDescriptor<TDocument>> propertiesSelector)
			where TDocument : class
		{
			propertiesSelector.ThrowIfNull(nameof(propertiesSelector));
			var mapper = new PropertyMappingDescriptor<TDocument>();
			propertiesSelector(mapper);
			ApplyPropertyMappings(mapper.Mappings);
			return (TConnectionSettings) this;
		}

		private void ApplyPropertyMappings<TDocument>(IList<IClrTypePropertyMapping<TDocument>> mappings) 
			where TDocument : class
		{
			foreach (var mapping in mappings)
			{
				var e = mapping.Property;
				var memberInfoResolver = new MemberInfoResolver(e);
				if (memberInfoResolver.Members.Count > 1)
					throw new ArgumentException("MapFieldNameFor can only map direct properties");

				if (memberInfoResolver.Members.Count < 1)
					throw new ArgumentException("Expression {0} does contain any member access".F(e));

				var memberInfo = memberInfoResolver.Members.Last();
				if (_propertyMappings.ContainsKey(memberInfo))
				{
					var newName = mapping.NewName;
					var mappedAs = _propertyMappings[memberInfo].Name;
					var typeName = typeof (TDocument).Name;
					if (mappedAs.IsNullOrEmpty() && newName.IsNullOrEmpty())
						throw new ArgumentException("Property mapping '{0}' on type is already ignored"
							.F(e, newName, mappedAs, typeName));
					if (mappedAs.IsNullOrEmpty())
						throw new ArgumentException("Property mapping '{0}' on type {3} can not be mapped to '{1}' it already has an ignore mapping"
							.F(e, newName, mappedAs, typeName));
					if (newName.IsNullOrEmpty())
						throw new ArgumentException("Property mapping '{0}' on type {3} can not be ignored it already has a mapping to '{2}'"
							.F(e, newName, mappedAs, typeName));
					throw new ArgumentException("Property mapping '{0}' on type {3} can not be mapped to '{1}' already mapped as '{2}'"
						.F(e, newName, mappedAs, typeName));
				}
				_propertyMappings.Add(memberInfo, mapping.ToPropertyMapping());
			}
		}

		public TConnectionSettings InferMappingFor<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>> selector)
			where TDocument : class
		{
			var inferMapping = selector(new ClrTypeMappingDescriptor<TDocument>());
			if (!inferMapping.IndexName.IsNullOrEmpty())
				this._defaultIndices.Add(inferMapping.Type, inferMapping.IndexName);

			if (!inferMapping.TypeName.IsNullOrEmpty())
				this._defaultTypeNames.Add(inferMapping.Type, inferMapping.TypeName);

			if (inferMapping.IdProperty != null)
#pragma warning disable CS0618 // Type or member is obsolete but will be private in the future OK to call here
				this.MapIdPropertyFor<TDocument>(inferMapping.IdProperty);
#pragma warning restore CS0618 

			if (inferMapping.Properties != null)
				this.ApplyPropertyMappings<TDocument>(inferMapping.Properties);
			
			return (TConnectionSettings) this;
		}

	}
}
