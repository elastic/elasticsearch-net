using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatIndicesRecord : ICatRecord
	{
		[JsonProperty("docs.count")]
		public string DocsCount { get; set; }

		[JsonProperty("docs.deleted")]
		public string DocsDeleted { get; set; }

		[JsonProperty("health")]
		public string Health { get; set; }

		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("pri")]
		public string Primary { get; set; }

		[JsonProperty("pri.store.size")]
		public string PrimaryStoreSize { get; set; }

		[JsonProperty("rep")]
		public string Replica { get; set; }

		[JsonProperty("store.size")]
		public string StoreSize { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("tm")]
		public string TotalMemory { get; set; }
	}
}