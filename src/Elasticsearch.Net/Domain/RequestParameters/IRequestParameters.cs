using System;
using System.Collections.Generic;
using System.IO;

namespace Elasticsearch.Net
{
	public interface IRequestParameters
	{
		HttpMethod DefaultHttpMethod { get; }

		/// <summary>
		/// The querystring that should be appended to the path of the request
		/// </summary>
		IDictionary<string, object> QueryString { get; set; }

		/// <summary>
		/// A method that can be set on the request to take ownership of creating the response object.
		/// When set this will be called instead of the internal .Deserialize();
		/// </summary>
		Func<IApiCallDetails, Stream, object> DeserializationOverride { get; set;  }

		/// <summary>
		/// Configuration for this specific request, i.e disable sniffing, custom timeouts etcetera.
		/// </summary>
		IRequestConfiguration RequestConfiguration { get; set; }

		TOut GetQueryStringValue<TOut>(string name);

		void AddQueryStringValue(string name, object value);


	}
}
