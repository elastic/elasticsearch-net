using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(BulkResponseItemJsonConverter))]
	public class BulkIndexResponseItem : BulkResponseItemBase
	{
		public override string Operation { get; internal set; }

        /// <summary>
        /// Null if Percolation was not requested while indexing this doc, otherwise returns the percolator _ids that matched (if any)
        /// </summary>
        [JsonProperty(PropertyName = "matches")]
        public IEnumerable<string> Matches { get; internal set; }
	}
}
