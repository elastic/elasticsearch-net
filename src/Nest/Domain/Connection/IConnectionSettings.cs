using System;
using System.Collections.Specialized;
using System.Linq;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	//TODO Remove


	///// <summary>
	///// Control how NEST's behaviour.
	///// </summary>
	//public interface IConnectionSettings : IConnectionSettings<IConnectionSettings>, IConnectionConfigurationValues
	//{
		
	//}
	//public interface IConnectionSettings<out T> : IConnectionConfiguration<T> where T : IConnectionSettings<T>
	//{

	//	/// <summary>
	//	/// This calls SetDefaultTypenameInferrer with an implementation that will pluralize type names.
	//	/// This used to be the default prior to Nest 1.0
	//	/// </summary>
	//	/// <returns></returns>
	//	T PluralizeTypeNames();

	//	/// <summary>
	//	/// Allows you to update internal the json.net serializer settings to your liking
	//	/// </summary>
	//	/// <param name="modifier"></param>
	//	/// <returns></returns>
	//	T SetJsonSerializerSettingsModifier(Action<JsonSerializerSettings> modifier);

	//	/// <summary>
	//	/// Add a custom JsonConverter to the build in json serialization by passing in a predicate for a type.
	//	/// This is faster then adding them using AddJsonConverters() because this way they will be part of the cached 
	//	/// Json.net contract for a type.
	//	/// </summary>
	//	T AddContractJsonConverters(params Func<Type, JsonConverter>[] contractSelectors);


	//	/// <summary>
	//	/// Index to default to when no index is specified.
	//	/// </summary>
	//	/// <param name="defaultIndex">When null/empty/not set might throw NRE later on
	//	/// when not specifying index explicitly while indexing.
	//	/// </param>
	//	/// <returns></returns>
	//	T SetDefaultIndex(string defaultIndex);


	//	/// <summary>
	//	/// By default NEST camelCases property names (EmailAddress => emailAddress) that do not have an explicit propertyname 
	//	/// either via an ElasticProperty attribute or because they are part of Dictionary where the keys should be treated verbatim.
	//	/// <pre>
	//	/// Here you can register a function that transforms propertynames (default casing, pre- or suffixing)
	//	/// </pre>
	//	/// </summary>
	//	T SetDefaultPropertyNameInferrer(Func<string, string> propertyNameSelector);

	//	/// <summary>
	//	/// Allows you to override how type names should be reprented, the default will call .ToLowerInvariant() on the type's name.
	//	/// </summary>
	//	T SetDefaultTypeNameInferrer(Func<Type, string> defaultTypeNameInferrer);


	//	/// <summary>
	//	/// Map types to a index names. Takes precedence over SetDefaultIndex().
	//	/// </summary>
	//	T MapDefaultTypeIndices(Action<FluentDictionary<Type, string>> mappingSelector);

	//	/// <summary>
	//	/// Allows you to override typenames, takes priority over the global SetDefaultTypeNameInferrer()
	//	/// </summary>
	//	T MapDefaultTypeNames(Action<FluentDictionary<Type, string>> mappingSelector);
	//}
}
