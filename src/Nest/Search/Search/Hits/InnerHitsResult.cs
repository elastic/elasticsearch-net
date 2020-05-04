// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	public class InnerHitsResult
	{
		[DataMember(Name = "hits")]
		public InnerHitsMetadata Hits { get; internal set; }

		/// <summary>
		/// Retrieve <see cref="Hits" /> documents as a strongly typed
		/// collection
		/// </summary>
		/// <typeparam name="T">The hits document type</typeparam>
		public IEnumerable<T> Documents<T>() where T : class =>
			Hits == null ? Enumerable.Empty<T>() : Hits.Documents<T>();
	}
}
