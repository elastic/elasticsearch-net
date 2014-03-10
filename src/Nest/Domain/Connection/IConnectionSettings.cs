using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{

	public class ConnectionSettings : ConnectionSettings<ConnectionSettings>
	{

		public ConnectionSettings(Uri uri, string defaultIndex) : base(uri, defaultIndex)
		{
			uri.ThrowIfNull("uri");
		}

		public ConnectionSettings(IConnectionPool connectionPool, string defaultIndex) : base(connectionPool, defaultIndex)
		{
			
		}
	}

	/// <summary>
	/// Control how NEST's behaviour.
	/// </summary>
	public class ConnectionSettings<T> : ConnectionConfiguration<T> , IConnectionSettingsValues 
		where T : ConnectionSettings<T> 
	{
		private string _defaultIndex;
		public string DefaultIndex
		{
			get
			{
				if (this._defaultIndex.IsNullOrEmpty())
					throw new NullReferenceException("No default index set on connection!");
				return this._defaultIndex;
			}
			private set { this._defaultIndex = value; }
		}

		public Func<Type, string> DefaultTypeNameInferrer { get; private set; }
		public FluentDictionary<Type, string> DefaultIndices { get; private set; }
		public FluentDictionary<Type, string> DefaultTypeNames { get; private set; }
		public Func<string, string> DefaultPropertyNameInferrer { get; private set; }

		//these are set once to make sure we don't query the Uri too often

		//Serializer settings
		public Action<JsonSerializerSettings> ModifyJsonSerializerSettings { get; private set; }

		public ReadOnlyCollection<Func<Type, JsonConverter>> ContractConverters { get; private set; }

		public ConnectionSettings(IConnectionPool uri, string defaultIndex) : base(uri)
		{
			defaultIndex.ThrowIfNullOrEmpty("defaultIndex");

			this.SetDefaultIndex(defaultIndex);
			
			this.DefaultTypeNameInferrer = (t => t.Name.ToLower()); 
			this.DefaultPropertyNameInferrer = (p => p.ToCamelCase()); 
			this.DefaultIndices = new FluentDictionary<Type, string>();
			this.DefaultTypeNames = new FluentDictionary<Type, string>();

			this.ModifyJsonSerializerSettings = (j) => { };
			this.ContractConverters = Enumerable.Empty<Func<Type, JsonConverter>>().ToList().AsReadOnly();
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
			this.DefaultTypeNameInferrer = this.LowerCaseAndPluralizeTypeNameInferrer;
			return (T)this;
		}

		/// <summary>
		/// Allows you to update internal the json.net serializer settings to your liking
		/// </summary>
		public T SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier)
		{
			if (modifier == null)
				return (T)this;
			this.ModifyJsonSerializerSettings = modifier;
			return (T)this;

		}
		/// <summary>
		/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
		/// This is faster then adding them using AddJsonConverters() because this way they will be part of the cached 
		/// Json.net contract for a type.
		/// </summary>
		public T AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors)
		{
			this.ContractConverters = contractSelectors.ToList().AsReadOnly();
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
			this.DefaultIndex = defaultIndex;
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
			this.DefaultPropertyNameInferrer = propertyNameSelector;
			return (T)this;
		}

		/// <summary>
		/// Allows you to override how type names should be reprented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		public T SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer)
		{
			defaultTypeNameInferrer.ThrowIfNull("defaultTypeNameInferrer");
			this.DefaultTypeNameInferrer = defaultTypeNameInferrer;
			return (T)this;
		}

		/// <summary>
		/// Map types to a index names. Takes precedence over SetDefaultIndex().
		/// </summary>
		public T MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this.DefaultIndices);
			return (T)this;
		}
		/// <summary>
		/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
		/// </summary>
		public T MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector)
		{
			mappingSelector.ThrowIfNull("mappingSelector");
			mappingSelector(this.DefaultTypeNames);
			return (T)this;
		}
	}

	public interface IConnectionSettingsValues : IConnectionConfigurationValues
	{
		FluentDictionary<Type, string> DefaultIndices { get; }
		FluentDictionary<Type, string> DefaultTypeNames { get; }
		string DefaultIndex { get; }
		Func<string, string> DefaultPropertyNameInferrer { get; }
		Func<Type, string> DefaultTypeNameInferrer { get; }
		Action<JsonSerializerSettings> ModifyJsonSerializerSettings { get; }
		ReadOnlyCollection<Func<Type, JsonConverter>> ContractConverters { get; }
	}


	/// <summary>
	/// Control how NEST's behaviour.
	/// </summary>
	public interface IConnectionSettings : IConnectionSettings<IConnectionSettings>, IConnectionConfigurationValues
	{
		
	}
	public interface IConnectionSettings<out T> : IConnectionConfiguration<T> where T : IConnectionSettings<T>
	{

		/// <summary>
		/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names.
		/// This used to be the default prior to Nest 1.0
		/// </summary>
		/// <returns></returns>
		T PluralizeTypeNames();

		/// <summary>
		/// Allows you to update internal the json.net serializer settings to your liking
		/// </summary>
		/// <param name="modifier"></param>
		/// <returns></returns>
		T SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier);

		/// <summary>
		/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
		/// This is faster then adding them using AddJsonConverters() because this way they will be part of the cached 
		/// Json.net contract for a type.
		/// </summary>
		T AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors);


		/// <summary>
		/// Index to default to when no index is specified.
		/// </summary>
		/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
		/// when not specifying index explicitly while indexing.
		/// </param>
		/// <returns></returns>
		T SetDefaultIndex(string defaultIndex);


		/// <summary>
		/// By default NEST camelCases property names (EmailAddress => emailAddress) that do not have an explicit propertyname 
		/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
		/// <pre>
		/// Here you can register a function that transforms propertynames (default casing, pre- or suffixing)
		/// </pre>
		/// </summary>
		T SetDefaultPropertyNameInferrer(Func<string, string> propertyNameSelector);

		/// <summary>
		/// Allows you to override how type names should be reprented, the default will call .ToLowerInvariant() on the type's name.
		/// </summary>
		T SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer);


		/// <summary>
		/// Map types to a index names. Takes precedence over SetDefaultIndex().
		/// </summary>
		T MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector);

		/// <summary>
		/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
		/// </summary>
		T MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector);
	}
}
