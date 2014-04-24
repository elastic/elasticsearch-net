using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
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
		public ConnectionSettings(IConnectionPool connectionPool, string defaultIndex = null) : base(connectionPool, defaultIndex)
		{
			
		}
	}
	/// <summary>
	/// Control how NEST's behaviour.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ConnectionSettings<T> : ConnectionConfiguration<T> , IConnectionSettingsValues 
		where T : ConnectionSettings<T> 
	{
		private string _defaultIndex;
		string IConnectionSettingsValues.DefaultIndex
		{
			get
			{
				if (this._defaultIndex.IsNullOrEmpty())
					throw new NullReferenceException("No default index set on connection!");
				return this._defaultIndex;
			}
		}

		private Func<Type, string> _defaultTypeNameInferrer;
		Func<Type, string> IConnectionSettingsValues.DefaultTypeNameInferrer { get { return _defaultTypeNameInferrer; } }

		private FluentDictionary<Type, string> _defaultIndices;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultIndices { get { return _defaultIndices; } }

		private FluentDictionary<Type, string> _defaultTypeNames;
		FluentDictionary<Type, string> IConnectionSettingsValues.DefaultTypeNames { get { return _defaultTypeNames; } }

		private Func<string, string> _defaultPropertyNameInferrer;
		Func<string, string> IConnectionSettingsValues.DefaultPropertyNameInferrer { get { return _defaultPropertyNameInferrer; } }

		//these are set once to make sure we don't query the Uri too often

		//Serializer settings
		private Action<JsonSerializerSettings> _modifyJsonSerializerSettings;
		Action<JsonSerializerSettings> IConnectionSettingsValues.ModifyJsonSerializerSettings { get { return _modifyJsonSerializerSettings; } }

		private ReadOnlyCollection<Func<Type, JsonConverter>> _contractConverters;
		ReadOnlyCollection<Func<Type, JsonConverter>> IConnectionSettingsValues.ContractConverters { get { return _contractConverters; } }

		public ConnectionSettings(IConnectionPool uri, string defaultIndex) : base(uri)
		{
			if (!defaultIndex.IsNullOrEmpty())
				this.SetDefaultIndex(defaultIndex);
			
			this._defaultTypeNameInferrer = (t => t.Name.ToLower()); 
			this._defaultPropertyNameInferrer = (p => p.ToCamelCase()); 
			this._defaultIndices = new FluentDictionary<Type, string>();
			this._defaultTypeNames = new FluentDictionary<Type, string>();

			this._modifyJsonSerializerSettings = (j) => { };
			this._contractConverters = Enumerable.Empty<Func<Type, JsonConverter>>().ToList().AsReadOnly();
		}
		public ConnectionSettings(Uri uri, string defaultIndex) 
			: this(new SingleNodeConnectionPool(uri ?? new Uri("http://localhost:9200")), defaultIndex)
		{
			
		}

		/// <summary>
		/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names. This used to be the default prior to Nest 0.90
		/// </summary>
		public T PluralizeTypeNames()
		{
			this._defaultTypeNameInferrer = this.LowerCaseAndPluralizeTypeNameInferrer;
			return (T)this;
		}

		/// <summary>
		/// Allows you to update internal the json.net serializer settings to your liking
		/// </summary>
		public T SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier)
		{
			if (modifier == null)
				return (T)this;
			this._modifyJsonSerializerSettings = modifier;
			return (T)this;

		}
		/// <summary>
		/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
		/// This is faster then adding them using AddJsonConverters() because this way they will be part of the cached 
		/// Json.net contract for a type.
		/// </summary>
		public T AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors)
		{
			this._contractConverters = contractSelectors.ToList().AsReadOnly();
			return (T)this;
		}

		/// <summary>
		/// Index to default to when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
		/// when not specifying index explicitly while indexing.
		/// </param>
		public T SetDefaultIndex(string defaultIndex)
		{
			this._defaultIndex = defaultIndex;
			return (T)this;
		}

		private string LowerCaseAndPluralizeTypeNameInferrer(Type type)
		{
			type.ThrowIfNull("type");
			return Inflector.MakePlural(type.Name).ToLower();
		}

		/// <summary>
		/// By default NEST camelCases property names (EmailAddress => emailAddress) that do not have an explicit propertyname 
		/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
		/// <pre>
		/// Here you can register a function that transforms propertynames (default casing, pre- or suffixing)
		/// </pre>
		/// </summary>
		public T SetDefaultPropertyNameInferrer(Func<string, string> propertyNameSelector)
		{
			this._defaultPropertyNameInferrer = propertyNameSelector;
			return (T)this;
		}

		/// <summary>
		/// Allows you to override how type names should be reprented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		public T SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer)
		{
			defaultTypeNameInferrer.ThrowIfNull("defaultTypeNameInferrer");
			this._defaultTypeNameInferrer = defaultTypeNameInferrer;
			return (T)this;
		}

		/// <summary>
		/// Map types to a index names. Takes precedence over SetDefaultIndex().
		/// </summary>
		public T MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this._defaultIndices);
			return (T)this;
		}
		/// <summary>
		/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
		/// </summary>
		public T MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this._defaultTypeNames);
			return (T)this;
		}
	}
}