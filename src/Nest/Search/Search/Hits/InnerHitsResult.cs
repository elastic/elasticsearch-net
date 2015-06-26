using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class InnerHitsResult
	{
		
		[JsonProperty("hits")]
		public InnerHitsMetaData Hits { get; internal set; }

		public IEnumerable<T> Documents<T>() where T : class
		{
			return this.Hits == null ? Enumerable.Empty<T>() : this.Hits.Documents<T>();
		}
	}
}