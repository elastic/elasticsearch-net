// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	/// <summary>
	/// The minimum interface your custom responses should implement when providing a response type
	/// to the low level client
	/// </summary>
	public interface IElasticsearchResponse
	{
		/// <summary>
		/// Sets and returns the <see cref="IApiCallDetails" /> diagnostic information
		/// </summary>
		IApiCallDetails ApiCall { get; set; }

		bool TryGetServerErrorReason(out string reason);
	}
}
