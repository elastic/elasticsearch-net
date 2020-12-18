// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	/// <summary>
	/// Holds re-usable prefixes for meta data headers.
	/// </summary>
	public interface IMetaDataHeaders
	{
		/// <summary>
		/// A reusable prefix for the meta data header sent for asynchronous operations.
		/// </summary>
		public string AsyncMetaDataHeaderPrefix { get; }

		/// <summary>
		/// A reusable prefix for the meta data header sent for synchronous operations.
		/// </summary>
		public string SyncMetaDataHeaderPrefix { get; }
	}
}
