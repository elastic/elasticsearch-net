// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	/// <summary>
	/// A provider for additional HTTP request headers.
	/// </summary>
	public interface IHeaderProvider
	{
		/// <summary>
		/// The name of the header produced by this provider.
		/// </summary>
		string HeaderName { get; }

		/// <summary>
		/// Produces the value for the header using information from the <see cref="RequestData"/>.
		/// </summary>
		/// <param name="requestData">Data about the request which may be used to produce the header value.</param>
		/// <returns></returns>
		string ProduceHeaderValue(RequestData requestData);
	}
}
