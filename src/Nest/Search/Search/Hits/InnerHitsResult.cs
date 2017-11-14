using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class InnerHitsResult
	{
		[JsonProperty("hits")]
		public InnerHitsMetadata Hits { get; internal set; }

		/// <summary>
		/// Retrieve <see cref="Hits"/> documents as a strongly typed
		/// collection
		/// </summary>
		/// <typeparam name="T">The hits document type</typeparam>
		public IEnumerable<T> Documents<T>() where T : class =>
			this.Hits == null ? Enumerable.Empty<T>() : this.Hits.Documents<T>();
	}
}
