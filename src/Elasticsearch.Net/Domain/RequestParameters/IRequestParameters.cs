using System;
using System.Collections.Generic;
using System.IO;

namespace Elasticsearch.Net
{
	public interface IRequestParameters
	{
		HttpMethod DefaultHttpMethod { get; }

		/// <summary>
		/// A method that can be set on the request to take ownership of creating the response object.
		/// When set this will be called instead of the internal .Deserialize();
		/// </summary>
		Func<IApiCallDetails, Stream, object> DeserializationOverride { get; set; }

		/// <summary>
		/// The querystring that should be appended to the path of the request
		/// </summary>
		Dictionary<string, object> QueryString { get; set; }

		/// <summary>
		/// Configuration for this specific request, i.e disable sniffing, custom timeouts etcetera.
		/// </summary>
		IRequestConfiguration RequestConfiguration { get; set; }

		/// <summary> Sets a query string param. If <paramref name="value" /> is null and the parameter exists it will be removed </summary>
		/// <param name="name">The query string parameter to add</param>
		/// <param name="value">The value to set, if null removes <paramref name="name" /> from the query string if it exists</param>
		void SetQueryString(string name, object value);

		bool ContainsQueryString(string name);

		/// <summary>
		/// Get's the value as its stored on the querystring using its original type
		/// </summary>
		TOut GetQueryStringValue<TOut>(string name);

		/// <summary>
		/// Gets the stringified representation of a query string value as it would be sent to elasticsearch.
		/// </summary>
		string GetResolvedQueryStringValue(string n, IConnectionConfigurationValues s);
	}
}
