// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net
{
	public interface IRequestParameters
	{
		HttpMethod DefaultHttpMethod { get; }
		
		bool SupportsBody { get; }

		/// <summary> Allows you to completely circumvent the serializer to build the final response.</summary>
		CustomResponseBuilderBase CustomResponseBuilder { get; set; }

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
		/// Gets the stringified representation of a query string value as it would be sent to Elasticsearch.
		/// </summary>
		string GetResolvedQueryStringValue(string n, IConnectionConfigurationValues s);

		/// <summary>
		/// Gets the HTTP Accept Header value from the shortened name. If the shortened name is not recognized,
		/// <c>null</c> is returned.
		/// </summary>
		string AcceptHeaderFromFormat(string format);
	}
}
