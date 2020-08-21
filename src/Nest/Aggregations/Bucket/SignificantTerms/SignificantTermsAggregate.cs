// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class SignificantTermsAggregate<TKey> : MultiBucketAggregate<SignificantTermsBucket<TKey>>
	{
		/// <summary>
		/// The background count
		/// </summary>
		public long? BgCount { get; set; }

		/// <summary>
		/// The document count
		/// </summary>
		public long DocCount { get; set; }
	}
}
