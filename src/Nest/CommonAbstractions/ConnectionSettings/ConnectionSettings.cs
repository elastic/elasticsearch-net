using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest.CommonAbstractions.ConnectionSettings;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Provides NEST's ElasticClient with configurationsettings
	/// </summary>
	public class ConnectionSettings : ConnectionSettings<ConnectionSettings>
	{
		/// <summary>
		/// Instantiate a new connectionsettings object that proves ElasticClient with configuration values
		/// </summary>
		/// <param name="uri">A single uri representing the root of the node you want to connect to
		/// <para>defaults to http://localhost:9200</para>
		/// </param>
		/// <param name="defaultIndex">The default index/alias name used for operations that expect an index/alias name, 
		/// By specifying it once alot of magic string can be avoided.
		/// <para>You can also specify specific default index/alias names for types using .SetDefaultTypeIndices(</para>
		/// <para>If you do not specify this, NEST might throw a runtime exception if an explicit indexname was not provided for a call</para>
		/// </param>
		public ConnectionSettings(Uri uri = null, string defaultIndex = null)
			: base(uri, defaultIndex)
		{
		}

		/// <summary>
		/// Instantiate a new connectionsettings object that proves ElasticClient with configuration values
		/// </summary>
		/// <param name="connectionPool">A connection pool implementation that'll tell the client what nodes are available</param>
		/// <param name="defaultIndex">The default index/alias name used for operations that expect an index/alias name, 
		/// By specifying it once alot of magic string can be avoided.
		/// <para>You can also specify specific default index/alias names for types using .SetDefaultTypeIndices(</para>
		/// <para>If you do not specify this, NEST might throw a runtime exception if an explicit indexname was not provided for a call</para>
		/// </param>
		public ConnectionSettings(IConnectionPool connectionPool, string defaultIndex = null)
			: base(connectionPool, defaultIndex)
		{

		}
	}
	/// <summary>
	/// Control how NEST's behaviour.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ConnectionSettings<TConnectionSettings> : ConnectionConfiguration<TConnectionSettings>, IConnectionSettingsValues
		where TConnectionSettings : ConnectionSettings<TConnectionSettings>
	{
		ConnectionSettings<TConnectionSettings> _assign(Action<IConnectionSettingsValues> assigner) => Fluent.Assign(this, assigner);

		private string _defaultIndex;
		string IConnectionSettingsValues.DefaultIndex => this._defaultIndex;

		private ElasticInferrer _inferrer;
		ElasticInferrer IConnectionSettingsValues.Inferrer => _inferrer;

		private Func<Type, string> _defaultTypeNameInferrer;
		Func<Type, string> IConnectionSettingsValues.DefaultTypeNameInferrer => _defaultTypeNameInferrer;

		private FluentDictionary<Type, string> _defaultIndices;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultIndices => _defaultIndices;

		private FluentDictionary<Type, string> _defaultTypeNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultTypeNames => _defaultTypeNames;

		private Func<string, string> _defaultFieldNameInferrer;
		Func<string, string> IConnectionSettingsValues.DefaultFieldNameInferrer => _defaultFieldNameInferrer;

		//Serializer settings
		private Action<JsonSerializerSettings> _modifyJsonSerializerSettings;
		Action<JsonSerializerSettings> IConnectionSettingsValues.ModifyJsonSerializerSettings => _modifyJsonSerializerSettings;

		private ReadOnlyCollection<Func<Type, JsonConverter>> _contractConverters;
		ReadOnlyCollection<Func<Type, JsonConverter>> IConnectionSettingsValues.ContractConverters => _contractConverters;

		private FluentDictionary<Type, string> _idProperties = new FluentDictionary<Type, string>();
		FluentDictionary<Type, string> IConnectionSettingsValues.IdProperties => _idProperties;

		private FluentDictionary<MemberInfo, IPropertyMapping> _propertyMappings = new FluentDictionary<MemberInfo, IPropertyMapping>();
		FluentDictionary<MemberInfo, IPropertyMapping> IConnectionSettingsValues.PropertyMappings => _propertyMappings;

		public ConnectionSettings(IConnectionPool connectionPool, string defaultIndex)
			: base(connectionPool)
		{
			if (!defaultIndex.IsNullOrEmpty())
				this.SetDefaultIndex(defaultIndex);

			this._defaultTypeNameInferrer = (t => t.Name.ToLowerInvariant());
			this._defaultFieldNameInferrer = (p => p.ToCamelCase());
			this._defaultIndices = new FluentDictionary<Type, string>();
			this._defaultTypeNames = new FluentDictionary<Type, string>();

			this._modifyJsonSerializerSettings = (j) => { };
			this._contractConverters = Enumerable.Empty<Func<Type, JsonConverter>>().ToList().AsReadOnly();
			this._inferrer = new ElasticInferrer(this);
		}

		public ConnectionSettings(Uri uri, string defaultIndex)
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")), defaultIndex)
		{

		}

		/// <summary>
		/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names. This used to be the default prior to Nest 0.90
		/// </summary>
		public TConnectionSettings PluralizeTypeNames()
		{
			this._defaultTypeNameInferrer = this.LowerCaseAndPluralizeTypeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Allows you to update internal the json.net serializer settings to your liking
		/// </summary>
		public TConnectionSettings SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier)
		{
			if (modifier == null)
				return (TConnectionSettings)this;
			this._modifyJsonSerializerSettings = modifier;
			return (TConnectionSettings)this;

		}
		/// <summary>
		/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
		/// </summary>
		public TConnectionSettings AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors)
		{
			this._contractConverters = contractSelectors.ToList().AsReadOnly();
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Index to default to when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
		/// when not specifying index explicitly while indexing.
		/// </param>
		public TConnectionSettings SetDefaultIndex(string defaultIndex)
		{
			this._defaultIndex = defaultIndex;
			return (TConnectionSettings)this;
		}

		private string LowerCaseAndPluralizeTypeNameInferrer(Type type)
		{
			type.ThrowIfNull("type");
			return Inflector.MakePlural(type.Name).ToLowerInvariant();
		}

		/// <summary>
		/// By default NEST camelCases property names (EmailAddress => emailAddress) that do not have an explicit FieldName 
		/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
		/// <pre>
		/// Here you can register a function that transforms FieldNames (default casing, pre- or suffixing)
		/// </pre>
		/// </summary>
		public TConnectionSettings SetDefaultFieldNameInferrer(Func<string, string> FieldNameSelector)
		{
			this._defaultFieldNameInferrer = FieldNameSelector;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Allows you to override how type names should be represented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		public TConnectionSettings SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer)
		{
			defaultTypeNameInferrer.ThrowIfNull("defaultTypeNameInferrer");
			this._defaultTypeNameInferrer = defaultTypeNameInferrer;
			return (TConnectionSettings)this;
		}

		/// <summary>
		/// Map types to a index names. Takes precedence over SetDefaultIndex().
		/// </summary>
		public TConnectionSettings MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this._defaultIndices);
			return (TConnectionSettings)this;
		}
		/// <summary>
		/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
		/// </summary>
		public TConnectionSettings MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this._defaultTypeNames);
			return (TConnectionSettings)this;
		}

		public TConnectionSettings MapIdPropertyFor<TDocument>(Expression<Func<TDocument, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");

			var memberInfo = new MemberInfoResolver(this, objectPath);
			var FieldName = memberInfo.Members.Single().Name;

			if (this._idProperties.ContainsKey(typeof(TDocument)))
			{
				if (this._idProperties[typeof(TDocument)].Equals(FieldName))
					return (TConnectionSettings)this;

				throw new ArgumentException("Cannot map '{0}' as the id property for type '{1}': it already has '{2}' mapped."
					.F(FieldName, typeof(TDocument).Name, this._idProperties[typeof(TDocument)]));
			}

			this._idProperties.Add(typeof(TDocument), FieldName);

			return (TConnectionSettings)this;
		}

		public TConnectionSettings MapPropertiesFor<TDocument>(Action<PropertyMappingDescriptor<TDocument>> propertiesSelector)
			where TDocument : class
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
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
				var memberInfoResolver = new MemberInfoResolver(this, e);
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
				this.MapIdPropertyFor<TDocument>(inferMapping.IdProperty);
			
			if (inferMapping.Properties != null)
				this.ApplyPropertyMappings<TDocument>(inferMapping.Properties);
			


			return (TConnectionSettings) this;
		}

	}
}
